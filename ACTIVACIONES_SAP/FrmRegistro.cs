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

/// <summary>
/// Nombre: MRS (Módulo de Registro de Solicitudes)
/// Objetivo: Crear un socio de negocio de tipo LEAD en SAP con las solicitudes registradas en la aplicación móvil "Asistencia social"
/// Tipo: Sincronizador(Se ejecuta cada 1 hora desde 192.168.0.21)
/// Lenguaje: C#
/// Versión: 1.0
/// Desarrollado por: Enrique Bernal, Jaime Rodríguez
/// Abreviaturas: SN(Socio de negocio)
/// Correo de incidencias: mrspabs @gmail.com
/// </summary>

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

        /// <summary>
        /// El programa se ejecuta en dos modos: automático y manual, el parametro de configuración se encuentra en el archivo CONFIGURACION.xml
        /// </summary>
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

        /// <summary>
        /// Obtiene la información de las solicitudes (en formato JSON) a través de un web service y las guarda en un DataTable
        /// </summary>
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

        /// <summary>
        /// Ingresa en una lista de objetos de tipo Solicitud las solicitudes que se encuentran en un DataTable
        /// </summary>
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

                        // Domicilio de casa. Desde la aplicación móvil se valida que exista el domicilio de casa y de cobro
                        MiSolicitud.domCasa_Calle = fila["domCasa_Calle"].ToString().ToUpper();
                        MiSolicitud.domCasa_numExt = fila["domCasa_numExt"].ToString().ToUpper();
                        MiSolicitud.domCasa_numInt = fila["domCasa_numInt"].ToString().ToUpper();
                        MiSolicitud.domCasa_EntreCalles = fila["domCasa_EntreCalles"].ToString().ToUpper();
                        MiSolicitud.domCasa_codigoPostal = fila["domCasa_codigoPostal"].ToString().ToUpper();

                        //Ejemplo: "domCasa_ColoniaID": "GIRASOLES ACUEDUCTO#ZAPOPAN"
                        List<string> ColoniaMunicipioCasa = fila["domCasa_ColoniaID"].ToString().Split('#').ToList();
                        MiSolicitud.Colonia_casa = ColoniaMunicipioCasa[0].ToUpper();
                        MiSolicitud.Municipio_casa = ColoniaMunicipioCasa[1].ToUpper();

                        // Domicilio de cobro
                        MiSolicitud.Calle_cobro = fila["domCobro_Calle"].ToString().ToUpper();
                        MiSolicitud.Num_ext_cobro = fila["domCobro_numExt"].ToString().ToUpper();
                        MiSolicitud.Num_int_cobro = fila["domCobro_numInt"].ToString().ToUpper();
                        MiSolicitud.Entre_calles_cobro = fila["domCobro_entreClles"].ToString().ToUpper();
                        MiSolicitud.Codigo_postal_cobro = fila["domCobro_codigoPostal"].ToString().ToUpper();

                        //Ejemplo: "domCobro_coloniaID": "GIRASOLES ACUEDUCTO#ZAPOPAN"
                        List<string> ColoniaMunicipioCobro = fila["domCobro_coloniaID"].ToString().Split('#').ToList();
                        MiSolicitud.Colonia_cobro = ColoniaMunicipioCobro[0].ToUpper();
                        MiSolicitud.Municipio_cobro = ColoniaMunicipioCobro[1].ToUpper();

                        try
                        {
                            string rfc = RfcFacil.RfcBuilder.ForNaturalPerson()
                                                       .WithName(MiSolicitud.Nombre)
                                                       .WithFirstLastName(MiSolicitud.ApellidoPaterno)
                                                       .WithSecondLastName(MiSolicitud.ApellidoMaterno)
                                                       .WithDate(Convert.ToInt32(MiSolicitud.year), Convert.ToInt32(MiSolicitud.mes), Convert.ToInt32(MiSolicitud.dia))
                                                       .Build().ToString();

                            MiSolicitud.RFC = rfc;
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex, "Error al generar RFC: " + ex.Message);
                            Logger.Fatal(ex, "Error al generar RFC: " + ex.Message);

                            continue;
                        }

                        // Falta mapear los datos de cobro ???

                        SolicitudesARegistrar.Add(MiSolicitud);

                        Logger.Info(MiSolicitud.solicitud_id + "-" + MiSolicitud.NumeroSolicitud + "-" + MiSolicitud.CodigoPromotor + "-" + MiSolicitud.NombrePromotor + "- RFC: " + MiSolicitud.RFC);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al cargar las solicitudes en una lista: " + ex.Message);
                Logger.Fatal(ex, "Error al cargar las solicitudes en una lista: " + ex.Message);
            }
        }

        /// <summary>
        /// Itera en el listado de solicitudes e invoca las siguientes funciones: 1. Registrar la solicitud en SAP 2. Actualizar el resultado hacia eCobro
        /// </summary>
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            seleccionarSolicitudes();
            int estatusRegistro = 1;
            string error = string.Empty;

            //// INICIO PRUEBA
            //SolicitudesARegistrar.Clear();

            //Solicitud misol = new Solicitud();

            //misol.solicitud_id = "1";
            //misol.afiliado_estadoCivil = "CASADO(A)";
            //misol.Agente = "AGENTE DE PRUEBA";
            //misol.NumeroSolicitud = "020105001000";
            //misol.CodigoPromotor = "P0123";
            //misol.NombrePromotor = "PROMOTOR DE PRUEBA";

            //misol.Telefono = "123456789";
            //misol.Nombre = "NOMBRE CLIENTE";
            //misol.ApellidoPaterno = "APELLIDO PATERNO CLIENTE";
            //misol.ApellidoMaterno = "APELLIDO MATERNO CLIENTE";
            //misol.Observaciones = "OBSERVACIONES";
            //misol.NuevoIngreso = "NUEVO INGRESO";
            //misol.CP = "12345";
            //misol.RFC = "ZIPR690616MT0";
            //misol.EsquemaPago = "COMISION";
            //misol.MsgError = "";
            //misol.CodigoActivacion = "MA0000001";
            //misol.Archivos = "";
            //misol.dia = 1;
            //misol.mes = 1;
            //misol.year = 1990;
            //misol.IDResultadoSAP = "";
            //misol.ResultadoSAP = "";
            //misol.ResultadoECOBRO = "";

            //misol.domCasa_codigoPostal = "12345";
            //misol.domCasa_Calle = "CALLE DE CASA";
            //misol.domCasa_numExt = "EXT CASA";
            //misol.domCasa_numInt = "INT CASA";
            //misol.domCasa_EntreCalles = "ENTRE CALLES CASA";
            //misol.Colonia_casa = "COLONIA CASA";
            //misol.Municipio_casa = "MUNICIPIO CASA";

            //misol.Calle_cobro = "CALLE COBRO";
            //misol.Num_ext_cobro = "EXTERIOR COBRO";
            //misol.Num_int_cobro = "INTERIOR COBRO";
            //misol.Entre_calles_cobro = "ENTRE CALLES COBRO";
            //misol.Codigo_postal_cobro = "6789";
            //misol.Colonia_cobro = "COLONIA COBRO";
            //misol.Municipio_cobro = "MUNICIPIO COBRO";

            //SolicitudesARegistrar.Add(misol);

            ////FIN PRUEBA

            foreach (Solicitud sol in SolicitudesARegistrar)
            {
                //Se conecta a SAP y agrega un socio de negocio de tipo Lead con la información de la solicitud
                estatusRegistro = registrarSolicitud(sol.Agente, sol.NumeroSolicitud, sol.CodigoPromotor, sol.NombrePromotor, sol.dia, sol.mes, sol.year,
                    sol.Telefono, sol.Nombre, sol.ApellidoPaterno, sol.ApellidoMaterno, sol.domCasa_Calle + " " + sol.domCasa_numExt + " " + sol.domCasa_numInt, sol.domCasa_EntreCalles, sol.Municipio_casa, sol.Colonia_casa, sol.Observaciones, sol.NuevoIngreso,
                     sol.domCasa_codigoPostal, sol.RFC, sol.EsquemaPago, sol.MsgError, sol.CodigoActivacion, sol.afiliado_estadoCivil, ref error,
                     sol.Calle_cobro, sol.Num_ext_cobro, sol.Num_int_cobro, sol.Entre_calles_cobro, sol.Codigo_postal_cobro, sol.Colonia_cobro, sol.Municipio_cobro
                     );

                //Registro exitoso = 1, fallido = 0
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
       
        /// <summary>
        /// Se conecta a SAP y agrega un socio de negocio de tipo Lead con la información de la solicitud.
        /// </summary>
        /// <returns>exito = 1, error = 2 </returns>
        private int registrarSolicitud(string agente, string solicitud, string codigoAsistente, string nombreAsistente,
                                        int dia, int mes, int year, string telefono, string nombre, string apellidoP, string apellidoM,
                                        string direccion, string entreCalles, string municipio, string colonia, string observaciones, string nvoIngreso,
                                        string codigoPostal, string rfc, string esquema, string msgError, string CodigoActivacion, string EstadoCivil, ref string error,
                                        string Calle_cobro, string Num_ext_cobro, string Num_int_cobro, string Entre_calles_cobro, string Codigo_postal_cobro, string Colonia_cobro, string Municipio_cobro)
        {
            SAPbobsCOM.BusinessPartners oSocioNegocio = null;
            string cardCodeGenerate = null;
            string nombreCompleto = null;

            try
            {
                Conexiones.ConexionSAP _oConnection = new Conexiones.ConexionSAP();

                _oConnection = new Conexiones.ConexionSAP();
                if (_oConnection.ConectarSAP(ref msgError))
                {
                    oCompany = _oConnection._oCompany;
                    nombreCompleto = nombre.TrimEnd(' ') + ' ' + apellidoP.TrimEnd(' ') + ' ' + apellidoM.TrimEnd(' ');
                    oSocioNegocio = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                    ObtenerDatosSolicitudValidacion(solicitud);
                    
                    //Asignacion de empresa
                    int SeriesLead = 0;
                    string empresa = CodigoActivacion.Substring(1, 1);
                    if (empresa.Equals("C"))
                        SeriesLead = 927;
                    else
                        SeriesLead = 926;
                        
                    //Datos generales
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

                    //Domicilio de casa
                    oSocioNegocio.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_BillTo;
                    oSocioNegocio.Addresses.AddressName = "DIRECCION 1";
                    oSocioNegocio.Addresses.AddressName2 = telefono;
                    oSocioNegocio.Addresses.Street = direccion;
                    oSocioNegocio.Addresses.BuildingFloorRoom = entreCalles;
                    oSocioNegocio.Addresses.City = municipio;
                    oSocioNegocio.Addresses.Block = colonia;
                    oSocioNegocio.Addresses.ZipCode = codigoPostal;
                    oSocioNegocio.Addresses.State = "JAL";

                    oSocioNegocio.Addresses.Add();

                    //Domicilio de cobro
                    oSocioNegocio.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_BillTo;
                    oSocioNegocio.Addresses.AddressName = "COBRO";
                    oSocioNegocio.Addresses.AddressName2 = telefono;
                    oSocioNegocio.Addresses.Street = Calle_cobro + " " +  Num_ext_cobro + " " + Num_int_cobro;
                    oSocioNegocio.Addresses.BuildingFloorRoom = Entre_calles_cobro;
                    oSocioNegocio.Addresses.City = Municipio_cobro;
                    oSocioNegocio.Addresses.Block = Colonia_cobro;
                    oSocioNegocio.Addresses.ZipCode = Codigo_postal_cobro;
                    oSocioNegocio.Addresses.State = "JAL";

                    Logger.Info("-----------------------------------------------------------------------------------------");
                    Logger.Info("Información de pre-contrato: " + agente + " - " + solicitud + " - " + codigoAsistente + " - " + nombreAsistente + " - " + telefono + " - " + nombre + " - " + apellidoP + " - " + apellidoM +
                                        " - " + direccion + " - " + entreCalles + " - " + municipio + " - " + colonia + " - " +
                                        " - " + (Calle_cobro + " " + Num_ext_cobro + " " + Num_int_cobro) + " - " + Entre_calles_cobro + " - " + Municipio_cobro + " - " + Colonia_cobro + 
                                        " - " + observaciones + " - " + nvoIngreso + " - " + codigoPostal + " - " + rfc + " - " + esquema + 
                                        " - " + msgError + " - " + CodigoActivacion);

                    if (oSocioNegocio.Add() != 0)
                    {
                        msgError = "Error: " + oCompany.GetLastErrorDescription();
                        error = msgError;
                        Logger.Info("Activacion Fallida: " + oCompany.GetLastErrorDescription());
                        Logger.Fatal("Activacion Fallida: " + oCompany.GetLastErrorDescription());
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
                else
                {
                    Logger.Error("Error al conectar a SAP: {1}"+ solicitud);
                    return 2;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error al registrar la solicitud {0} en SAP: {1}", solicitud, ex.Message);
                Logger.Fatal("Error al registrar la solicitud {0} en SAP: {1}", solicitud, ex.Message);

                error = "Solicitud: " + solicitud + ", Ex: " + ex.Message;
                return 2;
            }
        }
        
        /// <summary>
        /// Obtiene los datos del plan de acuerdo al prefijo de la solicitud. Ejemplo: 020405 es igual a 2CJ
        /// </summary>
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

        /// <summary>
        /// Retorna el grupo de los servicios a previsión
        /// </summary>
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

        /// <summary>
        /// Retorna el tipo de esquema del asistente de acuerdo a la siguiente validacion:
        /// si el número de ayudas de sueldo supera el máximo permitido, retorna COMISION, de lo contrario SUELDO.
        /// Ejemplo: el esquemo 15003M solo permite ingresar 2 ayudas de sueldo y las demas van a comisión
        /// </summary>
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

        /// <summary>
        /// Actualiza el resultado del registro de la solicitud hacia eCobro por medio de un Web service
        /// </summary>
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
                Logger.Fatal("Error en la Función de actualización en Web del cliente " + ex.ToString());
                sol.ResultadoECOBRO = "Error C#";

                return false;
            }
        }

        private void FrmRegistro_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.Info("Se cerró programa MRS");
            NLog.LogManager.Shutdown();
            salirAddon();
        }

        #endregion

    }
}

