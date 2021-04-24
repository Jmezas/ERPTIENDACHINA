using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIS.Business;
using SIS.Entity;
using SIS.Principal.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;

namespace SIS.Principal.Controllers
{
    public class GestionController : Controller
    {
        BGestion Gestion = new BGestion();
        BMantenimiento Mantenimiento = new BMantenimiento();
        Conversion Convertir = new Conversion();
        Authentication Authentication = new Authentication();
        BGeneral General = new BGeneral();
        // GET: Gestion
        public ActionResult ListaOC()
        {
            return View();
        }
        public ActionResult CreateOC()
        {
            return View();
        }
        public ActionResult AprobarOC()
        {
            return View();
        }

        public ActionResult ListaRegistroOC()
        {
            return View();
        }
        public ActionResult RegistrarOC()
        {
            return View();
        }
        public ActionResult CrearInventario()
        {
            return View();
        }
        public ActionResult ListaInventario()
        {
            return View();
        }
        public ActionResult ListaStock()
        {
            return View();
        }

        public ActionResult ListaKardex()
        {
            return View();
        }
        [HttpPost]
        public void InstOrdenCompra(EOrdenCompraCab oDatos, List<EOrdenCompraDet> Detalle)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                oDatos.empresa.Id = Authentication.UserLogued.Empresa.Id;
                //    Utils.WriteMessage(Gestion.RegistrarFactura(oDatos, Detalle, Usuario));
                var sMensaje = Gestion.RegistrarFactura(oDatos, Detalle, Usuario);
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
        public void ListaOrdenCompra(string Filltro, int numPag, int allReg, int Cant)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Gestion.ImpirmirListadoOC(Filltro, empresa, numPag, allReg, Cant)
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

        [HttpPost]
        public void ApronarOrdenC(int IdOc, string Motivo, int Flag)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                int Empresa = Authentication.UserLogued.Empresa.Id;
                Utils.WriteMessage(Gestion.AprobarOC(IdOc, Empresa, Motivo, Flag, Usuario));
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
        public void ObtnerDetOC(int Id)
        {
            try
            {
                int Empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                ResponseType.JSON,
                Gestion.ImpirmirOc(Id, Empresa));

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
        public void InstRegistrarOrdenCompra(EOrdenCompraCab oDatos, List<EOrdenCompraDet> Detalle)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                oDatos.empresa.Id = Authentication.UserLogued.Empresa.Id;
                oDatos.ESucursal.IdSucursal = Authentication.UserLogued.Sucursal.IdSucursal;

                var sMensaje = Gestion.RegistrarOrden(oDatos, Detalle, Usuario);
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
        public void ListaRegistroOC(string Filltro, string FechaIncio, string FechaFin, int Afecta, int Inlcuye, int numPag, int allReg, int Cant)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Gestion.ImpirmirRegistroOC(Filltro, FechaIncio, FechaFin, Afecta, Inlcuye, empresa, numPag, allReg, Cant)
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


        [HttpPost]
        public void InstMovimiento(EMovimiento oDatos, List<EMovimientoDet> Detalle)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                oDatos.Empresa.Id = Authentication.UserLogued.Empresa.Id;
                oDatos.Sucursal.IdSucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                //    Utils.WriteMessage(Gestion.RegistrarFactura(oDatos, Detalle, Usuario));
                var sMensaje = Gestion.RegistrarMovimiento(oDatos, Detalle, Usuario);
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
        public void ListaMovimientoCab(string Filltro, string FechaIncio, string FechaFin, int numPag, int allReg, int Cant)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Gestion.ListaMovimiento(Filltro, FechaIncio, FechaFin, empresa, numPag, allReg, Cant)
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

        [HttpPost]
        public void ListaMovimientoDet(int IdMov)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Gestion.ListaMovimientoDetalle(IdMov)
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
        [HttpPost]
        public void ListaStock(string Filtro, int numPag, int allReg, int cantFill)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                Utils.Write(
                     ResponseType.JSON,
                     Gestion.ListaStock(Filtro, empresa, sucursal, numPag, allReg, cantFill)
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

        [HttpPost]
        public void ListaKardex(string FechaIncio, string FechaFin, int idAlm, int idMat, int numPag, int allReg, int Cant)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                Utils.Write(
                    ResponseType.JSON,
                    Gestion.ListaKardex(empresa, sucursal, FechaIncio, FechaFin, idAlm, idMat, numPag, allReg, Cant)
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
        //reportes
        /// <summary>
        ///reporte  
        /// </summary>
        /// <param name="jmeza"></param>
        /// <returns></returns>
        #region Imprimri Orden de compra
        public ActionResult ReporteCompraExcelAll(int Id)
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 20, 20);
            int IdEmpresa = Authentication.UserLogued.Empresa.Id;
            EEmpresa Empresa = Mantenimiento.ListaEditEmpresa(IdEmpresa);

            List<EOrdenCompraDet> ImprimriOC = Gestion.ImpirmirOc(Id, IdEmpresa);
            try
            {
                //Permite visualizar el contenido del documento que es descargado en "Mis descargas"
                PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
                //Ruta dentro del servidor, aqui se guardara el PDF
                var path = Server.MapPath("/") + "Reporte/OC/pdf" + "Documento_" + "_OC_" + ImprimriOC[0].OrdenCompraCab.Serie + ".pdf";

                // Se utiliza system.IO para crear o sobreescribe el archivo si existe
                FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                //'iTextSharp para escribir en el documento PDF, sin esto no podemos ver el contenido
                PdfWriter.GetInstance(pdfDoc, file);

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

                string imagePath = Server.MapPath("/assets/images/LogoDefault.PNG");
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

                PdfPCell celContacto = new PdfPCell(new Phrase("www.biofer.com.pe", _standarTexto));
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

                PdfPCell cTipoDoc = new PdfPCell(new Phrase("ORDEN DE COMPRA", _standarTitulo));
                cTipoDoc.HorizontalAlignment = 1;
                cTipoDoc.Border = 0;
                cTipoDoc.BorderWidthRight = 1f;
                cTipoDoc.BorderWidthLeft = 1f;
                tableDer.AddCell(cTipoDoc);

                PdfPCell cNumFac = new PdfPCell(new Phrase("N°: " + ImprimriOC[0].OrdenCompraCab.Serie, _standarTitulo));
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

                PdfPCell cRuc = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Proveedor.Nombre, _standardFont));
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

                PdfPCell cDireccion = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Proveedor.Direccion, _standardFont));
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

                PdfPCell cPago = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Nombre, _standardFont));
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

                PdfPCell cFchaE = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.FechaRegistro, _standardFont));
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

                PdfPCell cRazonsocial = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Proveedor.NroDocumento, _standardFont));
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

                foreach (EOrdenCompraDet ListaRep in ImprimriOC)
                {

                    PdfPCell cCodigo = new PdfPCell(new Phrase(ListaRep.Cantidad.ToString(), _standardFont));
                    cCodigo.Border = 0;
                    cCodigo.Padding = 2;
                    cCodigo.PaddingBottom = 2;
                    cCodigo.HorizontalAlignment = 1;
                    cCodigo.Border = Rectangle.LEFT_BORDER;
                    Tableitems.AddCell(cCodigo);

                    PdfPCell cNombreMat = new PdfPCell(new Phrase(ListaRep.Material.Unidad.Nombre, _standardFont));
                    cNombreMat.Border = 0;
                    cNombreMat.Padding = 2;
                    cNombreMat.PaddingBottom = 2;
                    cNombreMat.HorizontalAlignment = 1;
                    cNombreMat.Border = Rectangle.LEFT_BORDER;
                    Tableitems.AddCell(cNombreMat);

                    PdfPCell cCantidad = new PdfPCell(new Phrase(ListaRep.Material.Nombre + " - " + ListaRep.Material.Marca.Nombre, _standardFont));
                    cCantidad.Border = 0;
                    cCantidad.Padding = 2;
                    cCantidad.PaddingBottom = 2;
                    cCantidad.HorizontalAlignment = 0;
                    cCantidad.Border = Rectangle.LEFT_BORDER;
                    Tableitems.AddCell(cCantidad);

                    PdfPCell cPrecio = new PdfPCell(new Phrase(ListaRep.Precio.ToString(), _standardFont));
                    cPrecio.Border = 0;
                    cPrecio.Padding = 2;
                    cPrecio.PaddingBottom = 2;
                    cPrecio.HorizontalAlignment = 1;
                    cPrecio.Border = Rectangle.LEFT_BORDER;
                    Tableitems.AddCell(cPrecio);

                    PdfPCell cImporte = new PdfPCell(new Phrase((ListaRep.Precio * ListaRep.Cantidad).ToString(), _standardFont));
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

                PdfPCell cLetras = new PdfPCell(new Phrase("SON: " + Convertir.enletras(ImprimriOC[0].OrdenCompraCab.Total.ToString()) + " " + ImprimriOC[0].OrdenCompraCab.Moneda.Nombre, _standardFont));
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

                PdfPCell cTituloSub = new PdfPCell(new Phrase("Sub Total ", _standarTextoNegrita));
                cTituloSub.Border = 0;
                // cTituloSub.Colspan = 4;
                cTituloSub.HorizontalAlignment = 0; // 0 = izquierda, 1 = centro, 2 = derecha
                cTituloSub.BorderWidthLeft = 1f;
                cTituloSub.BorderWidthTop = 1f;
                cTituloSub.Padding = 2;
                TableValor.AddCell(cTituloSub);

                PdfPCell cCeldaSubtotal = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.SubTotal.ToString(), _standarTextoNegrita));
                cCeldaSubtotal.Border = 0;
                // cCeldaSubtotal.Colspan = 2
                cCeldaSubtotal.HorizontalAlignment = 2;
                cCeldaSubtotal.BorderWidthRight = 1f;
                cCeldaSubtotal.BorderWidthTop = 1f;
                cCeldaSubtotal.Padding = 2;
                TableValor.AddCell(cCeldaSubtotal);



                PdfPCell cTituloIgv = new PdfPCell(new Phrase("Total I.G.V (18%)", _standarTextoNegrita));
                cTituloIgv.Border = 0;
                // cTituloIgv.Colspan = 4;
                cTituloIgv.HorizontalAlignment = 0; // 0 = izquierda, 1 = centro, 2 = derecha
                cTituloIgv.BorderWidthLeft = 1f;
                cTituloIgv.Padding = 2;
                TableValor.AddCell(cTituloIgv);

                PdfPCell cCeldaIgv = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.IGV.ToString(), _standarTextoNegrita));
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

                PdfPCell cCeldaTotalDoc = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Total.ToString(), _standarTextoNegrita));
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

                PdfPCell cCeldaMoneda = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Moneda.Nombre, _standarTextoNegrita));
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

                Response.ContentType = "application/pdf";

                // Set default file Name as current datetime 
                Response.AddHeader("content-disposition", "attachment; filename=Documento_" + "_OC_" + ImprimriOC[0].OrdenCompraCab.Serie + ".pdf");
                Response.Write(pdfDoc);

                Response.Flush();
                Response.End();
                System.IO.File.Delete(path);
                return View();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }

        }
        #endregion

        #region Reporte OC
        public ActionResult ReporteCompraPDF(string Filltro, int NumPag, int all, int Cantidad)
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 20, 20);
            int empresa = Authentication.UserLogued.Empresa.Id;
            List<EOrdenCompraCab> ListaCompra = Gestion.ImpirmirListadoOC(Filltro, empresa, NumPag, all, Cantidad);

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

                PdfPTable Tableitems = new PdfPTable(9);
                Tableitems.WidthPercentage = 100;
                Tableitems.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

                PdfPCell celCospan = new PdfPCell(new Phrase("REPORTE COMPRA"));
                celCospan.Colspan = 9;
                celCospan.Border = 0;
                celCospan.HorizontalAlignment = 1;
                Tableitems.AddCell(celCospan);


                PdfPCell cEspacio = new PdfPCell(new Phrase("    "));
                cEspacio.Colspan = 9;
                cEspacio.Border = 0;
                cEspacio.HorizontalAlignment = 1;
                Tableitems.AddCell(cEspacio);

                Single[] celdas2 = new Single[] { 1.5F, 2.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F };
                Tableitems.SetWidths(celdas2);
                PdfPCell cTituloCant = new PdfPCell(new Phrase("Serie", _standardFont));
                cTituloCant.Border = 1;
                cTituloCant.BorderWidthBottom = 1;
                cTituloCant.HorizontalAlignment = 1;
                cTituloCant.Padding = 5;
                Tableitems.AddCell(cTituloCant);

                PdfPCell CTituloSerie = new PdfPCell(new Phrase("Proveedor", _standardFont));
                CTituloSerie.Border = 1;
                CTituloSerie.BorderWidthBottom = 1;
                CTituloSerie.HorizontalAlignment = 1;
                CTituloSerie.Padding = 5;
                Tableitems.AddCell(CTituloSerie);

                PdfPCell cTipoPago = new PdfPCell(new Phrase("Tipo Pago", _standardFont));
                cTipoPago.Border = 1;
                cTipoPago.BorderWidthBottom = 1;
                cTipoPago.HorizontalAlignment = 1;
                cTipoPago.Padding = 5;
                Tableitems.AddCell(cTipoPago);

                PdfPCell cMonedaT = new PdfPCell(new Phrase("Moneda", _standardFont));
                cMonedaT.Border = 1;
                cMonedaT.BorderWidthBottom = 1;
                cMonedaT.HorizontalAlignment = 1;
                cMonedaT.Padding = 5;
                Tableitems.AddCell(cMonedaT);


                PdfPCell cFechaRegistro = new PdfPCell(new Phrase("Fecha Registro", _standardFont));
                cFechaRegistro.Border = 1;
                cFechaRegistro.BorderWidthBottom = 1;
                cFechaRegistro.HorizontalAlignment = 1;
                cFechaRegistro.Padding = 5;
                Tableitems.AddCell(cFechaRegistro);

                PdfPCell cFechaPago = new PdfPCell(new Phrase("Fecha Pago", _standardFont));
                cFechaPago.Border = 1;
                cFechaPago.BorderWidthBottom = 1;
                cFechaPago.HorizontalAlignment = 1;
                cFechaPago.Padding = 5;
                Tableitems.AddCell(cFechaPago);

                PdfPCell cSubTotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                cSubTotal.Border = 1;
                cSubTotal.BorderWidthBottom = 1;
                cSubTotal.HorizontalAlignment = 1;
                cSubTotal.Padding = 5;
                Tableitems.AddCell(cSubTotal);

                PdfPCell cIGV = new PdfPCell(new Phrase("IGv", _standardFont));
                cIGV.Border = 1;
                cIGV.BorderWidthBottom = 1;
                cIGV.HorizontalAlignment = 1;
                cIGV.Padding = 5;
                Tableitems.AddCell(cIGV);

                PdfPCell cTitulo = new PdfPCell(new Phrase("Total", _standardFont));
                cTitulo.Border = 1;
                cTitulo.BorderWidthBottom = 1;
                cTitulo.HorizontalAlignment = 1;
                cTitulo.Padding = 5;
                Tableitems.AddCell(cTitulo);

                Font letrasDatosTabla = FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.NORMAL);

                double IGV = 0, SubTotal = 0, Total = 0;

                foreach (EOrdenCompraCab cuentaPago in ListaCompra)
                {


                    PdfPCell cdocumento = new PdfPCell(new Phrase(cuentaPago.Serie, letrasDatosTabla));
                    cdocumento.Border = 0;
                    cdocumento.Padding = 2;
                    cdocumento.PaddingBottom = 2;
                    cdocumento.HorizontalAlignment = 1;
                    Tableitems.AddCell(cdocumento);

                    PdfPCell cSerie = new PdfPCell(new Phrase(cuentaPago.Proveedor.Nombre, letrasDatosTabla));
                    cSerie.Border = 0;
                    cSerie.Padding = 2;
                    cSerie.PaddingBottom = 2;
                    cSerie.HorizontalAlignment = 1;
                    Tableitems.AddCell(cSerie);

                    PdfPCell cellTipoPago = new PdfPCell(new Phrase(cuentaPago.Text, letrasDatosTabla));
                    cellTipoPago.Border = 0;
                    cellTipoPago.Padding = 2;
                    cellTipoPago.PaddingBottom = 2;
                    cellTipoPago.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellTipoPago);

                    PdfPCell cFecha = new PdfPCell(new Phrase(cuentaPago.Moneda.Nombre, letrasDatosTabla));
                    cFecha.Border = 0;
                    cFecha.Padding = 2;
                    cFecha.PaddingBottom = 2;
                    cFecha.HorizontalAlignment = 1;
                    Tableitems.AddCell(cFecha);



                    //


                    PdfPCell cellMoneda = new PdfPCell(new Phrase(cuentaPago.FechaRegistro, letrasDatosTabla));
                    cellMoneda.Border = 0;
                    cellMoneda.Padding = 2;
                    cellMoneda.PaddingBottom = 2;
                    cellMoneda.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellMoneda);


                    PdfPCell cellFechaRegistro = new PdfPCell(new Phrase(cuentaPago.FechaPago, letrasDatosTabla));
                    cellFechaRegistro.Border = 0;
                    cellFechaRegistro.Padding = 2;
                    cellFechaRegistro.PaddingBottom = 2;
                    cellFechaRegistro.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellFechaRegistro);


                    PdfPCell cellFSubTotal = new PdfPCell(new Phrase(Math.Round(cuentaPago.SubTotal, 2).ToString(), letrasDatosTabla));
                    cellFSubTotal.Border = 0;
                    cellFSubTotal.Padding = 2;
                    cellFSubTotal.PaddingBottom = 2;
                    cellFSubTotal.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellFSubTotal);

                    PdfPCell cellIGV = new PdfPCell(new Phrase(Math.Round(cuentaPago.IGV, 2).ToString(), letrasDatosTabla));
                    cellIGV.Border = 0;
                    cellIGV.Padding = 2;
                    cellIGV.PaddingBottom = 2;
                    cellIGV.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellIGV);

                    PdfPCell cNroDocumento = new PdfPCell(new Phrase(Math.Round(cuentaPago.Total, 2).ToString(), letrasDatosTabla));
                    cNroDocumento.Border = 0;
                    cNroDocumento.Padding = 2;
                    cNroDocumento.PaddingBottom = 2;
                    cNroDocumento.HorizontalAlignment = 1;
                    Tableitems.AddCell(cNroDocumento);


                    Total += Convert.ToDouble(cuentaPago.Total);
                    IGV += Convert.ToDouble(cuentaPago.IGV);
                    SubTotal += Convert.ToDouble(cuentaPago.SubTotal);

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



                PdfPCell cTTotales = new PdfPCell(new Phrase("SubTotal:   " + string.Format("{0:n}", SubTotal), _standardFont));
                cTTotales.Border = 0;
                cTTotales.Colspan = 2;
                cTTotales.HorizontalAlignment = 2; // 0 = izquierda, 1 = centro, 2 = derecha
                TableValor.AddCell(cTTotales);

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
                Response.AddHeader("content-disposition", "attachment; filename=ReporteCompras.pdf");
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
        public ActionResult ReporteCompraExcel(string Filltro, int NumPag, int AllReg, int CantiFill)
        {
            try
            {


                //Ruta de la plantilla
                FileInfo fTemplateFile = new FileInfo(Server.MapPath("/") + "Reporte/OC/Excel/" + "Compras.xlsx");

                var Usuario = Authentication.UserLogued.Usuario;
                int Empresa = Authentication.UserLogued.Empresa.Id;
                //Ruta del nuevo documento
                FileInfo fNewFile = new FileInfo(Server.MapPath("/") + "Reporte/OC/Excel/" + "Compras" + "_" + Usuario + ".xlsx");

                List<EOrdenCompraCab> ListaCompra = Gestion.ImpirmirListadoOC(Filltro, Empresa, NumPag, AllReg, CantiFill);
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
                double SubTotal = 0, IGV = 0, Total = 0;
                foreach (EOrdenCompraCab Detalle in ListaCompra)
                {
                    ws.Cells[iFilaDetIni + starRow, 1].Value = Detalle.Serie;
                    ws.Cells[iFilaDetIni + starRow, 2].Value = Detalle.Proveedor.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 3].Value = Detalle.Text;
                    ws.Cells[iFilaDetIni + starRow, 4].Value = Detalle.Moneda.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 5].Value = Detalle.FechaRegistro;
                    ws.Cells[iFilaDetIni + starRow, 6].Value = Detalle.FechaPago;
                    ws.Cells[iFilaDetIni + starRow, 7].Value = Math.Round(Convert.ToDouble(Detalle.SubTotal), 2);
                    ws.Cells[iFilaDetIni + starRow, 8].Value = Math.Round(Convert.ToDouble(Detalle.IGV), 2);
                    ws.Cells[iFilaDetIni + starRow, 9].Value = Math.Round(Convert.ToDouble(Detalle.Total), 2);
                    SubTotal += Convert.ToDouble(Detalle.SubTotal);
                    IGV += Convert.ToDouble(Detalle.IGV);
                    Total += Convert.ToDouble(Detalle.Total);
                    starRow++;

                }
                int Count = starRow + 1;

                ws.Cells[iFilaDetIni + (Count - 1), 6].Value = "Totales";
                ws.Cells[iFilaDetIni + (Count - 1), 7].Value = Math.Round(Convert.ToDouble(SubTotal), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 8].Value = Math.Round(Convert.ToDouble(IGV), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 9].Value = Math.Round(Convert.ToDouble(Total), 2);


                string modelRange = "A" + System.Convert.ToString(iFilaDetIni) + ":I" + (iFilaDetIni + Count - 1).ToString();
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
                Response.AddHeader("content-disposition", "attachment;  filename=" + "ReporteCompra" + "_" + Usuario + ".xlsx");

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

        //registro compras

        #region Reporte OC
        public ActionResult ReporteRegistroPDF(string Filltro, string FechaIncio, string FechaFin, int Afecta, int Inlcuye, int numPag, int allReg, int Cant)
        {
            Document pdfDoc = new Document(PageSize.A4, 5, 5, 20, 20);
            int empresa = Authentication.UserLogued.Empresa.Id;
            List<EOrdenCompraCab> ListaCompra = Gestion.ImpirmirRegistroOC(Filltro, FechaIncio, FechaFin, Afecta, Inlcuye, empresa, numPag, allReg, Cant);

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

                PdfPTable Tableitems = new PdfPTable(13);
                Tableitems.WidthPercentage = 100;
                Tableitems.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

                PdfPCell celCospan = new PdfPCell(new Phrase("REPORTE REGISTRO COMPRA"));
                celCospan.Colspan = 13;
                celCospan.Border = 0;
                celCospan.HorizontalAlignment = 1;
                Tableitems.AddCell(celCospan);


                PdfPCell cEspacio = new PdfPCell(new Phrase("    "));
                cEspacio.Colspan = 13;
                cEspacio.Border = 0;
                cEspacio.HorizontalAlignment = 1;
                Tableitems.AddCell(cEspacio);

                Single[] celdas2 = new Single[] { 1.05F, 1.0F, 1.0F, 1.0F, 1.0F, 1.3F, 1.0F, 1.0F, 2.0F, 1.05F, 1.05F, 0.8F, 1.0F };
                Tableitems.SetWidths(celdas2);
                PdfPCell cTituloCant = new PdfPCell(new Phrase("Serie", _standardFont));
                cTituloCant.Border = 1;
                cTituloCant.BorderWidthBottom = 1;
                cTituloCant.HorizontalAlignment = 1;
                cTituloCant.Padding = 5;
                Tableitems.AddCell(cTituloCant);

                PdfPCell CTituloSerie = new PdfPCell(new Phrase("Tipo Doc", _standardFont));
                CTituloSerie.Border = 1;
                CTituloSerie.BorderWidthBottom = 1;
                CTituloSerie.HorizontalAlignment = 1;
                CTituloSerie.Padding = 5;
                Tableitems.AddCell(CTituloSerie);

                PdfPCell cTipoPago = new PdfPCell(new Phrase("Tipo Pago", _standardFont));
                cTipoPago.Border = 1;
                cTipoPago.BorderWidthBottom = 1;
                cTipoPago.HorizontalAlignment = 1;
                cTipoPago.Padding = 5;
                Tableitems.AddCell(cTipoPago);

                PdfPCell cRegistro = new PdfPCell(new Phrase("FechaRegistro", _standardFont));
                cRegistro.Border = 1;
                cRegistro.BorderWidthBottom = 1;
                cRegistro.HorizontalAlignment = 1;
                cRegistro.Padding = 5;
                Tableitems.AddCell(cRegistro);


                PdfPCell cFechaRegistro = new PdfPCell(new Phrase("Fecha Pago", _standardFont));
                cFechaRegistro.Border = 1;
                cFechaRegistro.BorderWidthBottom = 1;
                cFechaRegistro.HorizontalAlignment = 1;
                cFechaRegistro.Padding = 5;
                Tableitems.AddCell(cFechaRegistro);

                PdfPCell cConceptot = new PdfPCell(new Phrase("Concepto", _standardFont));
                cConceptot.Border = 1;
                cConceptot.BorderWidthBottom = 1;
                cConceptot.HorizontalAlignment = 1;
                cConceptot.Padding = 5;
                Tableitems.AddCell(cConceptot);

                PdfPCell cincIgv = new PdfPCell(new Phrase("Inc. igv", _standardFont));
                cincIgv.Border = 1;
                cincIgv.BorderWidthBottom = 1;
                cincIgv.HorizontalAlignment = 1;
                cincIgv.Padding = 5;
                Tableitems.AddCell(cincIgv);

                PdfPCell cafecta = new PdfPCell(new Phrase("AfectaIGv", _standardFont));
                cafecta.Border = 1;
                cafecta.BorderWidthBottom = 1;
                cafecta.HorizontalAlignment = 1;
                cafecta.Padding = 5;
                Tableitems.AddCell(cafecta);

                PdfPCell cProveedor = new PdfPCell(new Phrase("Proveedor", _standardFont));
                cProveedor.Border = 1;
                cProveedor.BorderWidthBottom = 1;
                cProveedor.HorizontalAlignment = 1;
                cProveedor.Padding = 5;
                Tableitems.AddCell(cProveedor);


                PdfPCell cMonedaT = new PdfPCell(new Phrase("Moneda", _standardFont));
                cMonedaT.Border = 1;
                cMonedaT.BorderWidthBottom = 1;
                cMonedaT.HorizontalAlignment = 1;
                cMonedaT.Padding = 5;
                Tableitems.AddCell(cMonedaT);

                PdfPCell cSubTotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                cSubTotal.Border = 1;
                cSubTotal.BorderWidthBottom = 1;
                cSubTotal.HorizontalAlignment = 1;
                cSubTotal.Padding = 5;
                Tableitems.AddCell(cSubTotal);

                PdfPCell cIGV = new PdfPCell(new Phrase("IGV", _standardFont));
                cIGV.Border = 1;
                cIGV.BorderWidthBottom = 1;
                cIGV.HorizontalAlignment = 1;
                cIGV.Padding = 5;
                Tableitems.AddCell(cIGV);

                PdfPCell cTitulo = new PdfPCell(new Phrase("Total", _standardFont));
                cTitulo.Border = 1;
                cTitulo.BorderWidthBottom = 1;
                cTitulo.HorizontalAlignment = 1;
                cTitulo.Padding = 5;
                Tableitems.AddCell(cTitulo);

                Font letrasDatosTabla = FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.NORMAL);

                double IGV = 0, SubTotal = 0, Total = 0;

                foreach (EOrdenCompraCab cuentaPago in ListaCompra)
                {


                    PdfPCell cdocumento = new PdfPCell(new Phrase(cuentaPago.Serie, letrasDatosTabla));
                    cdocumento.Border = 0;
                    cdocumento.Padding = 2;
                    cdocumento.PaddingBottom = 2;
                    cdocumento.HorizontalAlignment = 1;
                    Tableitems.AddCell(cdocumento);

                    PdfPCell cDocumento = new PdfPCell(new Phrase(cuentaPago.Documento.Nombre, letrasDatosTabla));
                    cDocumento.Border = 0;
                    cDocumento.Padding = 2;
                    cDocumento.PaddingBottom = 2;
                    cDocumento.HorizontalAlignment = 1;
                    Tableitems.AddCell(cDocumento);

                    PdfPCell cellTipoPago = new PdfPCell(new Phrase(cuentaPago.Text, letrasDatosTabla));
                    cellTipoPago.Border = 0;
                    cellTipoPago.Padding = 2;
                    cellTipoPago.PaddingBottom = 2;
                    cellTipoPago.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellTipoPago);

                    PdfPCell cellFechaRegistro = new PdfPCell(new Phrase(cuentaPago.FechaRegistro, letrasDatosTabla));
                    cellFechaRegistro.Border = 0;
                    cellFechaRegistro.Padding = 2;
                    cellFechaRegistro.PaddingBottom = 2;
                    cellFechaRegistro.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellFechaRegistro);


                    PdfPCell cellFechaPago = new PdfPCell(new Phrase(cuentaPago.FechaPago, letrasDatosTabla));
                    cellFechaPago.Border = 0;
                    cellFechaPago.Padding = 2;
                    cellFechaPago.PaddingBottom = 2;
                    cellFechaPago.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellFechaPago);

                    PdfPCell csConcepto = new PdfPCell(new Phrase(cuentaPago.sConcepto, letrasDatosTabla));
                    csConcepto.Border = 0;
                    csConcepto.Padding = 2;
                    csConcepto.PaddingBottom = 2;
                    csConcepto.HorizontalAlignment = 1;
                    Tableitems.AddCell(csConcepto);

                    PdfPCell icluyeIGV = new PdfPCell(new Phrase(cuentaPago.icluyeIGV, letrasDatosTabla));
                    icluyeIGV.Border = 0;
                    icluyeIGV.Padding = 2;
                    icluyeIGV.PaddingBottom = 2;
                    icluyeIGV.HorizontalAlignment = 1;
                    Tableitems.AddCell(icluyeIGV);

                    PdfPCell AfectaStockString = new PdfPCell(new Phrase(cuentaPago.AfectaStockString, letrasDatosTabla));
                    AfectaStockString.Border = 0;
                    AfectaStockString.Padding = 2;
                    AfectaStockString.PaddingBottom = 2;
                    AfectaStockString.HorizontalAlignment = 1;
                    Tableitems.AddCell(AfectaStockString);

                    PdfPCell cellProveedor = new PdfPCell(new Phrase(cuentaPago.Proveedor.Nombre, letrasDatosTabla));
                    cellProveedor.Border = 0;
                    cellProveedor.Padding = 2;
                    cellProveedor.PaddingBottom = 2;
                    cellProveedor.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellProveedor);

                    PdfPCell cellMoneda = new PdfPCell(new Phrase(cuentaPago.Moneda.Nombre, letrasDatosTabla));
                    cellMoneda.Border = 0;
                    cellMoneda.Padding = 2;
                    cellMoneda.PaddingBottom = 2;
                    cellMoneda.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellMoneda);


                    PdfPCell cellFSubTotal = new PdfPCell(new Phrase(Math.Round(cuentaPago.SubTotal, 2).ToString(), letrasDatosTabla));
                    cellFSubTotal.Border = 0;
                    cellFSubTotal.Padding = 2;
                    cellFSubTotal.PaddingBottom = 2;
                    cellFSubTotal.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellFSubTotal);

                    PdfPCell cellIGV = new PdfPCell(new Phrase(Math.Round(cuentaPago.IGV, 2).ToString(), letrasDatosTabla));
                    cellIGV.Border = 0;
                    cellIGV.Padding = 2;
                    cellIGV.PaddingBottom = 2;
                    cellIGV.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellIGV);

                    PdfPCell cNroDocumento = new PdfPCell(new Phrase(Math.Round(cuentaPago.Total, 2).ToString(), letrasDatosTabla));
                    cNroDocumento.Border = 0;
                    cNroDocumento.Padding = 2;
                    cNroDocumento.PaddingBottom = 2;
                    cNroDocumento.HorizontalAlignment = 1;
                    Tableitems.AddCell(cNroDocumento);


                    Total += Convert.ToDouble(cuentaPago.Total);
                    IGV += Convert.ToDouble(cuentaPago.IGV);
                    SubTotal += Convert.ToDouble(cuentaPago.SubTotal);

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



                PdfPCell cTTotales = new PdfPCell(new Phrase("SubTotal:   " + string.Format("{0:n}", SubTotal), _standardFont));
                cTTotales.Border = 0;
                cTTotales.Colspan = 2;
                cTTotales.HorizontalAlignment = 2; // 0 = izquierda, 1 = centro, 2 = derecha
                TableValor.AddCell(cTTotales);

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
                Response.AddHeader("content-disposition", "attachment; filename=ReporteRegistroCompras.pdf");
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

        #region Reporte Registrado Excel
        public ActionResult ReporteRegistroExcel(string Filltro, string FechaIncio, string FechaFin, int Afecta, int Inlcuye, int numPag, int allReg, int Cant)
        {
            try
            {


                //Ruta de la plantilla
                FileInfo fTemplateFile = new FileInfo(Server.MapPath("/") + "Reporte/OC/Excel/" + "ComprasRegistro.xlsx");

                var Usuario = Authentication.UserLogued.Usuario;
                int Empresa = Authentication.UserLogued.Empresa.Id;
                //Ruta del nuevo documento
                FileInfo fNewFile = new FileInfo(Server.MapPath("/") + "Reporte/OC/Excel/" + "ComprasRegistro" + "_" + Usuario + ".xlsx");

                List<EOrdenCompraCab> ListaCompra = Gestion.ImpirmirRegistroOC(Filltro, FechaIncio, FechaFin, Afecta, Inlcuye, Empresa, numPag, allReg, Cant);
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
                double SubTotal = 0, IGV = 0, Total = 0;
                foreach (EOrdenCompraCab Detalle in ListaCompra)
                {
                    ws.Cells[iFilaDetIni + starRow, 1].Value = Detalle.Serie;
                    ws.Cells[iFilaDetIni + starRow, 2].Value = Detalle.Documento.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 3].Value = Detalle.Text;
                    ws.Cells[iFilaDetIni + starRow, 4].Value = Detalle.FechaRegistro;
                    ws.Cells[iFilaDetIni + starRow, 5].Value = Detalle.FechaPago;
                    ws.Cells[iFilaDetIni + starRow, 6].Value = Detalle.sConcepto;
                    ws.Cells[iFilaDetIni + starRow, 7].Value = Detalle.icluyeIGV;
                    ws.Cells[iFilaDetIni + starRow, 8].Value = Detalle.AfectaStockString;
                    ws.Cells[iFilaDetIni + starRow, 9].Value = Detalle.Proveedor.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 9].Style.WrapText = true;
                    ws.Cells[iFilaDetIni + starRow, 10].Value = Detalle.Moneda.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 11].Value = Math.Round(Convert.ToDouble(Detalle.SubTotal), 2);
                    ws.Cells[iFilaDetIni + starRow, 12].Value = Math.Round(Convert.ToDouble(Detalle.IGV), 2);
                    ws.Cells[iFilaDetIni + starRow, 13].Value = Math.Round(Convert.ToDouble(Detalle.Total), 2);
                    SubTotal += Convert.ToDouble(Detalle.SubTotal);
                    IGV += Convert.ToDouble(Detalle.IGV);
                    Total += Convert.ToDouble(Detalle.Total);
                    starRow++;

                }
                int Count = starRow + 1;

                ws.Cells[iFilaDetIni + (Count - 1), 10].Value = "Totales";
                ws.Cells[iFilaDetIni + (Count - 1), 11].Value = Math.Round(Convert.ToDouble(SubTotal), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 12].Value = Math.Round(Convert.ToDouble(IGV), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 13].Value = Math.Round(Convert.ToDouble(Total), 2);


                string modelRange = "A" + System.Convert.ToString(iFilaDetIni) + ":M" + (iFilaDetIni + Count - 1).ToString();
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
                Response.AddHeader("content-disposition", "attachment;  filename=" + "ReporteRegistroOC" + "_" + Usuario + ".xlsx");

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


        #region Imprimri Orden de compra
        public ActionResult ReporteRegistroDet(int Id)
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 20, 20);
            int IdEmpresa = Authentication.UserLogued.Empresa.Id;
            EEmpresa Empresa = Mantenimiento.ListaEditEmpresa(IdEmpresa);

            List<EOrdenCompraDet> ImprimriOC = Gestion.ImpirmirRegistroOcDet(Id, IdEmpresa);
            try
            {
                //Permite visualizar el contenido del documento que es descargado en "Mis descargas"
                PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
                //Ruta dentro del servidor, aqui se guardara el PDF
                var path = Server.MapPath("/") + "Reporte/OC/pdf" + "Documento_" + "_OC_" + ImprimriOC[0].OrdenCompraCab.Serie + ".pdf";

                // Se utiliza system.IO para crear o sobreescribe el archivo si existe
                FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                //'iTextSharp para escribir en el documento PDF, sin esto no podemos ver el contenido
                PdfWriter.GetInstance(pdfDoc, file);

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

                string imagePath = Server.MapPath("/assets/images/LogoDefault.PNG");
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

                PdfPCell celContacto = new PdfPCell(new Phrase("www.biofer.com.pe", _standarTexto));
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

                PdfPCell cTipoDoc = new PdfPCell(new Phrase("REGISTRO DE COMPRA", _standarTitulo));
                cTipoDoc.HorizontalAlignment = 1;
                cTipoDoc.Border = 0;
                cTipoDoc.BorderWidthRight = 1f;
                cTipoDoc.BorderWidthLeft = 1f;
                tableDer.AddCell(cTipoDoc);

                PdfPCell cNumFac = new PdfPCell(new Phrase("N°: " + ImprimriOC[0].OrdenCompraCab.Serie, _standarTitulo));
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

                PdfPCell cRuc = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Proveedor.Nombre, _standardFont));
                cRuc.Border = 0;
                cRuc.HorizontalAlignment = 0;
                cRuc.BorderWidthTop = 1f;
                cRuc.BorderWidthRight = 1f;
                cRuc.Padding = 5;
                TableCab.AddCell(cRuc);



                PdfPCell celDocumento = new PdfPCell(new Phrase("DOCUMENTO:", _standardFont));
                celDocumento.Border = 0;
                celDocumento.HorizontalAlignment = 0;
                celDocumento.BorderWidthLeft = 1f;
                celDocumento.Padding = 5;
                TableCab.AddCell(celDocumento);

                PdfPCell celDocumentoDes = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Proveedor.NroDocumento, _standardFont));
                celDocumentoDes.Border = 0;
                celDocumentoDes.HorizontalAlignment = 0;
                celDocumentoDes.BorderWidthRight = 1f;
                celDocumentoDes.Padding = 5;
                TableCab.AddCell(celDocumentoDes);



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

                PdfPCell cDireccion = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Proveedor.Direccion, _standardFont));
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
                //   cTiPago.BorderWidthBottom = 1f;
                cTiPago.Padding = 5;
                cTiPago.MinimumHeight = 30;
                TableCab.AddCell(cTiPago);

                PdfPCell cPago = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Nombre, _standardFont));
                cPago.Border = 0;
                cPago.HorizontalAlignment = 0;
                cPago.BorderWidthRight = 1f;
                // cPago.BorderWidthBottom = 1f;
                cPago.Padding = 5;
                cTiPago.MinimumHeight = 30;
                TableCab.AddCell(cPago);


                PdfPCell CTipocDoc = new PdfPCell(new Phrase("TIPO DOC:", _standardFont));
                CTipocDoc.Border = 0;
                CTipocDoc.HorizontalAlignment = 0;
                CTipocDoc.BorderWidthLeft = 1f;
                CTipocDoc.BorderWidthBottom = 1f;
                CTipocDoc.Padding = 5;
                CTipocDoc.MinimumHeight = 30;
                TableCab.AddCell(CTipocDoc);

                PdfPCell celTipoDocumento = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Documento.Nombre, _standardFont));
                celTipoDocumento.Border = 0;
                celTipoDocumento.HorizontalAlignment = 0;
                celTipoDocumento.BorderWidthRight = 1f;
                celTipoDocumento.BorderWidthBottom = 1f;
                celTipoDocumento.Padding = 5;
                celTipoDocumento.MinimumHeight = 30;
                TableCab.AddCell(celTipoDocumento);



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
                //cTituloFecha.BorderWidthBottom = 1f;
                cTituloFecha.Padding = 5;
                Tabledoble.AddCell(cTituloFecha);

                PdfPCell cFchaE = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.FechaRegistro, _standardFont));
                cFchaE.Border = 0; // borde cero
                cFchaE.HorizontalAlignment = 0;
                cFchaE.PaddingTop = 2;
                cFchaE.BorderWidthRight = 1f;
                //cFchaE.BorderWidthBottom = 1f;
                cFchaE.BorderWidthTop = 1f;
                cFchaE.Padding = 5;
                Tabledoble.AddCell(cFchaE);



                PdfPCell cTituloFechaV = new PdfPCell(new Phrase("FECHA VENCIMIENTO:", _standardFont));
                cTituloFechaV.Border = 0;
                cTituloFechaV.HorizontalAlignment = 0;
                cTituloFechaV.PaddingTop = 2;
                cTituloFechaV.BorderWidthLeft = 1f;
                //cTituloFecha.BorderWidthTop = 1f;
                //cTituloFechaV.BorderWidthBottom = 1f;
                cTituloFechaV.Padding = 5;
                Tabledoble.AddCell(cTituloFechaV);

                PdfPCell cFchaEVen = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.FechaPago, _standardFont));
                cFchaEVen.Border = 0; // borde cero
                cFchaEVen.HorizontalAlignment = 0;
                cFchaEVen.PaddingTop = 2;
                cFchaEVen.BorderWidthRight = 1f;
                //cFchaEVen.BorderWidthBottom = 1f;
                //cFchaEVen.BorderWidthTop = 1f;
                cFchaEVen.Padding = 5;
                Tabledoble.AddCell(cFchaEVen);


                PdfPCell cConceptoT = new PdfPCell(new Phrase("Concepto:", _standardFont));
                cConceptoT.Border = 0;
                cConceptoT.HorizontalAlignment = 0;
                cConceptoT.PaddingTop = 2;
                cConceptoT.BorderWidthLeft = 1f;
                //  cConceptoT.BorderWidthTop = 1f;
                cConceptoT.BorderWidthBottom = 1f;
                cConceptoT.Padding = 5;
                Tabledoble.AddCell(cConceptoT);

                PdfPCell celConcepto = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.sConcepto, _standardFont));
                celConcepto.Border = 0; // borde cero
                celConcepto.HorizontalAlignment = 0;
                celConcepto.PaddingTop = 2;
                celConcepto.BorderWidthRight = 1f;
                celConcepto.BorderWidthBottom = 1f;
                // celConcepto.BorderWidthTop = 1f;
                celConcepto.Padding = 5;
                Tabledoble.AddCell(celConcepto);


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

                PdfPCell cTitulo1 = new PdfPCell(new Phrase("N° OC:", _standardFont));
                cTitulo1.Border = 0;
                cTitulo1.HorizontalAlignment = 0;
                cTitulo1.PaddingTop = 2;
                cTitulo1.BorderWidthTop = 1f;
                cTitulo1.BorderWidthLeft = 1f;
                cTitulo1.Padding = 5;
                //  cTitulo1.BorderWidthBottom = 1;
                Tabledoble.AddCell(cTitulo1);

                PdfPCell cRazonsocial = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Numero, _standardFont));
                cRazonsocial.Border = 0; // borde cero
                cRazonsocial.HorizontalAlignment = 0;
                cRazonsocial.PaddingTop = 2;
                cRazonsocial.BorderWidthTop = 1f;
                cRazonsocial.BorderWidthRight = 1f;
                cRazonsocial.Padding = 5;
                //  cRazonsocial.BorderWidthBottom = 1;
                Tabledoble.AddCell(cRazonsocial);


                PdfPCell cmasIGV = new PdfPCell(new Phrase("MAS IGV:", _standardFont));
                cmasIGV.Border = 0;
                cmasIGV.HorizontalAlignment = 0;
                cmasIGV.PaddingTop = 2;
                //  cmasIGV.BorderWidthTop = 1f;
                cmasIGV.BorderWidthLeft = 1f;
                cmasIGV.Padding = 5;
                cmasIGV.BorderWidthBottom = 1;
                Tabledoble.AddCell(cmasIGV);

                PdfPCell CellIGV = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.icluyeIGV, _standardFont));
                CellIGV.Border = 0; // borde cero
                CellIGV.HorizontalAlignment = 0;
                CellIGV.PaddingTop = 2;
                // CellIGV.BorderWidthTop = 1f;
                CellIGV.BorderWidthRight = 1f;
                CellIGV.Padding = 5;
                CellIGV.BorderWidthBottom = 1;
                Tabledoble.AddCell(CellIGV);



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

                foreach (EOrdenCompraDet ListaRep in ImprimriOC)
                {

                    PdfPCell cCodigo = new PdfPCell(new Phrase(ListaRep.Cantidad.ToString(), _standardFont));
                    cCodigo.Border = 0;
                    cCodigo.Padding = 2;
                    cCodigo.PaddingBottom = 2;
                    cCodigo.HorizontalAlignment = 1;
                    cCodigo.Border = Rectangle.LEFT_BORDER;
                    Tableitems.AddCell(cCodigo);

                    PdfPCell cNombreMat = new PdfPCell(new Phrase(ListaRep.Material.Unidad.Nombre, _standardFont));
                    cNombreMat.Border = 0;
                    cNombreMat.Padding = 2;
                    cNombreMat.PaddingBottom = 2;
                    cNombreMat.HorizontalAlignment = 1;
                    cNombreMat.Border = Rectangle.LEFT_BORDER;
                    Tableitems.AddCell(cNombreMat);

                    PdfPCell cCantidad = new PdfPCell(new Phrase(ListaRep.Material.Nombre + " - " + ListaRep.Material.Marca.Nombre, _standardFont));
                    cCantidad.Border = 0;
                    cCantidad.Padding = 2;
                    cCantidad.PaddingBottom = 2;
                    cCantidad.HorizontalAlignment = 0;
                    cCantidad.Border = Rectangle.LEFT_BORDER;
                    Tableitems.AddCell(cCantidad);

                    PdfPCell cPrecio = new PdfPCell(new Phrase(ListaRep.Precio.ToString(), _standardFont));
                    cPrecio.Border = 0;
                    cPrecio.Padding = 2;
                    cPrecio.PaddingBottom = 2;
                    cPrecio.HorizontalAlignment = 1;
                    cPrecio.Border = Rectangle.LEFT_BORDER;
                    Tableitems.AddCell(cPrecio);

                    PdfPCell cImporte = new PdfPCell(new Phrase((ListaRep.Precio * ListaRep.Cantidad).ToString(), _standardFont));
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

                PdfPCell cLetras = new PdfPCell(new Phrase("SON: " + Convertir.enletras(ImprimriOC[0].OrdenCompraCab.Total.ToString()) + " " + ImprimriOC[0].OrdenCompraCab.Moneda.Nombre, _standardFont));
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

                PdfPCell cTituloSub = new PdfPCell(new Phrase("Sub Total ", _standarTextoNegrita));
                cTituloSub.Border = 0;
                // cTituloSub.Colspan = 4;
                cTituloSub.HorizontalAlignment = 0; // 0 = izquierda, 1 = centro, 2 = derecha
                cTituloSub.BorderWidthLeft = 1f;
                cTituloSub.BorderWidthTop = 1f;
                cTituloSub.Padding = 2;
                TableValor.AddCell(cTituloSub);

                PdfPCell cCeldaSubtotal = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.SubTotal.ToString(), _standarTextoNegrita));
                cCeldaSubtotal.Border = 0;
                // cCeldaSubtotal.Colspan = 2
                cCeldaSubtotal.HorizontalAlignment = 2;
                cCeldaSubtotal.BorderWidthRight = 1f;
                cCeldaSubtotal.BorderWidthTop = 1f;
                cCeldaSubtotal.Padding = 2;
                TableValor.AddCell(cCeldaSubtotal);



                PdfPCell cTituloIgv = new PdfPCell(new Phrase("Total I.G.V (18%)", _standarTextoNegrita));
                cTituloIgv.Border = 0;
                // cTituloIgv.Colspan = 4;
                cTituloIgv.HorizontalAlignment = 0; // 0 = izquierda, 1 = centro, 2 = derecha
                cTituloIgv.BorderWidthLeft = 1f;
                cTituloIgv.Padding = 2;
                TableValor.AddCell(cTituloIgv);

                PdfPCell cCeldaIgv = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.IGV.ToString(), _standarTextoNegrita));
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

                PdfPCell cCeldaTotalDoc = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Total.ToString(), _standarTextoNegrita));
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

                PdfPCell cCeldaMoneda = new PdfPCell(new Phrase(ImprimriOC[0].OrdenCompraCab.Moneda.Nombre, _standarTextoNegrita));
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

                Response.ContentType = "application/pdf";

                // Set default file Name as current datetime 
                Response.AddHeader("content-disposition", "attachment; filename=Documento_" + "_OC_" + ImprimriOC[0].OrdenCompraCab.Serie + ".pdf");
                Response.Write(pdfDoc);

                Response.Flush();
                Response.End();
                System.IO.File.Delete(path);
                return View();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }

        }
        #endregion

        #region Reporte Movimiento
        public ActionResult ReporteMovimientoPDF(string Filltro, string FechaIncio, string FechaFin, int numPag, int allReg, int Cant)
        {
            Document pdfDoc = new Document(PageSize.A4, 5, 5, 20, 20);
            int empresa = Authentication.UserLogued.Empresa.Id;
            List<EMovimiento> ListaCompra = Gestion.ListaMovimiento(Filltro, FechaIncio, FechaFin, empresa, numPag, allReg, Cant);

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

                PdfPTable Tableitems = new PdfPTable(8);
                Tableitems.WidthPercentage = 100;
                Tableitems.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

                PdfPCell celCospan = new PdfPCell(new Phrase("REPORTE MOMIVMIENTO"));
                celCospan.Colspan = 8;
                celCospan.Border = 0;
                celCospan.HorizontalAlignment = 1;
                Tableitems.AddCell(celCospan);


                PdfPCell cEspacio = new PdfPCell(new Phrase("    "));
                cEspacio.Colspan = 8;
                cEspacio.Border = 0;
                cEspacio.HorizontalAlignment = 1;
                Tableitems.AddCell(cEspacio);

                Single[] celdas2 = new Single[] { 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F };
                Tableitems.SetWidths(celdas2);
                PdfPCell cTituloCant = new PdfPCell(new Phrase("Serie", _standardFont));
                cTituloCant.Border = 1;
                cTituloCant.BorderWidthBottom = 1;
                cTituloCant.HorizontalAlignment = 1;
                cTituloCant.Padding = 5;
                Tableitems.AddCell(cTituloCant);

                PdfPCell CTituloSerie = new PdfPCell(new Phrase("Tipo Mov", _standardFont));
                CTituloSerie.Border = 1;
                CTituloSerie.BorderWidthBottom = 1;
                CTituloSerie.HorizontalAlignment = 1;
                CTituloSerie.Padding = 5;
                Tableitems.AddCell(CTituloSerie);

                PdfPCell cTipoPago = new PdfPCell(new Phrase("Fecha Emi.", _standardFont));
                cTipoPago.Border = 1;
                cTipoPago.BorderWidthBottom = 1;
                cTipoPago.HorizontalAlignment = 1;
                cTipoPago.Padding = 5;
                Tableitems.AddCell(cTipoPago);

                PdfPCell cRegistro = new PdfPCell(new Phrase("Moneda", _standardFont));
                cRegistro.Border = 1;
                cRegistro.BorderWidthBottom = 1;
                cRegistro.HorizontalAlignment = 1;
                cRegistro.Padding = 5;
                Tableitems.AddCell(cRegistro);


                PdfPCell cFechaRegistro = new PdfPCell(new Phrase("Tipo Ope.", _standardFont));
                cFechaRegistro.Border = 1;
                cFechaRegistro.BorderWidthBottom = 1;
                cFechaRegistro.HorizontalAlignment = 1;
                cFechaRegistro.Padding = 5;
                Tableitems.AddCell(cFechaRegistro);


                PdfPCell cSubTotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                cSubTotal.Border = 1;
                cSubTotal.BorderWidthBottom = 1;
                cSubTotal.HorizontalAlignment = 1;
                cSubTotal.Padding = 5;
                Tableitems.AddCell(cSubTotal);

                PdfPCell cIGV = new PdfPCell(new Phrase("IGV", _standardFont));
                cIGV.Border = 1;
                cIGV.BorderWidthBottom = 1;
                cIGV.HorizontalAlignment = 1;
                cIGV.Padding = 5;
                Tableitems.AddCell(cIGV);

                PdfPCell cTitulo = new PdfPCell(new Phrase("Total", _standardFont));
                cTitulo.Border = 1;
                cTitulo.BorderWidthBottom = 1;
                cTitulo.HorizontalAlignment = 1;
                cTitulo.Padding = 5;
                Tableitems.AddCell(cTitulo);

                Font letrasDatosTabla = FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.NORMAL);

                double IGV = 0, SubTotal = 0, Total = 0;

                foreach (EMovimiento cuentaPago in ListaCompra)
                {


                    PdfPCell cdocumento = new PdfPCell(new Phrase(cuentaPago.Serie, letrasDatosTabla));
                    cdocumento.Border = 0;
                    cdocumento.Padding = 2;
                    cdocumento.PaddingBottom = 2;
                    cdocumento.HorizontalAlignment = 1;
                    Tableitems.AddCell(cdocumento);

                    PdfPCell cDocumento = new PdfPCell(new Phrase(cuentaPago.Text, letrasDatosTabla));
                    cDocumento.Border = 0;
                    cDocumento.Padding = 2;
                    cDocumento.PaddingBottom = 2;
                    cDocumento.HorizontalAlignment = 1;
                    Tableitems.AddCell(cDocumento);

                    PdfPCell cellTipoPago = new PdfPCell(new Phrase(cuentaPago.FechaEmison, letrasDatosTabla));
                    cellTipoPago.Border = 0;
                    cellTipoPago.Padding = 2;
                    cellTipoPago.PaddingBottom = 2;
                    cellTipoPago.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellTipoPago);

                    PdfPCell cellFechaRegistro = new PdfPCell(new Phrase(cuentaPago.Moneda.Nombre, letrasDatosTabla));
                    cellFechaRegistro.Border = 0;
                    cellFechaRegistro.Padding = 2;
                    cellFechaRegistro.PaddingBottom = 2;
                    cellFechaRegistro.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellFechaRegistro);


                    PdfPCell cellFechaPago = new PdfPCell(new Phrase(cuentaPago.Documento.Nombre, letrasDatosTabla));
                    cellFechaPago.Border = 0;
                    cellFechaPago.Padding = 2;
                    cellFechaPago.PaddingBottom = 2;
                    cellFechaPago.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellFechaPago);



                    PdfPCell cellFSubTotal = new PdfPCell(new Phrase(Math.Round(cuentaPago.SubTotal, 2).ToString(), letrasDatosTabla));
                    cellFSubTotal.Border = 0;
                    cellFSubTotal.Padding = 2;
                    cellFSubTotal.PaddingBottom = 2;
                    cellFSubTotal.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellFSubTotal);

                    PdfPCell cellIGV = new PdfPCell(new Phrase(Math.Round(cuentaPago.IGV, 2).ToString(), letrasDatosTabla));
                    cellIGV.Border = 0;
                    cellIGV.Padding = 2;
                    cellIGV.PaddingBottom = 2;
                    cellIGV.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellIGV);

                    PdfPCell cNroDocumento = new PdfPCell(new Phrase(Math.Round(cuentaPago.Total, 2).ToString(), letrasDatosTabla));
                    cNroDocumento.Border = 0;
                    cNroDocumento.Padding = 2;
                    cNroDocumento.PaddingBottom = 2;
                    cNroDocumento.HorizontalAlignment = 1;
                    Tableitems.AddCell(cNroDocumento);


                    Total += Convert.ToDouble(cuentaPago.Total);
                    IGV += Convert.ToDouble(cuentaPago.IGV);
                    SubTotal += Convert.ToDouble(cuentaPago.SubTotal);

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



                PdfPCell cTTotales = new PdfPCell(new Phrase("SubTotal:   " + string.Format("{0:n}", SubTotal), _standardFont));
                cTTotales.Border = 0;
                cTTotales.Colspan = 2;
                cTTotales.HorizontalAlignment = 2; // 0 = izquierda, 1 = centro, 2 = derecha
                TableValor.AddCell(cTTotales);

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
                Response.AddHeader("content-disposition", "attachment; filename=ReporteRegistroCompras.pdf");
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


        #region Reporte Movimiento Excel
        public ActionResult ReporteMovimientoExcel(string Filltro, string FechaIncio, string FechaFin, int numPag, int allReg, int Cant)
        {
            try
            {


                //Ruta de la plantilla
                FileInfo fTemplateFile = new FileInfo(Server.MapPath("/") + "Reporte/OC/Excel/" + "Movimiento.xlsx");

                var Usuario = Authentication.UserLogued.Usuario;
                int Empresa = Authentication.UserLogued.Empresa.Id;
                //Ruta del nuevo documento
                FileInfo fNewFile = new FileInfo(Server.MapPath("/") + "Reporte/OC/Excel/" + "Movimiento" + "_" + Usuario + ".xlsx");

                List<EMovimiento> ListaCompra = Gestion.ListaMovimiento(Filltro, FechaIncio, FechaFin, Empresa, numPag, allReg, Cant);
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
                double SubTotal = 0, IGV = 0, Total = 0;
                foreach (EMovimiento Detalle in ListaCompra)
                {
                    ws.Cells[iFilaDetIni + starRow, 1].Value = Detalle.Serie;
                    ws.Cells[iFilaDetIni + starRow, 2].Value = Detalle.Text;
                    ws.Cells[iFilaDetIni + starRow, 3].Value = Detalle.FechaEmison;
                    ws.Cells[iFilaDetIni + starRow, 4].Value = Detalle.Moneda.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 5].Value = Detalle.Documento.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 6].Value = Math.Round(Convert.ToDouble(Detalle.SubTotal), 2);
                    ws.Cells[iFilaDetIni + starRow, 7].Value = Math.Round(Convert.ToDouble(Detalle.IGV), 2);
                    ws.Cells[iFilaDetIni + starRow, 8].Value = Math.Round(Convert.ToDouble(Detalle.Total), 2);
                    SubTotal += Convert.ToDouble(Detalle.SubTotal);
                    IGV += Convert.ToDouble(Detalle.IGV);
                    Total += Convert.ToDouble(Detalle.Total);
                    starRow++;

                }
                int Count = starRow + 1;

                ws.Cells[iFilaDetIni + (Count - 1), 5].Value = "Totales";
                ws.Cells[iFilaDetIni + (Count - 1), 6].Value = Math.Round(Convert.ToDouble(SubTotal), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 7].Value = Math.Round(Convert.ToDouble(IGV), 2);
                ws.Cells[iFilaDetIni + (Count - 1), 8].Value = Math.Round(Convert.ToDouble(Total), 2);


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
                Response.AddHeader("content-disposition", "attachment;  filename=" + "ReporteMovimiento" + "_" + Usuario + ".xlsx");

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


        #region Reporte Movimiento
        public ActionResult ReporteStockPDF(string Filtro, int numPag, int allReg, int cantFill)
        {
            Document pdfDoc = new Document(PageSize.A4, 5, 5, 20, 20);
            int empresa = Authentication.UserLogued.Empresa.Id;
            int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
            List<EMovimientoDet> ListaCompra = Gestion.ListaStock(Filtro, empresa, sucursal, numPag, allReg, cantFill);

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

                PdfPTable Tableitems = new PdfPTable(10);
                Tableitems.WidthPercentage = 100;
                Tableitems.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

                PdfPCell celCospan = new PdfPCell(new Phrase("REPORTE STOCK"));
                celCospan.Colspan = 10;
                celCospan.Border = 0;
                celCospan.HorizontalAlignment = 1;
                Tableitems.AddCell(celCospan);


                PdfPCell cEspacio = new PdfPCell(new Phrase("    "));
                cEspacio.Colspan = 10;
                cEspacio.Border = 0;
                cEspacio.HorizontalAlignment = 1;
                Tableitems.AddCell(cEspacio);

                Single[] celdas2 = new Single[] { 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F };
                Tableitems.SetWidths(celdas2);
                PdfPCell cTituloCant = new PdfPCell(new Phrase("Item", _standardFont));
                cTituloCant.Border = 1;
                cTituloCant.BorderWidthBottom = 1;
                cTituloCant.HorizontalAlignment = 1;
                cTituloCant.Padding = 5;
                Tableitems.AddCell(cTituloCant);

                PdfPCell CTituloSerie = new PdfPCell(new Phrase("Codigo ", _standardFont));
                CTituloSerie.Border = 1;
                CTituloSerie.BorderWidthBottom = 1;
                CTituloSerie.HorizontalAlignment = 1;
                CTituloSerie.Padding = 5;
                Tableitems.AddCell(CTituloSerie);

                PdfPCell cTipoPago = new PdfPCell(new Phrase("Material", _standardFont));
                cTipoPago.Border = 1;
                cTipoPago.BorderWidthBottom = 1;
                cTipoPago.HorizontalAlignment = 1;
                cTipoPago.Padding = 5;
                Tableitems.AddCell(cTipoPago);

                PdfPCell cRegistro = new PdfPCell(new Phrase("Marca", _standardFont));
                cRegistro.Border = 1;
                cRegistro.BorderWidthBottom = 1;
                cRegistro.HorizontalAlignment = 1;
                cRegistro.Padding = 5;
                Tableitems.AddCell(cRegistro);


                PdfPCell cFechaRegistro = new PdfPCell(new Phrase("Unidad", _standardFont));
                cFechaRegistro.Border = 1;
                cFechaRegistro.BorderWidthBottom = 1;
                cFechaRegistro.HorizontalAlignment = 1;
                cFechaRegistro.Padding = 5;
                Tableitems.AddCell(cFechaRegistro);


                PdfPCell cSubTotal = new PdfPCell(new Phrase("Categoria", _standardFont));
                cSubTotal.Border = 1;
                cSubTotal.BorderWidthBottom = 1;
                cSubTotal.HorizontalAlignment = 1;
                cSubTotal.Padding = 5;
                Tableitems.AddCell(cSubTotal);

                PdfPCell cIGV = new PdfPCell(new Phrase("Almacen", _standardFont));
                cIGV.Border = 1;
                cIGV.BorderWidthBottom = 1;
                cIGV.HorizontalAlignment = 1;
                cIGV.Padding = 5;
                Tableitems.AddCell(cIGV);

                PdfPCell cTitulo = new PdfPCell(new Phrase("Stock", _standardFont));
                cTitulo.Border = 1;
                cTitulo.BorderWidthBottom = 1;
                cTitulo.HorizontalAlignment = 1;
                cTitulo.Padding = 5;
                Tableitems.AddCell(cTitulo);

                PdfPCell cStockmin = new PdfPCell(new Phrase("Stock Min", _standardFont));
                cStockmin.Border = 1;
                cStockmin.BorderWidthBottom = 1;
                cStockmin.HorizontalAlignment = 1;
                cStockmin.Padding = 5;
                Tableitems.AddCell(cStockmin);

                PdfPCell cNotifi = new PdfPCell(new Phrase("Notificacion", _standardFont));
                cNotifi.Border = 1;
                cNotifi.BorderWidthBottom = 1;
                cNotifi.HorizontalAlignment = 1;
                cNotifi.Padding = 5;
                Tableitems.AddCell(cNotifi);

                Font letrasDatosTabla = FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.NORMAL);

              

                foreach (EMovimientoDet cuentaPago in ListaCompra)
                {


                    PdfPCell cdocumento = new PdfPCell(new Phrase(cuentaPago.Item.ToString(), letrasDatosTabla));
                    cdocumento.Border = 0;
                    cdocumento.Padding = 2;
                    cdocumento.PaddingBottom = 2;
                    cdocumento.HorizontalAlignment = 1;
                    Tableitems.AddCell(cdocumento);

                    PdfPCell cDocumento = new PdfPCell(new Phrase(cuentaPago.Material.Codigo, letrasDatosTabla));
                    cDocumento.Border = 0;
                    cDocumento.Padding = 2;
                    cDocumento.PaddingBottom = 2;
                    cDocumento.HorizontalAlignment = 1;
                    Tableitems.AddCell(cDocumento);

                    PdfPCell cellTipoPago = new PdfPCell(new Phrase(cuentaPago.Material.Nombre, letrasDatosTabla));
                    cellTipoPago.Border = 0;
                    cellTipoPago.Padding = 2;
                    cellTipoPago.PaddingBottom = 2;
                    cellTipoPago.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellTipoPago);

                    PdfPCell cellFechaRegistro = new PdfPCell(new Phrase(cuentaPago.Material.Marca.Nombre, letrasDatosTabla));
                    cellFechaRegistro.Border = 0;
                    cellFechaRegistro.Padding = 2;
                    cellFechaRegistro.PaddingBottom = 2;
                    cellFechaRegistro.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellFechaRegistro);


                    PdfPCell cellFechaPago = new PdfPCell(new Phrase(cuentaPago.Material.Unidad.Nombre, letrasDatosTabla));
                    cellFechaPago.Border = 0;
                    cellFechaPago.Padding = 2;
                    cellFechaPago.PaddingBottom = 2;
                    cellFechaPago.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellFechaPago);



                    PdfPCell cellFSubTotal = new PdfPCell(new Phrase(cuentaPago.Material.Categoria.Nombre, letrasDatosTabla));
                    cellFSubTotal.Border = 0;
                    cellFSubTotal.Padding = 2;
                    cellFSubTotal.PaddingBottom = 2;
                    cellFSubTotal.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellFSubTotal);

                    PdfPCell cellIGV = new PdfPCell(new Phrase(cuentaPago.Almacen.Nombre, letrasDatosTabla));
                    cellIGV.Border = 0;
                    cellIGV.Padding = 2;
                    cellIGV.PaddingBottom = 2;
                    cellIGV.HorizontalAlignment = 1;
                    Tableitems.AddCell(cellIGV);

                    PdfPCell cNroDocumento = new PdfPCell(new Phrase(cuentaPago.Cantidad.ToString(), letrasDatosTabla));
                    cNroDocumento.Border = 0;
                    cNroDocumento.Padding = 2;
                    cNroDocumento.PaddingBottom = 2;
                    cNroDocumento.HorizontalAlignment = 1;
                    Tableitems.AddCell(cNroDocumento);

                    PdfPCell Cnum = new PdfPCell(new Phrase(cuentaPago.Num.ToString(), letrasDatosTabla));
                    Cnum.Border = 0;
                    Cnum.Padding = 2;
                    Cnum.PaddingBottom = 2;
                    Cnum.HorizontalAlignment = 1;
                    Tableitems.AddCell(Cnum);


                    PdfPCell cTexto = new PdfPCell(new Phrase(cuentaPago.Text.ToString(), letrasDatosTabla));
                    cTexto.Border = 0;
                    cTexto.Padding = 2;
                    cTexto.PaddingBottom = 2;
                    cTexto.HorizontalAlignment = 1;
                    Tableitems.AddCell(cTexto);


                }

                PdfPCell cCeldaDetalle = new PdfPCell(Tableitems);
                cCeldaDetalle.Border = 1;
                cCeldaDetalle.BorderWidthBottom = 1;
                cCeldaDetalle.HorizontalAlignment = 1;
                cCeldaDetalle.Padding = 5;
                tblPrueba.AddCell(cCeldaDetalle);






                pdfDoc.Add(tblPrueba);

                // Close your PDF 
                pdfDoc.Close();



                Response.ContentType = "application/pdf";

                //Set default file Name as current datetime 
                Response.AddHeader("content-disposition", "attachment; filename=ReporteStock.pdf");
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

        #region Reporte Stock Excel
        public ActionResult ReporteStockExcel(string Filtro, int numPag, int allReg, int cantFill)
        {
            try
            {


                //Ruta de la plantilla
                FileInfo fTemplateFile = new FileInfo(Server.MapPath("/") + "Reporte/OC/Excel/" + "stock.xlsx");

                var Usuario = Authentication.UserLogued.Usuario;
                int Empresa = Authentication.UserLogued.Empresa.Id;
                int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                //Ruta del nuevo documento
                FileInfo fNewFile = new FileInfo(Server.MapPath("/") + "Reporte/OC/Excel/" + "stock" + "_" + Usuario + ".xlsx");

                List<EMovimientoDet> ListaCompra = Gestion.ListaStock(Filtro, Empresa, sucursal, numPag, allReg, cantFill);
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
                foreach (EMovimientoDet Detalle in ListaCompra)
                {
                    ws.Cells[iFilaDetIni + starRow, 1].Value = Convert.ToInt32(Detalle.Item);
                    ws.Cells[iFilaDetIni + starRow, 2].Value = Detalle.Material.Codigo;
                    ws.Cells[iFilaDetIni + starRow, 3].Value = Detalle.Material.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 4].Value = Detalle.Material.Marca.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 5].Value = Detalle.Material.Unidad.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 6].Value = Detalle.Material.Categoria.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 7].Value = Detalle.Almacen.Nombre;
                    ws.Cells[iFilaDetIni + starRow, 8].Value = Convert.ToInt32(Detalle.Cantidad);
                    ws.Cells[iFilaDetIni + starRow, 9].Value = Convert.ToInt32(Detalle.Num);
                    ws.Cells[iFilaDetIni + starRow, 10].Value = Detalle.Text;

                    starRow++;

                }
                int Count = starRow + 1;




                string modelRange = "A" + System.Convert.ToString(iFilaDetIni) + ":J" + (iFilaDetIni + Count - 1).ToString();
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
                Response.AddHeader("content-disposition", "attachment;  filename=" + "ReporteMovimiento" + "_" + Usuario + ".xlsx");

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
        //Reporte Kardex
        public ActionResult ReporteKardexExcel(string FechaIncio, string FechaFin, int idAlm, int idMat, int numPag, int allReg, int Cant)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                int Empresa = Authentication.UserLogued.Empresa.Id;
                int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                //Ruta de la plantilla
                FileInfo fTemplateFile = new FileInfo(Server.MapPath("/") + "Reporte/Inventario/Excel/" + "Kardex.xlsx");

                //Ruta del nuevo documento
                FileInfo fNewFile = new FileInfo(Server.MapPath("/") + "Reporte/Inventario/Excel/" + "Kardex" + "_" + Usuario + ".xlsx");
                ExcelPackage EP = new ExcelPackage(fNewFile, fTemplateFile);

                List<EMovimiento> ListaCompra = Gestion.ListaKardex(Empresa, sucursal, FechaIncio, FechaFin, idAlm, idMat, numPag, allReg, Cant);

                ExcelWorksheet ws = EP.Workbook.Worksheets[1];
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

                foreach (EMovimiento Detalle in ListaCompra)
                {
                    ws.Cells[iFilaDetIni + starRow, 1].Value = Detalle.FechaEmison;
                    ws.Cells[iFilaDetIni + starRow, 2].Value = Detalle.Serie;
                    ws.Cells[iFilaDetIni + starRow, 3].Value = Detalle.Cantidad;
                    ws.Cells[iFilaDetIni + starRow, 4].Value = Math.Round(Convert.ToDouble(Detalle.precioEntrada), 2);
                    ws.Cells[iFilaDetIni + starRow, 5].Value = Math.Round(Convert.ToDouble(Detalle.costoEntrada), 2);
                    ws.Cells[iFilaDetIni + starRow, 6].Value = Math.Round(Convert.ToDouble(Detalle.cantidadSalida), 2);
                    ws.Cells[iFilaDetIni + starRow, 7].Value = Math.Round(Convert.ToDouble(Detalle.precioSalida), 2);
                    ws.Cells[iFilaDetIni + starRow, 8].Value = Math.Round(Convert.ToDouble(Detalle.costoSalida), 2);
                    ws.Cells[iFilaDetIni + starRow, 9].Value = Detalle.totalStock;
                    starRow++;

                }
                int Count = starRow + 1;


                string modelRange = "A" + System.Convert.ToString(iFilaDetIni) + ":H" + (iFilaDetIni + Count - 1).ToString();
                var modelTable = ws.Cells[modelRange];
                modelTable.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                //Guardando Archivo...
                EP.Save();

                ExcelPackage pck = new ExcelPackage(fNewFile, false);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + "Kardex" + "_" + Usuario + ".xlsx");

                MemoryStream memoryStream = new MemoryStream();
                pck.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.End();
                pck.Dispose();
                return View();
                //System.IO.File.Delete(fNewFile.FullName);

            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public ActionResult ReporteKardex(string FechaIncio, string FechaFin, int idAlm, int idMat, int numPagina, int allReg, int Cant)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                int Empresa = Authentication.UserLogued.Empresa.Id;
                int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                List<EKardex> DTCabecera = new List<EKardex>();
                List<EMovimiento> ListaMovimiento = new List<EMovimiento>();

                //Ruta de la plantilla
                FileInfo fTemplateFile = new FileInfo(Server.MapPath("/") + "Reporte/Inventario/Excel/" + "Kardex_Valorizado.xlsx");
                //Ruta del nuevo documento
                FileInfo fNewFile = new FileInfo(Server.MapPath("/") + "Reporte/Inventario/Excel/" + "Kardex_Valorizado" + "_" + Usuario + ".xlsx");
                ExcelPackage pck = new ExcelPackage(fNewFile, fTemplateFile);

                int i = 1;
                ExcelWorksheet ws;
                int iFilaDetIni;
                int Cabecera;
                int subCabecera;
                int CabeceraReg;
                int numFilasExcel = 0;
                int numPag = 0;

                Cabecera = 1;
                subCabecera = 14;
                iFilaDetIni = 17;
                CabeceraReg = 3;

                ws = pck.Workbook.Worksheets[i];
                /*  =====================================================================================|
                    DATOS DE CABECERA PARA EL KARDEX VALORIZADO:
                    =====================================================================================|*/

                DTCabecera = General.ListaDatosCabecera(Empresa);
                List<EMovimiento> ListaCompra = Gestion.ListaKardex(Empresa, sucursal, FechaIncio, FechaFin, idAlm, idMat, numPagina, 1, Cant);

                if (ListaCompra.Count == 0)
                {
                    numPag = 1;
                }
                else
                {
                    numPag = int.Parse(ListaCompra[0].TotalPagina.ToString());
                }

                int numFilasExcelvalor = 0;
                int x = 1;
                int row = 0;

                double TotalCantidadEnt = 0;
                double TotalPrecioEnt = 0;
                double TotalCantidadSal = 0;
                double TotalPrecioSal = 0;

                //cabecera



                int idAlmacen = 0;
                int idMaterial = 0;
                int idAlmacenT = 0;
                int idMaterialT = 0;

                ListaMovimiento = Gestion.ListaKardex(Empresa, sucursal, FechaIncio, FechaFin, idAlm, idMat, x, 1, Cant);
                int row1 = 0;
                idAlmacen = ListaMovimiento[0].Almacen.IdAlmacen;
                idMaterial = ListaMovimiento[0].idMaterial;

                idAlmacenT = ListaMovimiento[0].Almacen.IdAlmacen;
                idMaterialT = ListaMovimiento[0].idMaterial;

                CabeceraData(DTCabecera, row, numFilasExcel, Cabecera, ws, CabeceraReg, subCabecera, ListaMovimiento[0].Almacen.Nombre, ListaMovimiento[0].material, ListaMovimiento[0].sCodigoMat, ListaMovimiento[0].sCodigoUnd, FechaIncio);

                foreach (EMovimiento lista in ListaMovimiento)
                {
                    if (idAlmacen != lista.Almacen.IdAlmacen || idMaterial != lista.idMaterial)
                    {
                        //int iCount = row1 + 1 + numFilasExcelvalor;
                        int iCount = Cant + numFilasExcelvalor;
                        // ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 5).Value = "TOTALES"
                        ws.Cells[iFilaDetIni + (iCount + 1), 5].Value = "TOTALES";
                        //TOTALES ENTRADAS:
                        //ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 6).Value = TotalCantidadEnt
                        //ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 8).Value = TotalPrecioEnt

                        ws.Cells[iFilaDetIni + (iCount + 1), 6].Value = TotalCantidadEnt;
                        ws.Cells[iFilaDetIni + (iCount + 1), 8].Value = TotalPrecioEnt;
                        //TOTALES SALIDAS:
                        //ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 9).Value = TotalCantidadSal
                        //ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 11).Value = TotalPrecioSal

                        ws.Cells[iFilaDetIni + (iCount + 1), 9].Value = TotalCantidadSal;
                        ws.Cells[iFilaDetIni + (iCount + 1), 11].Value = TotalPrecioSal;

                        string modelRange = "A" + (iFilaDetIni + numFilasExcelvalor) + ":N" + (iFilaDetIni + numFilasExcelvalor + Cant - 1).ToString();
                        var modelTable = ws.Cells[modelRange];
                        modelTable.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        modelTable.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        modelTable.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        modelTable.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                        idAlmacen = lista.Almacen.IdAlmacen;
                        idMaterial = lista.idMaterial;
                        numFilasExcel += 43;
                        numFilasExcelvalor += 43;
                        row1 = 0;
                        TotalCantidadEnt = 0;
                        TotalPrecioEnt = 0;
                        TotalCantidadSal = 0;
                        TotalPrecioSal = 0;

                        CabeceraData(DTCabecera, row, numFilasExcel, Cabecera, ws, CabeceraReg, subCabecera, lista.Almacen.Nombre, lista.material, lista.sCodigoMat, lista.sCodigoUnd, FechaIncio);
                    }
                    if (row1 == Cant)
                    {

                        //int iCount = row1 + 1 + numFilasExcelvalor;
                        int iCount = Cant + numFilasExcelvalor;
                        // ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 5).Value = "TOTALES"
                        ws.Cells[iFilaDetIni + (iCount + 1), 5].Value = "TOTALES";
                        //TOTALES ENTRADAS:
                        //ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 6).Value = TotalCantidadEnt
                        //ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 8).Value = TotalPrecioEnt

                        ws.Cells[iFilaDetIni + (iCount + 1), 6].Value = TotalCantidadEnt;
                        ws.Cells[iFilaDetIni + (iCount + 1), 8].Value = TotalPrecioEnt;
                        //TOTALES SALIDAS:
                        //ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 9).Value = TotalCantidadSal
                        //ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 11).Value = TotalPrecioSal

                        ws.Cells[iFilaDetIni + (iCount + 1), 9].Value = TotalCantidadSal;
                        ws.Cells[iFilaDetIni + (iCount + 1), 11].Value = TotalPrecioSal;

                        string modelRange = "A" + (iFilaDetIni + numFilasExcelvalor) + ":N" + (iFilaDetIni + numFilasExcelvalor + Cant - 1).ToString();
                        var modelTable = ws.Cells[modelRange];
                        modelTable.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        modelTable.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        modelTable.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        modelTable.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                        idAlmacen = lista.Almacen.IdAlmacen;
                        numFilasExcel += 43;
                        numFilasExcelvalor += 43;
                        row1 = 0;
                        TotalCantidadEnt = 0;
                        TotalPrecioEnt = 0;
                        TotalCantidadSal = 0;
                        TotalPrecioSal = 0;
                        CabeceraData(DTCabecera, row, numFilasExcel, Cabecera, ws, CabeceraReg, subCabecera, lista.Almacen.Nombre, lista.material, lista.sCodigoMat, lista.sCodigoUnd, FechaIncio);
                    }


                    ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 1].Value = lista.FechaEmison;
                    //TIPO (TABLA 10):
                    ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 2].Value = lista.Documento.Nombre;
                    //SERIE:
                    ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 3].Value = lista.Serie;
                    //NÚMERO:
                    ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 4].Value = lista.Numero;
                    //TIPO DE OPERACIÓN (TABLA 12):
                    ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 5].Value = lista.TipoOperacion;

                    if (lista.TipoMovimiento == "1")
                    {
                        // ---------- ENTRADAS ----------------------------------------|
                        // CANTIDAD:
                        ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 6].Value = lista.Cantidad;
                        TotalCantidadEnt = (TotalCantidadEnt + lista.Cantidad);
                        // COSTO UNITARIO:
                        ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 7].Value = lista.precioEntrada;
                        // COSTO TOTAL:
                        ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 8].Value = lista.costoEntrada;
                        TotalPrecioEnt = (TotalPrecioEnt + lista.costoEntrada);
                        // ------------------------------------------------------------|
                        ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 9].Value = "";
                        ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 10].Value = "";
                        ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 11].Value = "";
                    }

                    else
                    {
                        ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 6].Value = "";
                        ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 7].Value = "";
                        ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 8].Value = "";
                        // ---------- SALIDAS -----------------------------------------|
                        // CANTIDAD:
                        ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 9].Value = lista.cantidadSalida;
                        TotalCantidadSal = (TotalCantidadSal + lista.cantidadSalida);
                        //COSTO UNITARIO:
                        ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 10].Value = lista.precioSalida;
                        //COSTO TOTAL:
                        ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 11].Value = lista.costoSalida;
                        TotalPrecioSal = (TotalPrecioSal + lista.costoSalida);
                    }

                    // ---------- SALDO FINAL -------------------------------------|
                    //CANTIDAD:
                    ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 12].Value = lista.totalStock;
                    //COSTO UNITARIO:
                    ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 13].Value = lista.PrecioUnitario;
                    //COSTO TOTAL:
                    ws.Cells[iFilaDetIni + row1 + numFilasExcelvalor, 14].Value = lista.TotalPrecio;
                    //------------------------------------------------------------|

                    //TOTALES:


                    if (x == ListaMovimiento.Count)
                    {
                        // ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 5).Value = "TOTALES"
                        ws.Cells[iFilaDetIni + (numFilasExcelvalor + Cant + 1), 5].Value = "TOTALES";
                        //TOTALES ENTRADAS:
                        //ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 6).Value = TotalCantidadEnt
                        //ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 8).Value = TotalPrecioEnt

                        ws.Cells[iFilaDetIni + (numFilasExcelvalor + Cant + 1), 6].Value = TotalCantidadEnt;
                        ws.Cells[iFilaDetIni + (numFilasExcelvalor + Cant + 1), 8].Value = TotalPrecioEnt;
                        //TOTALES SALIDAS:
                        //ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 9).Value = TotalCantidadSal
                        //ws.Cells(iFilaDetIni + row + numFilasExcelvalor, 11).Value = TotalPrecioSal

                        ws.Cells[iFilaDetIni + (numFilasExcelvalor + Cant + 1), 9].Value = TotalCantidadSal;
                        ws.Cells[iFilaDetIni + (numFilasExcelvalor + Cant + 1), 11].Value = TotalPrecioSal;


                        string modelRange = "A" + (iFilaDetIni + numFilasExcelvalor) + ":N" + (iFilaDetIni + numFilasExcelvalor + Cant - 1).ToString();
                        var modelTable = ws.Cells[modelRange];
                        modelTable.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        modelTable.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        modelTable.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        modelTable.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


                    }



                    row1++;
                    x += 1;
                }

                //numFilasExcel += 43;
                //if (DTCabecera.Count > 0)
                //{
                //    if (ListaMovimiento.Count>0)
                //    {
                //        string modelRange = "A" + (iFilaDetIni + numFilasExcelvalor) + ":N" + (iFilaDetIni + numFilasExcelvalor + ListaMovimiento.Count - 1).ToString();
                //        var modelTable = ws.Cells[modelRange];
                //        modelTable.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                //        modelTable.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                //        modelTable.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                //        modelTable.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                //    }                            
                //}
                //numFilasExcelvalor += 43;
                //x += 1;



                pck.Save();

                pck.Dispose();

                ExcelPackage pck1 = new ExcelPackage(fNewFile, false);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + "Kardex_Valorizado" + "_" + Usuario + ".xlsx");

                MemoryStream memoryStream = new MemoryStream();
                pck1.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.End();
                pck1.Dispose();
                return View();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void CabeceraData(List<EKardex> DTCabecera, int row, int numFilasExcel, int Cabecera, ExcelWorksheet ws, int CabeceraReg, int subCabecera, string almacen, string producto, string sCodMaterial, string sCodUnidad, string FechaIncio)
        {
            foreach (EKardex obj in DTCabecera)
            {
                int iCount1 = row + 1 + numFilasExcel;

                ws.Cells["A" + (Cabecera + row + numFilasExcel) + ":K" + (Cabecera + row + numFilasExcel)].Merge = true;
                ws.Cells[Cabecera + row + numFilasExcel, 1].Value = obj.Formato;
                ws.Cells[Cabecera + row + numFilasExcel, 1].Style.Font.Size = 11;
                ws.Cells[Cabecera + row + numFilasExcel, 1].Style.Font.Bold = true;

                ws.Cells["A" + (Cabecera + (iCount1 + 1) + row) + ":E" + (Cabecera + (iCount1 + 1) + row)].Merge = true;
                ws.Cells[Cabecera + (iCount1 + 1) + row, 1].Value = obj.Periodo;
                ws.Cells[Cabecera + (iCount1 + 1) + row, 1].Style.Font.Size = 11;
                ws.Cells[Cabecera + (iCount1 + 1) + row, 1].Style.Font.Bold = true;

                ws.Cells["A" + (Cabecera + (iCount1 + 2) + row) + ":E" + (Cabecera + (iCount1 + 2) + row)].Merge = true;
                ws.Cells[Cabecera + (iCount1 + 2) + row, 1].Value = obj.TituloRuc;
                ws.Cells[Cabecera + (iCount1 + 2) + row, 1].Style.Font.Size = 11;
                ws.Cells[Cabecera + (iCount1 + 2) + row, 1].Style.Font.Bold = true;

                ws.Cells["A" + (Cabecera + (iCount1 + 3) + row) + ":E" + (Cabecera + (iCount1 + 3) + row)].Merge = true;
                ws.Cells["A" + (Cabecera + (iCount1 + 3) + row) + ":E" + (Cabecera + (iCount1 + 3) + row)].Style.WrapText = true;
                ws.Cells[Cabecera + (iCount1 + 3) + row, 1].Value = obj.Nombre;
                ws.Cells[Cabecera + (iCount1 + 3) + row, 1].Style.Font.Size = 11;
                ws.Cells[Cabecera + (iCount1 + 3) + row, 1].Style.Font.Bold = true;

                ws.Cells["A" + (Cabecera + (iCount1 + 4) + row) + ":E" + (Cabecera + (iCount1 + 4) + row)].Merge = true;
                ws.Cells[Cabecera + (iCount1 + 4) + row, 1].Value = obj.Establecimiento;
                ws.Cells[Cabecera + (iCount1 + 4) + row, 1].Style.Font.Size = 11;
                ws.Cells[Cabecera + (iCount1 + 4) + row, 1].Style.Font.Bold = true;

                ws.Cells["A" + (Cabecera + (iCount1 + 5) + row) + ":E" + (Cabecera + (iCount1 + 5) + row)].Merge = true;
                ws.Cells[Cabecera + (iCount1 + 5) + row, 1].Value = obj.Codigo;
                ws.Cells[Cabecera + (iCount1 + 5) + row, 1].Style.Font.Size = 11;
                ws.Cells[Cabecera + (iCount1 + 5) + row, 1].Style.Font.Bold = true;

                ws.Cells["A" + (Cabecera + (iCount1 + 6) + row) + ":E" + (Cabecera + (iCount1 + 6) + row)].Merge = true;
                ws.Cells[Cabecera + (iCount1 + 6) + row, 1].Value = obj.TipoTabla5;
                ws.Cells[Cabecera + (iCount1 + 6) + row, 1].Style.Font.Size = 11;
                ws.Cells[Cabecera + (iCount1 + 6) + row, 1].Style.Font.Bold = true;

                ws.Cells["A" + (Cabecera + (iCount1 + 7) + row) + ":E" + (Cabecera + (iCount1 + 7) + row)].Merge = true;
                ws.Cells[Cabecera + (iCount1 + 7) + row, 1].Value = obj.Descripcion;
                ws.Cells[Cabecera + (iCount1 + 7) + row, 1].Style.Font.Size = 11;
                ws.Cells[Cabecera + (iCount1 + 7) + row, 1].Style.Font.Bold = true;

                ws.Cells["A" + (Cabecera + (iCount1 + 8) + row) + ":E" + (Cabecera + (iCount1 + 8) + row)].Merge = true;
                ws.Cells[Cabecera + (iCount1 + 8) + row, 1].Value = obj.CodigoUnidad;
                ws.Cells[Cabecera + (iCount1 + 8) + row, 1].Style.Font.Size = 11;
                ws.Cells[Cabecera + (iCount1 + 8) + row, 1].Style.Font.Bold = true;

                ws.Cells["A" + (Cabecera + (iCount1 + 9) + row) + ":E" + (Cabecera + (iCount1 + 9) + row)].Merge = true;
                ws.Cells[Cabecera + (iCount1 + 9) + row, 1].Value = obj.MetodoEvaluacionT;
                ws.Cells[Cabecera + (iCount1 + 9) + row, 1].Style.Font.Size = 11;
                ws.Cells[Cabecera + (iCount1 + 9) + row, 1].Style.Font.Bold = true;

                //Cabecera 2
                //create variable para saltar linea
                int iCount2 = row + 1 + numFilasExcel;

                ws.Cells["F" + (CabeceraReg + row) + ":N" + (CabeceraReg + row)].Merge = true;
                ws.Cells["F" + (CabeceraReg + row) + ":F" + (CabeceraReg + row)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                ws.Cells[CabeceraReg + row, 6].Value = Convert.ToDateTime(FechaIncio).ToString("MMMM").ToUpper() + " " + Convert.ToDateTime(FechaIncio).Year;
                ws.Cells[CabeceraReg + row, 6].Style.Font.Size = 11;

                //RUC:
                ws.Cells["F" + (CabeceraReg + (iCount2) + row) + ":N" + (CabeceraReg + (iCount2) + row)].Merge = true;
                ws.Cells["F" + (CabeceraReg + (iCount2) + row) + ":N" + (CabeceraReg + (iCount2) + row)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                ws.Cells[CabeceraReg + (iCount2) + row, 6].Value = obj.RUC;
                ws.Cells[CabeceraReg + (iCount2) + row, 6].Style.Font.Size = 11;

                // APELLIDOS Y NOMBRES, DENOMINACIÓN O RAZÓN SOCIAL:
                ws.Cells["F" + (CabeceraReg + (iCount2 + 1) + row) + ":N" + (CabeceraReg + (iCount2 + 1) + row)].Merge = true;
                ws.Cells["F" + (CabeceraReg + (iCount2 + 1) + row) + ":N" + (CabeceraReg + (iCount2 + 1) + row)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                ws.Cells[CabeceraReg + (iCount2 + 1) + row, 6].Value = obj.RazonSocial;
                ws.Cells[CabeceraReg + (iCount2 + 1) + row, 6].Style.Font.Size = 11;

                //ESTABLECIMIENTO (1):
                ws.Cells["F" + (CabeceraReg + (iCount2 + 2) + row) + ":N" + (CabeceraReg + (iCount2 + 2) + row)].Merge = true;
                ws.Cells["F" + (CabeceraReg + (iCount2 + 2) + row) + ":N" + (CabeceraReg + (iCount2 + 2) + row)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                ws.Cells[CabeceraReg + (iCount2 + 2) + row, 6].Value = almacen;
                ws.Cells[CabeceraReg + (iCount2 + 2) + row, 6].Style.Font.Size = 11;

                //CÓDIGO DE LA EXISTENCIA::
                ws.Cells["F" + (CabeceraReg + (iCount2 + 3) + row) + ":N" + (CabeceraReg + (iCount1 + 3) + row)].Merge = true;
                ws.Cells["F" + (CabeceraReg + (iCount2 + 3) + row) + ":N" + (CabeceraReg + (iCount1 + 3) + row)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                ws.Cells[CabeceraReg + (iCount2 + 3) + row, 6].Value = sCodMaterial;
                ws.Cells[CabeceraReg + (iCount2 + 3) + row, 6].Style.Font.Size = 11;

                //TIPO (TABLA 5):
                ws.Cells["F" + (CabeceraReg + (iCount2 + 4) + row) + ":N" + (CabeceraReg + (iCount2 + 4) + row)].Merge = true;
                ws.Cells["F" + (CabeceraReg + (iCount2 + 4) + row) + ":N" + (CabeceraReg + (iCount2 + 4) + row)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                ws.Cells[CabeceraReg + (iCount2 + 4) + row, 6].Value = obj.valorTipoTabla5;
                ws.Cells[CabeceraReg + (iCount2 + 4) + row, 6].Style.Font.Size = 11;

                // DESCRIPCIÓN:
                ws.Cells["F" + (CabeceraReg + (iCount2 + 5) + row) + ":N" + (CabeceraReg + (iCount2 + 5) + row)].Merge = true;
                ws.Cells["F" + (CabeceraReg + (iCount2 + 5) + row) + ":N" + (CabeceraReg + (iCount2 + 5) + row)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                ws.Cells[CabeceraReg + (iCount2 + 5) + row, 6].Value = producto;
                ws.Cells[CabeceraReg + (iCount2 + 5) + row, 6].Style.Font.Size = 11;

                //CÓDIGO DE LA UNIDAD DE MEDIDA (TABLA 6):
                ws.Cells["F" + (CabeceraReg + (iCount2 + 6) + row) + ":N" + (CabeceraReg + (iCount2 + 6) + row)].Merge = true;
                ws.Cells["F" + (CabeceraReg + (iCount2 + 6) + row) + ":N" + (CabeceraReg + (iCount2 + 6) + row)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                ws.Cells[CabeceraReg + (iCount2 + 6) + row, 6].Value = sCodUnidad;
                ws.Cells[CabeceraReg + (iCount2 + 6) + row, 6].Style.Font.Size = 11;

                //MÉTODO DE VALUACIÓN:
                ws.Cells["F" + (CabeceraReg + (iCount2 + 7) + row) + ":N" + (CabeceraReg + (iCount2 + 7) + row)].Merge = true;
                ws.Cells["F" + (CabeceraReg + (iCount2 + 7) + row) + ":N" + (CabeceraReg + (iCount2 + 7) + row)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                ws.Cells[CabeceraReg + (iCount2 + 7) + row, 6].Value = obj.MetodoEvaluacion;
                ws.Cells[CabeceraReg + (iCount2 + 7) + row, 6].Style.Font.Size = 11;

                int iCount3 = row + 1 + numFilasExcel;

                //armando cabecera 2
                ws.Cells["A" + (subCabecera + row + numFilasExcel) + ":D" + (subCabecera + row + numFilasExcel + 1)].Merge = true;
                ws.Cells["A" + (subCabecera + row + numFilasExcel) + ":D" + (subCabecera + iCount3)].Style.WrapText = true;
                ws.Cells["A" + (subCabecera + row + numFilasExcel) + ":D" + (subCabecera + iCount3)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["A" + (subCabecera + row + numFilasExcel) + ":D" + (subCabecera + iCount3)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells["A" + (subCabecera + row + numFilasExcel) + ":D" + (subCabecera + iCount3)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells["A" + (subCabecera + row + numFilasExcel) + ":D" + (subCabecera + iCount3)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells["A" + (subCabecera + row + numFilasExcel) + ":D" + (subCabecera + iCount3)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + row + numFilasExcel, 1].Value = obj.nomDocumento;
                ws.Cells[subCabecera + row + numFilasExcel, 1].Style.Font.Size = 10;
                ws.Cells[subCabecera + row + numFilasExcel, 1].Style.Font.Bold = true;


                ws.Cells["E" + (subCabecera + row + numFilasExcel) + ":E" + (subCabecera + iCount3)].Merge = true;
                ws.Cells["E" + (subCabecera + row + numFilasExcel) + ":E" + (subCabecera + iCount3)].Style.WrapText = true;
                ws.Cells["E" + (subCabecera + row + numFilasExcel) + ":E" + (subCabecera + iCount3)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["E" + (subCabecera + row + numFilasExcel) + ":E" + (subCabecera + iCount3)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells["E" + (subCabecera + row + numFilasExcel) + ":E" + (subCabecera + iCount3)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells["E" + (subCabecera + row + numFilasExcel) + ":E" + (subCabecera + iCount3)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells["E" + (subCabecera + row + numFilasExcel) + ":E" + (subCabecera + iCount3)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + row + numFilasExcel, 5].Value = obj.TipoOperacion;
                ws.Cells[subCabecera + row + numFilasExcel, 5].Style.Font.Size = 10;
                ws.Cells[subCabecera + row + numFilasExcel, 5].Style.Font.Bold = true;


                ws.Cells["F" + (subCabecera + row + numFilasExcel) + ":H" + (subCabecera + iCount3 - 1)].Merge = true;
                ws.Cells["F" + (subCabecera + row + numFilasExcel) + ":H" + (subCabecera + iCount3 - 1)].Style.WrapText = true;
                ws.Cells["F" + (subCabecera + row + numFilasExcel) + ":H" + (subCabecera + iCount3 - 1)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["F" + (subCabecera + row + numFilasExcel) + ":H" + (subCabecera + iCount3 - 1)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells["F" + (subCabecera + row + numFilasExcel) + ":H" + (subCabecera + iCount3 - 1)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells["F" + (subCabecera + row + numFilasExcel) + ":H" + (subCabecera + iCount3 - 1)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + row + numFilasExcel, 6].Value = obj.Entrada;
                ws.Cells[subCabecera + row + numFilasExcel, 6].Style.Font.Size = 10;
                ws.Cells[subCabecera + row + numFilasExcel, 6].Style.Font.Bold = true;


                ws.Cells["I" + (subCabecera + row + numFilasExcel) + ":K" + (subCabecera + iCount3 - 1)].Merge = true;
                ws.Cells["I" + (subCabecera + row + numFilasExcel) + ":K" + (subCabecera + iCount3 - 1)].Style.WrapText = true;
                ws.Cells["I" + (subCabecera + row + numFilasExcel) + ":K" + (subCabecera + iCount3 - 1)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["I" + (subCabecera + row + numFilasExcel) + ":K" + (subCabecera + iCount3 - 1)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells["I" + (subCabecera + row + numFilasExcel) + ":K" + (subCabecera + iCount3 - 1)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells["I" + (subCabecera + row + numFilasExcel) + ":K" + (subCabecera + iCount3 - 1)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + row + numFilasExcel, 9].Value = obj.Salidas;
                ws.Cells[subCabecera + row + numFilasExcel, 9].Style.Font.Size = 10;
                ws.Cells[subCabecera + row + numFilasExcel, 9].Style.Font.Bold = true;


                ws.Cells["L" + (subCabecera + row + numFilasExcel) + ":N" + (subCabecera + iCount3 - 1)].Merge = true;
                ws.Cells["L" + (subCabecera + row + numFilasExcel) + ":N" + (subCabecera + iCount3 - 1)].Style.WrapText = true;
                ws.Cells["L" + (subCabecera + row + numFilasExcel) + ":N" + (subCabecera + iCount3 - 1)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["L" + (subCabecera + row + numFilasExcel) + ":N" + (subCabecera + iCount3 - 1)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells["L" + (subCabecera + row + numFilasExcel) + ":N" + (subCabecera + iCount3 - 1)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells["L" + (subCabecera + row + numFilasExcel) + ":N" + (subCabecera + iCount3 - 1)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + row + numFilasExcel, 12].Value = obj.SaldoFinal;
                ws.Cells[subCabecera + row + numFilasExcel, 12].Style.Font.Size = 10;
                ws.Cells[subCabecera + row + numFilasExcel, 12].Style.Font.Bold = true;


                ws.Cells[subCabecera + (iCount3 + 1) + row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 1].Style.WrapText = true;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 1].Value = obj.Fecha;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 1].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 1].Style.Font.Bold = true;

                ws.Cells[subCabecera + (iCount3 + 1) + row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 2].Style.WrapText = true;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 2].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 2].Value = obj.TipoTabla10;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 2].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 2].Style.Font.Bold = true;

                ws.Cells[subCabecera + (iCount3 + 1) + row, 3].Style.WrapText = true;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 3].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 3].Value = obj.Serie;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 3].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 3].Style.Font.Bold = true;

                ws.Cells[subCabecera + (iCount3 + 1) + row, 4].Style.WrapText = true;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 4].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 4].Value = obj.Numero;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 4].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 4].Style.Font.Bold = true;

                ws.Cells[subCabecera + (iCount3 + 1) + row, 5].Style.WrapText = true;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 5].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 5].Value = obj.Tabla12;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 5].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 5].Style.Font.Bold = true;

                ws.Cells["F" + (subCabecera + (iCount3) + row) + ":F" + (subCabecera + (iCount3) + row + 1)].Merge = true;
                ws.Cells["F" + (subCabecera + (iCount3) + row) + ":F" + (subCabecera + (iCount3) + row + 1)].Style.WrapText = true;
                ws.Cells["F" + (subCabecera + (iCount3) + row) + ":F" + (subCabecera + (iCount3) + row + 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells[subCabecera + (iCount3) + row, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3) + row, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 6].Value = obj.Cantidad;
                ws.Cells[subCabecera + (iCount3) + row, 6].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3) + row, 6].Style.Font.Bold = true;

                ws.Cells["G" + (subCabecera + (iCount3) + row) + ":G" + (subCabecera + (iCount3) + row + 1)].Merge = true;
                ws.Cells["G" + (subCabecera + (iCount3) + row) + ":G" + (subCabecera + (iCount3) + row + 1)].Style.WrapText = true;
                ws.Cells["G" + (subCabecera + (iCount3) + row) + ":G" + (subCabecera + (iCount3) + row + 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells[subCabecera + (iCount3) + row, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3) + row, 7].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 7].Value = obj.CostoUnitario;
                ws.Cells[subCabecera + (iCount3) + row, 7].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3) + row, 7].Style.Font.Bold = true;

                ws.Cells["H" + (subCabecera + (iCount3) + row) + ":H" + (subCabecera + (iCount3) + row + 1)].Merge = true;
                ws.Cells["H" + (subCabecera + (iCount3) + row) + ":H" + (subCabecera + (iCount3) + row + 1)].Style.WrapText = true;
                ws.Cells["H" + (subCabecera + (iCount3) + row) + ":H" + (subCabecera + (iCount3) + row + 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells[subCabecera + (iCount3) + row, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3) + row, 8].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 8].Value = obj.CostoTotal;
                ws.Cells[subCabecera + (iCount3) + row, 8].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3) + row, 8].Style.Font.Bold = true;

                ws.Cells["I" + (subCabecera + (iCount3) + row) + ":I" + (subCabecera + (iCount3) + row + 1)].Merge = true;
                ws.Cells["I" + (subCabecera + (iCount3) + row) + ":I" + (subCabecera + (iCount3) + row + 1)].Style.WrapText = true;
                ws.Cells["I" + (subCabecera + (iCount3) + row) + ":I" + (subCabecera + (iCount3) + row + 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["I" + (subCabecera + (iCount3) + row) + ":I" + (subCabecera + (iCount3) + row + 1)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3) + row, 9].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 9].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 9].Value = obj.Cantidad;
                ws.Cells[subCabecera + (iCount3) + row, 9].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3) + row, 9].Style.Font.Bold = true;

                ws.Cells["J" + (subCabecera + (iCount3) + row) + ":J" + (subCabecera + (iCount3) + row + 1)].Merge = true;
                ws.Cells["J" + (subCabecera + (iCount3) + row) + ":J" + (subCabecera + (iCount3) + row + 1)].Style.WrapText = true;
                ws.Cells["j" + (subCabecera + (iCount3) + row) + ":j" + (subCabecera + (iCount3) + row + 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["j" + (subCabecera + (iCount3) + row) + ":j" + (subCabecera + (iCount3) + row + 1)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3) + row, 10].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 10].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 10].Value = obj.CostoUnitario;
                ws.Cells[subCabecera + (iCount3) + row, 10].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3) + row, 10].Style.Font.Bold = true;

                ws.Cells["K" + (subCabecera + (iCount3) + row) + ":K" + (subCabecera + (iCount3) + row + 1)].Merge = true;
                ws.Cells["K" + (subCabecera + (iCount3) + row) + ":K" + (subCabecera + (iCount3) + row + 1)].Style.WrapText = true;
                ws.Cells["K" + (subCabecera + (iCount3) + row) + ":K" + (subCabecera + (iCount3) + row + 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["K" + (subCabecera + (iCount3) + row) + ":K" + (subCabecera + (iCount3) + row + 1)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3) + row, 11].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 11].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 11].Value = obj.CostoTotal;
                ws.Cells[subCabecera + (iCount3) + row, 11].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3) + row, 11].Style.Font.Bold = true;

                ws.Cells["L" + (subCabecera + (iCount3) + row) + ":L" + (subCabecera + (iCount3) + row + 1)].Merge = true;
                ws.Cells["L" + (subCabecera + (iCount3) + row) + ":L" + (subCabecera + (iCount3) + row + 1)].Style.WrapText = true;
                ws.Cells["L" + (subCabecera + (iCount3) + row) + ":L" + (subCabecera + (iCount3) + row + 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["L" + (subCabecera + (iCount3) + row) + ":L" + (subCabecera + (iCount3) + row + 1)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3) + row, 12].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 12].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 12].Value = obj.Cantidad;
                ws.Cells[subCabecera + (iCount3) + row, 12].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3) + row, 12].Style.Font.Bold = true;

                ws.Cells["M" + (subCabecera + (iCount3) + row) + ":M" + (subCabecera + (iCount3) + row + 1)].Merge = true;
                ws.Cells["M" + (subCabecera + (iCount3) + row) + ":M" + (subCabecera + (iCount3) + row + 1)].Style.WrapText = true;
                ws.Cells["M" + (subCabecera + (iCount3) + row) + ":M" + (subCabecera + (iCount3) + row + 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["M" + (subCabecera + (iCount3) + row) + ":M" + (subCabecera + (iCount3) + row + 1)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3) + row, 13].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 13].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 13].Value = obj.CostoUnitario;
                ws.Cells[subCabecera + (iCount3) + row, 13].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3) + row, 13].Style.Font.Bold = true;

                ws.Cells["N" + (subCabecera + (iCount3) + row) + ":N" + (subCabecera + (iCount3) + row + 1)].Merge = true;
                ws.Cells["N" + (subCabecera + (iCount3) + row) + ":N" + (subCabecera + (iCount3) + row + 1)].Style.WrapText = true;
                ws.Cells["N" + (subCabecera + (iCount3) + row) + ":N" + (subCabecera + (iCount3) + row + 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["N" + (subCabecera + (iCount3) + row) + ":N" + (subCabecera + (iCount3) + row + 1)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells[subCabecera + (iCount3) + row, 14].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 14].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3) + row, 14].Value = obj.CostoTotal;
                ws.Cells[subCabecera + (iCount3) + row, 14].Style.Font.Size = 10;
                ws.Cells[subCabecera + (iCount3) + row, 14].Style.Font.Bold = true;

                ws.Cells[subCabecera + (iCount3 + 1) + row, 14].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 13].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 12].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 11].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 10].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 9].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 8].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 7].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[subCabecera + (iCount3 + 1) + row, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            }

        }

    }
}