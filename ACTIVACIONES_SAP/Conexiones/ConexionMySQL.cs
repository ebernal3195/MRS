using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace MRS.Conexiones
{
    public class ConexionMySQL
    {
        #region METODOS

        /// <summary>
        /// Obtiene la conexión de MySQL
        /// </summary>
        /// <returns>Conexión</returns>
        public static MySqlConnection ObtenerConexionMySQL()
        {
            try
            {

                //MySqlConnection conectar = new MySqlConnection("server=" + Extensor.Configuracion.CONEXION_SAP.ServerNameMySQL +
                //                                                 "; database=" + Extensor.Configuracion.CONEXION_SAP.BaseMySQL +
                //                                                 "; Uid=" + Extensor.Configuracion.CONEXION_SAP.UserMySQL +
                //                                                 "; pwd=" + Extensor.Configuracion.CONEXION_SAP.PassMySQL + ";");

                MySqlConnection conectar = new MySqlConnection("server=" + "" +
                                                                 "; database=" + "" +
                                                                 "; Uid=" + "" +
                                                                 "; pwd=" + "" + ";");

                conectar.Open();
                return conectar;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la conexión con MySQL" + ex);
            }
        }

        #endregion
    }
}
