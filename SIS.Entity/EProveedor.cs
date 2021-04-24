using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
  public  class EProveedor:EGeneral
    {
        public EProveedor()
        {
            Ubigeo = new EUbigeo();
            Empresa = new EEmpresa();
        }
        public int IdProveedor { get; set; }
        public string NroDocumento { get; set; }
        public string Razonsocial { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public EEmpresa Empresa { get; set; }
        public EUbigeo Ubigeo { get; set; }
    }
}
