using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
  public  class ECategoria
    {
        public ECategoria()
        {
            Empresa = new EEmpresa();
        }
        public int IdCateogira { get; set; }
        public string Nombre { get; set; }
        public string Sigla { get; set; }
        public int IdLinea { get; set; }
        public string sLinea { get; set; }
        public EEmpresa Empresa { get; set; }
    }
}
