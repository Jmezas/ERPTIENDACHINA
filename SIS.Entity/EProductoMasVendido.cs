using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EProductoMasVendido
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string producto { get; set; }
        public float cantidad { get; set; }
    }
}
