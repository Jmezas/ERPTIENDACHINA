using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
  public  class EConsultaRUCDNI
    {
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string Condicion { get; set; }
        public string NombreComercial { get; set; }
        public string Tipo { get; set; }
        public string Inscripcion { get; set; }
        public string Estado { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string SistemaEmision { get; set; }
        public string ActividadExterior { get; set; }
        public string SistemaContabilidad { get; set; }
        public string Oficio { get; set; }
        public string EmisionElectronica { get; set; }
        public string PLE { get; set; }
        public List<object> representantes_legales { get; set; }
        public List<object> cantidad_trabajadores { get; set; }
    }
    public class EConsultaRUC
    {
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public object nombreComercial { get; set; }
        public List<object> telefonos { get; set; }
        public object tipo { get; set; }
        public string estado { get; set; }
        public string condicion { get; set; }
        public string direccion { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
        public object fechaInscripcion { get; set; }
        public object sistEmsion { get; set; }
        public object sistContabilidad { get; set; }
        public object actExterior { get; set; }
        public List<object> actEconomicas { get; set; }
        public List<object> cpPago { get; set; }
        public List<object> sistElectronica { get; set; }
        public object fechaEmisorFe { get; set; }
        public List<object> cpeElectronico { get; set; }
        public object fechaPle { get; set; }
        public List<object> padrones { get; set; }
        public object fechaBaja { get; set; }
        public object profesion { get; set; }
        public string ubigeo { get; set; }
        public string capital { get; set; }
    }
    public class EconsultaDNI
    {
        public string dni { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string codVerifica { get; set; }
        public string respuesta { get; set; }
    }
}
