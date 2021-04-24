using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class ECotizacion
    {
        public ECotizacion()
        {
            cliente = new ECliente();
            moneda = new EMoneda();
            empresa = new EEmpresa();
            Documento = new ETipoDocumento();
        }
        public int Idcotzacion { get;   set; }
 
        public ECliente cliente { get; set; }
        public EMoneda moneda { get; set; }
        public EEmpresa empresa { get; set; }
        public ETipoDocumento Documento { get; set; }
        public string fechaEmision { get; set; }
        public string fechaPago { get; set; }
        public string serie { get; set; }
        public string numero { get; set; }
        public float cantidad { get; set; }
        public float grabada { get; set; }
        public float exonerada { get; set; }
        public float inafecta { get; set; }
        public float igv { get; set; }
        public float total { get; set; }
        public float descuento { get; set; }
        public string cambio { get; set; }
        public string observacion { get; set; }
        public int TotalR { get; set; }
        public int TotalPagina { get; set; }
        public int item { get; set; }


    }
}
