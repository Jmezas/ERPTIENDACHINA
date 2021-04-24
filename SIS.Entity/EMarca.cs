using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
   public class EMarca
    {
        public EMarca()
        {
            Empresa = new EEmpresa();
        }
        public int IdMarca { get; set; }
        public string  Nombre { get; set; }
        public string Codigo { get; set; }
        public EEmpresa Empresa { get; set; }
        public int IdLinea { get; set; }
        public string sLinea { get; set; }
    }
}
