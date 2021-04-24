using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class ENotaCreditoDebito:EGeneral
    {
        public ENotaCreditoDebito()
        {
            Empresa = new EEmpresa();
            TipoDocumento = new ETipoDocumento();
            Sucursal = new ESucursal();
            Moneda = new EMoneda();
            cliente = new ECliente();
            NotaCreditoDebito = new EtipoNotaCreditoDebito();
            Venta = new EVenta();
            Almacen = new EAlmacen();
            detalle = new EDetalleCretioDebito();
        }
        public int IdNota { get; set; }
        public int item { get; set; }
        public EEmpresa Empresa { get; set; }
        public ETipoDocumento TipoDocumento { get; set; }
        public ESucursal Sucursal { get; set; }
        public EMoneda Moneda { get; set; }
        public DateTime FechaEmision { get; set; }
        public string sFechaEmision { get; set; }
        public ECliente cliente { get; set; }
        public float grabada { get; set; }
        public float exonerada { get; set; }
        public float inafecta { get; set; }
        public float gratuita { get; set; }
        public float descuento { get; set; }
        public float totalVenta { get; set; }
        public float cantidad { get; set; }
        public float igv { get; set; }
        public EtipoNotaCreditoDebito NotaCreditoDebito { get; set; }
        public EAlmacen Almacen { get; set; }
        public string Serie { get; set; }
        public int AfectaStock { get; set; }
        public string sAfectaStock { get; set; }
        public string Numero { get; set; }
        public string Motivo { get; set; }
        public string Observacion { get; set; }
        public EVenta Venta { get; set; } 
        public int TipoVenta { get; set; }
        public string sTipoVenta { get; set; }
        public EDetalleCretioDebito detalle { get; set; }
    }

    public class EtipoNotaCreditoDebito
    {

        public int Credito { get; set; }
        public int Debito { get; set; }
    }
    public class EDetalleCretioDebito
    {
        public EDetalleCretioDebito()
        {
            Material = new EMaterial();
        }
        public int index { get; set; }
        public EMaterial Material { get; set; }
        public float precio { get; set; }
        public float descuentoPor { get; set; }
        public float cantidad { get; set; }
        public float descuento { get; set; }
        public float Importe { get; set; }
    }
}
