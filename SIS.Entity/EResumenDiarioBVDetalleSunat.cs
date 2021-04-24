using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EResumenDiarioBVDetalleSunat:EGeneral
    {
        public EResumenDiarioBVDetalleSunat()
        {
            ResumenDiarioBVSunat = new EResumenDiarioBVSunat();
            UsuarioCreador = new EUsuario();
        }

        public EResumenDiarioBVSunat ResumenDiarioBVSunat { get; set; }
        public EUsuario UsuarioCreador { get; set; }

        public string CodigoTipoDocumento { get; set; }
        public string SerieDocumento { get; set; }
        public string NumeracionDocumentoInicial { get; set; }
        public string NumeracionDocumentoFin { get; set; }
        public string TotalValorVentaOperGravadas { get; set; }
        public string CodigoTipoMontoRDTVVOperGravadas { get; set; }
        public string TotalValorVentaOperInafectadas { get; set; }
        public string CodigoTipoMontoRDTVVOperInafectadas { get; set; }
        public string TotalValorVentaOperExoneradas { get; set; }
        public string CodigoTipoMontoRDTVVOperExoneradas { get; set; }
        public string IndicadorSumatoriaOtrosCargos { get; set; }
        public string SumatoriaOtrosCargos { get; set; }
        public string SumatoriaIsc { get; set; }
        public string SubTotalSumatoriaIsc { get; set; }
        public string CodigoSUNATTipoTributoIsc { get; set; }
        public string NombreTipoTributoIsc { get; set; }
        public string CodigoUneceTipoTributoIsc { get; set; }
        public string SumatoriaIgv { get; set; }
        public string SubTotalSumatoriaIgv { get; set; }
        public string CodigoSunatTipoTributoIgv { get; set; }
        public string NombreTipoTributoIgv { get; set; }
        public string CodigoUneceTipoTributoIgv { get; set; }
        public string ImporteTotalVenta { get; set; }
        public string TotalValorVentaOperGratuitas { get; set; }
        public string CodigoTipoMontoRDTVVOperGratuitas { get; set; }
        public int Numeracion { get; set; }
        public int IdTipoDocumentoIdentidad { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public int EstadoEnvio { get; set; }
        public string cpcSerieNumeracionComprobantePagoModificado { get; set; }
        public string cpcCodigoTipoDocumentoModificado { get; set; }
    }
}
