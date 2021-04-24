using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIS.Factory;
using SIS.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.IO;
using Ionic.Zip;

namespace SIS.Data
{
    public class DFacturaElectronica : DBHelper
    {
        private static DFacturaElectronica Instancia;
        private DataBase BaseDeDatos;

        public DFacturaElectronica(DataBase BaseDeDatos) : base(BaseDeDatos)
        {
            this.BaseDeDatos = BaseDeDatos;
        }

        public static DFacturaElectronica ObtenerInstancia(DataBase BaseDeDatos)
        {
            if (Instancia == null)
            {
                Instancia = new DFacturaElectronica(BaseDeDatos);
            }
            return Instancia;
        }

        public List<EVenta> ListarComprobante(int Comienzo, int Medida, int empresa, int Sucursal, string FechaEmi)
        {
            List<EVenta> oDatos = new List<EVenta>();
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("FAC_FiltrarComprobantes");
                    CreateHelper(Connection);
                    AddInParameter("@iComienzo", Comienzo);
                    AddInParameter("@iMedida", Medida);
                    AddInParameter("@iSucursal", Sucursal);
                    AddInParameter("@iEmpresa", empresa);
                    AddInParameter("@fechaEmi", FechaEmi);
                    using (var Reader = ExecuteReader())
                    {

                        while (Reader.Read())
                        {
                            EVenta oComprobante = new EVenta();
                            oComprobante.empresa.RUC = Reader["sRuc"].ToString();
                            oComprobante.Documento.Nombre = Reader["stipoDocumento"].ToString();
                            oComprobante.serie = Reader["serie"].ToString();
                            oComprobante.fechaEmision = Reader["dFecEmision"].ToString();
                            oComprobante.total = float.Parse(Reader["nTotalCab"].ToString());
                            oComprobante.TotalR = int.Parse(Reader["Total"].ToString());
                            oDatos.Add(oComprobante);

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
        public List<EVenta> ListarBoletaResumen(int Comienzo, int Medida, int empresa, int Sucursal, string FechaEmi)
        {
            List<EVenta> oDatos = new List<EVenta>();
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SetQuery("FAC_ResumenComprobantePorSerie");
                    CreateHelper(Connection);
                    AddInParameter("@iComienzo", Comienzo);
                    AddInParameter("@iMedida", Medida);
                    AddInParameter("@iSucursal", Sucursal);
                    AddInParameter("@iEmpresa", empresa);
                    AddInParameter("@fechaEmi", FechaEmi);
                    using (var Reader = ExecuteReader())
                    {

                        while (Reader.Read())
                        {
                            EVenta oComprobante = new EVenta();
                            oComprobante.Id = int.Parse(Reader["iidVenta"].ToString());
                            oComprobante.serie = (Reader["sSerie"].ToString());
                            oComprobante.numero = (Reader["iNumero"].ToString());
                            oComprobante.Documento.Nombre = (Reader["stipoDocumento"].ToString());
                            oComprobante.grabada = float.Parse(Reader["OpeGrabada"].ToString());
                            oComprobante.inafecta = float.Parse(Reader["OpeInafecta"].ToString());
                            oComprobante.exonerada = float.Parse(Reader["OpeExoneradas"].ToString());
                            oComprobante.gratuita = float.Parse(Reader["OpeGratuita"].ToString());
                            oComprobante.descuento = float.Parse(Reader["TotalDescuento"].ToString());
                            oComprobante.igv = float.Parse(Reader["nIgvCab"].ToString());
                            oComprobante.total = float.Parse(Reader["nTotalCab"].ToString());
                            oComprobante.fechaEmision = (Reader["dFecEmision"].ToString());
                            oComprobante.TotalR = int.Parse(Reader["Total"].ToString());
                            oDatos.Add(oComprobante);

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
        public string GeneraResumen(int IdVenta, int empresa, int sucursal, string fecha, string usuario, string ruta, string rutaServidor, string claveCertificado)
        {
            using (var Connection = GetConnection(BaseDeDatos))
            {
                try
                {
                    Connection.Open();
                    SqlTransaction tran = (SqlTransaction)Connection.BeginTransaction();
                    SetQuery("FAC_InsResumenBoleta");
                    CreateHelper(Connection, tran);
                    AddInParameter("@IdVenta", IdVenta);
                    AddInParameter("@IdEmpresa", empresa);
                    AddInParameter("@IdSucursal", sucursal);
                    AddInParameter("@Fecha", fecha);
                    AddInParameter("@usuario", usuario);
                    AddOutParameter("@Mensaje", (DbType)SqlDbType.VarChar);
                    ExecuteQuery();
                    var smensaje = GetOutput("@Mensaje").ToString();

                    string[] vMensaje = smensaje.Split('|');
                    if (vMensaje[0].Equals("success"))
                    {
                        int IdComprobente = int.Parse(vMensaje[2]);

                        List<EResumenDiarioBVDetalleSunat> loObj = new List<EResumenDiarioBVDetalleSunat>();
                        SetQuery("FAC_ObtenerResumenDiarioBVSunat");
                        CreateHelper(Connection, tran);
                        AddInParameter("@iIdResumenDiarioBV", IdComprobente);
                        AddInParameter("@Empresa", empresa);
                        AddInParameter("@sucursal", sucursal);
                        using (var Reader = ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                EResumenDiarioBVDetalleSunat oObj = new EResumenDiarioBVDetalleSunat();
                                oObj.ResumenDiarioBVSunat.Id = int.Parse(Reader["IdResumen"].ToString());
                                oObj.ResumenDiarioBVSunat.RazonSocialEmpresa = Reader["sRazonSocialEmpresa"].ToString();
                                oObj.ResumenDiarioBVSunat.NumeroDocumentoEmpresa = Reader["sNumeroDocumentoEmpresa"].ToString();
                                oObj.ResumenDiarioBVSunat.CodigoDocumentoEmpresa = Reader["sCodigoDocumentoEmpresa"].ToString();
                                oObj.ResumenDiarioBVSunat.FechaEmisionDocumento = Reader["sFechaEmisionDocumento"].ToString();
                                oObj.ResumenDiarioBVSunat.IdentificadorResumen = Reader["sIdentificadorResumen"].ToString();
                                oObj.ResumenDiarioBVSunat.FechaGeneracionResumen = Reader["sFechaGeneracionResumen"].ToString();
                                oObj.ResumenDiarioBVSunat.VersionUBL = Reader["sVersionUBL"].ToString();
                                oObj.ResumenDiarioBVSunat.VersionEstructuraDocumento = Reader["sVersionEstructuraDocumento"].ToString();
                                //Detalle
                                oObj.Id = int.Parse(Reader["iNumeroOrdenItem"].ToString());
                                oObj.CodigoTipoDocumento = Reader["sCodigoTipoDocumento"].ToString();
                                oObj.SerieDocumento = Reader["sSerieDocumento"].ToString();
                                oObj.NumeracionDocumentoInicial = Reader["sNumeracionDocumentoInicial"].ToString();
                                oObj.NumeracionDocumentoFin = Reader["sNumeracionDocumentoFin"].ToString();
                                oObj.TotalValorVentaOperGravadas = Reader["sOperGravadas"].ToString();
                                oObj.CodigoTipoMontoRDTVVOperGravadas = Reader["sCodigoGravadas"].ToString();
                                oObj.TotalValorVentaOperInafectadas = Reader["sOperInafectadas"].ToString();
                                oObj.CodigoTipoMontoRDTVVOperInafectadas = Reader["sCodigoInafectadas"].ToString();
                                oObj.TotalValorVentaOperExoneradas = Reader["sOperExoneradas"].ToString();
                                oObj.CodigoTipoMontoRDTVVOperExoneradas = Reader["sCodigoExoneradas"].ToString();
                                oObj.TotalValorVentaOperGratuitas = Reader["sOperGratuitas"].ToString();
                                oObj.CodigoTipoMontoRDTVVOperGratuitas = Reader["sCodigoGratuitas"].ToString();

                                oObj.IndicadorSumatoriaOtrosCargos = Reader["sOtrosCargosItem"].ToString();
                                oObj.SumatoriaOtrosCargos = Reader["sSumatoriaOtrosCargos"].ToString();
                                oObj.SumatoriaIsc = Reader["sSumatoriaISC"].ToString();
                                oObj.SubTotalSumatoriaIsc = Reader["sSubTotalSumatoriaISC"].ToString();
                                oObj.CodigoSUNATTipoTributoIsc = Reader["sCodigoSUNATTipoTributoISC"].ToString();
                                oObj.NombreTipoTributoIsc = Reader["sNombreTipoTributoISC"].ToString();
                                oObj.CodigoUneceTipoTributoIsc = Reader["sCodigoUNECETipoTributoISC"].ToString();
                                oObj.SumatoriaIgv = Reader["sSumatoriaIGV"].ToString();
                                oObj.SubTotalSumatoriaIgv = Reader["sSubTotalSumatoriaIGV"].ToString();
                                oObj.CodigoSunatTipoTributoIgv = Reader["sCodigoSUNATTipoTributoIGV"].ToString();
                                oObj.NombreTipoTributoIgv = Reader["sNombreTipoTributoIGV"].ToString();
                                oObj.CodigoUneceTipoTributoIgv = Reader["sCodigoUNECETipoTributoIGV"].ToString();
                                oObj.ImporteTotalVenta = Reader["sImporteTotalVenta"].ToString();
                                oObj.Numeracion = Convert.ToInt32(Reader["iNumeracion"].ToString());
                                oObj.IdTipoDocumentoIdentidad = Convert.ToInt32(Reader["iID_TipoDocumentoIdentidad"].ToString());
                                oObj.NumeroDocumentoIdentidad = Reader["sNumeroDocumentoIdentidad"].ToString();
                                oObj.EstadoEnvio = Convert.ToInt32(Reader["iEstado"].ToString());
                                oObj.cpcSerieNumeracionComprobantePagoModificado = (Reader["sSerieNumeracionComprobantePagoModificado"].ToString());
                                oObj.cpcCodigoTipoDocumentoModificado = (Reader["sCodigoTipoDocumentoModificado"].ToString());
                                loObj.Add(oObj);
                            }
                            var serializador = new ESerializadorResumen();
                            string TramaXml = serializador.ArmarXML(loObj);

                            var fileName = loObj[0].ResumenDiarioBVSunat.NumeroDocumentoEmpresa + "-" + loObj[0].ResumenDiarioBVSunat.IdentificadorResumen + ".xml";
                            var zipName = loObj[0].ResumenDiarioBVSunat.NumeroDocumentoEmpresa + "-" + loObj[0].ResumenDiarioBVSunat.IdentificadorResumen + ".zip";

                            ruta = ruta + '/' + loObj[0].ResumenDiarioBVSunat.NumeroDocumentoEmpresa + "/" + loObj[0].ResumenDiarioBVSunat.FechaGeneracionResumen.Split('-')[0] + "/" + loObj[0].ResumenDiarioBVSunat.FechaGeneracionResumen.Split('-')[1];

                             System.IO.Directory.CreateDirectory(ruta);

                            System.IO.File.WriteAllBytes(ruta + fileName,
                                    GenerarXml(TramaXml));

                            FirmarXml(ruta + fileName, "Invoice", rutaServidor, claveCertificado);

                            using (ZipFile zip = new ZipFile())
                            {
                                zip.AddFile(ruta + fileName, "");
                                zip.Save(ruta + zipName);
                            }

                            SetQuery("FAC_ActulizarEstado");
                            CreateHelper(Connection, tran);
                            AddInParameter("@Id", IdVenta);
                            AddInParameter("@idempresa", empresa);
                            AddInParameter("@idSucursal", sucursal);
                            AddInParameter("@Usuario", usuario);
                            AddOutParameter("@Mensaje", (DbType)SqlDbType.VarChar);
                            ExecuteQuery();
                            string mnsj = GetOutput("@Mensaje").ToString();
                            if (!GetOutput("@Mensaje").ToString().Split('|')[0].Equals("success"))
                            {
                                throw new Exception();
                            }
                            else
                            {
                                File.Delete(ruta + fileName);
                            }
                        }

                    }

                    tran.Commit();
                    return smensaje;
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
        public byte[] GenerarXml(string xml)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true,
                Encoding = Encoding.GetEncoding("UTF-8")
            };

            MemoryStream ms = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(ms, xmlWriterSettings))
            {
                writer.WriteRaw(xml);
            }

            return StreamToByteArray(ms);
        }
        private string FirmarXml(string pathxml, String etiquetapadre, string ruta, string claveCertificado)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(pathxml);

            //var ruta = Server.MapPath("~/Certificado/");
            var DigestValue = "";
            var SignatureValue = "";

            //X509Certificate2 myCert = new X509Certificate2(ruta + "Certificado_Sunat_PFX_Comerc.deCombustiblesTriveño_20171018.pfx", "#DESW2ASDZX");

            X509Certificate2 myCert = new X509Certificate2(ruta, claveCertificado);
            RSACryptoServiceProvider rsaKey = (RSACryptoServiceProvider)myCert.PrivateKey;
            SignedXml signedXml = new SignedXml(xmlDoc);

            signedXml.SigningKey = rsaKey;

            Reference reference = new Reference();
            reference.Uri = "";
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);

            KeyInfo KeyInfo = new KeyInfo();
            X509Chain X509Chain = new X509Chain();
            X509Chain.Build(myCert);
            X509ChainElement local_element = X509Chain.ChainElements[0];
            KeyInfoX509Data x509Data = new KeyInfoX509Data(local_element.Certificate);
            String subjectName = local_element.Certificate.Subject;
            x509Data.AddSubjectName(subjectName);
            KeyInfo.AddClause(x509Data);

            signedXml.KeyInfo = KeyInfo;

            signedXml.ComputeSignature();

            XmlElement xmlDigitalSignature = signedXml.GetXml();

            xmlDigitalSignature.Prefix = "ds";
            signedXml.ComputeSignature();
            foreach (XmlNode node in xmlDigitalSignature.SelectNodes("descendant-or-self::*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#']"))
            {
                if (node.LocalName == "Signature")
                {
                    XmlAttribute newAtribute = xmlDoc.CreateAttribute("Id");
                    newAtribute.Value = "SignatureSP";
                    node.Attributes.Append(newAtribute);
                }
            }

            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            string l_xpath = "";

            if (pathxml.Contains("-01-")) //factura
            {
                nsMgr.AddNamespace("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
                nsMgr.AddNamespace("ccts", "urn:un:unece:uncefact:documentation:2");
                nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");

                nsMgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                nsMgr.AddNamespace("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
                nsMgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
                nsMgr.AddNamespace("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
                nsMgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                nsMgr.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
                //l_xpath = "/tns:Invoice/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent";
                l_xpath = "/tns:Invoice/ext:UBLExtensions/ext:UBLExtension[2]/ext:ExtensionContent";
            }
            if (pathxml.Contains("-03-")) //BOLETA
            {
                nsMgr.AddNamespace("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
                nsMgr.AddNamespace("ccts", "urn:un:unece:uncefact:documentation:2");
                nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");

                nsMgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                nsMgr.AddNamespace("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
                nsMgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
                nsMgr.AddNamespace("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
                nsMgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                nsMgr.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");

                l_xpath = "/tns:Invoice/ext:UBLExtensions/ext:UBLExtension[2]/ext:ExtensionContent";
            }
            if (pathxml.Contains("-07-")) //nota de credito
            {
                nsMgr.AddNamespace("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
                nsMgr.AddNamespace("ccts", "urn:un:unece:uncefact:documentation:2");
                nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2");

                nsMgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                nsMgr.AddNamespace("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
                nsMgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
                nsMgr.AddNamespace("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
                nsMgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                nsMgr.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");

                l_xpath = "/tns:CreditNote/ext:UBLExtensions/ext:UBLExtension[2]/ext:ExtensionContent";
            }
            if (pathxml.Contains("-08-"))//nota de d�bito
            {
                nsMgr.AddNamespace("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
                nsMgr.AddNamespace("ccts", "urn:un:unece:uncefact:documentation:2");
                nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:DebitNote-2");

                nsMgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                nsMgr.AddNamespace("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
                nsMgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
                nsMgr.AddNamespace("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
                nsMgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                nsMgr.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");

                l_xpath = "/tns:DebitNote/ext:UBLExtensions/ext:UBLExtension[2]/ext:ExtensionContent";
            }
            if (pathxml.Contains("RA")) // documento de baja
            {
                nsMgr.AddNamespace("tns", "urn:sunat:names:specification:ubl:peru:schema:xsd:VoidedDocuments-1");
                nsMgr.AddNamespace("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
                nsMgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                nsMgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
                nsMgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                nsMgr.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");

                l_xpath = "/tns:VoidedDocuments/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent";
            }
            if (pathxml.Contains("RC"))// documento de revision de boleta
            {
                nsMgr.AddNamespace("tns", "urn:sunat:names:specification:ubl:peru:schema:xsd:SummaryDocuments-1");
                nsMgr.AddNamespace("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
                nsMgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                nsMgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
                nsMgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                nsMgr.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");

                l_xpath = "/tns:SummaryDocuments/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent";
            }

            XmlNode counterSignature = xmlDoc.SelectSingleNode(l_xpath, nsMgr);
            counterSignature.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

            XmlNodeList elemList = xmlDoc.GetElementsByTagName("DigestValue");
            for (int i = 0; i < elemList.Count; i++)
            {
                DigestValue = elemList[i].InnerXml;
            }

            XmlNodeList elemList2 = xmlDoc.GetElementsByTagName("SignatureValue");
            for (int i = 0; i < elemList2.Count; i++)
            {
                SignatureValue = elemList2[i].InnerXml;
            }
            xmlDoc.Save(pathxml);
            return DigestValue + "|" + SignatureValue;
        }

        private byte[] StreamToByteArray(Stream inputStream)
        {
            if (!inputStream.CanRead)
            {
                throw new ArgumentException();
            }

            if (inputStream.CanSeek)
            {
                inputStream.Seek(0, SeekOrigin.Begin);
            }

            byte[] output = new byte[inputStream.Length];
            int bytesRead = inputStream.Read(output, 0, output.Length);
            return output;
        }
    }
}
