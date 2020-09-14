using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Conexiones
{
   public class ConexionSAP
    {
        #region VARIABLES

        public SAPbobsCOM.Company _oCompany = null;

        #endregion

        #region METODOS

        /// <summary>
        /// Realiza la conexión con SAP
        /// </summary>
        /// <param name="msgError">Mensaje de error de SAP</param>
        /// <returns>True / False</returns>
        public bool ConectarSAP(ref string msgError)
        {
            try
            {
                if (_oCompany == null)
                {
                    _oCompany = new Company();
                }

                DesconectarSAP();

                if (!_oCompany.Connected)
                {
                    _oCompany.Server = Extensor.Configuracion.CONEXION_SAP.ServerNameSAP;
                    _oCompany.LicenseServer = Extensor.Configuracion.CONEXION_SAP.ServidorLicenciasSAP;
                    _oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2016;
                    _oCompany.CompanyDB = Extensor.Configuracion.CONEXION_SAP.BaseSAP;
                    _oCompany.UserName = Extensor.Configuracion.CONEXION_SAP.UserSAP;
                    _oCompany.Password = Extensor.Configuracion.CONEXION_SAP.PassSAP;
                    _oCompany.DbUserName = Extensor.Configuracion.CONEXION_SAP.UserSQL;
                    _oCompany.DbPassword = Extensor.Configuracion.CONEXION_SAP.PassSQL;
                    _oCompany.language = BoSuppLangs.ln_Spanish_La;

                    int CodigoError = _oCompany.Connect();

                    if (CodigoError != 0)
                    {
                        string oDescription = null;
                        _oCompany.GetLastError(out CodigoError, out oDescription);
                        msgError = Convert.ToString(CodigoError) + " :: " + oDescription;
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar con SAP : " + ex);
            }
        }

        /// <summary>
        /// Se desconecta de SAP 
        /// </summary>
        public void DesconectarSAP()
        {
            try
            {
                if (_oCompany.Connected)
                {
                    if (!_oCompany.InTransaction)
                    {
                        _oCompany.Disconnect();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desconectar SAP: " + ex);
            }
        }

        #endregion
    }
}
