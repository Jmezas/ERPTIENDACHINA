using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIS.Business;
using SIS.Entity;
using SIS.Principal.Models;
using ZXing;
using ZXing.QrCode;
using System.Linq.Dynamic;

namespace SIS.Principal.Controllers
{
    public class MantenimientoController : Controller
    {
        BMantenimiento Mantenimiento = new BMantenimiento();
        Authentication Authentication = new Authentication();
        EnviarCorreo Corrreo = new EnviarCorreo();
        // GET: Mantenimiento
        public ActionResult Categoria()
        {
            return View();
        }
        public ActionResult Marca()
        {
            return View();
        }
        public ActionResult Material()
        {
            return View();
        }
        public ActionResult SubCategoria()
        {
            return View();
        }
        public ActionResult Unidad()
        {
            return View();
        }

        public ActionResult Cliente()
        {
            return View();
        }
        public ActionResult Proveedor()
        {
            return View();
        }

        public ActionResult Sucursal()
        {
            return View();
        }
        public ActionResult Almacen()
        {
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult AlmacenPermisos()
        {
            return View();
        }
        public ActionResult Caja()
        {
            return View();
        }
        public ActionResult Tipo()
        {
            return View();
        }
        public ActionResult TipoSerie()
        {
            return View();
        }
        public ActionResult Color()
        {
            return View();
        }
        public ActionResult Modelo()
        {
            return View();
        }
        ///Controles
        ///
        [HttpPost]
        public void InstMaterial(EMaterial Material)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                Material.Empresa.Id = Authentication.UserLogued.Empresa.Id;
                Utils.WriteMessage(Mantenimiento.InstMaterial(Material, Usuario));
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
        public void ListaEditMaterial(int Id)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditMaterial(Id, empresa)
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


        public ActionResult ListadoMaterial()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            int pagesize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            int empresa = Authentication.UserLogued.Empresa.Id;
            List<EMaterial> List = Mantenimiento.ListaMaterial(empresa);

            var customerData = (from x in List
                                select x);

            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m=> m.Text.Contains(searchValue) || m.Codigo.Contains(searchValue)|| m.Nombre.Contains(searchValue)|| m.Unidad.Nombre.Contains(searchValue)|| m.Marca.Nombre.Contains(searchValue)|| m.Modelo.Nombre.Contains(searchValue)|| m.Categoria.Nombre.Contains(searchValue) ||m.SubCateoria.Nombre.Contains(searchValue));
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                if (sortColumn.Trim() != "")
                {
                    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
                }
            }
            recordsTotal = customerData.Count();
            var data = customerData.Skip(skip).Take(pagesize);
            return Json(new { draw=draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal,data=data });

        }




        //generar codigo de barra

        public ActionResult GenerarCodigo(string sLista)
        {
            List<string> listaEq = (List<string>)Utils.Deserialize(sLista, typeof(List<string>));
            var generar = "";

            Document pdfDoc = new Document(PageSize.A4, 5, 5, 10, 10);



            //Permite visualizar el contenido del documento que es descargado en "Mis descargas"
            PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);

            //Definiendo parametros para la fuente de la cabecera y pie de pagina
            iTextSharp.text.Font fuente = FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

            //Se define la cabecera del documento
            HeaderFooter cabecera = new HeaderFooter(new Phrase("Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), fuente), false);//'el valor es false porque no habra numeración
            pdfDoc.Header = cabecera;
            cabecera.Border = 0;// Rectangle.BOTTOM_BORDER
            cabecera.Alignment = HeaderFooter.ALIGN_RIGHT;

            HeaderFooter pie = new HeaderFooter(new Phrase("pagia", fuente), true);

            pdfDoc.Footer = pie;
            pie.Border = iTextSharp.text.Rectangle.TOP_BORDER;
            pie.Alignment = HeaderFooter.ALIGN_RIGHT;

            //Open PDF Document to write data 
            pdfDoc.Open();

            PdfPTable tblPrueba = new PdfPTable(1);
            tblPrueba.WidthPercentage = 100;

            PdfPTable TableValorqr = new PdfPTable(3);
            TableValorqr.WidthPercentage = 100;

            PdfPTable BarCodeTable = new PdfPTable(3);
            BarCodeTable.SetTotalWidth(new float[] { 100, 10, 100, });
            BarCodeTable.DefaultCell.Border = PdfPCell.NO_BORDER;

            PdfPTable detalle = new PdfPTable(1);

            for (int i = 0; i < listaEq.Count; i++)
            {

                generar = listaEq[i].Split('|')[1];
                var qr = GenerateQRCodeImage(generar);
                System.Drawing.Image imagen = qr;
                iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(imagen, System.Drawing.Imaging.ImageFormat.Jpeg);
                pdfImage.ScaleToFit(100.0F, 70.0F);

                PdfPCell letras = new PdfPCell(new Phrase("datos generales"));
                letras.HorizontalAlignment = 0;
                letras.Border = 1;
                BarCodeTable.AddCell(letras);

                PdfPCell celImg2 = new PdfPCell(pdfImage);
                celImg2.HorizontalAlignment = 0;
                celImg2.Border = 1;
                BarCodeTable.AddCell(celImg2);


            }
            PdfPCell addDetalle = new PdfPCell(BarCodeTable);
            addDetalle.Colspan = 0;
            addDetalle.Border = 0;
            addDetalle.PaddingBottom = 3;
            TableValorqr.AddCell(addDetalle);

            PdfPCell cCeldaValorss = new PdfPCell(BarCodeTable);
            cCeldaValorss.Colspan = 0;
            cCeldaValorss.Border = 1;
            cCeldaValorss.PaddingBottom = 3;
            tblPrueba.AddCell(cCeldaValorss);

            pdfDoc.Add(tblPrueba);

            // Close your PDF 
            pdfDoc.Close();
            Response.ContentType = "application/pdf";

            // Set default file Name as current datetime 
            Response.AddHeader("content-disposition", "attachment; filename=Documento.pdf");
            Response.Write(pdfDoc);

            Response.Flush();
            Response.End();

            return View();

        }
        public System.Drawing.Image GenerateQRCodeImage(string qrcodeText)
        {
            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new QrCodeEncodingOptions
                {
                    Width = 100,
                    Height = 80
                }
            };


            var result = barcodeWriter.Write(qrcodeText);

            var barcodeBitmap = new Bitmap(result);
            using (MemoryStream memory = new MemoryStream())
            {
                barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                byte[] bytes = memory.ToArray();

                MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
                ms.Write(bytes, 0, bytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                return image;
            }
        }

        [HttpPost]
        public void InstUnidad(EUnidad Unidad)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                Unidad.Empresa.Id = Authentication.UserLogued.Empresa.Id;
                Utils.WriteMessage(Mantenimiento.InstUnidad(Unidad, Usuario));
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
        public void ListaEditUnidad(int Id)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditUnidad(Id, empresa)
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

        public ActionResult ListaUnidad()
        {
            int empresa = Authentication.UserLogued.Empresa.Id;
            var List = Mantenimiento.ListaUnidad(empresa);
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void InstMarca(EMarca Marca)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                Marca.Empresa.Id = Authentication.UserLogued.Empresa.Id;
                Utils.WriteMessage(Mantenimiento.InstMarca(Marca, Usuario));
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
        public void ListaEditMarca(int Id)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditMarca(Id, empresa)
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

        public ActionResult ListaMarca()
        {
            int empresa = Authentication.UserLogued.Empresa.Id;
            var List = Mantenimiento.ListaMarca(empresa);
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }
        // categoria
        [HttpPost]
        public void InstCategoria(ECategoria Categoria)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                Categoria.Empresa.Id = Authentication.UserLogued.Empresa.Id;
                Utils.WriteMessage(Mantenimiento.InstCategoria(Categoria, Usuario));
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
        public void ListaEditCategoria(int Id)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditCategoria(Id, empresa)
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

        public ActionResult ListaCategoria()
        {
            int empresa = Authentication.UserLogued.Empresa.Id;
            var List = Mantenimiento.ListaCategoria(empresa);
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }

        //sub Categoria
        [HttpPost]
        public void InstSubCategoria(ESubCateoria SubCategoria)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                SubCategoria.Empresa.Id = Authentication.UserLogued.Empresa.Id;
                Utils.WriteMessage(Mantenimiento.InstSubCategoria(SubCategoria, Usuario));
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
        public void ListaEditSubCategoria(int Id)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditSubCategoria(Id, empresa)
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

        public ActionResult ListaSubCategoria()
        {
            int empresa = Authentication.UserLogued.Empresa.Id;
            var List = Mantenimiento.ListaSubCategoria(empresa);
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }

        //cliente

        [HttpPost]
        public void InstCliente(ECliente Cliente)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                Cliente.Empresa.Id = Authentication.UserLogued.Empresa.Id;
                Utils.WriteMessage(Mantenimiento.InstCliente(Cliente, Usuario));
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
        public void ListaEditCliente(int IdCliente)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditCliente(IdCliente, empresa)
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

        public ActionResult ListaCliente()
        {
            //int empresa = Authentication.UserLogued.Empresa.Id;
            //var List = Mantenimiento.ListaCliente(empresa);
            //return Json(new { data = List }, JsonRequestBehavior.AllowGet);
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            int pagesize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            int empresa = Authentication.UserLogued.Empresa.Id;
            List<ECliente> List = Mantenimiento.ListaCliente(empresa);

            var customerData = (from x in List
                                select x);

            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.Text.Contains(searchValue) ||
                                                       m.Razonsocial.Contains(searchValue) ||
                                                       m.NroDocumento.Contains(searchValue) ||
                                                       m.Telefono.Contains(searchValue) ||
                                                       m.Celular.Contains(searchValue) ||
                                                       m.Direccion.Contains(searchValue));
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                if (sortColumn.Trim() != "")
                {
                    customerData = customerData.OrderBy(sortColumn + " " + "desc");
                }
            }
            recordsTotal = customerData.Count();
            var data = customerData.Skip(skip).Take(pagesize);
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        } 

        //proveedor 
        [HttpPost]
        public void InstProveedor(EProveedor proveedor)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                proveedor.Empresa.Id = Authentication.UserLogued.Empresa.Id;
                Utils.WriteMessage(Mantenimiento.InstProveedor(proveedor, Usuario));
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
        public void ListaEditProveedor(int Id)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditProveedor(Id, empresa)
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

        public ActionResult ListaProveedor()
        {

            int empresa = Authentication.UserLogued.Empresa.Id;
            var List = Mantenimiento.ListaProveedor(empresa);
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }
        //Sucursal
        [HttpPost]
        public void InstSucursal(ESucursal oDatos)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                oDatos.Empresa = new EEmpresa
                {
                    Id = Authentication.UserLogued.Empresa.Id
                };

                Utils.WriteMessage(Mantenimiento.InstSucursal(oDatos, Usuario));
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
        public void ListaEditSucursal(int Id)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditSucursal(Id, empresa)
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

        public ActionResult ListaSucursal()
        {

            int empresa = Authentication.UserLogued.Empresa.Id;
            var List = Mantenimiento.ListaSucursal(empresa);
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }

        //Almacen
        [HttpPost]
        public void InstAlmacen(EAlmacen oDatos)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                oDatos.Empresa.Id = Authentication.UserLogued.Empresa.Id;
                Utils.WriteMessage(Mantenimiento.InstAlmacen(oDatos, Usuario));
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
        public void ListaEditAlmacen(int Id)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditAlmacen(Id, empresa)
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

        public ActionResult ListaAlmacen()
        {

            int empresa = Authentication.UserLogued.Empresa.Id;
            var List = Mantenimiento.ListaAlmacen(empresa);
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void Eliminar(int Id, int IdFlag)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                Utils.WriteMessage(Mantenimiento.Eliminar(Id, IdFlag, Usuario));
            }
            catch (Exception Exception)
            {
                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 2, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }
        }

        public ActionResult ListaPermisoAlmacen()
        {

            int empresa = Authentication.UserLogued.Empresa.Id;
            var List = Mantenimiento.ListaPermisoAlmacen(empresa);
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void ListaCBO_UsuarioAlmacen(int Flag)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaCBO_UsuarioAlmacen(Flag, empresa));
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
        public void ListaAlmacenPermisos(int Flag, string login)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaAlmacenPermisos(Flag, login, empresa, sucursal));
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
        public void InstUpdPermisoAlmacem(List<EAlmacen> sLista, int Flag, string Usuario)
        {

            try
            {
                var userLoger = Authentication.UserLogued.Usuario;
                sLista.Select(c =>
                {
                    c.Usuario.Usuario = Usuario;
                    c.Empresa.Id = Authentication.UserLogued.Empresa.Id;
                    return c;
                }).ToList();



                Utils.WriteMessage(Mantenimiento.InstUpdPermisoAlmacem(sLista, Flag, userLoger));
            }
            catch (Exception Exception)
            {

                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 1, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }

        }

        public ActionResult ListaEmpresa()
        {

            var List = Mantenimiento.ListaEmpresa();
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void InstEmpresa(EEmpresa oDatos, HttpPostedFileBase Logo, HttpPostedFileBase Certificado)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                if (Logo != null)
                {
                    var ex = Logo.ContentType.Split('/');
                    string adjuntologo = DateTime.Now.ToString("yyyyMMddHHmmss") + "logo." + ex[1];
                    Logo.SaveAs(Server.MapPath("~/Imagenes/Empresa/" + adjuntologo));
                    oDatos.Logo = adjuntologo;

                    var mensaje = Mantenimiento.InstEmpresa(oDatos, Usuario);
                    Utils.WriteMessage(mensaje);


                    var IdEmpresa = mensaje.Split('|');
                    if (IdEmpresa[0] == "success")
                    {
                        var Lista = Mantenimiento.ListaEnviarCorreo(int.Parse(IdEmpresa[2]));

                        Corrreo.SendMailEmpresa("Acceso al sistema", Cuerpo(Lista), Lista.Empresa.Correo);
                    }



                }
                else if (Certificado != null)
                {
                    string Adjuntocertificado = DateTime.Now.ToString("yyyyMMddHHmmss") + Certificado.FileName;
                    Certificado.SaveAs(Server.MapPath("~/Certificado/" + Adjuntocertificado));
                    oDatos.Certificado = Adjuntocertificado;
                    var mensaje = Mantenimiento.InstEmpresa(oDatos, Usuario);
                    Utils.WriteMessage(mensaje);


                    var IdEmpresa = mensaje.Split('|');
                    if (IdEmpresa[0] == "success")
                    {
                        var Lista = Mantenimiento.ListaEnviarCorreo(int.Parse(IdEmpresa[2]));

                        Corrreo.SendMailEmpresa("Acceso al sistema", Cuerpo(Lista), Lista.Empresa.Correo);
                    }
                }
                else
                {
                    Utils.WriteMessage(Mantenimiento.InstEmpresa(oDatos, Usuario));
                }

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
        public void ObtenerEmpresa(int IdEmpresa)
        {
            try
            {
                var model = Mantenimiento.ListaEditEmpresa(IdEmpresa);
                if (model.Logo == "" || model.Certificado == "")
                {
                    model.Logo = $"/Imagenes/Empresa/LogoDefault.png";
                    model.Certificado = $"";
                }
                else
                {
                    model.Logo = $"/Imagenes/Empresa/{model.Logo}";
                    model.Certificado = $"/Certificado/{model.Certificado}";
                }

                Utils.Write(ResponseType.JSON, model);
            }
            catch (Exception Exception)
            {
                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 1, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }
        }

        public ActionResult ListadoMenu()
        {
            var List = Mantenimiento.ListaMenu();
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void BuscarMenuId(int Id)
        {
            try
            {
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditMenu(Id)
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
        public void RegistrarMenu(EMenu eMenu, string Usuario)
        {
            try
            {
                Usuario = Authentication.UserLogued.Usuario;
                Utils.WriteMessage(Mantenimiento.Insertar_UpdateMenu(eMenu, Usuario)
                    );
            }
            catch (Exception Exception)
            {
                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 2, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }
        }


        public ActionResult ListadoCaja(int sucursal)
        {
            var empresa = Authentication.UserLogued.Empresa.Id;
            var List = Mantenimiento.ListaCaja(empresa, sucursal);
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void BuscarCajaId(int Id)
        {
            try
            {
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditCaja(Id)
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
        public void RegistrarCaja(EGeneral eMenu, int sucursal)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                var empresa = Authentication.UserLogued.Empresa.Id;

                Utils.WriteMessage(Mantenimiento.Insertar_UpdateCaja(eMenu, empresa, sucursal, Usuario)
                    );
            }
            catch (Exception Exception)
            {
                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 2, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }
        }

        public ActionResult Listadotipo()
        {
            var empresa = Authentication.UserLogued.Empresa.Id;
            var List = Mantenimiento.ListaTipo(empresa);
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void BuscarTipoId(int Id)
        {
            var empresa = Authentication.UserLogued.Empresa.Id;
            try
            {
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditTipo(Id, empresa)
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
        public void RegistrarTipo(EGeneral eMenu)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                var empresa = Authentication.UserLogued.Empresa.Id;
                Utils.WriteMessage(Mantenimiento.Insertar_UpdateTipo(eMenu, empresa, Usuario)
                    );
            }
            catch (Exception Exception)
            {
                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 2, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }
        }


        public ActionResult ListaTipoSerie()
        {
            var empresa = Authentication.UserLogued.Empresa.Id;
            var sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
            var List = Mantenimiento.ListaTipoSerie(empresa, sucursal);
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void BuscarTipoSerieId(int Id)
        {
            var empresa = Authentication.UserLogued.Empresa.Id;
            var sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
            try
            {
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditTipoDoc(Id, empresa, sucursal)
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
        public void Insertar_UpdateTipoDoc(ETipoSerie eMenu)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                var empresa = Authentication.UserLogued.Empresa.Id;
                var sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                Utils.WriteMessage(Mantenimiento.Insertar_UpdateTipoDoc(eMenu, empresa, Usuario, sucursal));
            }
            catch (Exception Exception)
            {
                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 2, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }
        }

        #region COLOR
        public ActionResult ListaColor()
        {
            var empresa = Authentication.UserLogued.Empresa.Id; 
            var List = Mantenimiento.ListaColor(empresa);
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void ListaEditColor(int Id)
        { 
            try
            {
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditColor(Id)
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
        public void Insertar_UpdateColor(EGeneral eMenu)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                var empresa = Authentication.UserLogued.Empresa.Id;
                Utils.WriteMessage(Mantenimiento.Insertar_UpdateColor(eMenu, empresa, Usuario));
            }
            catch (Exception Exception)
            {
                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 2, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }
        }
        #endregion

        #region COLOR
        public ActionResult ListaModelo()
        {
            var empresa = Authentication.UserLogued.Empresa.Id;
            var List = Mantenimiento.ListaModelo(empresa);
            return Json(new { data = List }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void ListaEditModelo(int Id)
        {
            try
            {
                Utils.Write(
                    ResponseType.JSON,
                    Mantenimiento.ListaEditModelo(Id)
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
        public void Insertar_UpdateModelo(EGeneral eMenu)
        {
            try
            {
                var Usuario = Authentication.UserLogued.Usuario;
                var empresa = Authentication.UserLogued.Empresa.Id;
                Utils.WriteMessage(Mantenimiento.Insertar_UpdateModelo(eMenu, empresa, Usuario));
            }
            catch (Exception Exception)
            {
                Utils.Write(
                    ResponseType.JSON,
                    "{ Code: 2, ErrorMessage: \"" + Exception.Message + "\" }"
                );
            }
        }
        #endregion

        /// <summary>
        /// envio de mensaje
        /// </summary>
        /// <param name="comtrol"></param>
        /// <returns></returns>
        //enviar correo
        public static string Cuerpo(EPerfil comtrol)
        {
            string Msm = "<meta content='es - pe' http-equiv='Content - Language' />" +
               "<meta http-equiv='Content - Type' content='text / html; charset = utf - 8'>" +
               "<body style='background: #F1F3F6;'>" +
               "<div style='margin: 10px 50px; padding: 10px 20px; text - align: center; '>" +

                " </div> " +
                "<div style='background: #FFFFFF; margin: 10px 50px; padding: 10px 20px;'>" +
                "<h2 style='color: #283D50;'>Acceso al sistema</h2>" +
                 "<hr> " +
           "<p><span class='style3'><strong> EMPRESA: " + comtrol.Empresa.RUC + " - " + comtrol.Empresa.Nombre + ".'</strong></span></p>" +
           "<h3 style=0color: #283D50;>Control:</h3>" +
           "<p>Usuario    : " + comtrol.Usuario + "</p>" +
           "<p>contraseña : " + comtrol.UsuarioCreador.Password + "</p>" +
           "<p>Perfil     : " + comtrol.Nombre + "</p>" +
           "<p>Nombre Usuario: " + comtrol.UsuarioCreador.Nombre + "</p>" +
           "<p>Correo     : " + comtrol.Empresa.Correo + "</p>" +
           "<p>Direccion  : " + comtrol.Empresa.Direccion + "</p>" +
           "<p>Comunicacion : " + comtrol.Empresa.Telefono + '-' + comtrol.Empresa.Celular + "</p>" +
            "<hr>" +
            "<b>datos de usuario es temporal, puede editar la infomacion una ves dentro del sistema" + "</b>" +
            "<div style='text-align: center'>" +
            "<h2 style='color: #283D50;'>SISTEMA DE ERP</h2>" +
           "</div>" +
           "</div>" +
           "</body>";




            return Msm;
        }
    }
}