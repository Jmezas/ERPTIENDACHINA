using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EFactBolSunatDetalle
    {
        public EFactBolSunatDetalle()
        {
            FactBolSunat = new EFactBolSunat();

        }

        public string UnidadMedidaItem { get; set; }
        public string CantidadItem { get; set; }
        public string DescripcionDetalladaProducto { get; set; }
        public string ValorVentaUnitario { get; set; }
        public string PrecioVenta { get; set; }
        public string CodigoTipoPrecioVenta { get; set; }
        public string AfectacionIGV { get; set; }
        public string SubtotalAfectacionIGV { get; set; }
        public string CodigoTipoAfectacionIGV { get; set; }
        public string CodigoSUNATTipoTributoIGV { get; set; }
        public string NombreTipoTributoIGV { get; set; }
        public string CodigoUNECETipoTributoIGV { get; set; }
        public string SistemaISC { get; set; }
        public string SubtotalSistemaISC { get; set; }
        public string CodigoTipoSistemaISC { get; set; }
        public string CodigoSUNATTipoTributoISC { get; set; }
        public string NombreTipoTributoISC { get; set; }
        public string CodigoUNECETipoTributoISC { get; set; }
        public string ValorVentaItem { get; set; }
        public string NumeroOrdenItem { get; set; }
        public string ValorReferencialUnitario { get; set; }
        public string CodigoTipoValorReferencial { get; set; }
        public string FechaReg { get; set; }
        public string EstadoReg { get; set; }
        public string IndicadorDescuentosItem { get; set; }
        public string DescuentosItem { get; set; }
        public string Importe { get; set; }
        public string igv { get; set; }

        public EFactBolSunat FactBolSunat { get; set; }

    }
}
