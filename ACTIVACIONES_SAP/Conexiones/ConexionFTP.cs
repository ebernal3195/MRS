using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace MRS.Conexiones
{
    class ConexionFTP
    {
        public static SftpClient obtenerConexionFTP()
        {
            PrivateKeyFile keyFile;

            try
            {
                keyFile = new PrivateKeyFile("keyToServerA");

                SftpClient miConexionFTP = new SftpClient("35.167.149.196", "ubuntu", keyFile);

                miConexionFTP.Connect();

                return miConexionFTP;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar con el servidor mediante FTP" + ex);
            }
        }
    }
}
