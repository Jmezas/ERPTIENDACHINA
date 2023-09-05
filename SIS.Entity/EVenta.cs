using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EVenta : EGeneral
    {
        public EVenta()
        {
            cliente = new ECliente();
            moneda = new EMoneda();
            empresa = new EEmpresa();
            Documento = new ETipoDocumento();
            Documento = new ETipoDocumento();
            Sucursal = new ESucursal();
            Comprobante = new EComprobanteFac();
            Almacen = new EAlmacen();
            Envio= new ETipoEnvio();
        }
        public int IdVenta { get; set; }
        public EAlmacen Almacen { get; set; }
        public ECliente cliente { get; set; }
        public EMoneda moneda { get; set; }
        public EEmpresa empresa { get; set; }
        public ETipoDocumento Documento { get; set; }
        public ESucursal Sucursal { get; set; }
        public string fechaEmision { get; set; }
        public string fechaPago { get; set; }
        public string serie { get; set; }
        public EComprobanteFac Comprobante { get; set; }
        public string numero { get; set; }
        public float cantidad { get; set; }
        public float grabada { get; set; }
        public float exonerada { get; set; }
        public float inafecta { get; set; }
        public float gratuita { get; set; }
        public float igv { get; set; }
        public float total { get; set; }
        public float descuento { get; set; }
        public string cambio { get; set; }
        public string observacion { get; set; }
        public int TotalR { get; set; } 
        public int item { get; set; }
        public string resolucion { get; set; }
        public string DigestValue { get; set; }
        public string estadoComprobante { get; set; }

        public string montoRecibido { get; set; }
        public string vuelto { get; set; }
        public int metodoPago { get; set; }
        public string NotaPago { get; set; }
        public float CostoEnvio { get; set; }
        public string motivo { get; set; }
        public string fechaEnvio { get; set; }
        public string EstadoEnvio { get; set; }
        public string vendedor { get; set; }
        public string sCanalesVenta { get; set; }
        public string TextBanco { get; set; }
        public ETipoEnvio Envio { get; set; }
    }
}
