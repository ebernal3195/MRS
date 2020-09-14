using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.Net;
using Newtonsoft.Json;
using MailKit.Net.Imap;
using System.Data.SqlClient;
using SAPbobsCOM;
using NLog.Config;

namespace MRS
{
    public partial class FrmRegistro : Form
    {
        #region CONSTRUCTOR
        public FrmRegistro()
        {
            InitializeComponent();
        }
        #endregion

        #region VARIABLES

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static Conexiones.ConexionSAP _oConnection = new Conexiones.ConexionSAP();
        private DataTable MyDt = new DataTable();
        private List<Solicitud> SolicitudesARegistrar = new List<Solicitud>();
        private SAPbobsCOM.Company oCompany;
        public static List<Listas.ListaSolicitud> DatosSolicitud = new List<Listas.ListaSolicitud>();
        public static Listas.ListaSolicitud itemSolicitud = new Listas.ListaSolicitud();

        string rutaArchivosSAP = "";
        string rutaArchivosWEB = "";
        string rutaArchivosLocal = "";

        #endregion

        #region Metodos

        private void FrmRegistro_Load(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Se inició programa MRS");

                if (!Extensor.CargarConfiguraciones())
                {
                    Application.Exit();
                }

                cargarConfiguracionApp();

                if (Extensor.Configuracion.CONEXION_SAP.EjecucionAutomatica == "Y")
                {
                    btnConsultar.PerformClick();
                    btnRegistrar.PerformClick();

                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error al cargar configuraciones: {0}", ex.Message);
            }
        }

        private void cargarConfiguracionApp()
        {
            rutaArchivosWEB = Extensor.Configuracion.CONEXION_SAP.RutaArchivoWEB;
            rutaArchivosSAP = Extensor.Configuracion.CONEXION_SAP.RutaArchivosSAP;
            rutaArchivosLocal = Extensor.Configuracion.CONEXION_SAP.RutaArchivoLocal;

            txtBDSAP.Text = Extensor.Configuracion.CONEXION_SAP.ServerNameSAP + " - " + Extensor.Configuracion.CONEXION_SAP.BaseSAP;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            salirAddon();
        }

        public void salirAddon()
        {
            try
            {
                if (oCompany != null)
                {
                    if (oCompany.Connected == true)
                    {
                        oCompany.Disconnect();
                    }
                }
                Application.Exit();
            }
            catch (Exception ex)
            {
                Application.Exit();
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            obtenerSolicitudes();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            seleccionarSolicitudes();
            int estatusRegistro = 1;
            string error = string.Empty;

            foreach (Solicitud sol in SolicitudesARegistrar)
            {
                //Registrar solicitud en SAP
                estatusRegistro = registrarSolicitud(sol.Agente, sol.NumeroSolicitud, sol.CodigoPromotor, sol.NombrePromotor, sol.dia, sol.mes, sol.year,
                    sol.Telefono, sol.Nombre, sol.ApellidoPaterno, sol.ApellidoMaterno, sol.domCasa_Calle + " " + sol.domCasa_numExt + " " + sol.domCasa_numInt, sol.EntreCalles, sol.Municipio, sol.Colonia, sol.Observaciones, sol.NuevoIngreso,
                     sol.domCasa_codigoPostal, sol.RFC, sol.EsquemaPago, sol.MsgError, sol.CodigoActivacion, sol.afiliado_estadoCivil, ref error);

                //Si el registro fue exitoso == 1
                
                switch (estatusRegistro)
                {
                    case 1:
                        sol.IDResultadoSAP = "1";
                        sol.ResultadoSAP = "Correcto";
                        ActualizarResultadoHaciaWeb(sol);
                        break;
                    case 2:
                        sol.IDResultadoSAP = "0";
                        sol.ResultadoSAP = error;
                        ActualizarResultadoHaciaWeb(sol);
                        break;
                }
            }
        }

        private void FrmRegistro_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.Info("Se cerró programa MRS");
            NLog.LogManager.Shutdown();
            salirAddon();
        }

        private void obtenerSolicitudes()
        {
            string WSconsultarSolicitudes;
            WSconsultarSolicitudes = Extensor.Configuracion.CONEXION_SAP.WebServiceConsultarSolicitudes;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(WSconsultarSolicitudes);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader lector = new StreamReader(stream);
                {
                    //Obtiene el Json con las solicitudes
                    string json = lector.ReadToEnd();

                    //Recorta el Json al nivel de los objetos
                    int pos = json.IndexOf("[");
                    json = json.Remove(0, pos);
                    json = json.Remove(json.Length - 1, 1);

                    //Muestra las solicitudes en el Grid
                    MyDt = JsonConvert.DeserializeObject<DataTable>(json);

                    gridSolicitudes.DataSource = MyDt;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error al consultar las solicitudes en Web: {0}", ex.Message);
                Application.Exit();
            }
        }

        private void seleccionarSolicitudes()
        {
            try
            {
                if (MyDt.Rows.Count > 0)
                {
                    foreach (DataRow fila in MyDt.Rows)
                    {
                        Solicitud MiSolicitud = new Solicitud();

                        MiSolicitud.solicitud_id = fila["solicitud_id"].ToString().ToUpper();
                        MiSolicitud.NumeroSolicitud = fila["solicitud_serie"].ToString().ToUpper();
                        MiSolicitud.CodigoPromotor = fila["solicitud_codigoPromotor"].ToString().ToUpper();
                        MiSolicitud.NombrePromotor = fila["solicitud_nombrePromotor"].ToString().ToUpper();
                        MiSolicitud.CodigoActivacion = fila["solicitud_codigoActivacion"].ToString().ToUpper();
                        MiSolicitud.EsquemaPago = fila["solicitud_esquemaPago"].ToString().ToUpper();
                        MiSolicitud.Observaciones = fila["solicitud_observaciones"].ToString().ToUpper();
                        MiSolicitud.NuevoIngreso = fila["solicitud_nuevoIngreso"].ToString().ToUpper();
                        //MiSolicitud.fechaCaptura -- Se ingresa del sistema al momento de activar
                        //Misolicitud.latitud -- No se utiliza en SAP
                        //Misolicitud.longitud -- No se utiliza en SAP
                        MiSolicitud.Nombre = fila["afiliado_nombre"].ToString().ToUpper();
                        MiSolicitud.ApellidoPaterno = fila["afiliado_apellidoPaterno"].ToString().ToUpper();
                        MiSolicitud.ApellidoMaterno = fila["afiliado_apellidoMaterno"].ToString().ToUpper();
                        List<string> Fecha = fila["afiliado_fechaNacimiento"].ToString().Split('-').ToList();
                        MiSolicitud.year = Convert.ToInt32(Fecha[0]);
                        MiSolicitud.mes = Convert.ToInt32(Fecha[1]);
                        MiSolicitud.dia = Convert.ToInt32(Fecha[2]);
                        MiSolicitud.afiliado_estadoCivil = fila["afiliado_estadoCivil"].ToString().ToUpper();
                        // Misolicitu.ocupacion -- No se usa en SAP
                        MiSolicitud.Telefono = fila["afiliado_telefono"].ToString();
                        // Misolicitud.tipo_domiciolo - Se tomara como DIRECCION 1
                        List<string> ColoniaMunicipio = fila["domCobro_coloniaID"].ToString().Split('#').ToList();
                        MiSolicitud.Colonia = ColoniaMunicipio[0].ToUpper();
                        MiSolicitud.Municipio = ColoniaMunicipio[1].ToUpper();
                        MiSolicitud.domCasa_Calle = fila["domCobro_Calle"].ToString().ToUpper();
                        MiSolicitud.domCasa_numExt = fila["domCobro_numExt"].ToString().ToUpper();
                        MiSolicitud.domCasa_numInt = fila["domCobro_numInt"].ToString().ToUpper();
                        MiSolicitud.EntreCalles = fila["domCobro_entreClles"].ToString().ToUpper();
                        MiSolicitud.domCasa_codigoPostal = fila["domCobro_codigoPostal"].ToString().ToUpper();
                        // Datos de cobro


                        string rfc = RfcFacil.RfcBuilder.ForNaturalPerson()
                                                   .WithName(MiSolicitud.Nombre)
                                                   .WithFirstLastName(MiSolicitud.ApellidoPaterno)
                                                   .WithSecondLastName(MiSolicitud.ApellidoMaterno)
                                                   .WithDate(Convert.ToInt32(MiSolicitud.year), Convert.ToInt32(MiSolicitud.mes), Convert.ToInt32(MiSolicitud.dia))
                                                   .Build().ToString();

                        MiSolicitud.RFC = rfc;

                        // Falta mapear los datos de cobro

                        SolicitudesARegistrar.Add(MiSolicitud);

                        Logger.Info(MiSolicitud.solicitud_id + "-" + MiSolicitud.NumeroSolicitud + "-" + MiSolicitud.CodigoPromotor + "-" + MiSolicitud.NombrePromotor + "- RFC: " + MiSolicitud.RFC);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error al cargar las solicitudes en una lista: {0}", ex.Message);
            }
        }

        private int registrarSolicitud(string agente, string solicitud, string codigoAsistente, string nombreAsistente,
                                        int dia, int mes, int year, string telefono, string nombre, string apellidoP, string apellidoM,
                                        string direccion, string entreCalles, string municipio, string colonia, string observaciones, string nvoIngreso,
                                        string codigoPostal, string rfc, string esquema, string msgError, string CodigoActivacion, string EstadoCivil, ref string error)
        {
            try
            {
                SAPbobsCOM.BusinessPartners oSocioNegocio = null;
                string cardCodeGenerate = null;
                string nombreCompleto = null;

                Conexiones.ConexionSAP _oConnection = new Conexiones.ConexionSAP();
                try
                {
                    _oConnection = new Conexiones.ConexionSAP();
                    if (_oConnection.ConectarSAP(ref msgError))
                    {
                        oCompany = _oConnection._oCompany;
                        nombreCompleto = nombre.TrimEnd(' ') + ' ' + apellidoP.TrimEnd(' ') + ' ' + apellidoM.TrimEnd(' ');
                        oSocioNegocio = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                        ObtenerDatosSolicitudValidacion(solicitud);
                        int SeriesLead = 0;
                        string empresa = CodigoActivacion.Substring(1, 1);
                        if (empresa.Equals("C"))
                            SeriesLead = 927;
                        else
                            SeriesLead = 926;

                        oSocioNegocio.Series = SeriesLead;
                        oSocioNegocio.CardType = SAPbobsCOM.BoCardTypes.cLid;
                        oSocioNegocio.GroupCode = ObtenerGrupoLead();
                        oSocioNegocio.CardName = nombreCompleto;
                        oSocioNegocio.FederalTaxID = rfc;
                        oSocioNegocio.DebitorAccount = Extensor.Configuracion.VENTANA.CuentaLead;
                        oSocioNegocio.UserFields.Fields.Item("U_EstadoCivil").Value = EstadoCivil;
                        oSocioNegocio.UserFields.Fields.Item("U_QCapturaContrato").Value = "AppM - " + codigoAsistente;
                        oSocioNegocio.UserFields.Fields.Item("U_SolicitudInt").Value = solicitud;
                        oSocioNegocio.UserFields.Fields.Item("U_CodigoPromotor").Value = codigoAsistente;
                        oSocioNegocio.UserFields.Fields.Item("U_NomProm").Value = nombreAsistente;
                        oSocioNegocio.UserFields.Fields.Item("U_Dia").Value = dia.ToString();
                        oSocioNegocio.UserFields.Fields.Item("U_Mes").Value = mes.ToString();
                        oSocioNegocio.UserFields.Fields.Item("U_Year").Value = year.ToString();
                        oSocioNegocio.UserFields.Fields.Item("U_ComentarioContrato").Value = observaciones;
                        oSocioNegocio.UserFields.Fields.Item("U_PersonaNvoIngreso").Value = nvoIngreso;
                        oSocioNegocio.UserFields.Fields.Item("U_NumArt_").Value = DatosSolicitud.ElementAt(0).plan;
                        oSocioNegocio.UserFields.Fields.Item("U_Dsciption").Value = DatosSolicitud.ElementAt(0).nombrePlan;
                        oSocioNegocio.UserFields.Fields.Item("U_PrefijoPlan").Value = DatosSolicitud.ElementAt(0).prefijoPlan;
                        oSocioNegocio.UserFields.Fields.Item("U_FechaCaptura").Value = DateTime.Now.ToShortDateString();
                        oSocioNegocio.UserFields.Fields.Item("U_HoraCaptura").Value = DateTime.Now.ToString("HH:mm:ss");

                        string esquemaValidacion = string.Empty;

                        if (esquema.Contains("SUELDO"))
                        {
                            esquemaValidacion = Extensor.ValidarEsquemaPago(esquema, codigoAsistente);
                            esquema = esquemaValidacion;
                        }
                        else
                        {
                            esquemaValidacion = "COMISION";
                            esquema = esquemaValidacion;
                        }

                        oSocioNegocio.UserFields.Fields.Item("U_Esquema_pago").Value = esquema;
                        oSocioNegocio.UserFields.Fields.Item("U_Test").Value = "Y";
                        oSocioNegocio.UserFields.Fields.Item("U_CodigoActivacion").Value = CodigoActivacion;

                        if (nombreCompleto.Length > 70)
                        {
                            oSocioNegocio.UserFields.Fields.Item("U_BeneficiarioPagoRe").Value = nombreCompleto.ToString().Substring(1, 70);
                        }
                        else
                        {
                            oSocioNegocio.UserFields.Fields.Item("U_BeneficiarioPagoRe").Value = nombreCompleto;
                        }
                        oSocioNegocio.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_BillTo;
                        oSocioNegocio.Addresses.AddressName = "COBRO";
                        oSocioNegocio.Addresses.AddressName2 = telefono;
                        oSocioNegocio.Addresses.Street = direccion;
                        oSocioNegocio.Addresses.BuildingFloorRoom = entreCalles;
                        oSocioNegocio.Addresses.City = municipio;
                        oSocioNegocio.Addresses.Block = colonia;
                        oSocioNegocio.Addresses.ZipCode = codigoPostal;
                        oSocioNegocio.Addresses.State = "JAL";

                        Logger.Info("-----------------------------------------------------------------------------------------");
                        Logger.Info("Información de pre-contrato: " + agente + " - " + solicitud + " - " + codigoAsistente + " - " + nombreAsistente + " - " + telefono + " - " + nombre + " - " + apellidoP + " - " + apellidoM +
                                          " - " + direccion + " - " + entreCalles + " - " + municipio + " - " + colonia + " - " + observaciones + " - " + nvoIngreso + " - " + codigoPostal + " - " + rfc + " - " + esquema + " - " +
                                                msgError + " - " + CodigoActivacion);

                        if (oSocioNegocio.Add() != 0)
                        {
                            msgError = "Error: " + oCompany.GetLastErrorDescription();
                            error = msgError;
                            Logger.Info("Activacion Fallida: " + oCompany.GetLastErrorDescription());
                            return 2;
                        }
                        else
                        {
                            oCompany.GetNewObjectCode(out cardCodeGenerate);
                            Logger.Info("Activacion exitosa: " + cardCodeGenerate);
                            msgError = string.Empty;
                            error = msgError;
                            return 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Error al registrar la solicitud {0} en SAP: {1}", solicitud, ex.Message);
                    error = "Solicitud: " + solicitud + ", Ex: " + ex.Message;
                    return 2;
                }


                Logger.Info("Solicitud registrada en SAP {0}", solicitud);
                error = string.Empty;
                return 1;

            }
            catch (Exception ex)
            {
                Logger.Error("Error al registrar la solicitud {0} en SAP: {1}", solicitud, ex.Message);
                error = "Solicitud: " + solicitud + ", Ex: " + ex.Message;
                return 2;
            }
        }

        private static void ObtenerDatosSolicitudValidacion(string solicitud)
        {
            SqlConnection conexion = null;
            SqlDataReader reader = null;
            SqlCommand comando = new SqlCommand();
            DatosSolicitud.Clear();
            try
            {
                conexion = Conexiones.ConexionSQL.ObtenerConexionSQL();
                comando.Connection = conexion;
                comando.CommandText = @"SELECT TOP 1
                                                T1.Series AS Series ,
                                                T0.U_Codigo_Plan AS CodigoPlan ,
                                                T0.U_Descripcion_Plan AS NombrePlan ,
                                                T0.U_Prefijo_Contr AS PrefijoSolicitud
                                        FROM    dbo.[@COMISIONES] T0
                                                INNER JOIN dbo.NNM1 T1 ON T0.U_Empresa = T1.Remark
                                                                            AND T1.ObjectCode = 2
                                        WHERE   T0.U_Prefijo_Sol = SUBSTRING('" + solicitud + "', 1, 6)";
                reader = comando.ExecuteReader();
                if (reader.HasRows == true)
                {
                    if (reader.Read())
                    {
                        itemSolicitud = new Listas.ListaSolicitud();
                        itemSolicitud.serieLead = Convert.ToInt32(reader["Series"]);
                        itemSolicitud.plan = reader["CodigoPlan"].ToString();
                        itemSolicitud.nombrePlan = reader["NombrePlan"].ToString();
                        itemSolicitud.prefijoPlan = reader["PrefijoSolicitud"].ToString();
                        DatosSolicitud.Add(itemSolicitud);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener serie Lead: " + ex.Message);
            }
            finally
            {
                conexion.Close();
                SqlConnection.ClearPool(conexion);
                conexion.Dispose();
            }
        }

        private static int ObtenerGrupoLead()
        {
            SqlConnection conexion = null;
            SqlDataReader reader = null;
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion = Conexiones.ConexionSQL.ObtenerConexionSQL();
                comando.Connection = conexion;
                comando.CommandText = @"SELECT TOP 1
                                                GroupCode
                                        FROM    dbo.OCRG
                                        WHERE   GroupName LIKE '%PABS%'";
                reader = comando.ExecuteReader();
                if (reader.HasRows == true)
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["GroupCode"]);
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener grupo Lead: " + ex.Message);
            }
            finally
            {
                conexion.Close();
                SqlConnection.ClearPool(conexion);
                conexion.Dispose();
            }
        }

        public static string ValidarEsquemaPago(string esquema, string codigoAsistente)
        {
            SqlConnection conexion = null;
            SqlDataReader reader = null;
            SqlCommand comando = new SqlCommand();
            int numeroAfiliaciones = 0;
            int numeroContratos = 0;

            try
            {
                conexion = Conexiones.ConexionSQL.ObtenerConexionSQL();
                comando.Connection = conexion;
                comando.CommandText = @"SELECT TOP 1  ISNULL(T2.U_NumeroAyuda,0) AS NumeroAyudas ,
                                                T1.U_CantidadContratos AS CantidadContratos
                                        FROM    dbo.OHEM T0
                                                INNER JOIN dbo.[@CONFIG_ESQUEMAS] T1 ON T0.U_Esquema_pago = T1.U_Esquema
                                                LEFT JOIN dbo.[@AYUDAS] T2 ON T0.firstName = T2.U_CodigoAsistente
                                        WHERE   T0.firstName = '" + codigoAsistente + "'";
                reader = comando.ExecuteReader();
                if (reader.HasRows == true)
                {
                    if (reader.Read())
                    {
                        numeroAfiliaciones = Convert.ToInt32(reader["NumeroAyudas"]);
                        numeroContratos = Convert.ToInt32(reader["CantidadContratos"]);
                    }
                }

                if (numeroAfiliaciones >= numeroContratos)
                {
                    return "COMISION";
                }
                else
                {
                    return "SUELDO";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar esquema: " + ex.Message);
            }
            finally
            {
                conexion.Close();
                SqlConnection.ClearPool(conexion);
                conexion.Dispose();
            }
        }

        private bool ActualizarResultadoHaciaWeb(Solicitud sol)
        {
            string result;
            string WebServiceActualizarSolicitudes;
            WebServiceActualizarSolicitudes = Extensor.Configuracion.CONEXION_SAP.WebServiceActualizarSolicitudes;

            try
            {
                //Consumo de WS -> Actualizar hacia eCobro
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(WebServiceActualizarSolicitudes);
                request.ContentType = "application/json";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    //{"solicitudes": [{"solicitud_serie": "020101001758","registrada": "1","resultado": "mensaje_de_solicitud_registrada"}]}

                    string json = "{\"solicitudes\": [{\"solicitud_serie\": \"" + sol.NumeroSolicitud + "\",\"registrada\": \"" + sol.IDResultadoSAP + "\",\"resultado\": \"" + sol.ResultadoSAP + "\"}]}";
                    streamWriter.Write(json);
                }

                //Respuesta de WS -> Actualización correcta o incorrecta
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                if (result.Contains(sol.NumeroSolicitud))
                {
                    sol.ResultadoECOBRO = "Correcto";
                    Logger.Info("Actualizacion en eCobro con estatus: " + sol.IDResultadoSAP + " - " + sol.ResultadoSAP);
                    return true;
                }
                else
                {
                    Logger.Info("Error al actualizar en eCobro {Environment.NewLine}");
                    sol.ResultadoECOBRO = "Error";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info("Error en la Función de actualización en Web del cliente " + ex.ToString());
                sol.ResultadoECOBRO = "Error C#";

                return false;
            }
        }


        #endregion

    }
}

