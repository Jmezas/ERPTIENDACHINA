using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIS.Data;
using SIS.Entity;
using SIS.Factory;

namespace SIS.Business
{
    public class BGeneral
    {
        private static BGeneral Instancia;
        private DGeneral Data = DGeneral.ObtenerInstancia(DataBase.SqlServer);
        public static BGeneral ObtenerInstancia()
        {
            if (Instancia == null)
            {
                Instancia = new BGeneral();
            }
            return Instancia;
        }

        public List<EGeneral> CBOLista(int Id, int Empresa)
        {
            try
            {
                return Data.CBOLista(Id, Empresa);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public List<EGeneral> CBOListaId(int flag, int Id, int Empresa)
        {
            try
            {
                return Data.CBOListaId(flag, Id, Empresa);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }

        }
        public List<EGeneral> CBOListaIdAlmacen(int sucursal, int Empresa, string usuario)
        {

            try
            {
                return Data.CBOListaIdAlmacen(sucursal, Empresa, usuario);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public EGeneral CoidgoGenerado(int Empresa)
        {
            try
            {
                return Data.CoidgoGenerado(Empresa);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public List<EUbigeo> ListarUbigeo(string Acction, string idPais, string IdDep, string IdProv, string IdDis)
        {
            try
            {
                return Data.ListarUbigeo(Acction, idPais, IdDep, IdProv, IdDis);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public EGeneral GetSerieNumero(int Empresa, int Flag)
        {
            try
            {
                return Data.GetSerieNumero(Empresa, Flag);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public List<EGeneral> FiltroProvCli(string filtro, int Tipo, int flag, int Empresa)
        {
            try
            {
                return Data.FiltroProvCli(filtro, Tipo, flag, Empresa);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public List<EMaterial> FiltroProducto(string filtro, int Empresa)
        {
            try
            {
                return Data.FiltroProducto(filtro, Empresa);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }

        }
        public List<EGeneral> ListaEmpresaLogin()
        {
            try
            {
                return Data.ListaEmpresaLogin();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }

        }
        public EGeneral VisorStock(int empresa, int sucursal, int material, int almacen)
        {
            try
            {
                return Data.VisorStock(empresa, sucursal, material, almacen);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public List<EGeneral> FiltroBusquedaProducto(int Empresa, int idSucursal, int idAlmacen, string filtro)
        {
            try
            {
                return Data.FiltroBusquedaProducto(Empresa, idSucursal, idAlmacen, filtro);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public List<EKardex> ListaDatosCabecera(int idEmpresa)
        {
            try
            {
                return Data.ListaDatosCabecera(idEmpresa);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
