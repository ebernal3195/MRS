using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Conexiones
{
   public class ConexionSQL
    {
        #region METODOS

        /// <summary>
        /// Realiza la conexión hacia SQL server
        /// </summary>
        /// <returns></returns>
        public static SqlConnection ObtenerConexionSQL()
        {
            try
            {
                SqlConnection conectar = new SqlConnection("data source=" + Extensor.Configuracion.CONEXION_SAP.ServerNameSAP +
                                                                 "; initial catalog=" + Extensor.Configuracion.CONEXION_SAP.BaseSAP+
                                                                 "; user id=" + Extensor.Configuracion.CONEXION_SAP.UserSQL +
                                                                 "; password =" + Extensor.Configuracion.CONEXION_SAP.PassSQL + ";");


                conectar.Open();
                return conectar;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la conexión con SQL" + ex);
            }
        }
        #endregion
    }
}
