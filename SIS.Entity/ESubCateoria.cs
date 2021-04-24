using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class ESubCateoria
    {
        public ESubCateoria()
        {
            Categoria = new ECategoria();
            Empresa = new EEmpresa();
        }
        public int IdSubCategoira { get; set; }
        public string Nombre { get; set; }
        public string Sigla { get; set; }

        public ECategoria Categoria { get; set; }
        public EEmpresa Empresa { get; set; }
    }
}
