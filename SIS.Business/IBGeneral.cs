using SIS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Business
{
    public interface IBGeneral
    {
        Task<List<EGeneral>> ListaEmpresaLogin();

    }
}
