using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
   public class EResumenDiarioBVSunat:EGeneral
    {
        public EResumenDiarioBVSunat()
        {
            UsuarioCreador = new EUsuario();
        }
        public string RazonSocialEmpresa { get; set; }
        public string NumeroDocumentoEmpresa { get; set; }
        public string CodigoDocumentoEmpresa { get; set; }
        public string FechaEmisionDocumento { get; set; }
        public string IdentificadorResumen { get; set; }
        public string FechaGeneracionResumen { get; set; }
        public string VersionUBL { get; set; }
        public string VersionEstructuraDocumento { get; set; }
        public string IndEstadoEnvio { get; set; }
        public string IndEstadoAprobacion { get; set; }
        public string FechaHoraEnvio { get; set; }
        public string FechaHoraRecepcionConstancia { get; set; }

        public EUsuario UsuarioCreador { get; set; }
    }
}
