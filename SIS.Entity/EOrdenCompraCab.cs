using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EOrdenCompraCab : EGeneral
    {
        public EOrdenCompraCab()
        {
            empresa = new EEmpresa();
            Proveedor = new EProveedor();
            Moneda = new EMoneda();
            ESucursal = new ESucursal();
            Almacen = new EAlmacen();
            Documento = new ETipoDocumento();
        }
        public int IdCompra { get; set; }
        public EEmpresa empresa { get; set; }
        public EProveedor Proveedor { get; set; }
        public EMoneda Moneda { get; set; }
        public ESucursal ESucursal { get; set; }
        public EAlmacen Almacen { get; set; }
        public ETipoDocumento Documento { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaAtencion { get; set; }
        public string FechaPago { get; set; }
        public float Cantidad { get; set; }
        public float SubTotal { get; set; }
        public float IGV { get; set; }
        public float Total { get; set; }
        public float ProceIGV { get; set; }
        public string TipoCambio { get; set; }
        public int TotalR { get; set; }
        public int TotalPagina { get; set; }
        public string EstadoDoc { get; set; }
        public bool AfectaStock { get; set; }
        public string AfectaStockString { get; set; }
        public string NroDocumento { get; set; }
        public int IConcepto { get; set; }
        public string sConcepto { get; set; }
        public string Observacion { get; set; }
        public int sIGV { get; set; }
        public string icluyeIGV { get; set; }

    }
}
