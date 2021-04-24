using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EApertura
    {
        public EApertura()
        {
            caja = new EGeneral();
            usuario = new EUsuario();
            moneda = new EMoneda();
        }
        public int IdApertura { get; set; }
        public EGeneral caja { get; set; }
        public EUsuario usuario { get; set; }
        public string fechaApertura { get; set; }
        public string fechaCierre { get; set; }
        public EMoneda moneda { get; set; }
        public double montoApertura { get; set; }
        public double montoCierre { get; set; }
        public string estadoApertura { get; set; }
        public string TotalR { get; set; }
        public string TotalPagina { get; set; }
    }
}
