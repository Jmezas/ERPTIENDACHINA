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
    public class BGestion
    {
        private static BGestion Instancia;
        private DGestion Data = DGestion.ObtenerInstancia(DataBase.SqlServer);
        public static BGestion ObtenerInstancia()
        {
            if (Instancia == null)
            {
                Instancia = new BGestion();
            }
            return Instancia;
        }


        public string RegistrarFactura(EOrdenCompraCab oDatos, List<EOrdenCompraDet> Detalle, string Usuario)
        {
            try
            {
                return Data.RegistrarFactura(oDatos, Detalle, Usuario);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public List<EOrdenCompraDet> ImpirmirOc(int Idoc, int Empresa)
        {
            try
            {
                return Data.ImpirmirOc(Idoc, Empresa);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }

        }
        public List<EOrdenCompraCab> ImpirmirListadoOC(string Filltro, int Empresa, int numPag, int allReg, int Cant)
        {
            try
            {
                return Data.ImpirmirListadoOC(Filltro, Empresa, numPag, allReg, Cant);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public string AprobarOC(int IdOc, int Empresa, string Motivo, int Flag, string Usuario)
        {
            try
            {
                return Data.AprobarOC(IdOc, Empresa, Motivo, Flag, Usuario);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }

        }
        public string RegistrarOrden(EOrdenCompraCab oDatos, List<EOrdenCompraDet> Detalle, string Usuario)
        {
            try
            {
                return Data.RegistrarOrden(oDatos, Detalle, Usuario);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public List<EOrdenCompraCab> ImpirmirRegistroOC(string Filltro, string FechaIncio, string FechaFin, int Afecta, int Inlcuye, int Empresa, int numPag, int allReg, int Cant)
        {
            try
            {
                return Data.ImpirmirRegistroOC(Filltro, FechaIncio, FechaFin, Afecta, Inlcuye, Empresa, numPag, allReg, Cant);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public List<EOrdenCompraDet> ImpirmirRegistroOcDet(int Idoc, int Empresa)
        {
            try
            {
                return Data.ImpirmirRegistroOcDet(Idoc, Empresa);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        public string RegistrarMovimiento(EMovimiento oDatos, List<EMovimientoDet> Detalle, string Usuario)
        {
            try
            {
                return Data.RegistrarMovimiento(oDatos, Detalle, Usuario);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public List<EMovimiento> ListaMovimiento(string Filltro, string FechaIncio, string FechaFin, int Empresa, int numPag, int allReg, int Cant)
        {
            try
            {
                return Data.ListaMovimiento( Filltro,  FechaIncio,  FechaFin,  Empresa,  numPag,  allReg,  Cant);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public List<EMovimientoDet> ListaMovimientoDetalle(int IdMov)
        {
            try
            {
                return Data.ListaMovimientoDetalle(IdMov);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public List<EMovimientoDet> ListaStock(string Filtro, int empresa, int sucursal, int numPag, int allReg, int cantFill)
        {
            try
            {
                return Data.ListaStock( Filtro,  empresa,  sucursal,  numPag,  allReg,  cantFill);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }

        }
        public List<EMovimiento> ListaKardex(int Empresa, int Sucursal, string FechaIncio, string FechaFin, int iIdAlm, int iIdMat, int numPag, int allReg, int Cant)
        {
            try
            {
                return Data.ListaKardex(Empresa, Sucursal, FechaIncio, FechaFin, iIdAlm, iIdMat, numPag, allReg, Cant);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
