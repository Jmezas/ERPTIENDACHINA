using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EAlmacen
    {
        public EAlmacen()
        {
            Ubigeo = new EUbigeo();
            Sucursal = new ESucursal();
            Empresa = new EEmpresa();
            Usuario = new EUsuario();
        }
        public int IdAlmacen { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        
        public EUbigeo Ubigeo { get; set; }
        public ESucursal Sucursal { get; set; }
        public EEmpresa Empresa { get; set; }
        public EUsuario Usuario { get; set; }
        public string Estado { get; set; }
    }
}
