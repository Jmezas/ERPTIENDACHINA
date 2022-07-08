using OfficeOpenXml;
using SIS.Business;
using SIS.Entity;
using SIS.Principal.Models;
using System;
using System.Collections.Generic;
using System.IO; 
using System.Web.Mvc;


namespace SIS.Principal.Controllers
{
    public class ReportesController : Controller
    {
        Authentication Authentication = new Authentication();
        BReportes Venta = new BReportes();
        // GET: Reportes
        public ActionResult VentasXProducto()
        {
            return View();
        }
        public ActionResult ReporteVentasGeneral()
        {
            return View();
        }
        public ActionResult ProductoMasVendido()
        {
            return View();
        }
        public ActionResult VentasTipoPago()
        {
            return View();
        }

        [HttpPost]
        public void ResumenVentasXProducto(int flag, string filtro, string FechaIncio, string FechaFin, int numPag, int allReg, int Cant, string vendedor, int sucursal)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                var rolUsuario = Authentication.UserLogued.Usuario;

                //FLAG = 1 ES VENTAS POS
                if (flag == 1)
                {
                    //VENTAS POS
                    Utils.Write(
                        ResponseType.JSON,
                        Venta.ResumenVentasXProductoPOS(filtro, empresa, rolUsuario, FechaIncio, FechaFin, numPag, allReg, Cant, vendedor, sucursal)
                    );
                }
                else
                {
                    //VENTAS REGULARES
                    //falta realizar
                    Utils.Write(
                        ResponseType.JSON,
                        Venta.ResumenVentasXProductoRegular(filtro, empresa, FechaIncio, FechaFin, numPag, allReg, Cant, vendedor, sucursal)
                    );
                }
            }
            catch (Exception Exception)
            {
                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 1, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }
        }

        [HttpPost]
        public void VentasGenerales(int flag, string filtro, string FechaIncio, string FechaFin, int numPag, int allReg, int Cant, int sucursal)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                var rolUsuario = Authentication.UserLogued.Usuario;
                //int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                //FLAG = 1 ES VENTAS POS
                if (flag == 1)
                {
                    //VENTAS POS
                    Utils.Write(
                        ResponseType.JSON,
                        Venta.VentasGeneralesPos(filtro, empresa, sucursal, rolUsuario, FechaIncio, FechaFin, numPag, allReg, Cant)
                    );
                }
                else
                {
                    //VENTAS REGULARES
                    //falta realizar
                    Utils.Write(
                        ResponseType.JSON,
                        Venta.VentasGeneralesRegular(filtro, empresa, sucursal, FechaIncio, FechaFin, numPag, allReg, Cant)
                    );
                }
            }
            catch (Exception Exception)
            {
                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 1, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }
        }

        [HttpPost]
        public void VentasPorTipoPago(int flag, string filtro, string FechaIncio, string FechaFin, int numPag, int allReg, int Cant, int sucursal)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                var rolUsuario = Authentication.UserLogued.Usuario;
                //int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                //FLAG = 1 ES VENTAS POS
                if (flag == 1)
                {
                    //VENTAS POS
                    Utils.Write(
                        ResponseType.JSON,
                        Venta.VentasPorTipoPagoPos(filtro, empresa, sucursal, rolUsuario, FechaIncio, FechaFin, numPag, allReg, Cant)
                    );
                }
                else
                {
                    //VENTAS REGULARES
                    //falta realizar
                    Utils.Write(
                        ResponseType.JSON,
                        Venta.VentasPorTipoPagoRegular(filtro, empresa, sucursal, FechaIncio, FechaFin, numPag, allReg, Cant)
                    );
                }
            }
            catch (Exception Exception)
            {
                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 1, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }
        }


        [HttpPost]
        public void ListaProductoMasVendido()
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                     ResponseType.JSON,
                     Venta.ListaProductoMasVendido(empresa)
                 );
            }
            catch (Exception Exception)
            {
                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 1, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }
        }


        #region Reporte Excel Resumen de ventas por producto
        public ActionResult ResumenExcelVentasXProductos(int venta, string cliente, string FechaIncio, string FechaFin, int numPag, int allReg, int Cant, string vendedor, int sucursal)
        {
            try
            {
                //Ruta de la plantilla
                FileInfo fTemplateFile = new FileInfo(Server.MapPath("/") + "Reporte/Venta/excel/" + "ResumenVentasXproductos.xlsx");

                var Usuario = Authentication.UserLogued.Usuario;
                int Empresa = Authentication.UserLogued.Empresa.Id;
                // int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                //Ruta del nuevo documento
                FileInfo fNewFile = new FileInfo(Server.MapPath("/") + "Reporte/Venta/excel/" + "Venta" + "_" + Usuario + ".xlsx");

                List<EVentaDetalle> ListaCompra;


                if (venta == 1)
                {
                    //VENTAS POS
                    ListaCompra = Venta.ResumenVentasXProductoPOS(cliente, Empresa, Usuario, FechaIncio, FechaFin, numPag, allReg, Cant, vendedor, sucursal);

                }
                else
                {
                    //VENTAS REGULAR
                    ListaCompra = Venta.ResumenVentasXProductoRegular(cliente, Empresa, FechaIncio, FechaFin, numPag, allReg, Cant, vendedor, sucursal);
                }

                ExcelPackage pck = new ExcelPackage(fNewFile, fTemplateFile);

                var ws = pck.Workbook.Worksheets[1];
                ////** aqui
                var path = Server.MapPath("/") + "assets/images/LogoDefault.png";

                //int rowIndex = 0;//numeros
                //int colIndex = 3; //letras
                //int Height = 220;
                //int Width = 120;

                DateTime today = DateTime.Now;
                ws.Cells[6, 2].Value = today;
                ws.Cells[7, 2].Value = Usuario;
                int iFilaDetIni = 10;
                int starRow = 1;
                double cantidad = 0, precio = 0, descuento = 0, precDscto = 0, importe = 0, importeSnIgv = 0;

                foreach (EVentaDetalle Detalle in ListaCompra)
                {
                    ws.Cells[iFilaDetIni + starRow, 1].Value = Detalle.Venta.vendedor;
                    ws.Cells[iFilaDetIni + starRow, 2].Value = Detalle.Sucursal.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 3].Value = Detalle.Venta.fechaEmision;
                    ws.Cells[iFilaDetIni + starRow, 4].Value = Detalle.Venta.serie + '-' + Detalle.Venta.numero;
                    ws.Cells[iFilaDetIni + starRow, 5].Value = Detalle.Venta.moneda.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 6].Value = Detalle.Venta.cliente.NroDocumento + '-' + Detalle.Venta.cliente.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 7].Value = Detalle.material.Codigo;
                    ws.Cells[iFilaDetIni + starRow, 8].Value = Detalle.material.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 9].Value = Detalle.material.Unidad.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 10].Value = Detalle.material.Marca.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 11].Value = Detalle.material.Modelo.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 12].Value = Detalle.material.Etipo.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 13].Value = Detalle.material.EColor.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 14].Value = Detalle.material.Talla.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 15].Value = Detalle.material.Temporada.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 16].Value = Detalle.material.Categoria.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 17].Value = Math.Round(Convert.ToDouble(Detalle.cantidad), 2);
                    ws.Cells[iFilaDetIni + starRow, 18].Value = Math.Round(Convert.ToDouble(Detalle.precio), 2);
                    ws.Cells[iFilaDetIni + starRow, 19].Value = Math.Round(Convert.ToDouble(Detalle.descuento), 2);
                    ws.Cells[iFilaDetIni + starRow, 20].Value = Math.Round(Convert.ToDouble(Detalle.precio - Detalle.descuento), 2);
                    ws.Cells[iFilaDetIni + starRow, 21].Value = Math.Round(Convert.ToDouble(Detalle.Importe), 2);
                    ws.Cells[iFilaDetIni + starRow, 22].Value = Math.Round(Convert.ToDouble(Detalle.Importe / 1.18), 3);
                    //ws.Cells[iFilaDetIni + starRow, 22].Value = Math.Round(Convert.ToDouble(Detalle.Venta.CostoEnvio), 2);
                    //ws.Cells[iFilaDetIni + starRow, 23].Value = Detalle.Venta.Envio.Nombre;
                    //ws.Cells[iFilaDetIni + starRow, 24].Value = Detalle.Venta.fechaEnvio;
                    //ws.Cells[iFilaDetIni + starRow, 25].Value = Detalle.Venta.sCanalesVenta;

                    cantidad += Convert.ToDouble(Detalle.cantidad);
                    precio += Convert.ToDouble(Detalle.precio);
                    descuento += Convert.ToDouble(Detalle.descuento);
                    precDscto += Convert.ToDouble(Detalle.precio - Detalle.descuento);
                    importe += Convert.ToDouble(Detalle.Importe);
                    importeSnIgv += Convert.ToDouble(Detalle.Importe / 1.18);

                    //costoEnvio += Convert.ToDouble(Detalle.Venta.CostoEnvio);
                    starRow++;
                }

                int Count = starRow + 1;
                ws.Cells[iFilaDetIni + (Count - 1), 16].Value = "Totales";
                ws.Cells[iFilaDetIni + (Count - 1), 17].Value = Math.Round(Convert.ToDouble(cantidad), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 18].Value = Math.Round(Convert.ToDouble(precio), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 19].Value = Math.Round(Convert.ToDouble(descuento), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 20].Value = Math.Round(Convert.ToDouble(precDscto), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 21].Value = Math.Round(Convert.ToDouble(importe), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 22].Value = Math.Round(Convert.ToDouble(importeSnIgv), 2);

                string modelRange = "A" + System.Convert.ToString(iFilaDetIni) + ":v" + (iFilaDetIni + Count - 1).ToString();
                var modelTable = ws.Cells[modelRange];
                modelTable.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                //Guardando Archivo...
                pck.Save();
                //  Liberando...
                pck.Dispose();

                OfficeOpenXml.ExcelPackage pcks = new OfficeOpenXml.ExcelPackage(fNewFile, false);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + "ResumenVentasXproductos" + "_" + Usuario + ".xlsx");

                MemoryStream memoryStream = new MemoryStream();
                pcks.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.End();
                pcks.Dispose();

                System.IO.File.Delete(fNewFile.FullName);
                return View();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        #region Reporte Ventas Generales
        public ActionResult ReporteVentasGeneralesExcel(string cliente, string FechaIncio, string FechaFin, int numPag, int allReg, int Cant, int venta, int sucursal)
        {
            try
            {
                //Ruta de la plantilla
                FileInfo fTemplateFile = new FileInfo(Server.MapPath("/") + "Reporte/Venta/excel/" + "ReporteVentasGeneral.xlsx");

                var Usuario = Authentication.UserLogued.Usuario;
                int Empresa = Authentication.UserLogued.Empresa.Id;
                //int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                //Ruta del nuevo documento
                FileInfo fNewFile = new FileInfo(Server.MapPath("/") + "Reporte/Venta/excel/" + "Venta" + "_" + Usuario + ".xlsx");

                List<EVenta> ListaCompra;
                if (venta == 1)
                {
                    ListaCompra = Venta.VentasGeneralesPos(cliente, Empresa, sucursal, Usuario, FechaIncio, FechaFin, numPag, allReg, Cant);

                }
                else
                {
                    ListaCompra = Venta.VentasGeneralesRegular(cliente, Empresa, sucursal, FechaIncio, FechaFin, numPag, allReg, Cant);
                }
                ExcelPackage pck = new ExcelPackage(fNewFile, fTemplateFile);

                var ws = pck.Workbook.Worksheets[1];
                ////** aqui
                //var path = Server.MapPath("/") + "assets/images/LogoDefault.png";

                //int rowIndex = 0;//numeros
                //int colIndex = 3; //letras
                //int Height = 220;
                //int Width = 120;

                DateTime today = DateTime.Now;
                ws.Cells[6, 2].Value = today;
                ws.Cells[7, 2].Value = Usuario;
                int iFilaDetIni = 10;
                int starRow = 1;
                double SubTotal = 0, IGV = 0, Total = 0, exonerada = 0, inafecta = 0, descuento = 0, envio = 0, totPagar = 0;
                foreach (EVenta Detalle in ListaCompra)
                {
                    ws.Cells[iFilaDetIni + starRow, 1].Value = Detalle.Documento.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 2].Value = Detalle.serie;
                    ws.Cells[iFilaDetIni + starRow, 3].Value = Detalle.numero;
                    ws.Cells[iFilaDetIni + starRow, 4].Value = Detalle.cliente.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 5].Value = Detalle.cliente.NroDocumento;
                    ws.Cells[iFilaDetIni + starRow, 6].Value = Detalle.cliente.Direccion;
                    ws.Cells[iFilaDetIni + starRow, 7].Value = Detalle.cliente.Email;
                    ws.Cells[iFilaDetIni + starRow, 8].Value = Detalle.cliente.Telefono;
                    ws.Cells[iFilaDetIni + starRow, 9].Value = Detalle.moneda.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 10].Value = Detalle.fechaEmision;
                    ws.Cells[iFilaDetIni + starRow, 11].Value = Math.Round(Convert.ToDouble(Detalle.cantidad), 2);
                    ws.Cells[iFilaDetIni + starRow, 12].Value = Math.Round(Convert.ToDouble(Detalle.grabada), 2);
                    ws.Cells[iFilaDetIni + starRow, 13].Value = Math.Round(Convert.ToDouble(Detalle.inafecta), 2);
                    ws.Cells[iFilaDetIni + starRow, 14].Value = Math.Round(Convert.ToDouble(Detalle.exonerada), 2);
                    ws.Cells[iFilaDetIni + starRow, 15].Value = Math.Round(Convert.ToDouble(Detalle.igv), 2);
                    ws.Cells[iFilaDetIni + starRow, 16].Value = Math.Round(Convert.ToDouble(Detalle.total), 2);
                    ws.Cells[iFilaDetIni + starRow, 17].Value = Math.Round(Convert.ToDouble(Detalle.descuento), 2);
                    ws.Cells[iFilaDetIni + starRow, 18].Value = Math.Round(Convert.ToDouble(Detalle.CostoEnvio), 2);
                    ws.Cells[iFilaDetIni + starRow, 19].Value = Math.Round(Convert.ToDouble(Detalle.total + Detalle.CostoEnvio), 2);
                    ws.Cells[iFilaDetIni + starRow, 20].Value = Detalle.Text;
                    ws.Cells[iFilaDetIni + starRow, 21].Value = Detalle.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 22].Value = Detalle.observacion;
                    ws.Cells[iFilaDetIni + starRow, 23].Value = Detalle.Comprobante.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 24].Value = Detalle.sCanalesVenta;

                    SubTotal += Convert.ToDouble(Detalle.grabada);
                    exonerada += Convert.ToDouble(Detalle.exonerada);
                    inafecta += Convert.ToDouble(Detalle.inafecta);
                    IGV += Convert.ToDouble(Detalle.igv);
                    Total += Convert.ToDouble(Detalle.total);
                    descuento += Convert.ToDouble(Detalle.descuento);
                    envio += Convert.ToDouble(Detalle.CostoEnvio);
                    totPagar += Convert.ToDouble(Detalle.total + Detalle.CostoEnvio);
                    starRow++;
                }

                int Count = starRow + 1;
                ws.Cells[iFilaDetIni + (Count - 1), 11].Value = "Totales";
                ws.Cells[iFilaDetIni + (Count - 1), 12].Value = Math.Round(Convert.ToDouble(SubTotal), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 13].Value = Math.Round(Convert.ToDouble(inafecta), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 14].Value = Math.Round(Convert.ToDouble(exonerada), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 15].Value = Math.Round(Convert.ToDouble(IGV), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 16].Value = Math.Round(Convert.ToDouble(Total), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 17].Value = Math.Round(Convert.ToDouble(descuento), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 18].Value = Math.Round(Convert.ToDouble(envio), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 19].Value = Math.Round(Convert.ToDouble(totPagar), 2);

                string modelRange = "A" + System.Convert.ToString(iFilaDetIni) + ":X" + (iFilaDetIni + Count - 1).ToString();
                var modelTable = ws.Cells[modelRange];
                modelTable.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                //Guardando Archivo...
                pck.Save();
                //  Liberando...
                pck.Dispose();

                OfficeOpenXml.ExcelPackage pcks = new OfficeOpenXml.ExcelPackage(fNewFile, false);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + "ReporteVentasGeneral" + "_" + Usuario + ".xlsx");

                MemoryStream memoryStream = new MemoryStream();
                pcks.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.End();
                pcks.Dispose();

                System.IO.File.Delete(fNewFile.FullName);
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Reporte Ventas por tipo de pago
        public ActionResult ReporteVentasPorTipoPagoExcel(string filtro, string FechaIncio, string FechaFin, int numPag, int allReg, int Cant, int venta, int sucursal)
        {
            try
            {
                //Ruta de la plantilla
                FileInfo fTemplateFile = new FileInfo(Server.MapPath("/") + "Reporte/Venta/excel/" + "ReporteVentasPorTipoPago.xlsx");

                var Usuario = Authentication.UserLogued.Usuario;
                int Empresa = Authentication.UserLogued.Empresa.Id;
                //int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                //Ruta del nuevo documento
                FileInfo fNewFile = new FileInfo(Server.MapPath("/") + "Reporte/Venta/excel/" + "Venta" + "_" + Usuario + ".xlsx");

                List<EVentaDetalle> ListaCompra;
                if (venta == 1)
                {
                    ListaCompra = Venta.VentasPorTipoPagoPos(filtro, Empresa, sucursal, Usuario, FechaIncio, FechaFin, numPag, allReg, Cant);

                }
                else
                {
                    ListaCompra = Venta.VentasPorTipoPagoRegular(filtro, Empresa, sucursal, FechaIncio, FechaFin, numPag, allReg, Cant);
                }
                ExcelPackage pck = new ExcelPackage(fNewFile, fTemplateFile);

                var ws = pck.Workbook.Worksheets[1];
                ////** aqui
                //var path = Server.MapPath("/") + "assets/images/LogoDefault.png";

                //int rowIndex = 0;//numeros
                //int colIndex = 3; //letras
                //int Height = 220;
                //int Width = 120;

                DateTime today = DateTime.Now;
                ws.Cells[6, 2].Value = today;
                ws.Cells[7, 2].Value = Usuario;
                int iFilaDetIni = 10;
                int starRow = 1;
                double monto = 0;
                foreach (EVentaDetalle Detalle in ListaCompra)
                {
                    ws.Cells[iFilaDetIni + starRow, 1].Value = Detalle.Venta.serie;
                    ws.Cells[iFilaDetIni + starRow, 2].Value = Detalle.Venta.numero;
                    ws.Cells[iFilaDetIni + starRow, 3].Value = Detalle.Venta.fechaEmision;
                    ws.Cells[iFilaDetIni + starRow, 4].Value = Detalle.Venta.Text;
                    ws.Cells[iFilaDetIni + starRow, 5].Value = Detalle.Venta.TextBanco;
                    ws.Cells[iFilaDetIni + starRow, 6].Value = Math.Round(Convert.ToDouble(Detalle.precio), 2);
                    ws.Cells[iFilaDetIni + starRow, 7].Value = Detalle.Venta.moneda.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 8].Value = Detalle.Sucursal.Nombre;

                    monto += Convert.ToDouble(Detalle.precio);
                    starRow++;
                }

                int Count = starRow + 1;
                ws.Cells[iFilaDetIni + (Count - 1), 5].Value = "Totales";
                ws.Cells[iFilaDetIni + (Count - 1), 6].Value = Math.Round(Convert.ToDouble(monto), 2);

                string modelRange = "A" + System.Convert.ToString(iFilaDetIni) + ":H" + (iFilaDetIni + Count - 1).ToString();
                var modelTable = ws.Cells[modelRange];
                modelTable.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                //Guardando Archivo...
                pck.Save();
                //  Liberando...
                pck.Dispose();

                OfficeOpenXml.ExcelPackage pcks = new OfficeOpenXml.ExcelPackage(fNewFile, false);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + "ReporteVentasPorTipoPago" + "_" + Usuario + ".xlsx");

                MemoryStream memoryStream = new MemoryStream();
                pcks.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.End();
                pcks.Dispose();

                System.IO.File.Delete(fNewFile.FullName);
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

    }
}
