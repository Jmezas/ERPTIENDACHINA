using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
  public  class EVentaDetalle
    {
        public EVentaDetalle()
        {
            material = new EMaterial(); 
            Empresa = new EEmpresa();
            Sucursal = new ESucursal();
            Almacen = new EAlmacen();
            Venta = new EVenta();
            Comprobante = new EComprobanteFac();
        }
        public int IdVentaDetalle { get; set; }
        public EMaterial material { get; set; }
        public EEmpresa Empresa { get; set; }
        public ESucursal Sucursal { get; set; } 
        public EAlmacen Almacen { get; set; }
        public EVenta Venta { get; set; }
        public EComprobanteFac Comprobante { get; set; }
        public float cantidad { get; set; }
        public float precio { get; set; }
        public float Importe { get; set; }
        public float fImportecnIGV { get; set; }
        public float descuentoPor { get; set; }
        public float descuento { get; set; }
        public float precioDesc { get; set; }
        public float operacion { get; set; }
        public string condicionPago { get; set; }
        public int TotalR { get; set; }
        public int TotalPagina { get; set; }
        public int item { get; set; }
        public int TipoPrecio { get; set; }
    }
}
