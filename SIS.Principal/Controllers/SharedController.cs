using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIS.Business;
using SIS.Entity;
using SIS.Principal.Models;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Configuration;
using ZXing;
using System.IO;
using System.Drawing.Imaging;
using System.Net;
namespace SIS.Principal.Controllers
{
    public class SharedController : Controller
    {
        Authentication Authentication = new Authentication();
        BGeneral General = new BGeneral();
        // GET: Shared



        public ActionResult ImprimirEtiqueta()
        {

            ViewBag.Lista = TempData["oDatos"];
            return View();
        }

        [HttpPost]
        public void ImprimirBarra(List<EBarra> sLista)
        {
            List<EBarra> oDatos = new List<EBarra>();
            for (int i = 0; i < sLista.Count; i++)
            {
                EBarra obj = new EBarra();
                byte[] imageByteData = GenerateBarCodeZXing(sLista[i].codigo);
                string imageBase64Data = Convert.ToBase64String(imageByteData);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                obj.codigo = sLista[i].codigo;
                obj.imgen = imageDataURL;
                obj.nombre = sLista[i].nombre;
                obj.precio = sLista[i].precio;
                obj.cantidad = sLista[i].cantidad;

                oDatos.Add(obj);
            }
            TempData["oDatos"] = oDatos;
            Utils.WriteMessage("success|correcto");

            //  return RedirectToAction("ImprimirEtiqueta", "Shared");
        }

        private byte[] GenerateBarCodeZXing(string data)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 60,
                    Height = 40,
                    Margin = 0,
                    PureBarcode = true
                }
            };
            var imgBitmap = writer.Write(data);
            using (var stream = new MemoryStream())
            {
                imgBitmap.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }

        [HttpPost]
        public void ListaCombo(int Id)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    General.CBOLista(Id, empresa)
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
        public void ListaComboId(int flag, int Id)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    General.CBOListaId(flag, Id, empresa));
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
        public void ListaComboSucursal()
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                string usuario = Authentication.UserLogued.Usuario;

                Utils.Write(
                    ResponseType.JSON,
                    General.CBOListaIdAlmacen(sucursal, empresa, usuario));
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
        public void CoidgoGenerado()
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    General.CoidgoGenerado(empresa));
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
        public void ListarUbigeo(string Acction, string idPais, string IdDep, string IdProv, string IdDis)
        {
            try
            {
                Utils.Write(
                    ResponseType.JSON,
                    General.ListarUbigeo(Acction, idPais, IdDep, IdProv, IdDis));
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
        public void GetSerieNum(int Flag)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    General.GetSerieNumero(empresa, Flag));
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
        public void FiltroProvCli(string filtro, int Tipo, int flag)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    General.FiltroProvCli(filtro, Tipo, flag, empresa));
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
        public void FiltroProducto(string filtro)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    General.FiltroProducto(filtro, empresa));
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
        public void ListaEmpresaLogin()
        {
            try
            {

                Utils.Write(
                    ResponseType.JSON,
                    General.ListaEmpresaLogin());
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
        public void VisorStock(int Material, int Almacen)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;
                Utils.Write(
                    ResponseType.JSON,
                    General.VisorStock(empresa, sucursal, Material, Almacen));
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
        public void FiltroBusquedaProducto(int idAlmacen, string filtro)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                int sucursal = Authentication.UserLogued.Sucursal.IdSucursal;

                Utils.Write(
                    ResponseType.JSON,
                    General.FiltroBusquedaProducto(empresa, sucursal, idAlmacen, filtro));
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
        public void SearchSunat(string numeroRuc)
        {
            try
            {
                var client = new RestClient(WebConfigurationManager.AppSettings["UrlApiRest"]);
                var request = new RestRequest("consulta.php", Method.GET);
                request.AddParameter("nruc", numeroRuc.Trim());
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                JObject jsonOnline = JObject.Parse(content);
                var json = Convert.ToString(jsonOnline["result"]);
                var model = JsonConvert.DeserializeObject<EConsultaRUCDNI>(json);

                Utils.Write(
                     ResponseType.JSON,
                     model
                 );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpPost]
        public void SearchSunatRUCDNI(int tipo, string numeroRuc)
        {
            try
            {
                if (tipo == 3)
                {
                    string URL = "https://dniruc.apisperu.com/api/v1/ruc/" + numeroRuc + WebConfigurationManager.AppSettings["UrlTokenRUCDNI"];

                    var json = new WebClient().DownloadString(URL);


                    var model = JsonConvert.DeserializeObject<EConsultaRUC>(json);

                    Utils.Write(
                         ResponseType.JSON,
                         model
                     );
                }
                else
                {
                    string URL = "https://dniruc.apisperu.com/api/v1/dni/" + numeroRuc + WebConfigurationManager.AppSettings["UrlTokenRUCDNI"];

                    var json = new WebClient().DownloadString(URL);


                    var model = JsonConvert.DeserializeObject<EconsultaDNI>(json);

                    Utils.Write(
                         ResponseType.JSON,
                         model
                     );

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //buscar api
        [HttpPost]
        public void SearchSunatRUC(string numeroRuc)
        {

            var client = new RestClient(WebConfigurationManager.AppSettings["UrlConsultaDocumento"]);
            var url = client.BaseUrl + "ruc/" + numeroRuc;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                        {
                            Utils.Write(
                                 ResponseType.JSON,
                                "{ Code: 1, ErrorMessage: \"" + "Paso algo en la consulta API" + "\" }");
                        }
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            var jsonObjects = JsonConvert.DeserializeObject<object>(responseBody);
                            var model = JsonConvert.DeserializeObject<EConsultaRuc>(jsonObjects.ToString());
                            Utils.Write(ResponseType.JSON, model);
                        }
                    }
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
        public void SearchSunatDNI(string numeroRuc)
        {

            var client = new RestClient(WebConfigurationManager.AppSettings["UrlConsultaDocumento"]);
            var url = client.BaseUrl + "dni/" + numeroRuc;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                        {
                            Utils.Write(
                                 ResponseType.JSON,
                                "{ Code: 1, ErrorMessage: \"" + "Paso algo en la consulta API" + "\" }");
                        }
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            var jsonObjects = JsonConvert.DeserializeObject<object>(responseBody);
                            var model = JsonConvert.DeserializeObject<EconsultaDNI>(jsonObjects.ToString());
                            Utils.Write(ResponseType.JSON, model);
                        }
                    }
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
    }
}