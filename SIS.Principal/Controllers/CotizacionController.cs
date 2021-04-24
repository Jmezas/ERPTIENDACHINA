using SIS.Principal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIS.Business;
using SIS.Entity;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;

namespace SIS.Principal.Controllers
{
    public class CotizacionController : Controller
    {
        Authentication Authentication = new Authentication();
        BCotizacion Cotizacion = new BCotizacion();
        BMantenimiento Mantenimiento = new BMantenimiento();
        Conversion Convertir = new Conversion();
        // GET: Cotizacion
        public ActionResult Nuevo()
        {
            return View();
        }
        public ActionResult Lista()
        {
            return View();
        }
        [HttpPost]
        public void InstCotizacion(ECotizacion oDatos, List<EDetCotizacion> Detalle)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                oDatos.empresa.Id = Authentication.UserLogued.Empresa.Id;
                var sMensaje = Cotizacion.RegistrarCotizacion(oDatos, Detalle, Usuario);
                Utils.WriteMessage(sMensaje, Utils.WithAdditionals);
            }
            catch (Exception Exception)
            {
                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 2, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }
        }

        [HttpPost]
        public void ListaCotizacion(string cliente, int moneda, string FechaIncio, string FechaFin, int numPag, int allReg, int Cant)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Cotizacion.ListaCotizacion(cliente, moneda, empresa, FechaIncio, FechaFin, numPag, allReg, Cant)
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


        //reporte

        #region Reporte PDF
        public ActionResult ReporteCompraPDF(string cliente, int moneda, string FechaIncio, string FechaFin, int numPag, int allReg, int Cant)
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 20, 20);
            int empresa = Authentication.UserLogued.Empresa.Id;
            List<ECotizacion> ListaCompra = Cotizacion.ListaCotizacion(cliente, moneda, empresa, FechaIncio, FechaFin, numPag, allReg, Cant);

            if (ListaCompra.Count > 0)
            {
                //Permite visualizar el contenido del documento que es descargado en "Mis descargas"
                PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);

                //Definiendo parametros para la fuente de la cabecera y pie de pagina
                Font fuente = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD, Color.BLACK);

                //Se define la cabecera del documento
                HeaderFooter cabecera = new HeaderFooter(new Phrase("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), fuente), false);//'el valor es false porque no habra numeración
                pdfDoc.Header = cabecera;
                cabecera.Border = 0;// Rectangle.BOTTOM_BORDER
                cabecera.Alignment = HeaderFooter.ALIGN_RIGHT;


                HeaderFooter pie = new HeaderFooter(new Phrase("pagia", fuente), true);

                pdfDoc.Footer = pie;
                pie.Border = Rectangle.TOP_BORDER;
                pie.Alignment = HeaderFooter.ALIGN_RIGHT;

                //Open PDF Document to write data 
                pdfDoc.Open();

                PdfPTable tblPrueba = new PdfPTable(1);
                tblPrueba.WidthPercentage = 100;


                //Se define la fuente para el texto del reporte
                Font _standardFont = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD);

                PdfPTable Tableitems = new PdfPTable(11);
                Tableitems.WidthPercentage = 100;
                Tableitems.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

                PdfPCell celCospan = new PdfPCell(new Phrase("REPORTE COTIZACION"));
                celCospan.Colspan = 11;
                celCospan.Border = 0;
                celCospan.HorizontalAlignment = 1;
                Tableitems.AddCell(celCospan);


                PdfPCell cEspacio = new PdfPCell(new Phrase("    "));
                cEspacio.Colspan = 11;
                cEspacio.Border = 0;
                cEspacio.HorizontalAlignment = 1;
                Tableitems.AddCell(cEspacio);

                Single[] celdas2 = new Single[] { 1.0F, 2.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F };
                Tableitems.SetWidths(celdas2);
                PdfPCell cTituloCant = new PdfPCell(new Phrase("Serie", _standardFont));
                cTituloCant.Border = 1;
                cTituloCant.BorderWidthBottom = 1;
                cTituloCant.HorizontalAlignment = 1;
                cTituloCant.Padding = 5;
                Tableitems.AddCell(cTituloCant);

                PdfPCell CTituloSerie = new PdfPCell(new Phrase("Cliente", _standardFont));
                CTituloSerie.Border = 1;
                CTituloSerie.BorderWidthBottom = 1;
                CTituloSerie.HorizontalAlignment = 1;
                CTituloSerie.Padding = 5;
                Tableitems.AddCell(CTituloSerie);

                PdfPCell cTipoPago = new PdfPCell(new Phrase("Moneda", _standardFont));
                cTipoPago.Border = 1;
                cTipoPago.BorderWidthBottom = 1;
                cTipoPago.HorizontalAlignment = 1;
                cTipoPago.Padding = 5;
                Tableitems.AddCell(cTipoPago);

                PdfPCell cMonedaT = new PdfPCell(new Phrase("Fecha Registro", _standardFont));
                cMonedaT.Border = 1;
                cMonedaT.BorderWidthBottom = 1;
                cMonedaT.HorizontalAlignment = 1;
                cMonedaT.Padding = 5;
                Tableitems.AddCell(cMonedaT);


                PdfPCell cFechaRegistro = new PdfPCell(new Phrase("Cantidad", _standardFont));
                cFechaRegistro.Border = 1;
                cFechaRegistro.BorderWidthBottom = 1;
                cFechaRegistro.HorizontalAlignment = 1;
                cFechaRegistro.Padding = 5;
                Tableitems.AddCell(cFechaRegistro);

                PdfPCell cFechaPago = new PdfPCell(new Phrase("Ope. Grabada", _standardFont));
                cFechaPago.Border = 1;
                cFechaPago.BorderWidthBottom = 1;
                cFechaPago.HorizontalAlignment = 1;
                cFechaPago.Padding = 5;
                Tableitems.AddCell(cFechaPago);

                PdfPCell cSubTotal = new PdfPCell(new Phrase("Ope. Exone.", _standardFont));
                cSubTotal.Border = 1;
                cSubTotal.BorderWidthBottom = 1;
                cSubTotal.HorizontalAlignment = 1;
                cSubTotal.Padding = 5;
                Tableitems.AddCell(cSubTotal);

                PdfPCell cIGV = new PdfPCell(new Phrase("Ope. Inafecta	", _standardFont));
                cIGV.Border = 1;
                cIGV.BorderWidthBottom = 1;
                cIGV.HorizontalAlignment = 1;
                cIGV.Padding = 5;
                Tableitems.AddCell(cIGV);

                PdfPCell cTitulo = new PdfPCell(new Phrase("IGV", _standardFont));
                cTitulo.Border = 1;
                cTitulo.BorderWidthBottom = 1;
                cTitulo.HorizontalAlignment = 1;
                cTitulo.Padding = 5;
                Tableitems.AddCell(cTitulo);


                PdfPCell cTituloTotal = new PdfPCell(new Phrase("Total", _standardFont));
                cTituloTotal.Border = 1;
                cTituloTotal.BorderWidthBottom = 1;
                cTituloTotal.HorizontalAlignment = 1;
                cTituloTotal.Padding = 5;
                Tableitems.AddCell(cTituloTotal);


                PdfPCell cTituloDescuento = new PdfPCell(new Phrase("Total Descuento", _standardFont));
                cTituloDescuento.Border = 1;
                cTituloDescuento.BorderWidthBottom = 1;
                cTituloDescuento.HorizontalAlignment = 1;
                cTituloDescuento.Padding = 5;
                Tableitems.AddCell(cTituloDescuento);

                Font letrasDatosTabla = FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.NORMAL);

                double IGV = 0, SubTotal = 0, Total = 0, exonerada = 0, inafecta = 0, descuento = 0;

                foreach (ECotizacion cuentaPago in ListaCompra)
                {


                    PdfPCell cdocumento = new PdfPCell(new Phrase(cuentaPago.serie, letrasDatosTabla));
                    cdocumento.Border = 0;
                    cdocumento.Padding = 2;
                    cdocumento.PaddingBottom = 2;
                    cdocumento.HorizontalAlignment = 1;
                    Tableitems.AddCell(cdocumento);

                    PdfPCell cSerie = new PdfPCell(new Phrase(cuentaPago.cliente.Nombre, letrasDatosTabla));
                    cSerie.Border = 0;
                    cSerie.Padding = 2;
                    cSerie.PaddingBottom = 2;
                    cSerie.HorizontalAlignment = 1;
                    Tableitems.AddCell(cSerie);

                    PdfPCell cellTipoPago = new PdfPCell(new Phrase(cuentaPago.moneda.Nombre, letrasDatosTabla));
                    cellTipoPago.Border = 0;
                    cellTipoPago.Padding = 2;
                    cellTipoPago.PaddingBottom = 2;
                    cellTipoPago.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellTipoPago);

                    PdfPCell cFecha = new PdfPCell(new Phrase(cuentaPago.fechaEmision, letrasDatosTabla));
                    cFecha.Border = 0;
                    cFecha.Padding = 2;
                    cFecha.PaddingBottom = 2;
                    cFecha.HorizontalAlignment = 1;
                    Tableitems.AddCell(cFecha);


                    PdfPCell cellMoneda = new PdfPCell(new Phrase(cuentaPago.cantidad.ToString(), letrasDatosTabla));
                    cellMoneda.Border = 0;
                    cellMoneda.Padding = 2;
                    cellMoneda.PaddingBottom = 2;
                    cellMoneda.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellMoneda);


                    PdfPCell cellFechaRegistro = new PdfPCell(new Phrase(Math.Round(cuentaPago.grabada, 2).ToString(), letrasDatosTabla));
                    cellFechaRegistro.Border = 0;
                    cellFechaRegistro.Padding = 2;
                    cellFechaRegistro.PaddingBottom = 2;
                    cellFechaRegistro.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellFechaRegistro);


                    PdfPCell cellFSubTotal = new PdfPCell(new Phrase(Math.Round(cuentaPago.exonerada, 2).ToString(), letrasDatosTabla));
                    cellFSubTotal.Border = 0;
                    cellFSubTotal.Padding = 2;
                    cellFSubTotal.PaddingBottom = 2;
                    cellFSubTotal.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellFSubTotal);

                    PdfPCell cellIGV = new PdfPCell(new Phrase(Math.Round(cuentaPago.inafecta, 2).ToString(), letrasDatosTabla));
                    cellIGV.Border = 0;
                    cellIGV.Padding = 2;
                    cellIGV.PaddingBottom = 2;
                    cellIGV.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellIGV);
                    PdfPCell cigvcel = new PdfPCell(new Phrase(Math.Round(cuentaPago.igv, 2).ToString(), letrasDatosTabla));
                    cigvcel.Border = 0;
                    cigvcel.Padding = 2;
                    cigvcel.PaddingBottom = 2;
                    cigvcel.HorizontalAlignment = 1;
                    Tableitems.AddCell(cigvcel);

                    PdfPCell cNroDocumento = new PdfPCell(new Phrase(Math.Round(cuentaPago.total, 2).ToString(), letrasDatosTabla));
                    cNroDocumento.Border = 0;
                    cNroDocumento.Padding = 2;
                    cNroDocumento.PaddingBottom = 2;
                    cNroDocumento.HorizontalAlignment = 1;
                    Tableitems.AddCell(cNroDocumento);


                    PdfPCell sdecuento = new PdfPCell(new Phrase(Math.Round(cuentaPago.descuento, 2).ToString(), letrasDatosTabla));
                    sdecuento.Border = 0;
                    sdecuento.Padding = 2;
                    sdecuento.PaddingBottom = 2;
                    sdecuento.HorizontalAlignment = 1;
                    Tableitems.AddCell(sdecuento);


                    Total += Convert.ToDouble(cuentaPago.total);
                    IGV += Convert.ToDouble(cuentaPago.igv);
                    SubTotal += Convert.ToDouble(cuentaPago.grabada);
                    exonerada += Convert.ToDouble(cuentaPago.exonerada);
                    inafecta += Convert.ToDouble(cuentaPago.inafecta);
                    descuento += Convert.ToDouble(cuentaPago.descuento);
                }

                PdfPCell cCeldaDetalle = new PdfPCell(Tableitems);
                cCeldaDetalle.Border = 1;
                cCeldaDetalle.BorderWidthBottom = 1;
                cCeldaDetalle.HorizontalAlignment = 1;
                cCeldaDetalle.Padding = 5;
                tblPrueba.AddCell(cCeldaDetalle);


                PdfPCell cCeldaTotal = new PdfPCell(new Phrase("     "));
                cCeldaTotal.Colspan = 10;
                cCeldaTotal.Border = 0;
                cCeldaTotal.HorizontalAlignment = 0;
                tblPrueba.AddCell(cCeldaTotal);

                PdfPTable TableValor = new PdfPTable(1);
                TableValor.WidthPercentage = 100;

                float[] celdas = new float[] { 5.0F };
                TableValor.SetWidths(celdas);



                PdfPCell cTTotales = new PdfPCell(new Phrase("Ope. grabada:   " + string.Format("{0:n}", SubTotal), _standardFont));
                cTTotales.Border = 0;
                cTTotales.Colspan = 2;
                cTTotales.HorizontalAlignment = 2; // 0 = izquierda, 1 = centro, 2 = derecha
                TableValor.AddCell(cTTotales);

                PdfPCell cexonerada = new PdfPCell(new Phrase("Ope. exonerada:   " + string.Format("{0:n}", exonerada), _standardFont));
                cexonerada.Border = 0;
                cexonerada.Colspan = 2;
                cexonerada.HorizontalAlignment = 2; // 0 = izquierda, 1 = centro, 2 = derecha
                TableValor.AddCell(cexonerada);

                PdfPCell cinafecta = new PdfPCell(new Phrase("Ope. inafecta:   " + string.Format("{0:n}", inafecta), _standardFont));
                cinafecta.Border = 0;
                cinafecta.Colspan = 2;
                cinafecta.HorizontalAlignment = 2; // 0 = izquierda, 1 = centro, 2 = derecha
                TableValor.AddCell(cinafecta);




                PdfPCell sIGV = new PdfPCell(new Phrase("IGV:   " + string.Format("{0:n}", IGV), _standardFont));
                sIGV.Border = 0;
                sIGV.Colspan = 2;
                sIGV.HorizontalAlignment = 2; // 0 = izquierda, 1 = centro, 2 = derecha
                TableValor.AddCell(sIGV);

                PdfPCell cTotales = new PdfPCell(new Phrase("Total:   " + string.Format("{0:n}", Total), _standardFont));
                cTotales.Border = 0;
                cTotales.Colspan = 2;
                cTotales.HorizontalAlignment = 2; // 0 = izquierda, 1 = centro, 2 = derecha
                TableValor.AddCell(cTotales);

                PdfPCell cTotalesDescuento = new PdfPCell(new Phrase("Descuento:   " + string.Format("{0:n}", descuento), _standardFont));
                cTotalesDescuento.Border = 0;
                cTotalesDescuento.Colspan = 2;
                cTotalesDescuento.HorizontalAlignment = 2; // 0 = izquierda, 1 = centro, 2 = derecha
                TableValor.AddCell(cTotalesDescuento);

                PdfPCell cCeldaValor = new PdfPCell(TableValor);
                cCeldaValor.Colspan = 2;
                cCeldaValor.Border = 0;
                cCeldaValor.BorderWidthBottom = 1;
                cCeldaValor.PaddingBottom = 2;
                tblPrueba.AddCell(cCeldaValor);


                pdfDoc.Add(tblPrueba);

                // Close your PDF 
                pdfDoc.Close();



                Response.ContentType = "application/pdf";

                //Set default file Name as current datetime 
                Response.AddHeader("content-disposition", "attachment; filename=ReporteCotizacion.pdf");
                Response.Write(pdfDoc);
                Response.Flush();
                Response.End();
            }
            else
            {
                throw new Exception("No se encontró ningún registro.");

            }
            return View();
        }
        #endregion

        #region Reporte Excel
        public ActionResult ReporteCompraExcel(string cliente, int moneda, string FechaIncio, string FechaFin, int numPag, int allReg, int Cant)
        {
            try
            {
                //Ruta de la plantilla
                FileInfo fTemplateFile = new FileInfo(Server.MapPath("/") + "Reporte/Venta/excel/" + "Cotizacion.xlsx");

                var Usuario = Authentication.UserLogued.Usuario;
                int Empresa = Authentication.UserLogued.Empresa.Id;
                //Ruta del nuevo documento
                FileInfo fNewFile = new FileInfo(Server.MapPath("/") + "Reporte/Venta/excel/" + "Cotizacion" + "_" + Usuario + ".xlsx");

                List<ECotizacion> ListaCompra = Cotizacion.ListaCotizacion(cliente, moneda, Empresa, FechaIncio, FechaFin, numPag, allReg, Cant);
                OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(fNewFile, fTemplateFile);

                var ws = pck.Workbook.Worksheets[1];
                ////** aqui
                var path = Server.MapPath("/") + "assets/images/LogoDefault.png";

                int rowIndex = 0;//numeros
                int colIndex = 3; //letras
                int Height = 220;
                int Width = 120;

                System.Drawing.Image logo = System.Drawing.Image.FromFile(path);
                ExcelPicture pic = ws.Drawings.AddPicture("Sample", logo);
                pic.SetPosition(rowIndex, 0, colIndex, 0);
                //pic.SetPosition(PixelTop, PixelLeft);
                pic.SetSize(Height, Width);
                //// aqui



                DateTime today = DateTime.Now;
                ws.Cells[6, 2].Value = today;
                ws.Cells[7, 2].Value = Usuario;
                int iFilaDetIni = 10;
                int starRow = 1;
                double SubTotal = 0, IGV = 0, Total = 0, exonerada = 0, inafecta = 0, descuento = 0;
                foreach (ECotizacion Detalle in ListaCompra)
                {
                    ws.Cells[iFilaDetIni + starRow, 1].Value = Detalle.serie;
                    ws.Cells[iFilaDetIni + starRow, 2].Value = Detalle.cliente.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 3].Value = Detalle.moneda.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 4].Value = Detalle.fechaEmision;
                    ws.Cells[iFilaDetIni + starRow, 5].Value = Math.Round(Convert.ToDouble(Detalle.cambio), 2);
                    ws.Cells[iFilaDetIni + starRow, 6].Value = Math.Round(Convert.ToDouble(Detalle.grabada), 2);
                    ws.Cells[iFilaDetIni + starRow, 7].Value = Math.Round(Convert.ToDouble(Detalle.inafecta), 2);
                    ws.Cells[iFilaDetIni + starRow, 8].Value = Math.Round(Convert.ToDouble(Detalle.exonerada), 2);
                    ws.Cells[iFilaDetIni + starRow, 9].Value = Math.Round(Convert.ToDouble(Detalle.igv), 2);
                    ws.Cells[iFilaDetIni + starRow, 10].Value = Math.Round(Convert.ToDouble(Detalle.total), 2);
                    ws.Cells[iFilaDetIni + starRow, 11].Value = Math.Round(Convert.ToDouble(Detalle.descuento), 2);
                    SubTotal += Convert.ToDouble(Detalle.grabada);
                    exonerada += Convert.ToDouble(Detalle.exonerada);
                    inafecta += Convert.ToDouble(Detalle.inafecta);
                    IGV += Convert.ToDouble(Detalle.igv);
                    Total += Convert.ToDouble(Detalle.total);
                    descuento += Convert.ToDouble(Detalle.descuento);
                    starRow++;

                }
                int Count = starRow + 1;

                ws.Cells[iFilaDetIni + (Count - 1), 5].Value = "Totales";
                ws.Cells[iFilaDetIni + (Count - 1), 6].Value = Math.Round(Convert.ToDouble(SubTotal), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 7].Value = Math.Round(Convert.ToDouble(exonerada), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 8].Value = Math.Round(Convert.ToDouble(inafecta), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 9].Value = Math.Round(Convert.ToDouble(IGV), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 10].Value = Math.Round(Convert.ToDouble(Total), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 11].Value = Math.Round(Convert.ToDouble(descuento), 2);

                string modelRange = "A" + System.Convert.ToString(iFilaDetIni) + ":K" + (iFilaDetIni + Count - 1).ToString();
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
                Response.AddHeader("content-disposition", "attachment;  filename=" + "ReporteCotizacion" + "_" + Usuario + ".xlsx");

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

        #region Imprimri Cotizacion
        public ActionResult imprimirCotizacion(int Id)
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 20, 20);
            MemoryStream ns = new MemoryStream();
            int IdEmpresa = Authentication.UserLogued.Empresa.Id;
            EEmpresa Empresa = Mantenimiento.ListaEditEmpresa(IdEmpresa);

            List<EDetCotizacion> ImprimriOC = Cotizacion.ImpirmirCotizacion(Id);
            try
            {
                //Permite visualizar el contenido del documento que es descargado en "Mis descargas"
                PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
                //Ruta dentro del servidor, aqui se guardara el PDF
                var path = Server.MapPath("/") + "Reporte/Venta/pdf" + "Documento_" + "_Cotizacion_" + ImprimriOC[0].Cotizacion.serie + ".pdf";

                // Se utiliza system.IO para crear o sobreescribe el archivo si existe
                FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                //'iTextSharp para escribir en el documento PDF, sin esto no podemos ver el contenido
                PdfWriter.GetInstance(pdfDoc, file);
                PdfWriter pw = PdfWriter.GetInstance(pdfDoc, ns);
                //  'Open PDF Document to write data 
                pdfDoc.Open();
                pdfDoc.SetMargins(35f, 25f, 25f, 35f);
                pdfDoc.NewPage();

                //'Armando el diseño de la factura
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, Color.BLACK);
                iTextSharp.text.Font _standardFontTabla = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.BOLD, Color.WHITE);
                iTextSharp.text.Font _standarTitulo = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 12, iTextSharp.text.Font.BOLD, Color.BLACK);
                iTextSharp.text.Font _standarTexto = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 9, iTextSharp.text.Font.BOLD, Color.BLACK);
                iTextSharp.text.Font _standarTextoNegrita = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 10, iTextSharp.text.Font.BOLD, Color.BLACK);


                PdfPTable tblPrueba = new PdfPTable(2);
                tblPrueba.WidthPercentage = 100;

                PdfPTable TableCabecera = new PdfPTable(3);
                TableCabecera.WidthPercentage = 100;

                float[] celdasCabecera = new float[] { 0.1F, 0.4F, 0.2F };
                TableCabecera.SetWidths(celdasCabecera);


                PdfPTable TableQR = new PdfPTable(1);
                TableQR.WidthPercentage = 100;


                float[] celdasQR = new float[] { 2.0F };
                TableQR.SetWidths(celdasQR);

                PdfPCell celCospan = new PdfPCell();
                celCospan.Colspan = 3;
                celCospan.Border = 0;
                TableCabecera.AddCell(celCospan);

                //'AGREGANDO IMAGEN:      

                string imagePath = "";
                if (Empresa.Logo == "")
                {
                    imagePath = Server.MapPath("/assets/images/LogoDefault.PNG");
                }
                else
                {
                    imagePath = Server.MapPath("/Imagenes/Empresa/" + Empresa.Logo);
                }

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imagePath);
                jpg.ScaleToFit(90.0F, 100.0F);

                PdfPTable tableIzqigm = new PdfPTable(1);
                PdfPCell celImg = new PdfPCell(jpg);
                celImg.HorizontalAlignment = 0;
                celImg.Border = 0;
                tableIzqigm.AddCell(celImg);

                PdfPCell celIzqs = new PdfPCell(tableIzqigm);
                celIzqs.Border = 0;
                TableCabecera.AddCell(celIzqs);

                PdfPTable tableIzq = new PdfPTable(1);
                tableIzq.TotalWidth = 250.0F;
                tableIzq.LockedWidth = true;
                tableIzq.SpacingBefore = 150.0F;
                tableIzq.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell celInfo = new PdfPCell(new Phrase(Empresa.Nombre, _standarTitulo));
                celInfo.HorizontalAlignment = 1;
                celInfo.Border = 0;
                tableIzq.AddCell(celInfo);

                PdfPCell celContacto = new PdfPCell(new Phrase(Empresa.PaginaWeb, _standarTexto));
                celContacto.HorizontalAlignment = 1;
                celContacto.Border = 0;
                // celContacto.Colspan = 2;
                tableIzq.AddCell(celContacto);

                PdfPCell celDireccion = new PdfPCell(new Phrase(Empresa.Direccion + " " + Empresa.Telefono + "| Celular:" + Empresa.Celular, _standarTexto));
                celDireccion.HorizontalAlignment = 1;
                celDireccion.Border = 0;

                //celDireccion.Colspan = 2;
                tableIzq.AddCell(celDireccion);

                PdfPCell celEmail = new PdfPCell(new Phrase("E-mail:" + Empresa.Correo, _standarTexto));
                celEmail.HorizontalAlignment = 1;
                celEmail.Border = 0;
                //celTelefono.Colspan = 2;
                tableIzq.AddCell(celEmail);

                PdfPCell celIzq = new PdfPCell(tableIzq);
                celIzq.Border = 0;
                TableCabecera.AddCell(celIzq);

                PdfPTable tableDer = new PdfPTable(1);



                PdfPCell celEspacio1 = new PdfPCell(new Phrase(""));
                celEspacio1.Border = 0;
                tableDer.AddCell(celEspacio1);

                //


                PdfPCell cRucs = new PdfPCell(new Phrase("RUC - " + Empresa.RUC, _standarTitulo));
                cRucs.Border = 0;
                cRucs.HorizontalAlignment = 1;
                cRucs.BorderWidthRight = 1f;
                cRucs.BorderWidthLeft = 1f;
                cRucs.BorderWidthTop = 1f;
                cRucs.Padding = 8;
                tableDer.AddCell(cRucs);

                PdfPCell cTipoDoc = new PdfPCell(new Phrase("COTIZACION", _standarTitulo));
                cTipoDoc.HorizontalAlignment = 1;
                cTipoDoc.Border = 0;
                cTipoDoc.BorderWidthRight = 1f;
                cTipoDoc.BorderWidthLeft = 1f;
                tableDer.AddCell(cTipoDoc);

                PdfPCell cNumFac = new PdfPCell(new Phrase("N°: " + ImprimriOC[0].Cotizacion.serie, _standarTitulo));
                cNumFac.Border = 0;
                cNumFac.HorizontalAlignment = 1;
                cNumFac.BorderWidthRight = 1f;
                cNumFac.BorderWidthLeft = 1f;
                cNumFac.BorderWidthBottom = 1f;
                tableDer.AddCell(cNumFac);

                PdfPCell celDer = new PdfPCell(tableDer);
                celDer.Border = 0;
                TableCabecera.AddCell(celDer);

                PdfPCell cCeldaDetallec = new PdfPCell(TableCabecera);
                cCeldaDetallec.Colspan = 2;
                cCeldaDetallec.Border = 0;
                cCeldaDetallec.BorderWidthBottom = 0;
                tblPrueba.AddCell(cCeldaDetallec);

                PdfPCell cEspacio = new PdfPCell(new Phrase("      "));
                cEspacio.Colspan = 2;
                cEspacio.Border = 0;
                cEspacio.HorizontalAlignment = 0;
                tblPrueba.AddCell(cEspacio);

                PdfPTable TableCab = new PdfPTable(2);
                ///TableCab.WidthPercentage = 100;
                float[] celdas1 = new float[] { 0.75F, 2.0F };
                TableCab.TotalWidth = 325.0F;
                TableCab.LockedWidth = true;
                TableCab.SpacingBefore = 300.0F;
                TableCab.SetWidths(celdas1);
                TableCab.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfPCell cTitulo2 = new PdfPCell(new Phrase("SEÑOR(ES):", _standardFont));
                cTitulo2.Border = 0;
                cTitulo2.HorizontalAlignment = 0;
                cTitulo2.BorderWidthTop = 1f;
                cTitulo2.BorderWidthLeft = 1f;
                cTitulo2.Padding = 5;
                TableCab.AddCell(cTitulo2);

                PdfPCell cRuc = new PdfPCell(new Phrase(ImprimriOC[0].Cotizacion.cliente.Nombre, _standardFont));
                cRuc.Border = 0;
                cRuc.HorizontalAlignment = 0;
                cRuc.BorderWidthTop = 1f;
                cRuc.BorderWidthRight = 1f;
                cRuc.Padding = 5;
                TableCab.AddCell(cRuc);

                PdfPCell cTituloEspacio = new PdfPCell(new Phrase());
                cTituloEspacio.Border = 0;
                cTituloEspacio.HorizontalAlignment = 0;
                cTituloEspacio.BorderWidthLeft = 1f;
                TableCab.AddCell(cTituloEspacio);

                PdfPCell cRucEspacio = new PdfPCell(new Phrase());
                cRucEspacio.Border = 0;
                cRucEspacio.HorizontalAlignment = 0;
                cRucEspacio.BorderWidthRight = 1f;
                TableCab.AddCell(cRucEspacio);

                PdfPCell cTitulo3 = new PdfPCell(new Phrase("DIRECCION:", _standardFont));
                cTitulo3.Border = 0;
                cTitulo3.HorizontalAlignment = 0;
                cTitulo3.BorderWidthLeft = 1f;
                cTitulo3.Padding = 5;
                TableCab.AddCell(cTitulo3);

                PdfPCell cDireccion = new PdfPCell(new Phrase(ImprimriOC[0].Cotizacion.cliente.Direccion, _standardFont));
                cDireccion.Border = 0;
                cDireccion.HorizontalAlignment = 0;
                cDireccion.BorderWidthRight = 1f;
                cDireccion.Padding = 5;
                TableCab.AddCell(cDireccion);

                PdfPCell cTituloEspacioPago = new PdfPCell(new Phrase());
                cTituloEspacioPago.Border = 0;
                cTituloEspacioPago.HorizontalAlignment = 0;
                cTituloEspacioPago.BorderWidthLeft = 1f;
                TableCab.AddCell(cTituloEspacioPago);

                PdfPCell cRucEspacios = new PdfPCell(new Phrase());
                cRucEspacios.Border = 0;
                cRucEspacios.HorizontalAlignment = 0;
                cRucEspacios.BorderWidthRight = 1f;
                TableCab.AddCell(cRucEspacios);

                PdfPCell cTiPago = new PdfPCell(new Phrase("CONDICIONES DE PAGO:", _standardFont));
                cTiPago.Border = 0;
                cTiPago.HorizontalAlignment = 0;
                cTiPago.BorderWidthLeft = 1f;
                cTiPago.BorderWidthBottom = 1f;
                cTiPago.Padding = 5;
                cTiPago.MinimumHeight = 30;
                TableCab.AddCell(cTiPago);

                PdfPCell cPago = new PdfPCell(new Phrase(ImprimriOC[0].Cotizacion.Documento.Nombre.ToString(), _standardFont));
                cPago.Border = 0;
                cPago.HorizontalAlignment = 0;
                cPago.BorderWidthRight = 1f;
                cPago.BorderWidthBottom = 1f;
                cPago.Padding = 5;
                cTiPago.MinimumHeight = 30;
                TableCab.AddCell(cPago);

                PdfPCell cEspacio1 = new PdfPCell(new Phrase());
                cEspacio1.Colspan = 4;
                cEspacio1.Border = 0;
                cEspacio1.HorizontalAlignment = 0;
                //cEspacio1.MinimumHeight = 35;
                TableCab.AddCell(cEspacio1);

                PdfPCell cCeldaDetallecs = new PdfPCell(TableCab);
                cCeldaDetallecs.Colspan = 0;
                cCeldaDetallecs.Border = 0;
                cCeldaDetallecs.PaddingBottom = 2;
                tblPrueba.AddCell(cCeldaDetallecs);

                PdfPTable Tabledoble = new PdfPTable(2);
                float[] celdases = new float[] { 1F, 1.5F };
                Tabledoble.SetWidths(celdases);
                Tabledoble.TotalWidth = 200.0F;
                Tabledoble.LockedWidth = true;
                Tabledoble.SpacingBefore = 200.0F;
                Tabledoble.HorizontalAlignment = Element.ALIGN_RIGHT;


                PdfPCell cTituloFecha = new PdfPCell(new Phrase("FECHA EMISIÓN:", _standardFont));
                cTituloFecha.Border = 0;
                cTituloFecha.HorizontalAlignment = 0;
                cTituloFecha.PaddingTop = 2;
                cTituloFecha.BorderWidthLeft = 1f;
                cTituloFecha.BorderWidthTop = 1f;
                cTituloFecha.BorderWidthBottom = 1f;
                cTituloFecha.Padding = 5;
                Tabledoble.AddCell(cTituloFecha);

                PdfPCell cFchaE = new PdfPCell(new Phrase(ImprimriOC[0].Cotizacion.fechaEmision, _standardFont));
                cFchaE.Border = 0; // borde cero
                cFchaE.HorizontalAlignment = 0;
                cFchaE.PaddingTop = 2;
                cFchaE.BorderWidthRight = 1f;
                cFchaE.BorderWidthBottom = 1f;
                cFchaE.BorderWidthTop = 1f;
                cFchaE.Padding = 5;
                Tabledoble.AddCell(cFchaE);

                PdfPCell espacio = new PdfPCell(new Phrase(""));
                espacio.Border = 0;
                espacio.HorizontalAlignment = 1; // 0 = izquierda, 1 = centro, 2 = derecha
                espacio.Padding = 5;
                Tabledoble.AddCell(espacio);

                PdfPCell espacio1 = new PdfPCell(new Phrase(""));
                espacio1.Border = 0;
                espacio1.HorizontalAlignment = 1; // 0 = izquierda, 1 = centro, 2 = derecha
                espacio1.Padding = 5;
                Tabledoble.AddCell(espacio1);

                PdfPCell cTitulo1 = new PdfPCell(new Phrase("N° RUC:", _standardFont));
                cTitulo1.Border = 0;
                cTitulo1.HorizontalAlignment = 0;
                cTitulo1.PaddingTop = 2;
                cTitulo1.BorderWidthTop = 1f;
                cTitulo1.BorderWidthLeft = 1f;
                cTitulo1.Padding = 5;
                cTitulo1.BorderWidthBottom = 1;
                Tabledoble.AddCell(cTitulo1);

                PdfPCell cRazonsocial = new PdfPCell(new Phrase(ImprimriOC[0].Cotizacion.cliente.NroDocumento, _standardFont));
                cRazonsocial.Border = 0; // borde cero
                cRazonsocial.HorizontalAlignment = 0;
                cRazonsocial.PaddingTop = 2;
                cRazonsocial.BorderWidthTop = 1f;
                cRazonsocial.BorderWidthRight = 1f;
                cRazonsocial.Padding = 5;
                cRazonsocial.BorderWidthBottom = 1;
                Tabledoble.AddCell(cRazonsocial);

                PdfPCell cEspacio2 = new PdfPCell(new Phrase());
                cEspacio2.Colspan = 4;
                cEspacio2.Border = 0;
                cEspacio2.HorizontalAlignment = 0;
                //cEspacio1.MinimumHeight = 35;
                Tabledoble.AddCell(cEspacio2);

                PdfPCell cCelda = new PdfPCell(Tabledoble);
                cCelda.Colspan = 0;
                cCelda.Border = 0;
                cCelda.PaddingBottom = 2;
                tblPrueba.AddCell(cCelda);

                PdfPTable Tableitems = new PdfPTable(5);
                Tableitems.WidthPercentage = 100;

                float[] celdas2 = new float[] { 1.0F, 1.0F, 2.0F, 1.0F, 1.0F, };
                Tableitems.SetWidths(celdas2);

                PdfPCell cTituloCodigo = new PdfPCell(new Phrase("CANT", _standardFontTabla));
                cTituloCodigo.BorderWidthBottom = 1;
                cTituloCodigo.HorizontalAlignment = 1; // 0 = izquierda, 1 = centro, 2 = derecha
                cTituloCodigo.Padding = 5;
                cTituloCodigo.BackgroundColor = new Color(System.Drawing.Color.MidnightBlue);
                Tableitems.AddCell(cTituloCodigo);

                PdfPCell cTituloDesc = new PdfPCell(new Phrase("UNID", _standardFontTabla));

                cTituloDesc.BorderWidthBottom = 1;
                cTituloDesc.HorizontalAlignment = 1; // 0 = izquierda, 1 = centro, 2 = derecha
                cTituloDesc.Padding = 5;
                cTituloDesc.BackgroundColor = new Color(System.Drawing.Color.MidnightBlue);
                Tableitems.AddCell(cTituloDesc);

                PdfPCell cTituloCant = new PdfPCell(new Phrase("DESCRIPCION", _standardFontTabla));

                cTituloCant.BorderWidthBottom = 1;
                cTituloCant.HorizontalAlignment = 1; // 0 = izquierda, 1 = centro, 2 = derecha
                cTituloCant.Padding = 5;
                cTituloCant.BackgroundColor = new Color(System.Drawing.Color.MidnightBlue);
                Tableitems.AddCell(cTituloCant);

                PdfPCell cTituloPrecio = new PdfPCell(new Phrase("PRECIO UNITARIO", _standardFontTabla));

                cTituloPrecio.BorderWidthBottom = 1;
                cTituloPrecio.HorizontalAlignment = 1; // 0 = izquierda, 1 = centro, 2 = derecha
                cTituloPrecio.Padding = 5;
                cTituloPrecio.BackgroundColor = new Color(System.Drawing.Color.MidnightBlue);
                Tableitems.AddCell(cTituloPrecio);

                PdfPCell cTituloSubT = new PdfPCell(new Phrase("TOTAL", _standardFontTabla));

                cTituloSubT.BorderWidthBottom = 1;
                cTituloSubT.HorizontalAlignment = 1; // 0 = izquierda, 1 = centro, 2 = derecha
                cTituloSubT.Padding = 5;
                cTituloSubT.BackgroundColor = new Color(System.Drawing.Color.MidnightBlue);
                Tableitems.AddCell(cTituloSubT);

                foreach (EDetCotizacion ListaRep in ImprimriOC)
                {

                    PdfPCell cCodigo = new PdfPCell(new Phrase(ListaRep.cantidad.ToString(), _standardFont));
                    cCodigo.Border = 0;
                    cCodigo.Padding = 2;
                    cCodigo.PaddingBottom = 2;
                    cCodigo.HorizontalAlignment = 1;
                    cCodigo.Border = Rectangle.LEFT_BORDER;
                    Tableitems.AddCell(cCodigo);

                    PdfPCell cNombreMat = new PdfPCell(new Phrase(ListaRep.material.Unidad.Nombre, _standardFont));
                    cNombreMat.Border = 0;
                    cNombreMat.Padding = 2;
                    cNombreMat.PaddingBottom = 2;
                    cNombreMat.HorizontalAlignment = 1;
                    cNombreMat.Border = Rectangle.LEFT_BORDER;
                    Tableitems.AddCell(cNombreMat);

                    PdfPCell cCantidad = new PdfPCell(new Phrase(ListaRep.material.Nombre + " - " + ListaRep.material.Marca.Nombre, _standardFont));
                    cCantidad.Border = 0;
                    cCantidad.Padding = 2;
                    cCantidad.PaddingBottom = 2;
                    cCantidad.HorizontalAlignment = 0;
                    cCantidad.Border = Rectangle.LEFT_BORDER;
                    Tableitems.AddCell(cCantidad);

                    PdfPCell cPrecio = new PdfPCell(new Phrase(ListaRep.precio.ToString(), _standardFont));
                    cPrecio.Border = 0;
                    cPrecio.Padding = 2;
                    cPrecio.PaddingBottom = 2;
                    cPrecio.HorizontalAlignment = 1;
                    cPrecio.Border = Rectangle.LEFT_BORDER;
                    Tableitems.AddCell(cPrecio);

                    PdfPCell cImporte = new PdfPCell(new Phrase((ListaRep.Importe).ToString(), _standardFont));
                    cImporte.Border = 0;
                    cImporte.Padding = 2;
                    cImporte.PaddingBottom = 2;
                    cImporte.HorizontalAlignment = 1;
                    cImporte.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                    Tableitems.AddCell(cImporte);
                }


                PdfPCell cCeldaDetalle = new PdfPCell(Tableitems);
                cCeldaDetalle.Colspan = 4;
                cCeldaDetalle.Border = 0;
                cCeldaDetalle.BorderWidthBottom = 1;
                cCeldaDetalle.PaddingBottom = 4;
                cCeldaDetalle.FixedHeight = 460f;
                tblPrueba.AddCell(cCeldaDetalle);

                PdfPTable TableValorLetra = new PdfPTable(1);
                TableValorLetra.WidthPercentage = 100;

                PdfPCell cLetras = new PdfPCell(new Phrase("SON: " + Convertir.enletras(ImprimriOC[0].Cotizacion.total.ToString()) + " " + ImprimriOC[0].Cotizacion.moneda.Nombre, _standardFont));
                cLetras.Border = 0;
                cLetras.Padding = 2;
                cLetras.PaddingBottom = 2;
                cLetras.HorizontalAlignment = 0;
                cLetras.Colspan = 1;
                TableValorLetra.AddCell(cLetras);


                PdfPCell cCeldaLetras = new PdfPCell(TableValorLetra);
                cCeldaLetras.Colspan = 0;
                cCeldaLetras.Border = 0;
                cCeldaLetras.PaddingBottom = 5;
                tblPrueba.AddCell(cCeldaLetras);

                PdfPCell cCeldaTotal = new PdfPCell(new Phrase());
                cCeldaTotal.Colspan = 10;
                cCeldaTotal.Border = 0;
                cCeldaTotal.HorizontalAlignment = 0;
                tblPrueba.AddCell(cCeldaTotal);


                PdfPTable TableValor = new PdfPTable(2);
                TableValor.TotalWidth = 225.0F;
                TableValor.LockedWidth = true;
                TableValor.SpacingBefore = 180.0F;
                TableValor.HorizontalAlignment = Element.ALIGN_RIGHT;
                //TableValor.WidthPercentage = 100;

                float[] celdas = new float[] { 1.0F, 1.0F };
                TableValor.SetWidths(celdas);

                PdfPCell cTituloSub = new PdfPCell(new Phrase("Ope. Grabda", _standarTextoNegrita));
                cTituloSub.Border = 0;
                // cTituloSub.Colspan = 4;
                cTituloSub.HorizontalAlignment = 0; // 0 = izquierda, 1 = centro, 2 = derecha
                cTituloSub.BorderWidthLeft = 1f;
                cTituloSub.BorderWidthTop = 1f;
                cTituloSub.Padding = 2;
                TableValor.AddCell(cTituloSub);

                PdfPCell cCeldaSubtotal = new PdfPCell(new Phrase(ImprimriOC[0].Cotizacion.grabada.ToString(), _standarTextoNegrita));
                cCeldaSubtotal.Border = 0;
                // cCeldaSubtotal.Colspan = 2
                cCeldaSubtotal.HorizontalAlignment = 2;
                cCeldaSubtotal.BorderWidthRight = 1f;
                cCeldaSubtotal.BorderWidthTop = 1f;
                cCeldaSubtotal.Padding = 2;
                TableValor.AddCell(cCeldaSubtotal);

                PdfPCell cexonerada = new PdfPCell(new Phrase("Ope. exonerada ", _standarTextoNegrita));
                cexonerada.Border = 0;
                // cTituloSub.Colspan = 4;
                cexonerada.HorizontalAlignment = 0; // 0 = izquierda, 1 = centro, 2 = derecha
                cexonerada.BorderWidthLeft = 1f;
                //  cexonerada.BorderWidthTop = 1f;
                cexonerada.Padding = 2;
                TableValor.AddCell(cexonerada);

                PdfPCell celexonerada = new PdfPCell(new Phrase(ImprimriOC[0].Cotizacion.exonerada.ToString(), _standarTextoNegrita));
                celexonerada.Border = 0;
                // cCeldaSubtotal.Colspan = 2
                celexonerada.HorizontalAlignment = 2;
                celexonerada.BorderWidthRight = 1f;
                //  celexonerada.BorderWidthTop = 1f;
                celexonerada.Padding = 2;
                TableValor.AddCell(celexonerada);



                PdfPCell cinafecta = new PdfPCell(new Phrase("Ope. inafecta ", _standarTextoNegrita));
                cinafecta.Border = 0;
                // cTituloSub.Colspan = 4;
                cinafecta.HorizontalAlignment = 0; // 0 = izquierda, 1 = centro, 2 = derecha
                cinafecta.BorderWidthLeft = 1f;
                // cinafecta.BorderWidthTop = 1f;
                cinafecta.Padding = 2;
                TableValor.AddCell(cinafecta);

                PdfPCell cCelinafecta = new PdfPCell(new Phrase(ImprimriOC[0].Cotizacion.inafecta.ToString(), _standarTextoNegrita));
                cCelinafecta.Border = 0;
                // cCeldaSubtotal.Colspan = 2
                cCelinafecta.HorizontalAlignment = 2;
                cCelinafecta.BorderWidthRight = 1f;
                // cCelinafecta.BorderWidthTop = 1f;
                cCelinafecta.Padding = 2;
                TableValor.AddCell(cCelinafecta);


                PdfPCell cdescuento = new PdfPCell(new Phrase("Total descuento", _standarTextoNegrita));
                cdescuento.Border = 0;
                // cTituloSub.Colspan = 4;
                cdescuento.HorizontalAlignment = 0; // 0 = izquierda, 1 = centro, 2 = derecha
                cdescuento.BorderWidthLeft = 1f;
                //cdescuento.BorderWidthTop = 1f;
                cdescuento.Padding = 2;
                TableValor.AddCell(cdescuento);

                PdfPCell celcdescuento = new PdfPCell(new Phrase(ImprimriOC[0].Cotizacion.descuento.ToString(), _standarTextoNegrita));
                celcdescuento.Border = 0;
                // cCeldaSubtotal.Colspan = 2
                celcdescuento.HorizontalAlignment = 2;
                celcdescuento.BorderWidthRight = 1f;
                // celcdescuento.BorderWidthTop = 1f;
                celcdescuento.Padding = 2;
                TableValor.AddCell(celcdescuento);


                PdfPCell cTituloIgv = new PdfPCell(new Phrase("Total I.G.V (18%)", _standarTextoNegrita));
                cTituloIgv.Border = 0;
                // cTituloIgv.Colspan = 4;
                cTituloIgv.HorizontalAlignment = 0; // 0 = izquierda, 1 = centro, 2 = derecha
                cTituloIgv.BorderWidthLeft = 1f;
                cTituloIgv.Padding = 2;
                TableValor.AddCell(cTituloIgv);

                PdfPCell cCeldaIgv = new PdfPCell(new Phrase(ImprimriOC[0].Cotizacion.igv.ToString(), _standarTextoNegrita));
                cCeldaIgv.Border = 0;
                cCeldaIgv.HorizontalAlignment = 2;
                cCeldaIgv.BorderWidthRight = 1f;
                cCeldaIgv.Padding = 2;
                TableValor.AddCell(cCeldaIgv);

                PdfPCell cTituloImporte = new PdfPCell(new Phrase("IMPORTE TOTAL ", _standarTextoNegrita));
                cTituloImporte.Border = 0;
                // cTituloImporte.Colspan = 4;
                cTituloImporte.HorizontalAlignment = 0; // 0 = izquierda, 1 = centro, 2 = derecha
                cTituloImporte.BorderWidthLeft = 1f;
                cTituloImporte.Padding = 2;
                TableValor.AddCell(cTituloImporte);

                PdfPCell cCeldaTotalDoc = new PdfPCell(new Phrase(ImprimriOC[0].Cotizacion.total.ToString(), _standarTextoNegrita));
                cCeldaTotalDoc.Border = 0;
                cCeldaTotalDoc.HorizontalAlignment = 2;
                cCeldaTotalDoc.BorderWidthRight = 1f;
                cCeldaTotalDoc.Padding = 2;
                TableValor.AddCell(cCeldaTotalDoc);

                //
                PdfPCell cTituloMoneda = new PdfPCell(new Phrase("MONEDA", _standarTextoNegrita));
                cTituloMoneda.Border = 0;
                // cTituloMoneda.Colspan = 4;
                cTituloMoneda.HorizontalAlignment = 0; // 0 = izquierda, 1 = centro, 2 = derecha
                cTituloMoneda.BorderWidthLeft = 1f;
                cTituloMoneda.BorderWidthBottom = 1f;
                cTituloMoneda.Padding = 2;
                cTituloMoneda.MinimumHeight = 15;
                TableValor.AddCell(cTituloMoneda);

                PdfPCell cCeldaMoneda = new PdfPCell(new Phrase(ImprimriOC[0].Cotizacion.moneda.Nombre, _standarTextoNegrita));
                cCeldaMoneda.Border = 0;
                cCeldaMoneda.HorizontalAlignment = 2;
                cCeldaMoneda.BorderWidthRight = 1f;
                cCeldaMoneda.BorderWidthBottom = 1f;
                cCeldaMoneda.Padding = 2;
                cCeldaMoneda.MinimumHeight = 15;
                TableValor.AddCell(cCeldaMoneda);

                PdfPCell cCeldaValor = new PdfPCell(TableValor);
                cCeldaValor.Colspan = 5;
                cCeldaValor.Border = 0;
                cCeldaValor.PaddingBottom = 4;
                tblPrueba.AddCell(cCeldaValor);

                //Generar Codigo de barra

                pdfDoc.Add(tblPrueba);

                // Close your PDF 
                pdfDoc.Close();
                /*
                Response.ContentType = "application/pdf";

                // Set default file Name as current datetime 
                Response.AddHeader("content-disposition", "attachment; filename=Documento_" + "_OC_" + ImprimriOC[0].Cotizacion.serie + ".pdf");
                Response.Write(pdfDoc);

                Response.Flush();
                Response.End();
             */

                byte[] bysStream = ns.ToArray();
                ns = new MemoryStream();
                ns.Write(bysStream, 0, bysStream.Length);
                ns.Position = 0;

               // System.IO.File.Delete(path);


                return new FileStreamResult(ns, "application/pdf");
                //return View();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }

        }
        #endregion

    }
}