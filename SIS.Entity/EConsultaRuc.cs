using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EConsultaRuc
    {
        public int TipoRespuesta { get; set; }
        public string MensajeRespuesta { get; set; } 
        public string RazonSocial { get; set; }
        public string TipoContribuyente { get; set; }
        public string NombreComercial { get; set; }
        public string FechaInscripcion { get; set; }
        public string FechaInicioActividades { get; set; }
        public string EstadoContribuyente { get; set; }
        public string CondicionContribuyente { get; set; }
        public string DomicilioFiscal { get; set; }
        public string SistemaEmisionComprobante { get; set; } 
        public string ActividadesEconomicas { get; set; }
        public string ComprobantesPago { get; set; } 
        public string AfiliadoPLEDesde { get; set; }
        public string Padrones { get; set; }
    }
}
