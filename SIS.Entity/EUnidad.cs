using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EUnidad
    {
        public EUnidad()
        {
            Empresa = new EEmpresa();
        }
        public int IdUnidad { get; set; }
        public string CodigSunat { get; set; }
        public string Nombre { get; set; }
        public string Sigla { get; set; }
        public EEmpresa Empresa { get; set; }
    }
}
