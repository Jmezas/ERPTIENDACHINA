using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EMovimiento : EGeneral
    {
        public EMovimiento()
        {
            Moneda = new EMoneda();
            Sucursal = new ESucursal();
            Documento = new ETipoDocumento();
            Empresa = new EEmpresa();
            Almacen = new EAlmacen();
        }
        public int IdMovimiento { get; set; }
        public string FechaEmison { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public EMoneda Moneda { get; set; }
        public ESucursal Sucursal { get; set; }
        public EAlmacen Almacen { get; set; }
        public ETipoDocumento Documento { get; set; }
        public EEmpresa Empresa { get; set; }
        public float Cantidad { get; set; }
        public float SubTotal { get; set; }
        public float IGV { get; set; }
        public float Total { get; set; }
        public int TotalR { get; set; }
        public int TotalPagina { get; set; }
        public string Observacion { get; set; }
        //21/10/2020 - Day
        public float precioEntrada { get; set; }
        public float costoEntrada { get; set; }
        public float cantidadSalida { get; set; }
        public float precioSalida { get; set; }
        public float costoSalida { get; set; }
        public float totalStock { get; set; }
        public string material { get; set; }
        public string TipoOperacion { get; set; }
        public string TipoMovimiento { get; set; }
        public float PrecioUnitario { get; set; }
        public float TotalPrecio { get; set; }
        public int idMaterial { get; set; }
        public string sCodigoMat { get; set; }
        public string sCodigoUnd { get; set; }

    }
}
