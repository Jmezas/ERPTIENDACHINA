using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIS.Entity;
using SIS.Factory;
using System.Data.SqlClient;
using System.Data;

namespace SIS.Data
{
    public class DGestion : DBHelper
    {
        private static DGestion Instancia;
        private DataBase BaseDeDatos;

        public DGestion(DataBase BaseDeDatos) : base(BaseDeDatos)
        {
            this.BaseDeDatos = BaseDeDatos;
        }

        public static DGestion ObtenerInstancia(DataBase BaseDeDatos)
        {
            if (Instancia == null)
            {
                Instancia = new DGestion(BaseDeDatos);
            }
            return Instancia;
        }

        public string RegistrarFactura(EOrdenCompraCab oDatos, List<EOrdenCompraDet> Detalle, string Usuario)
        {
            using (var Connection = GetConnection(BaseDeDatos))
            {
                string sMensaje = "";
                try
                {
                    Connection.Open();
                    SqlTransaction tran = (SqlTransaction)Connection.BeginTransaction();
                    SetQuery("LOG_InsOrden_Compra_Cab");
                    CreateHelper(Connection, tran);
                    AddInParameter("@IdOrdenComCab", oDatos.IdCompra);
                    AddInParameter("@IdEmpresa", oDatos.empresa.Id);
                    AddInParameter("@IdProv", oDatos.Proveedor.IdProveedor);
                    AddInParameter("@IdTipoPago", oDatos.Id);
                    AddInParameter("@sSerieDoc", oDatos.Serie);
                    AddInParameter("@sNroDoc", oDatos.Numero);
                    AddInParameter("@IdMoneda", oDatos.Moneda.IdMoneda);
                    AddInParameter("@dFechaRegistro", DateTime.Parse(oDatos.FechaRegistro));
                    AddInParameter("@dFechaAtencion", DateTime.Parse(oDatos.FechaAtencion));
                    AddInParameter("@dFechaPago", DateTime.Parse(oDatos.FechaPago));
                    AddInParameter("@fCantidadTotalCab", oDatos.Cantidad);
                    AddInParameter("@fSubTotalCab", oDatos.SubTotal);
                    AddInParameter("@fIGVCab", oDatos.IGV);
                    AddInParameter("@fTotalCab", oDatos.Total);
                    AddInParameter("@fPorcIgv", oDatos.ProceIGV);
                    AddInParameter("@fTipoCambio", oDatos.TipoCambio);
                    AddInParameter("@sCodReg", Usuario);
                    AddOutParameter("@Mensaje", (DbType)SqlDbType.VarChar);
                    ExecuteQuery();
                    sMensaje = GetOutput("@Mensaje").ToString();
                    string[] vMensaje = sMensaje.Split('|');
                    if (vMensaje[0].Equals("success"))
                    {
                        string[] vValues = vMensaje[2].Split('&');
                        int iIVenta = int.Parse(vValues[0]);

                        string[] dMensaje;
                        foreach (EOrdenCompraDet oDetalle in Detalle)
                        {
                            oDetalle.OrdenCompraCab.IdCompra = iIVenta;
                            SetQuery("LOG_InsOrden_Compra_det");
                            CreateHelper(Connection, tran);
                            AddInParameter("@iIdOrdenComCab", oDetalle.OrdenCompraCab.IdCompra);
                            AddInParameter("@iIdMat", oDetalle.Material.IdMaterial);
                            AddInParameter("@fCantidad", oDetalle.Cantidad);
                            AddInParameter("@fPrecio", oDetalle.Precio);
                            AddInParameter("@sCodReg", Usuario);
                            AddOutParameter("@Mensaje", (DbType)SqlDbType.VarChar);
                            ExecuteQuery();
                            dMensaje = GetOutput("@Mensaje").ToString().Split('|');
                            if (!dMensaje[0].Equals("success"))
                            {
                                throw new Exception();
                            }
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                    tran.Commit();
                    return sMensaje;
                }
                catch (Exception Exception)
                {
                    sMensaje = "error|" + Exception.Message;
                    return sMensaje;
                }

                finally { Connection.Close(); }
            }
        }



        public List<EOrdenCompraDet> ImpirmirOc(int Idoc, int Empresa)
        {
            List<EOrdenCompraDet> oDatos = new List<EOrdenCompraDet>();
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_ImprimirOc");
                    CreateHelper(Connection);
                    AddInParameter("@IdOc", Idoc);
                    AddInParameter("@IdEmpresa", Empresa);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EOrdenCompraDet obj = new EOrdenCompraDet();
                            obj.OrdenCompraCab.IdCompra = int.Parse(Reader["IdOrdenComCab"].ToString());
                            obj.OrdenCompraCab.Proveedor.Id = int.Parse(Reader["iIdProveedor"].ToString());
                            obj.OrdenCompraCab.Proveedor.Nombre = (Reader["Proveedor"].ToString());
                            obj.OrdenCompraCab.Proveedor.NroDocumento = (Reader["sNroDoc"].ToString());
                            obj.OrdenCompraCab.Proveedor.Direccion = (Reader["sDireccion"].ToString());
                            obj.OrdenCompraCab.Nombre = Reader["TipoPago"].ToString();
                            obj.OrdenCompraCab.Id = int.Parse(Reader["IdPago"].ToString());
                            obj.OrdenCompraCab.Moneda.Nombre = Reader["Moneda"].ToString();
                            obj.OrdenCompraCab.Moneda.IdMoneda = int.Parse(Reader["IdMoneda"].ToString());
                            obj.OrdenCompraCab.Serie = Reader["SerieDoc"].ToString();
                            obj.OrdenCompraCab.FechaRegistro = Reader["dFechaRegistro"].ToString();
                            obj.OrdenCompraCab.FechaPago = Reader["dFechaPago"].ToString();
                            obj.OrdenCompraCab.Cantidad = float.Parse(Reader["Cantidad"].ToString());
                            obj.OrdenCompraCab.SubTotal = float.Parse(Reader["fSubTotalCab"].ToString());
                            obj.OrdenCompraCab.IGV = float.Parse(Reader["fIGVCab"].ToString());
                            obj.OrdenCompraCab.Total = float.Parse(Reader["fTotalCab"].ToString());
                            obj.Material.Nombre = Reader["sNomMaterial"].ToString();
                            obj.Material.IdMaterial = int.Parse(Reader["iIdMat"].ToString());
                            obj.Material.Codigo = Reader["sCodMaterial"].ToString();
                            obj.Material.Unidad.Nombre = (Reader["sNombreUMD"].ToString());
                            obj.Material.Marca.Nombre = (Reader["Marca"].ToString());
                            obj.Material.Categoria.Nombre = (Reader["Categoria"].ToString());
                            obj.Cantidad = float.Parse(Reader["fCantidad"].ToString());
                            obj.Precio = float.Parse(Reader["fPrecio"].ToString());
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

        public List<EOrdenCompraCab> ImpirmirListadoOC(string Filltro, int Empresa, int numPag, int allReg, int Cant)
        {
            List<EOrdenCompraCab> oDatos = new List<EOrdenCompraCab>();
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_ListaOrdenCompra");
                    CreateHelper(Connection);
                    AddInParameter("@Filltro", Filltro);
                    AddInParameter("@IdEmpresa", Empresa);
                    AddInParameter("@numPagina", numPag);
                    AddInParameter("@allReg", allReg);
                    AddInParameter("@iCantFilas", Cant);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EOrdenCompraCab obj = new EOrdenCompraCab();
                            obj.IdCompra = int.Parse(Reader["IdOrdenComCab"].ToString());
                            obj.Proveedor.Nombre = (Reader["Proveedor"].ToString());
                            obj.Text = Reader["TipoPago"].ToString();
                            obj.Moneda.Nombre = Reader["Moneda"].ToString();
                            obj.Serie = Reader["SerieDoc"].ToString();
                            obj.FechaRegistro = Reader["dFechaRegistro"].ToString();
                            obj.FechaPago = Reader["dFechaPago"].ToString();
                            obj.Cantidad = float.Parse(Reader["Cantidad"].ToString());
                            obj.SubTotal = float.Parse(Reader["fSubTotalCab"].ToString());
                            obj.IGV = float.Parse(Reader["fIGVCab"].ToString());
                            obj.Total = float.Parse(Reader["fTotalCab"].ToString());
                            obj.TotalR = int.Parse(Reader["Total"].ToString());
                            obj.TotalPagina = int.Parse(Reader["totalPaginas"].ToString());
                            obj.EstadoDoc = Reader["Estado"].ToString();
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

        public string AprobarOC(int IdOc, int Empresa, string Motivo, int Flag, string Usuario)
        {
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_AprobarOC");
                    CreateHelper(Connection);
                    AddInParameter("@IdOc", IdOc);
                    AddInParameter("@IdEmpresa", Empresa);
                    AddInParameter("@Motivo", Motivo);
                    AddInParameter("@Flag", Flag);
                    AddInParameter("@usuario", Usuario);
                    AddOutParameter("@Mensaje", (DbType)SqlDbType.VarChar);
                    ExecuteQuery();
                    var smensaje = GetOutput("@Mensaje").ToString();


                    return GetOutput("@Mensaje").ToString();
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
        }
        //registrar OC-
        public string RegistrarOrden(EOrdenCompraCab oDatos, List<EOrdenCompraDet> Detalle, string Usuario)
        {
            using (var Connection = GetConnection(BaseDeDatos))
            {
                string sMensaje = "";
                try
                {
                    Connection.Open();
                    SqlTransaction tran = (SqlTransaction)Connection.BeginTransaction();
                    SetQuery("LOG_InstRegistrarOC");
                    CreateHelper(Connection, tran);
                    AddInParameter("@IdOrden", 0);
                    AddInParameter("@IdEmpresa", oDatos.empresa.Id);
                    AddInParameter("@IdSucursal", oDatos.ESucursal.IdSucursal);
                    AddInParameter("@sSerie", oDatos.Serie);
                    AddInParameter("@sNumero", oDatos.Numero);
                    AddInParameter("@iTipoDoc", oDatos.Documento.IdDocumento);
                    AddInParameter("@Nrodoc", oDatos.NroDocumento);
                    AddInParameter("@sTipoP", oDatos.Id);
                    AddInParameter("@iConcepto", oDatos.IConcepto);
                    AddInParameter("@dFechaCompra", DateTime.Parse(oDatos.FechaRegistro));
                    AddInParameter("@dFechaVencimiento", DateTime.Parse(oDatos.FechaPago));
                    AddInParameter("@iMoneda", (oDatos.Moneda.IdMoneda));
                    AddInParameter("@sIGV", oDatos.sIGV);
                    AddInParameter("@IOrdenC", oDatos.IdCompra, AllowNull);
                    AddInParameter("@IProveedor", oDatos.Proveedor.IdProveedor);
                    AddInParameter("@sObservacion", oDatos.Observacion, AllowNull);
                    AddInParameter("@User", Usuario);
                    AddInParameter("@IdAlmacen", oDatos.Almacen.IdAlmacen);
                    AddInParameter("@sAfecta", oDatos.AfectaStock);
                    AddInParameter("@fCantidadTotalCab", oDatos.Cantidad);
                    AddInParameter("@fSubTotalCab", oDatos.SubTotal);
                    AddInParameter("@fIGVCab", oDatos.IGV);
                    AddInParameter("@fTotalCab", oDatos.Total);
                    AddOutParameter("@Mensaje", (DbType)SqlDbType.VarChar);
                    ExecuteQuery();
                    sMensaje = GetOutput("@Mensaje").ToString();
                    if (oDatos.AfectaStock == true)
                    {
                        string[] vMensaje = sMensaje.Split('|');
                        if (vMensaje[0].Equals("success"))
                        {
                            string[] vValues = vMensaje[2].Split('&');
                            int iIVenta = int.Parse(vValues[0]);

                            string[] dMensaje;
                            foreach (EOrdenCompraDet oDetalle in Detalle)
                            {
                                oDetalle.OrdenCompraCab.IdCompra = iIVenta;
                                SetQuery("LOG_InstRegistrarOCDet");
                                CreateHelper(Connection, tran);
                                AddInParameter("@IdOrden", oDetalle.OrdenCompraCab.IdCompra);
                                AddInParameter("@Idmat", oDetalle.Material.IdMaterial);
                                AddInParameter("@fCantidad", oDetalle.Cantidad);
                                AddInParameter("@fPrecio", oDetalle.Precio);
                                AddInParameter("@IdEmpresa", oDatos.empresa.Id);
                                AddInParameter("@IdSucursal", oDatos.ESucursal.IdSucursal);
                                AddInParameter("@IdAlmacen", oDetalle.Almacen.IdAlmacen);
                                AddInParameter("@sCodReg", Usuario);
                                AddOutParameter("@Mensaje", (DbType)SqlDbType.VarChar);
                                ExecuteQuery();
                                dMensaje = GetOutput("@Mensaje").ToString().Split('|');
                                if (!dMensaje[0].Equals("success"))
                                {
                                    throw new Exception();
                                }
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    tran.Commit();
                    return sMensaje;
                }
                catch (Exception Exception)
                {
                    sMensaje = "error|" + Exception.Message;
                    return sMensaje;
                }

                finally { Connection.Close(); }
            }
        }


        public List<EOrdenCompraCab> ImpirmirRegistroOC(string Filltro, string FechaIncio, string FechaFin, int Afecta, int Inlcuye, int Empresa, int numPag, int allReg, int Cant)
        {
            List<EOrdenCompraCab> oDatos = new List<EOrdenCompraCab>();
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_ListaRegistroOC");
                    CreateHelper(Connection);
                    AddInParameter("@Filltro", Filltro);
                    AddInParameter("@FechaInicio", FechaIncio);
                    AddInParameter("@FechaFin", FechaFin);
                    AddInParameter("@sAfecta", Afecta);
                    AddInParameter("@IncluyeIGV", Inlcuye);
                    AddInParameter("@IdEmpresa", Empresa);
                    AddInParameter("@numPagina", numPag);
                    AddInParameter("@allReg", allReg);
                    AddInParameter("@iCantFilas", Cant);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EOrdenCompraCab obj = new EOrdenCompraCab();
                            obj.IdCompra = int.Parse(Reader["IdOrden"].ToString());
                            obj.Serie = Reader["SerieDoc"].ToString();
                            obj.Documento.Nombre = Reader["TipoDoc"].ToString();
                            obj.Text = Reader["TipoPago"].ToString();
                            obj.FechaRegistro = Reader["dFechaCompra"].ToString();
                            obj.FechaPago = Reader["dFechaVencimiento"].ToString();
                            obj.sConcepto = Reader["Concepto"].ToString();
                            obj.Moneda.Nombre = Reader["Moneda"].ToString();
                            obj.icluyeIGV = Reader["IncluyeIGV"].ToString();
                            obj.Numero = Reader["OC"].ToString();
                            obj.Proveedor.Nombre = (Reader["sRazonSocial"].ToString());
                            obj.AfectaStockString = Reader["Afecta"].ToString();
                            obj.Cantidad = float.Parse(Reader["fCantidadTotalCab"].ToString());
                            obj.SubTotal = float.Parse(Reader["fSubTotalCab"].ToString());
                            obj.IGV = float.Parse(Reader["fIGVCab"].ToString());
                            obj.Total = float.Parse(Reader["fTotalCab"].ToString());
                            obj.TotalR = int.Parse(Reader["Total"].ToString());
                            obj.TotalPagina = int.Parse(Reader["totalPaginas"].ToString());
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
        public List<EOrdenCompraDet> ImpirmirRegistroOcDet(int Idoc, int Empresa)
        {
            List<EOrdenCompraDet> oDatos = new List<EOrdenCompraDet>();
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_ImprimirRegistroOC");
                    CreateHelper(Connection);
                    AddInParameter("@IdOc", Idoc);
                    AddInParameter("@IdEmpresa", Empresa);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EOrdenCompraDet obj = new EOrdenCompraDet();
                            obj.OrdenCompraCab.IdCompra = int.Parse(Reader["IdOrden"].ToString());
                            obj.OrdenCompraCab.Serie = Reader["SerieDoc"].ToString();
                            obj.OrdenCompraCab.Numero = Reader["OC"].ToString();
                            obj.OrdenCompraCab.Documento.Nombre = (Reader["TipoDoc"].ToString());
                            obj.OrdenCompraCab.Nombre = Reader["TipoPago"].ToString();
                            obj.OrdenCompraCab.FechaRegistro = Reader["dFechaCompra"].ToString();
                            obj.OrdenCompraCab.FechaPago = Reader["dFechaVencimiento"].ToString();
                            obj.OrdenCompraCab.sConcepto = Reader["Concepto"].ToString();
                            obj.OrdenCompraCab.icluyeIGV = Reader["IncluyeIGV"].ToString();
                            obj.OrdenCompraCab.AfectaStockString = Reader["Afecta"].ToString();
                            obj.OrdenCompraCab.Proveedor.Id = int.Parse(Reader["iIdTipoDoc"].ToString());
                            obj.OrdenCompraCab.Proveedor.Nombre = (Reader["Proveedor"].ToString());
                            obj.OrdenCompraCab.Proveedor.NroDocumento = (Reader["sNroDoc"].ToString());
                            obj.OrdenCompraCab.Proveedor.Direccion = (Reader["sDireccion"].ToString());
                            obj.OrdenCompraCab.Moneda.Nombre = Reader["Moneda"].ToString();
                            obj.OrdenCompraCab.Cantidad = float.Parse(Reader["Cantidad"].ToString());
                            obj.OrdenCompraCab.SubTotal = float.Parse(Reader["fSubTotalCab"].ToString());
                            obj.OrdenCompraCab.IGV = float.Parse(Reader["fIGVCab"].ToString());
                            obj.OrdenCompraCab.Total = float.Parse(Reader["fTotalCab"].ToString());
                            obj.Material.Nombre = Reader["sNomMaterial"].ToString();
                            obj.Material.IdMaterial = int.Parse(Reader["iIdMat"].ToString());
                            obj.Material.Codigo = Reader["sCodMaterial"].ToString();
                            obj.Material.Unidad.Nombre = (Reader["sNombreUMD"].ToString());
                            obj.Material.Marca.Nombre = (Reader["Marca"].ToString());
                            obj.Material.Categoria.Nombre = (Reader["Categoria"].ToString());
                            obj.Cantidad = float.Parse(Reader["fCantidad"].ToString());
                            obj.Precio = float.Parse(Reader["fPrecio"].ToString());
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


        public string RegistrarMovimiento(EMovimiento oDatos, List<EMovimientoDet> Detalle, string Usuario)
        {
            using (var Connection = GetConnection(BaseDeDatos))
            {
                string sMensaje = "";
                try
                {
                    Connection.Open();
                    SqlTransaction tran = (SqlTransaction)Connection.BeginTransaction();
                    SetQuery("LOG_InsMovimentos");
                    CreateHelper(Connection, tran);
                    AddInParameter("@IdMovimiento", oDatos.IdMovimiento);
                    AddInParameter("@IdEmpresa", oDatos.Empresa.Id);
                    AddInParameter("@IdSucursal", oDatos.Sucursal.IdSucursal);
                    AddInParameter("@dFecEmision", DateTime.Parse(oDatos.FechaEmison));
                    AddInParameter("@sSerie", oDatos.Serie);
                    AddInParameter("@sNumero", oDatos.Numero);
                    AddInParameter("@iTipoMov", oDatos.Id);//tipo de movmiento
                    AddInParameter("@iMoneda", oDatos.Moneda.IdMoneda);
                    AddInParameter("@IdAlmacen", (oDatos.Almacen.IdAlmacen));
                    AddInParameter("@IdOperacion", (oDatos.Documento.IdDocumento));
                    AddInParameter("@fCantidad", (oDatos.Cantidad));
                    AddInParameter("@fSubTotal", oDatos.SubTotal);
                    AddInParameter("@fIGV", oDatos.IGV);
                    AddInParameter("@fTotal", oDatos.Total);
                    AddInParameter("@sObservacion", oDatos.Observacion);
                    AddInParameter("@sCodReg", Usuario);
                    AddInParameter("@contenedor", oDatos.contenedor, AllowNull);
                    AddOutParameter("@Mensaje", (DbType)SqlDbType.VarChar);
                    ExecuteQuery();
                    sMensaje = GetOutput("@Mensaje").ToString();
                    string[] vMensaje = sMensaje.Split('|');
                    if (vMensaje[0].Equals("success"))
                    {
                        string[] vValues = vMensaje[2].Split('&');
                        int iIVenta = int.Parse(vValues[0]);

                        string[] dMensaje;
                        string alerta;
                        foreach (EMovimientoDet oDetalle in Detalle)
                        {
                            oDetalle.Movimiento.IdMovimiento = iIVenta;
                            SetQuery("LOG_InsMovimientoDetalle");
                            CreateHelper(Connection, tran);
                            AddInParameter("@TipoMov", oDatos.Id);
                            AddInParameter("@IdMovimiento", oDetalle.Movimiento.IdMovimiento);
                            AddInParameter("@Idmat", oDetalle.Material.IdMaterial);
                            AddInParameter("@fCantidad", oDetalle.Cantidad);
                            AddInParameter("@fPrecio", oDetalle.Precio);
                            AddInParameter("@IdEmpresa", oDatos.Empresa.Id);
                            AddInParameter("@IdSucursal", oDatos.Sucursal.IdSucursal);
                            AddInParameter("@IdAlmacen", oDetalle.Almacen.IdAlmacen);
                            AddInParameter("@sCodReg", Usuario);
                            AddOutParameter("@Mensaje", (DbType)SqlDbType.VarChar);
                            ExecuteQuery();
                            dMensaje = GetOutput("@Mensaje").ToString().Split('|');
                            if (!dMensaje[0].Equals("success"))
                            {
                                //throw new Exception();
                                return alerta = dMensaje[0] + "|" + dMensaje[1];

                            }
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                    tran.Commit();
                    return sMensaje;
                }
                catch (Exception Exception)
                {
                    sMensaje = "error|" + Exception.Message;
                    return sMensaje;
                }

                finally { Connection.Close(); }
            }
        }
        public List<EMovimiento> ListaMovimiento(string Filltro, string FechaIncio, string FechaFin, int Empresa, int numPag, int allReg, int Cant)
        {
            List<EMovimiento> oDatos = new List<EMovimiento>();
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_ListaMovimiento");
                    CreateHelper(Connection);
                    AddInParameter("@Filltro", Filltro);
                    AddInParameter("@FechaInicio", FechaIncio);
                    AddInParameter("@FechaFin", FechaFin);
                    AddInParameter("@IdEmpresa", Empresa);
                    AddInParameter("@numPagina", numPag);
                    AddInParameter("@allReg", allReg);
                    AddInParameter("@iCantFilas", Cant);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EMovimiento obj = new EMovimiento();
                            obj.IdMovimiento = int.Parse(Reader["IdMovimiento"].ToString());
                            obj.FechaEmison = Reader["dFecEmision"].ToString();
                            obj.Serie = Reader["SerieNum"].ToString();
                            obj.Text = Reader["TipoM"].ToString();
                            obj.Moneda.Nombre = Reader["Moneda"].ToString();
                            obj.Documento.Nombre = Reader["Operacion"].ToString();
                            obj.Cantidad = float.Parse(Reader["fCantidad"].ToString());
                            obj.SubTotal = float.Parse(Reader["fSubTotalCab"].ToString());
                            obj.IGV = float.Parse(Reader["fIGVCab"].ToString());
                            obj.Total = float.Parse(Reader["fTotalCab"].ToString());
                            obj.TotalR = int.Parse(Reader["Total"].ToString());
                            obj.TotalPagina = int.Parse(Reader["totalPaginas"].ToString());
                            obj.contenedor = Reader["contenedor"].ToString();
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

        public List<EMovimientoDet> ListaMovimientoDetalle(int IdMov)
        {
            List<EMovimientoDet> oDatos = new List<EMovimientoDet>();
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_ListaDetalleMovimiento");
                    CreateHelper(Connection);
                    AddInParameter("@IdMov", IdMov);

                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EMovimientoDet obj = new EMovimientoDet();
                            obj.Movimiento.Id = int.Parse(Reader["IdMovimiento"].ToString());
                            obj.Item = int.Parse(Reader["item"].ToString());
                            obj.Material.Codigo = Reader["sCodMaterial"].ToString();
                            obj.Material.Nombre = Reader["sNomMaterial"].ToString();
                            obj.Material.Unidad.Nombre = Reader["sNombreUMD"].ToString();
                            obj.Material.Categoria.Nombre = Reader["Categoria"].ToString();
                            obj.Cantidad = float.Parse(Reader["fCantidad"].ToString());
                            obj.Precio = float.Parse(Reader["fPrecio"].ToString());
                            obj.Total = int.Parse(Reader["Total"].ToString());
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

        public List<EMovimientoDet> ListaStock(string Filtro, int empresa, int sucursal, int numPag, int allReg, int cantFill)
        {
            List<EMovimientoDet> oDatos = new List<EMovimientoDet>();
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_ListaStock");
                    CreateHelper(Connection);
                    AddInParameter("@filtro", Filtro);
                    AddInParameter("@IdSucursal", sucursal);
                    AddInParameter("@IdEmpresa", empresa);
                    AddInParameter("@numPagina", numPag);
                    AddInParameter("@allReg", allReg);
                    AddInParameter("@iCantFilas", cantFill);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EMovimientoDet obj = new EMovimientoDet();
                            obj.Item = int.Parse(Reader["item"].ToString());
                            obj.Material.Codigo = Reader["sCodMaterial"].ToString();
                            obj.Material.Nombre = Reader["sNomMaterial"].ToString();
                            obj.Material.Marca.Nombre = Reader["Marca"].ToString();
                            obj.Material.Unidad.Nombre = Reader["sNombreUMD"].ToString();
                            obj.Material.Categoria.Nombre = Reader["Categoria"].ToString();
                            obj.Almacen.Nombre = (Reader["sAlmacen"].ToString());
                            obj.Cantidad = float.Parse(Reader["sStock"].ToString());
                            obj.Num = float.Parse(Reader["sStockMin"].ToString());
                            obj.Text = (Reader["notifi"].ToString());
                            obj.Total = int.Parse(Reader["Total"].ToString());
                            obj.TotalPagina = int.Parse(Reader["totalPaginas"].ToString());
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
        public List<EMovimiento> ListaKardex(int Empresa, int Sucursal, string FechaIncio, string FechaFin, int iIdAlm, int iIdMat, int numPag, int allReg, int Cant)
        {
            List<EMovimiento> oDatos = new List<EMovimiento>();
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("LOG_VALORIZADO_ALMACENES_REPORTE");
                    CreateHelper(Connection);
                    AddInParameter("IdEmpresa", Empresa);
                    AddInParameter("@IdSucursal", Sucursal);
                    AddInParameter("@dFecInicio", FechaIncio);
                    AddInParameter("@dFecFinal", FechaFin);
                    AddInParameter("@iIdAlm", iIdAlm);
                    AddInParameter("@iIdMat", iIdMat);
                    AddInParameter("@allReg", allReg);
                    AddInParameter("@numPagina", numPag);
                    AddInParameter("@iCantFilas", Cant);
                    using (var Reader = ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            EMovimiento obj = new EMovimiento();
                            obj.FechaEmison = Reader["Fecha"].ToString();
                            obj.Serie = Reader["Serie"].ToString();
                            obj.Numero = Reader["Numero"].ToString();
                            obj.Cantidad = float.Parse(Reader["CantidadEntrada"].ToString());
                            obj.precioEntrada = float.Parse(Reader["PrecioEntrada"].ToString());
                            obj.costoEntrada = float.Parse(Reader["CostoTotalEnt"].ToString());
                            obj.cantidadSalida = float.Parse(Reader["CantidadSalida"].ToString());
                            obj.precioSalida = float.Parse(Reader["PrecioSalida"].ToString());
                            obj.costoSalida = float.Parse(Reader["CostoTotalSal"].ToString());
                            obj.totalStock = float.Parse(Reader["TotalStock"].ToString());
                            obj.TotalPagina = int.Parse(Reader["totalPaginas"].ToString());
                            obj.Almacen.Nombre = Reader["sAlmacen"].ToString();
                            obj.material = Reader["sNomMaterial"].ToString();
                            obj.TotalR = int.Parse(Reader["Total"].ToString());
                            obj.Documento.Nombre = Reader["Tipo"].ToString();
                            obj.TipoOperacion = Reader["TipoOperacion"].ToString();
                            obj.TipoMovimiento = Reader["TipoMovimiento"].ToString();
                            obj.PrecioUnitario = float.Parse(Reader["PrecioUnitario"].ToString());
                            obj.TotalPrecio = float.Parse(Reader["TotalPrecio"].ToString());
                            obj.Almacen.IdAlmacen = int.Parse(Reader["iIdAlm"].ToString());
                            obj.idMaterial = int.Parse(Reader["IdMat"].ToString());
                            obj.sCodigoMat = Reader["sCodMaterial"].ToString();
                            obj.sCodigoUnd = Reader["CodUnD"].ToString();
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

    }
}
