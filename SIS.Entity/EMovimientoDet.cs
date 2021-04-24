using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EMovimientoDet:EGeneral
    {
        public EMovimientoDet()
        {
            Movimiento = new EMovimiento();
            Material = new EMaterial();
            Sucursal = new ESucursal();
            Almacen = new EAlmacen();
        }
        public EMovimiento Movimiento { get; set; }
        public EMaterial Material { get; set; }
        public float Cantidad { get; set; }
        public float Precio { get; set; }
        public ESucursal Sucursal { get; set; }
        public EAlmacen Almacen { get; set; }
       public int Item { get; set; }
        public int Total { get; set; }
       
    }
}
