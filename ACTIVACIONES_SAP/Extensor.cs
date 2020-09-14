using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MRS
{
    public static class Extensor
    {
        #region VARIABLES

        public static CONFIGURACION Configuracion;

        public static List<Listas.ListaSocioNegocio> DatosSN = new List<Listas.ListaSocioNegocio>();
        public static Listas.ListaSocioNegocio itemDatosSN = new Listas.ListaSocioNegocio();
        public static List<Listas.ListaMunicipios> DatosMunicipios = new List<Listas.ListaMunicipios>();
        public static Listas.ListaMunicipios itemMunicipios = new Listas.ListaMunicipios();
        public static List<Listas.ListaColonias> DatosColonias = new List<Listas.ListaColonias>();
        public static Listas.ListaColonias itemColonias = new Listas.ListaColonias();
        public static List<Listas.ListaSolicitud> DatosSolicitud = new List<Listas.ListaSolicitud>();
        public static Listas.ListaSolicitud itemSolicitud = new Listas.ListaSolicitud();
        private static SAPbobsCOM.Company oCompany = null;

        #endregion

        #region METODOS

        /// <summary>
        /// Carga la clase del xml 
        /// </summary>
        /// <returns>Clase de configuraciones</returns>
        public static bool CargarConfiguraciones()
        {
            try
            {
                Configuracion = new CONFIGURACION();
                Configuracion = Configuracion.CargarXML("XMLConfig\\Configuracion.xml");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"No se pudo cargar el archivo de configuraciones: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Serializa el xml de configuración
        /// </summary>
        /// <param name="obj">Objeto de la clase</param>
        /// <param name="NombreXml">Nombre del xml</param>
        /// <returns></returns>
        public static CONFIGURACION CargarXML(this CONFIGURACION obj, string NombreXml)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(CONFIGURACION));
            try
            {
                if (System.IO.File.Exists(NombreXml))
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(NombreXml);
                    obj = (CONFIGURACION)serializer.Deserialize(sr);
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show(@"No se puede cargar el archivo: " + ex.Message);
            }
            return obj;
        }

        public static bool CodigoActivacion(string solicitud)
        {
            SqlConnection conexion = null;
            SqlDataReader reader = null;
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion = Conexiones.ConexionSQL.ObtenerConexionSQL();
                comando.Connection = conexion;
                comando.CommandText = @"SELECT  CardCode
                                        FROM    dbo.OCRD
                                        WHERE   U_SolicitudInt = '" + solicitud + "'";
                reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar si existe código: " + ex.Message);
            }
            finally
            {
                conexion.Close();
                SqlConnection.ClearPool(conexion);
                conexion.Dispose();
            }

        }

        public static List<Listas.ListaSocioNegocio> ObtenerDatosSN(string solicitud)
        {
            SqlConnection conexion = null;
            SqlDataReader reader = null;
            SqlCommand comando = new SqlCommand();
            DatosSN.Clear();
            itemDatosSN = null;
            try
            {
                conexion = Conexiones.ConexionSQL.ObtenerConexionSQL();
                comando.Connection = conexion;
                comando.CommandText = @"SELECT  ISNULL(T0.U_CodigoPromotor, '') AS CodigoAsistente ,
                                                ISNULL(T0.U_NomProm, '') AS NombrePromotor ,
                                                CONCAT(T0.U_Dia, '/', T0.U_Mes, '/', T0.U_Year) AS FechaNacimiento ,
                                                ISNULL(T1.Address2, '') AS Telefono ,
                                                ISNULL(T0.CardName, '') AS NombreSN ,
                                                ISNULL(T1.Street, '') AS Direccion ,
                                                ISNULL(T1.Building, '') AS EntreCalles ,
                                                ISNULL(T1.City, '') AS Municipio ,
                                                ISNULL(T1.Block, '') AS Colonia ,
                                                ISNULL(T0.U_ComentarioContrato, '') AS Observaciones ,
                                                ISNULL(T0.U_PersonaNvoIngreso, '') AS PersonaNvoIngreso ,
                                                ISNULL(T1.ZipCode, '') AS CodigoPostal ,
                                                ISNULL(T0.LicTradNum, '') AS RFC ,
                                                ISNULL(T0.U_CodigoActivacion, '') AS CodigoActivacion ,
                                                ISNULL(T0.U_QCapturaContrato, '') AS QuienCaptura ,
                                                CASE WHEN T0.U_Esquema_pago IS NULL
                                                         THEN CASE WHEN T2.U_Esquema_pago LIKE '%SUELDO%' THEN 'SUELDO'
                                                                   ELSE T2.U_Esquema_pago
                                                              END
                                                         ELSE T0.U_Esquema_pago
                                                    END AS Esquema
                                        FROM    dbo.OCRD T0
                                                LEFT JOIN dbo.CRD1 T1 ON T0.CardCode = T1.CardCode
                                                                         AND T1.Address = 'DIRECCION 1'
                                                INNER JOIN dbo.OHEM T2 ON T0.U_CodigoPromotor = T2.firstName
                                        WHERE   U_SolicitudInt = '" + solicitud + "'";
                reader = comando.ExecuteReader();
                if (reader.HasRows == true)
                {
                    if (reader.Read())
                    {
                        itemDatosSN = new Listas.ListaSocioNegocio();
                        itemDatosSN.codigoAsistente = reader["CodigoAsistente"].ToString();
                        itemDatosSN.NombreAsistente = reader["NombrePromotor"].ToString();
                        itemDatosSN.FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]);
                        itemDatosSN.Telefono = reader["Telefono"].ToString();
                        itemDatosSN.NombreSN = reader["NombreSN"].ToString();
                        itemDatosSN.Direccion = reader["Direccion"].ToString();
                        itemDatosSN.EntreCalles = reader["EntreCalles"].ToString();
                        itemDatosSN.Municipio = reader["Municipio"].ToString();
                        itemDatosSN.Colonia = reader["Colonia"].ToString();
                        itemDatosSN.Observaciones = reader["Observaciones"].ToString();
                        itemDatosSN.NvoIngreso = reader["PersonaNvoIngreso"].ToString();
                        itemDatosSN.CodigoPostal = reader["CodigoPostal"].ToString();
                        itemDatosSN.RFC = reader["RFC"].ToString();
                        itemDatosSN.CodigoActivacion = reader["CodigoActivacion"].ToString();
                        itemDatosSN.QuienCaptura = reader["QuienCaptura"].ToString();
                        itemDatosSN.Esquema = reader["Esquema"].ToString();
                        DatosSN.Add(itemDatosSN);
                    }
                }
                return DatosSN;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos SN: " + ex.Message);
            }
            finally
            {
                conexion.Close();
                SqlConnection.ClearPool(conexion);
                conexion.Dispose();
            }
        }

        public static List<Listas.ListaMunicipios> ObtenerMunicipios()
        {
            SqlConnection conexion = null;
            SqlDataReader reader = null;
            SqlCommand comando = new SqlCommand();
            DatosMunicipios.Clear();
            itemMunicipios = null;
            try
            {
                conexion = Conexiones.ConexionSQL.ObtenerConexionSQL();
                comando.Connection = conexion;
                comando.CommandText = @"SELECT  Value AS Municipio
                                        FROM    dbo.CUVV
                                        WHERE   IndexID = " + Extensor.Configuracion.VENTANA.IndexID_CUVV + "" +
                                        "ORDER BY Value";
                reader = comando.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        itemMunicipios = new Listas.ListaMunicipios();
                        itemMunicipios.Municipio = reader["Municipio"].ToString();
                        DatosMunicipios.Add(itemMunicipios);
                    }

                }
                itemMunicipios.Municipio = "";
                DatosMunicipios.Add(itemMunicipios);

                return DatosMunicipios;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Municipios: " + ex.Message);
            }
            finally
            {
                conexion.Close();
                SqlConnection.ClearPool(conexion);
                conexion.Dispose();
            }

        }

        public static List<Listas.ListaColonias> ObtenerColonias(string municipio)
        {
            SqlConnection conexion = null;
            SqlDataReader reader = null;
            SqlCommand comando = new SqlCommand();
            DatosColonias.Clear();
            itemColonias = null;
            try
            {
                conexion = Conexiones.ConexionSQL.ObtenerConexionSQL();
                comando.Connection = conexion;
                comando.CommandText = @"SELECT  U_Colonia as Colonia
                                        FROM    dbo.[@COLONIAS]
                                        WHERE   U_Municipio = '" + municipio + "'" +
                                        "ORDER BY U_Colonia ";
                reader = comando.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        itemColonias = new Listas.ListaColonias();
                        itemColonias.Colonia = reader["Colonia"].ToString();
                        DatosColonias.Add(itemColonias);
                    }
                }
                return DatosColonias;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Colonias: " + ex.Message);
            }
            finally
            {
                conexion.Close();
                SqlConnection.ClearPool(conexion);
                conexion.Dispose();
            }

        }

        public static List<Listas.ListaSocioNegocio> ObtenerDatosSolicitud(string solicitud)
        {
            SqlConnection conexion = null;
            SqlDataReader reader = null;
            SqlCommand comando = new SqlCommand();
            DatosSN.Clear();
            itemDatosSN = null;
            try
            {
                conexion = Conexiones.ConexionSQL.ObtenerConexionSQL();
                comando.Connection = conexion;
                comando.CommandText = @"SELECT TOP 1
                                                T3.firstName AS codigoAsistente ,
                                                CONCAT(T3.middleName, ' ', T3.lastName) AS NombrePromotor ,
                                                CASE WHEN T3.U_Esquema_pago LIKE '%SUELDO%' THEN 'SUELDO'
                                                     ELSE T3.U_Esquema_pago
                                                END AS Esquema
                                        FROM    dbo.OWTR T0
                                                INNER JOIN dbo.WTR1 T1 ON T0.DocEntry = T1.DocEntry
                                                INNER JOIN dbo.OSLP T2 ON T0.SlpCode = T2.SlpCode
                                                INNER JOIN dbo.OHEM T3 ON T2.SlpCode = T3.salesPrson
                                                INNER JOIN dbo.[@COMISIONES] T4 ON T4.U_Prefijo_Sol = SUBSTRING(T1.U_Serie,
                                                              1, 6)
                                        WHERE   T1.[U_Serie] = '" + solicitud + "' " +
                                                "AND T0.CANCELED <> 'Y' " +
                                                "AND T0.DataSource <> 'N' " +
                                                "AND ( T0.U_TipoMov = 'OFICINAS - PROMOTORES' " +
                                                      "OR T0.U_TipoMov = 'ADMON CONTRATOS - PROMOTOR' " +
                                                    ")" +
                                        "ORDER BY T0.DocEntry DESC";
                reader = comando.ExecuteReader();
                if (reader.HasRows == true)
                {
                    if (reader.Read())
                    {
                        itemDatosSN = new Listas.ListaSocioNegocio();
                        itemDatosSN.codigoAsistente = reader["CodigoAsistente"].ToString();
                        itemDatosSN.NombreAsistente = reader["NombrePromotor"].ToString();
                        itemDatosSN.Esquema = reader["Esquema"].ToString();
                        DatosSN.Add(itemDatosSN);
                    }
                }
                return DatosSN;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos SN solicitud: " + ex.Message);
            }
            finally
            {
                conexion.Close();
                SqlConnection.ClearPool(conexion);
                conexion.Dispose();
            }
        }

        public static void GenearLeadSAP2(string agente, string solicitud, string codigoAsistente, string nombreAsistente, int dia, int mes, int year, string telefono, string nombre, string apellidoP, string apellidoM,
                                          string direccion, string entreCalles, string municipio, string colonia, string observaciones, string nvoIngreso, string codigoPostal, string rfc, string esquema,
                                            ref string msgError, ref string CodigoActivacion)
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
                    oCompany.StartTransaction();
                    nombreCompleto = nombre.TrimEnd(' ') + ' ' + apellidoP.TrimEnd(' ') + ' ' + apellidoM.TrimEnd(' ');
                    oSocioNegocio = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                    ObtenerDatosSolicitudValidacion(solicitud);
                    oSocioNegocio.Series = DatosSolicitud.ElementAt(0).serieLead;
                    oSocioNegocio.CardType = SAPbobsCOM.BoCardTypes.cLid;
                    oSocioNegocio.GroupCode = ObtenerGrupoLead();
                    oSocioNegocio.CardName = nombreCompleto;
                    oSocioNegocio.FederalTaxID = rfc;
                    oSocioNegocio.DebitorAccount = Extensor.Configuracion.VENTANA.CuentaLead;
                    oSocioNegocio.UserFields.Fields.Item("U_QCapturaContrato").Value = agente;
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
                    oSocioNegocio.UserFields.Fields.Item("U_Esquema_pago").Value = esquema;

                    if (nombreCompleto.Length > 70)
                    {
                        oSocioNegocio.UserFields.Fields.Item("U_BeneficiarioPagoRe").Value = nombreCompleto.ToString().Substring(1, 70);
                    }
                    else
                    {
                        oSocioNegocio.UserFields.Fields.Item("U_BeneficiarioPagoRe").Value = nombreCompleto;
                    }
                    oSocioNegocio.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_BillTo;
                    oSocioNegocio.Addresses.AddressName = "DIRECCION 1";
                    oSocioNegocio.Addresses.AddressName2 = telefono;
                    oSocioNegocio.Addresses.Street = direccion;
                    oSocioNegocio.Addresses.BuildingFloorRoom = entreCalles;
                    oSocioNegocio.Addresses.City = municipio;
                    oSocioNegocio.Addresses.Block = colonia;
                    oSocioNegocio.Addresses.ZipCode = codigoPostal;
                    oSocioNegocio.Addresses.State = "JAL";

                    if (oSocioNegocio.Add() != 0)
                    {
                        msgError = "Error: " + oCompany.GetLastErrorDescription();
                    }
                    else
                    {
                        //oCompany.GetNewObjectCode(out cardCodeGenerate);
                        msgError = "";
                    }
                }
                else
                {
                    msgError = "Error: " + msgError;
                }
            }
            catch (Exception ex)
            {
                msgError = "Error: " + msgError + " : " + ex.Message;
            }
            finally
            {
                if (!msgError.Contains("Error"))
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                    //CodigoActivacion = ObtenerCodigoActivacionGenerado(cardCodeGenerate);
                    //msgError = "El código de activación es: " + CodigoActivacion;
                }
                else
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }
                if (oSocioNegocio != null)
                {
                    GC.SuppressFinalize(oSocioNegocio);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oSocioNegocio);
                    oSocioNegocio = null;
                }
                GC.Collect();
                ClearMemory();
                oCompany.Disconnect();
            }
        }

        public static void GenearLeadSAP(string agente, string solicitud, string codigoAsistente, string nombreAsistente, int dia, int mes, int year, string telefono, string nombre, string apellidoP, string apellidoM,
                                          string direccion, string entreCalles, string municipio, string colonia, string observaciones, string nvoIngreso, string codigoPostal, string rfc, string esquema,
                                            ref string msgError, ref string CodigoActivacion)
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
                    oCompany.StartTransaction();
                    nombreCompleto = nombre.TrimEnd(' ') + ' ' + apellidoP.TrimEnd(' ') + ' ' + apellidoM.TrimEnd(' ');
                    oSocioNegocio = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                    ObtenerDatosSolicitudValidacion(solicitud);
                    oSocioNegocio.Series = DatosSolicitud.ElementAt(0).serieLead;
                    oSocioNegocio.CardType = SAPbobsCOM.BoCardTypes.cLid;
                    oSocioNegocio.GroupCode = ObtenerGrupoLead();
                    oSocioNegocio.CardName = nombreCompleto;
                    oSocioNegocio.FederalTaxID = rfc;
                    oSocioNegocio.DebitorAccount = Extensor.Configuracion.VENTANA.CuentaLead;
                    oSocioNegocio.UserFields.Fields.Item("U_QCapturaContrato").Value = agente;
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
                    oSocioNegocio.UserFields.Fields.Item("U_Esquema_pago").Value = esquema;

                    if (nombreCompleto.Length > 70)
                    {
                        oSocioNegocio.UserFields.Fields.Item("U_BeneficiarioPagoRe").Value = nombreCompleto.ToString().Substring(1, 70);
                    }
                    else
                    {
                        oSocioNegocio.UserFields.Fields.Item("U_BeneficiarioPagoRe").Value = nombreCompleto;
                    }
                    oSocioNegocio.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_BillTo;
                    oSocioNegocio.Addresses.AddressName = "DIRECCION 1";
                    oSocioNegocio.Addresses.AddressName2 = telefono;
                    oSocioNegocio.Addresses.Street = direccion;
                    oSocioNegocio.Addresses.BuildingFloorRoom = entreCalles;
                    oSocioNegocio.Addresses.City = municipio;
                    oSocioNegocio.Addresses.Block = colonia;
                    oSocioNegocio.Addresses.ZipCode = codigoPostal;
                    oSocioNegocio.Addresses.State = "JAL";

                    if (oSocioNegocio.Add() != 0)
                    {
                        msgError = "Error: " + oCompany.GetLastErrorDescription();
                    }
                    else
                    {
                        oCompany.GetNewObjectCode(out cardCodeGenerate);
                        msgError = "";
                    }
                }
                else
                {
                    msgError = "Error: " + msgError;
                }
            }
            catch (Exception ex)
            {
                msgError = "Error: " + msgError + " : " + ex.Message;
            }
            finally
            {
                if (!msgError.Contains("Error"))
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                    CodigoActivacion = ObtenerCodigoActivacionGenerado(cardCodeGenerate);
                    msgError = "El código de activación es: " + CodigoActivacion;
                }
                else
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }
                if (oSocioNegocio != null)
                {
                    GC.SuppressFinalize(oSocioNegocio);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oSocioNegocio);
                    oSocioNegocio = null;
                }
                GC.Collect();
                ClearMemory();
                oCompany.Disconnect();
            }
        }

        private static string ObtenerCodigoActivacionGenerado(string cardCodeGenerate)
        {
            SqlConnection conexion = null;
            SqlDataReader reader = null;
            SqlCommand comando = new SqlCommand();
            string codigoAct = null;
            try
            {
                conexion = Conexiones.ConexionSQL.ObtenerConexionSQL();
                comando.Connection = conexion;
                comando.CommandText = @"SELECT  U_CodigoActivacion AS Codigo
                                        FROM    dbo.OCRD
                                        WHERE   CardCode = '" + cardCodeGenerate + "'";
                reader = comando.ExecuteReader();
                if (reader.HasRows == true)
                {
                    if (reader.Read())
                    {
                        codigoAct = reader["Codigo"].ToString();
                    }
                }
                return codigoAct;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener código de activación Lead: " + ex.Message);
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
        /// Libera la memoria de la aplicación de SAP
        /// </summary>
        /// <param name="procHandle">Proceso asociado</param>
        /// <param name="min">Mínimo</param>
        /// <param name="max">Máximo</param>
        /// <returns>Proceso</returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool SetProcessWorkingSetSize(IntPtr procHandle, Int32 min, Int32 max);

        /// <summary>
        /// Libera la memoria
        /// </summary>
        private static void ClearMemory()
        {
            Process mm = null;
            mm = Process.GetCurrentProcess();
            SetProcessWorkingSetSize(mm.Handle, -1, -1);
        }

        #endregion

    }
}
