using SIS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Data
{
    public interface IDGeneral
    {
        Task<List<EGeneral>> ListaEmpresaLogin();
    }
}
