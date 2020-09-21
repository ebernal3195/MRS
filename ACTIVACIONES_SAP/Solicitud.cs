using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS
{
    public class Solicitud
    {
        public string solicitud_id { get; set; }
        public string afiliado_estadoCivil { get; set; }
        public string Agente { get; set; }
        public string NumeroSolicitud { get; set; }
        public string CodigoPromotor { get; set; }
        public string NombrePromotor { get; set; }

        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Observaciones { get; set; }
        public string NuevoIngreso { get; set; }
        public string CP { get; set; }
        public string RFC { get; set; }
        public string EsquemaPago { get; set; }
        public string MsgError { get; set; }
        public string CodigoActivacion { get; set; }
        public string Archivos { get; set; }
        public int dia { get; set; }
        public int mes { get; set; }
        public int year { get; set; }
        public string IDResultadoSAP { get; set; }
        public string ResultadoSAP { get; set; }
        public string ResultadoECOBRO { get; set; }

        public string domCasa_codigoPostal { get; set; }
        public string domCasa_Calle { get; set; }
        public string domCasa_numExt { get; set; }
        public string domCasa_numInt { get; set; }
        public string domCasa_EntreCalles { get; set; }
        public string Colonia_casa { get; set; }
        public string Municipio_casa { get; set; }

        public string Calle_cobro { get; set; }
        public string Num_ext_cobro { get; set; }
        public string Num_int_cobro { get; set; }
        public string Entre_calles_cobro { get; set; }
        public string Codigo_postal_cobro { get; set; }
        public string Colonia_cobro { get; set; }
        public string Municipio_cobro { get; set; }

    }
}
