using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIS.Entity;
using SIS.Factory;
namespace SIS.Data
{
    public class DGeneral : DBHelper
    {
        private static DGeneral Instancia;
        private DataBase BaseDeDatos;

        public DGeneral(DataBase BaseDeDatos) : base(BaseDeDatos)
        {
            this.BaseDeDatos = BaseDeDatos;
        }

        public static DGeneral ObtenerInstancia(DataBase BaseDeDatos)
        {
            if (Instancia == null)
            {
                Instancia = new DGeneral(BaseDeDatos);
            }
            return Instancia;
        }

        public List<EGeneral> CBOLista(int Id, int Empresa)
        {
            List<EGeneral> oDatos = new List<EGeneral>();
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_CBOLista");
                    CreateHelper(Connection);
                    AddInParameter("@IdFlag", Id);
                    AddInParameter("@IdEmpresa", Empresa);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EGeneral obj = new EGeneral();
                            obj.Id = int.Parse(Reader["Id"].ToString());
                            obj.Nombre = (Reader["Nombre"].ToString());
                            oDatos.Add(obj);
                        }
                    }
                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                finally
                {
                    Connection.Close();
                }
                return oDatos;
            }
        }
        public List<EGeneral> CBOListaId(int flag, int Id, int Empresa)
        {
            List<EGeneral> oDatos = new List<EGeneral>();
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_CBOListaId");
                    CreateHelper(Connection);
                    AddInParameter("@IdFlag", flag);
                    AddInParameter("@Id", Id);
                    AddInParameter("@IdEmpresa", Empresa);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EGeneral obj = new EGeneral();
                            obj.Id = int.Parse(Reader["Id"].ToString());
                            obj.Nombre = (Reader["Nombre"].ToString());
                            oDatos.Add(obj);
                        }
                    }
                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                finally
                {
                    Connection.Close();
                }
                return oDatos;
            }
        }

        public List<EGeneral> CBOListaIdAlmacen(int sucursal, int Empresa, string usuario)
        {
            List<EGeneral> oDatos = new List<EGeneral>();
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_ListaCBOAlmacen");
                    CreateHelper(Connection);
                    AddInParameter("@IdSucursal", sucursal);
                    AddInParameter("@IdEmpresa", Empresa);
                    AddInParameter("@sLogin", usuario);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EGeneral obj = new EGeneral();
                            obj.Id = int.Parse(Reader["Id"].ToString());
                            obj.Nombre = (Reader["Nombre"].ToString());
                            oDatos.Add(obj);
                        }
                    }
                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                finally
                {
                    Connection.Close();
                }
                return oDatos;
            }
        }

        public EGeneral CoidgoGenerado(int Empresa)
        {
            EGeneral obj = null;
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_Codigo_Autogenerado_x_Producto");
                    CreateHelper(Connection);
                    AddInParameter("@Empresa", Empresa);
                    using (var Reader = ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            obj = new EGeneral();
                            obj.Nombre = Reader["Codigo"].ToString();

                        }
                    }
                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                finally
                {
                    Connection.Close();
                }
                return obj;

            }
        }

        public List<EUbigeo> ListarUbigeo(string Acction, string idPais, string IdDep, string IdProv, string IdDis)
        {
            List<EUbigeo> lUbigeo = new List<EUbigeo>();
            using (var Connection = GetConnection(this.BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_Cbo_Lista_de_Ubicaciones");
                    CreateHelper(Connection);
                    AddInParameter("@saccion", Acction);
                    AddInParameter("@sIdPais", idPais);
                    AddInParameter("@sIdDep", IdDep);
                    AddInParameter("@sIdProv", IdProv);
                    AddInParameter("@sIdDis", IdDis);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EUbigeo oUbigeo = new EUbigeo();
                            oUbigeo.CodigoInei = Reader["CODIGO"].ToString();
                            oUbigeo.Nombre = Reader["DESCRIPCION"].ToString();
                            lUbigeo.Add(oUbigeo);
                        }
                    }
                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                finally
                {
                    Connection.Close();
                }
            }
            return lUbigeo;
        }
        public EGeneral GetSerieNumero(int Empresa, int Flag)
        {
            EGeneral obj = null;
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_GetSerieNumero");
                    CreateHelper(Connection);
                    AddInParameter("@Idempresa", Empresa);
                    AddInParameter("@flag", Flag);
                    using (var Reader = ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            obj = new EGeneral();
                            obj.Nombre = Reader["SerieNum"].ToString();

                        }
                    }
                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                finally
                {
                    Connection.Close();
                }
                return obj;

            }
        }


        public List<EGeneral> FiltroProvCli(string filtro, int Tipo, int flag, int Empresa)
        {
            List<EGeneral> lUbigeo = new List<EGeneral>();
            using (var Connection = GetConnection(this.BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_BusquedaProCli");
                    CreateHelper(Connection);
                    AddInParameter("@vFiltro", filtro);
                    AddInParameter("@tipoDoc", Tipo);
                    AddInParameter("@flag", flag);
                    AddInParameter("@IdEmpresa", Empresa);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EGeneral obj = new EGeneral();
                            obj.Id = int.Parse(Reader["Id"].ToString());
                            obj.Text = Reader["Doc"].ToString();//documento
                            obj.Nombre = Reader["Razon"].ToString();//
                            obj.Dir = Reader["Direccion"].ToString();
                            lUbigeo.Add(obj);
                        }
                    }
                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                finally
                {
                    Connection.Close();
                }
            }
            return lUbigeo;
        }

        public List<EMaterial> FiltroProducto(string filtro, int Empresa)
        {
            List<EMaterial> lUbigeo = new List<EMaterial>();
            using (var Connection = GetConnection(this.BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_BusquedaMaterial");
                    CreateHelper(Connection);
                    AddInParameter("@Filltro", filtro);
                    AddInParameter("@IdEmpresa", Empresa);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EMaterial obj = new EMaterial();
                            obj.IdMaterial = int.Parse(Reader["iIdMat"].ToString());
                            obj.Nombre = Reader["sNomMaterial"].ToString();//documento
                            obj.Codigo = Reader["sCodMaterial"].ToString();//
                            obj.PrecioCompra = float.Parse(Reader["fPrecioCompra"].ToString());
                            obj.PrecioVenta = float.Parse(Reader["fPrecioVenta"].ToString());
                            obj.Categoria.Nombre = Reader["Categoria"].ToString();
                            obj.Marca.Nombre = Reader["Marca"].ToString();
                            obj.Unidad.Nombre = Reader["Unidad"].ToString();
                            obj.Descuento = double.Parse(Reader["descuento"].ToString());
                            obj.PrecioUnidad = float.Parse(Reader["nUnidad"].ToString());
                            obj.PrecioDocena = float.Parse(Reader["nDocena"].ToString());
                            obj.PrecioCaja = float.Parse(Reader["ncaja"].ToString());
                            obj.PrecioMedia = float.Parse(Reader["nmedia"].ToString());
                            obj.PrecioCuarto = float.Parse(Reader["ncuarto"].ToString());
                            obj.CantCaja = int.Parse(Reader["cantCaja"].ToString());
                            lUbigeo.Add(obj);
                        }
                    }
                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                finally
                {
                    Connection.Close();
                }
            }
            return lUbigeo;
        }
        public List<EGeneral> ListaEmpresaLogin()
        {
            List<EGeneral> oDatos = new List<EGeneral>();
            using (var Connection = GetConnection(this.BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("GEN_ListaEmpresaLogin");
                    CreateHelper(Connection);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EGeneral obj = new EGeneral();
                            obj.Text = (Reader["sRUC"].ToString());
                            obj.Nombre = Reader["sRazonSocialE"].ToString();
                            oDatos.Add(obj);
                        }
                    }
                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                finally
                {
                    Connection.Close();
                }
            }
            return oDatos;
        }


        public EGeneral VisorStock(int empresa, int sucursal, int material , int almacen)
        {
            EGeneral obj = null;
            using (var Connection = GetConnection(this.BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_VistaStock");
                    CreateHelper(Connection);
                    AddInParameter("@IdEmpresa", empresa);
                    AddInParameter("@IdSucursal", sucursal); 
                    AddInParameter("@IdMaterial", material);
                    AddInParameter("@IdAlmacen", almacen);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            obj = new EGeneral();
                            obj.Num = float.Parse(Reader["sStock"].ToString());
                            

                        }
                    }
                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                finally
                {
                    Connection.Close();
                }
            }
            return obj;
        }
        public List<EGeneral> FiltroBusquedaProducto(int Empresa, int idSucursal, int idAlmacen, string filtro)
        {
            List<EGeneral> lProducto = new List<EGeneral>();
            using (var Connection = GetConnection(this.BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_BusquedaProducto");
                    CreateHelper(Connection);
                    AddInParameter("@IdEmpresa", Empresa);
                    AddInParameter("@idSucursal", idSucursal);
                    AddInParameter("@idAlmacen", idAlmacen);
                    AddInParameter("@vFiltro", filtro);

                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EGeneral obj = new EGeneral();
                            obj.Id = int.Parse(Reader["Id"].ToString());
                            obj.Text = Reader["CodMaterial"].ToString();//documento
                            obj.Nombre = Reader["sNomMaterial"].ToString();//  
                            obj.sCodigo = Reader["sCodigoSunat"].ToString();
                            lProducto.Add(obj);
                        }
                    }
                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                finally
                {
                    Connection.Close();
                }
            }
            return lProducto;
        }
        public List<EKardex> ListaDatosCabecera(int idEmpresa)
        {
            List<EKardex> oDatos = new List<EKardex>();
            using (var Connection = GetConnection(this.BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_GetDatosCab_Kardex_Valorizado");
                    CreateHelper(Connection);
                    AddInParameter("@idEmpresa", idEmpresa);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EKardex obj = new EKardex();
                            obj.Nombre = (Reader["Nombre"].ToString());
                            obj.Establecimiento = Reader["Establecimiento"].ToString();
                            obj.Codigo = Reader["Codigo"].ToString();
                            obj.TipoTabla5 = Reader["TituloTipo5"].ToString();
                            obj.Descripcion = Reader["Descripcion"].ToString();
                            obj.CodigoUnidad = Reader["CodigoUnidad"].ToString();
                            obj.MetodoEvaluacionT = Reader["TituloMetodoEvaluacion"].ToString();
                            obj.RUC = Reader["RUC"].ToString();
                            obj.RazonSocial = Reader["RazonSocial"].ToString();
                            obj.TipoTabla10 = Reader["TituloTipo10"].ToString();
                            obj.UM = Reader["UM"].ToString();
                            obj.MetodoEvaluacion = Reader["MetodoEvaluacion"].ToString();
                            obj.Formato = Reader["Formato"].ToString();
                            obj.Periodo = Reader["Periodo"].ToString();
                            obj.TituloRuc = Reader["TituloRuc"].ToString();
                            obj.nomDocumento = Reader["nomDocumento"].ToString();
                            obj.TipoOperacion = Reader["TipoOperacion"].ToString();
                            obj.Entrada = Reader["Entrada"].ToString();
                            obj.Salidas = Reader["Salidas"].ToString();
                            obj.SaldoFinal = Reader["SaldoFinal"].ToString();
                            obj.Fecha = Reader["Fecha"].ToString();
                            obj.TipoTabla10 = Reader["TituloTipo10"].ToString();
                            obj.Serie = Reader["Serie"].ToString();
                            obj.Numero = Reader["Numero"].ToString();
                            obj.Tabla12 = Reader["Tabla"].ToString();
                            obj.Cantidad = Reader["Cantidad"].ToString();
                            obj.CostoUnitario = Reader["CostoUnitario"].ToString();
                            obj.CostoTotal = Reader["CostoTotal"].ToString();
                            obj.valorTipoTabla5 = Reader["TipoTabla5"].ToString();
                            oDatos.Add(obj);
                        }
                    }
                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                finally
                {
                    Connection.Close();
                }
            }
            return oDatos;
        }
    }
}
