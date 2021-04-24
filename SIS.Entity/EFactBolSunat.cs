using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EFactBolSunat
    {

        public int IdComprobantePago { get; set; }
        public int IdUsuarioReg { get; set; }
        public string FechaEmision { get; set; }
        public string HoraEmision { get; set; }
        public string RazonSocialEmpresa { get; set; }
        public string NombreComercialEmpresa { get; set; }
        public string DomicilioFiscalCodigoUbigeo { get; set; }
        public string DomicilioFiscalDireccion { get; set; }
        public string DomicilioFiscalUrbanizacion { get; set; }
        public string DomicilioFiscalProvincia { get; set; }
        public string DomicilioFiscalDepartamento { get; set; }
        public string DomicilioFiscalDistrito { get; set; }
        public string DomicilioFiscalCodigoPais { get; set; }
        public string NumeroDocumentoEmpresa { get; set; }
        public string CodigoDocumentoEmpresa { get; set; }
        public string CodigoTipoDocumento { get; set; }
        public string SerieDocumento { get; set; }
        public int NumeracionDocumento { get; set; }
        public string SerieNumeroDocumento { get; set; }
        public string NumeroDocumentoCliente { get; set; }
        public string CodigoDocumentoCliente { get; set; }
        public string ApellidoNombreRazonSocialCliente { get; set; }
        public string DireccionCliente { get; set; }
        public string CodigoTipoMontoTVVOperGravadas { get; set; }
        public string TotalValorVentaOperGravadas { get; set; }
        public string CodigoTipoMontoTVVOperInafectadas { get; set; }
        public string TotalValorVentaOperInafectadas { get; set; }
        public string CodigoTipoMontoTVVOperExoneradas { get; set; }
        public string TotalValorVentaOperExoneradas { get; set; }
        public string CodigoTipoMontoTVVOperGratuitas { get; set; }
        public string TotalValorVentaOperGratuitas { get; set; }
        public string DescuentosGlobales { get; set; }
        public string SumatoriaIGV { get; set; }
        public string SubTotalSumatoriaIGV { get; set; }
        public string CodigoSUNATTipoTributoIGV { get; set; }
        public string NombreTipoTributoIGV { get; set; }
        public string CodigoUNECETipoTributoIGV { get; set; }
        public string SumatoriaISC { get; set; }
        public string SubTotalSumatoriaISC { get; set; }
        public string CodigoSUNATTipoTributoISC { get; set; }
        public string NombreTipoTributoISC { get; set; }
        public string CodigoUNECETipoTributoISC { get; set; }
        public string SumatoriaOtrosCargos { get; set; }
        public string CodigoTipoMontoTotalDescuentos { get; set; }
        public string TotalDescuentos { get; set; }
        public string ImporteTotalVenta { get; set; }
        public string CodigoISOTipoMoneda { get; set; }
        public string VersionUBL { get; set; }
        public string VersionEstructuraDocumento { get; set; }
        public string TipoLeyenda { get; set; }
        public string Leyenda { get; set; }
        public string DigestValue { get; set; }
        public string SignatureValue { get; set; }
        public string SerieNumeroAnticipo { get; set; }
        public string ImporteTotalAnticipo { get; set; }
        public string SucursalNombre { get; set; }
        public string SucursalDireccion { get; set; }
        public string CondicionPago { get; set; }
        public string ResolucionSunat { get; set; }
        public string Vendedor { get; set; }
        public string Placa { get; set; }
        public string Observaciones { get; set; }
        public string FechaVencimiento { get; set; }
        public string NombreTipoDocumento { get; set; }
        public bool EstadoVigente { get; set; }
        public string MotivoAnulacion { get; set; }
        public string FechaHoraReg { get; set; }
        public string EstadoAprobacion { get; set; }
        public string FechaRecepcionConstancia { get; set; }
        public string CodigoRecepcionConstancia { get; set; }
        public string DescripcionRecepcionConstancia { get; set; }
        public string ComprobantePagoAfectado { get; set; }
        public string CodigoLeyendaRetencion { get; set; }
        public string PorcentajeRetencion { get; set; }
        public string MontoRetencion { get; set; }
        public string LeyendaRetencion { get; set; }
        public string CodigoMontoRetencion { get; set; }
        public string CodigoServicioRetencion { get; set; }
        public string ServicioRetencion { get; set; }
    }
}
