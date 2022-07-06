using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class ESucursal
    {
        public ESucursal()
        {
            Ubigeo = new EUbigeo();
            IdSucursal = 0;
            //Empresa = new EEmpresa();
        }
        public int IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direcciones { get; set; }
        public string AutorizacionSUNAT { get; set; }

        public EUbigeo Ubigeo { get; set; }
        public string Referencia { get; set; }

        public int Item { get; set; }
        public EEmpresa Empresa { get; set; }
        public string codigo { get; set; }
    }
}
