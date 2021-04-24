using SIS.Principal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIS.Business;
using SIS.Entity;
using ERP.Service;
using System.IO;
using System.Xml;
using System.Text;

namespace SIS.Principal.Controllers
{
    public class FacturaElectronicaController : Controller
    {
        Authentication Authentication = new Authentication();
        BFacturaElectronica factura = new BFacturaElectronica();
        BMantenimiento Mantenimiento = new BMantenimiento();

        EnviarDocumentoService servicioSunat = new EnviarDocumentoService();
        // GET: FacturaElectronica
        public ActionResult EnvioComPago()
        {
            return View();
        }
        public ActionResult GenerarResumenEnvio()
        {
            return View();
        }
        [HttpPost]
        public void ListarComprobante(int Comienzo, int Medida, int Sucursal, string FechaEmi)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    factura.ListarComprobante(Comienzo, Medida, empresa, Sucursal, FechaEmi)
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
        public void ListarBoletaResumen(int Comienzo, int Medida, int Sucursal, string FechaEmi)
        {
            try
            {
                int empresa = Authentication.UserLogued.Empresa.Id;
                Utils.Write(
                    ResponseType.JSON,
                    factura.ListarBoletaResumen(Comienzo, Medida, empresa, Sucursal, FechaEmi)
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
        public void EnviarCompPagoFact(string sLista)
        {
            try
            {
                List<string> listaEq = (List<string>)Utils.Deserialize(sLista, typeof(List<string>));
                int empresa = Authentication.UserLogued.Empresa.Id;
                EEmpresa Empresa = Mantenimiento.ListaEditEmpresa(empresa);

              
                var output = Server.MapPath("~/RespuestaSunat/");
                string response = "";
                string responseSuccess = "Archivos enviados: " + "\n";
                string responseError = "Archivos no enviados: " + "\n";

               // SunatServices sunatService = new SunatServices(input, output);


                for (int i = 0; i < listaEq.Count; i++)
                {
                    string ruc = listaEq[i].Split('|')[0];
                    string anio = listaEq[i].Split('|')[1].Split('-')[0];
                    string mes = listaEq[i].Split('|')[1].Split('-')[1];
                    string tipoDoc = listaEq[i].Split('|')[2];
                    string id = listaEq[i].Split('|')[3];
                    string rptaSunatTipo = "";
                    switch (tipoDoc)
                    {
                        case "01":
                            rptaSunatTipo = "F";
                            break;
                        case "07":
                            rptaSunatTipo = "C";
                            break;
                        case "08":
                            rptaSunatTipo = "D";
                            break;
                    }

                    // response = sunatService.sendDocument(ruc + "\\" + anio + "\\" + mes + "\\" + tipoDoc + "\\" + ruc + "-" + tipoDoc + "-" + id + ".zip");
                    var NameOfFileZip = ruc + "\\" + anio + "\\" + mes + "\\" + tipoDoc + "\\" + ruc + "-" + tipoDoc + "-" + id + ".zip";
                    var input = Server.MapPath("~/Comprobantes/" + ruc + "/" + anio + "/" + mes + "/" + tipoDoc + "/" + ruc + "-" + tipoDoc + "-" + id + ".zip");
                    servicioSunat.Inicializar(new ParametrosConexion
                    {
                        Ruc = ruc,
                        UserName = Empresa.UsuarioSol,
                        Password = Empresa.ClaveSol,
                        EndPointUrl = Empresa.EndPointUrl
                        //https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService?wsdl
                    });

                  var folder = (input );
                  //  NameOfFileZip = NameOfFileZip.Split('\\').Last();
                    //  byte[] allbytes = System.IO.File.ReadAllBytes(folder);

                 
                    byte[] bytes = Encoding.ASCII.GetBytes(folder);
                    var allbytes = Convert.ToBase64String(bytes);
                    var resultado = servicioSunat.EnviarDocumento(new DocumentoSunat
                    {

                        TramaXml = allbytes,//"es un zip en base 64",
                        NombreArchivo = NameOfFileZip// $"{nombreArchivo}.zip"
                    });



                    if (response.Split('|')[0] == "error")
                    {
                        responseError += "-> " + ruc + "- " + tipoDoc + "-" + id + ".zip." + " Código de Error: " + response.Split('|')[1].Split('&')[0] + "\n";
                    }
                    else
                    {
                        responseSuccess += "-> " + ruc + "-" + tipoDoc + "-" + id + ".zip" + "\n";
                    }

                    //  oObj = facturacionelectronica.registrarRespuestaSUNAT(rptaSunatTipo, id, 0, response.Split('|')[1].Split('&')[0], response.Split('|')[1].Split('&')[1]);
                }

                response = "info|" + responseSuccess + "\n------------------------------------------------------------------------\n\n" + responseError;

                Utils.WriteMessage(response);

            }
            catch (Exception Exception)
            {
                Utils.WriteMessage("error|" + Exception.Message);
            }
        }
        [HttpPost]
        public void ResumnetoEnvio(string sLista, string fecha, int sucursal)
        {
            try
            {
                List<string> listaEq = (List<string>)Utils.Deserialize(sLista, typeof(List<string>));

                List<string> oObj = listaEq;
                var sMensaje = "";
                int empresa = Authentication.UserLogued.Empresa.Id;
                var Usuario = Authentication.UserLogued.Usuario;

                EEmpresa Empresa = Mantenimiento.ListaEditEmpresa(empresa);
                var ruta = Server.MapPath("~/ResumenesBV/");
                var rutaServidor = Server.MapPath("~/Certificado/" + Empresa.Certificado);
                var claveCertificado = Empresa.ClaveCertificado;
                for (int i = 0; i < listaEq.Count; i++)
                {
                    sMensaje = factura.GeneraResumen(int.Parse(listaEq[i]), empresa, sucursal, fecha, Usuario, ruta, rutaServidor, claveCertificado);

                }

                Utils.WriteMessage(sMensaje);
            }
            catch (Exception Exception)
            {
                Utils.WriteMessage("error|" + Exception.Message);
            }

        }
    }
}