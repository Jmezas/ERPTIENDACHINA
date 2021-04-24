using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EDetCotizacion
    {
        public EDetCotizacion()
        {
            material = new EMaterial();
            Cotizacion = new ECotizacion();
            Empresa = new EEmpresa();
            Sucursal = new ESucursal();
            Almacen = new EAlmacen();

        }
        public int Idcotizacion { get; set; }
        public EMaterial material { get; set; }
        public EEmpresa Empresa { get; set; }
        public ESucursal Sucursal { get; set; }
        public ECotizacion Cotizacion { get; set; }
        public EAlmacen Almacen { get; set; }
        public float cantidad { get; set; }
        public float precio { get; set; }
        public float Importe { get; set; }
        public float fImportecnIGV { get; set; }
        public float descuentoPor { get; set; }
        public float descuento { get; set; }
        public float precioDesc { get; set; }
        public float operacion { get; set; }
    }
}
