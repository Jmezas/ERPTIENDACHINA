using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace SIS.Entity
{
   public class ESerializadorResumen
    {
        public string ArmarXML(List<EResumenDiarioBVDetalleSunat> loObj)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", "no");
            doc.AppendChild(docNode);

            XmlNode Invoice = doc.CreateElement("SummaryDocuments");
            doc.AppendChild(Invoice);

            //----------------------------------------------------------
            //                  Invoice Attributes
            //----------------------------------------------------------

            XmlAttribute xmlns = doc.CreateAttribute("xmlns");
            xmlns.Value = "urn:sunat:names:specification:ubl:peru:schema:xsd:SummaryDocuments-1";
            Invoice.Attributes.Append(xmlns);

            XmlAttribute cac = doc.CreateAttribute("xmlns:cac");
            cac.Value = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
            Invoice.Attributes.Append(cac);

            XmlAttribute cbc = doc.CreateAttribute("xmlns:cbc");
            cbc.Value = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
            Invoice.Attributes.Append(cbc);

            XmlAttribute ds = doc.CreateAttribute("xmlns:ds");
            ds.Value = "http://www.w3.org/2000/09/xmldsig#";
            Invoice.Attributes.Append(ds);

            XmlAttribute ext = doc.CreateAttribute("xmlns:ext");
            ext.Value = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";
            Invoice.Attributes.Append(ext);

            XmlAttribute sac = doc.CreateAttribute("xmlns:sac");
            sac.Value = "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1";
            Invoice.Attributes.Append(sac);

            XmlAttribute xsi = doc.CreateAttribute("xmlns:xsi");
            xsi.Value = "http://www.w3.org/2001/XMLSchema-instance";
            Invoice.Attributes.Append(xsi);


            //----------------------------------------------------------
            //                / Invoice Attributes
            //----------------------------------------------------------

            XmlNode UBLExtensions = doc.CreateElement("ext:UBLExtensions", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            Invoice.AppendChild(UBLExtensions);

            XmlNode UBLExtension0 = doc.CreateElement("ext:UBLExtension", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            UBLExtensions.AppendChild(UBLExtension0);

            var nodoExtension = doc.CreateElement("ext:ExtensionContent", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            UBLExtension0.AppendChild(nodoExtension);

            XmlNode UBLVersionID = doc.CreateElement("cbc:UBLVersionID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            UBLVersionID.InnerText = loObj[0].ResumenDiarioBVSunat.VersionUBL;
            Invoice.AppendChild(UBLVersionID);

            XmlNode CustomizationID = doc.CreateElement("cbc:CustomizationID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            CustomizationID.InnerText = loObj[0].ResumenDiarioBVSunat.VersionEstructuraDocumento;
            Invoice.AppendChild(CustomizationID);

            XmlNode ID = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            ID.InnerText = loObj[0].ResumenDiarioBVSunat.IdentificadorResumen;
            Invoice.AppendChild(ID);

            XmlNode ReferenceDate = doc.CreateElement("cbc:ReferenceDate", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            ReferenceDate.InnerText = loObj[0].ResumenDiarioBVSunat.FechaEmisionDocumento;
            Invoice.AppendChild(ReferenceDate);

            XmlNode IssueDate = doc.CreateElement("cbc:IssueDate", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            IssueDate.InnerText = loObj[0].ResumenDiarioBVSunat.FechaGeneracionResumen;
            Invoice.AppendChild(IssueDate);

            XmlNode Signature = doc.CreateElement("cac:Signature", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            Invoice.AppendChild(Signature);

            XmlNode IdSignature = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            IdSignature.InnerText = "IDSignCCS";
            Signature.AppendChild(IdSignature);

            XmlNode SignatoryParty = doc.CreateElement("cac:SignatoryParty", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            Signature.AppendChild(SignatoryParty);

            XmlNode PartyIdentification = doc.CreateElement("cac:PartyIdentification", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            SignatoryParty.AppendChild(PartyIdentification);

            XmlNode IdPartyIdentification = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            IdPartyIdentification.InnerText = loObj[0].ResumenDiarioBVSunat.NumeroDocumentoEmpresa;
            PartyIdentification.AppendChild(IdPartyIdentification);

            XmlNode PartyName0 = doc.CreateElement("cac:PartyName", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            SignatoryParty.AppendChild(PartyName0);

            XmlNode Name0 = doc.CreateElement("cbc:Name", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            var cdataRazonSoc = doc.CreateCDataSection(loObj[0].ResumenDiarioBVSunat.RazonSocialEmpresa);
            Name0.AppendChild(cdataRazonSoc);
            PartyName0.AppendChild(Name0);

            XmlNode DigitalSignatureAttachment = doc.CreateElement("cac:DigitalSignatureAttachment", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            Signature.AppendChild(DigitalSignatureAttachment);

            XmlNode ExternalReference = doc.CreateElement("cac:ExternalReference", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            DigitalSignatureAttachment.AppendChild(ExternalReference);

            XmlNode URI1 = doc.CreateElement("cbc:URI", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            URI1.InnerText = "SignatureSP";
            ExternalReference.AppendChild(URI1);

            XmlNode AccountingSupplierParty = doc.CreateElement("cac:AccountingSupplierParty", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            Invoice.AppendChild(AccountingSupplierParty);

            XmlNode CustomerAssignedAccountID = doc.CreateElement("cbc:CustomerAssignedAccountID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            CustomerAssignedAccountID.InnerText = loObj[0].ResumenDiarioBVSunat.NumeroDocumentoEmpresa;
            AccountingSupplierParty.AppendChild(CustomerAssignedAccountID);

            XmlNode AdditionalAccountID = doc.CreateElement("cbc:AdditionalAccountID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            AdditionalAccountID.InnerText = loObj[0].ResumenDiarioBVSunat.CodigoDocumentoEmpresa;
            AccountingSupplierParty.AppendChild(AdditionalAccountID);

            XmlNode Party = doc.CreateElement("cac:Party", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            AccountingSupplierParty.AppendChild(Party);

            XmlNode PartyLegalEntity = doc.CreateElement("cac:PartyLegalEntity", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            Party.AppendChild(PartyLegalEntity);

            XmlNode RegistrationName = doc.CreateElement("cbc:RegistrationName", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            var cdataRazonSoc2 = doc.CreateCDataSection(loObj[0].ResumenDiarioBVSunat.RazonSocialEmpresa);
            RegistrationName.AppendChild(cdataRazonSoc2);
            PartyLegalEntity.AppendChild(RegistrationName);


            foreach (EResumenDiarioBVDetalleSunat det in loObj)
            {
                XmlNode InvoiceLine = doc.CreateElement("sac:SummaryDocumentsLine", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
                Invoice.AppendChild(InvoiceLine);

                XmlNode IDI = doc.CreateElement("cbc:LineID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                IDI.InnerText = det.Id.ToString();
                InvoiceLine.AppendChild(IDI);

                XmlNode DocumentTypeCode = doc.CreateElement("cbc:DocumentTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                DocumentTypeCode.InnerText = det.CodigoTipoDocumento;
                InvoiceLine.AppendChild(DocumentTypeCode);

                //nuevo
                XmlNode SDLID = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                SDLID.InnerText = det.SerieDocumento + "-" + det.Numeracion;
                InvoiceLine.AppendChild(SDLID); 
 
                //nuevo
                XmlNode SDLAccountingCustomerParty = doc.CreateElement("cac:AccountingCustomerParty", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                InvoiceLine.AppendChild(SDLAccountingCustomerParty);
                //nuevo
                XmlNode SDLCustomerAssignedAccountID = doc.CreateElement("cbc:CustomerAssignedAccountID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                SDLCustomerAssignedAccountID.InnerText = det.NumeroDocumentoIdentidad;
                SDLAccountingCustomerParty.AppendChild(SDLCustomerAssignedAccountID);
                //nuevo
                XmlNode SDLAdditionalAccountID = doc.CreateElement("cbc:AdditionalAccountID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                SDLAdditionalAccountID.InnerText = det.IdTipoDocumentoIdentidad.ToString();
                SDLAccountingCustomerParty.AppendChild(SDLAdditionalAccountID);

                //Nuevo para los dos campos de NC y ND
                //nuevo
                if (det.CodigoTipoDocumento == "07" || det.CodigoTipoDocumento == "08")
                {
                    XmlNode Billing = doc.CreateElement("cac:BillingReference", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                    InvoiceLine.AppendChild(Billing);

                    XmlNode InvoiceDocument = doc.CreateElement("cac:InvoiceDocumentReference", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                    InvoiceLine.AppendChild(InvoiceDocument);
                    Billing.AppendChild(InvoiceDocument);

                    XmlNode IDNC = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    IDNC.InnerText = det.cpcSerieNumeracionComprobantePagoModificado.ToString();
                    InvoiceDocument.AppendChild(IDNC);
                    //Billing.AppendChild(IDNC);

                    XmlNode DocumentTypeCodeNC = doc.CreateElement("cbc:DocumentTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    DocumentTypeCodeNC.InnerText = det.cpcCodigoTipoDocumentoModificado.ToString();
                    InvoiceDocument.AppendChild(DocumentTypeCodeNC);
                    //Billing.AppendChild(DocumentTypeCodeNC);
                }


                //nuevo
                XmlNode SDLStatus = doc.CreateElement("cac:Status", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                InvoiceLine.AppendChild(SDLStatus);
                //nuevo
                XmlNode SDLConditionCode = doc.CreateElement("cbc:ConditionCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                SDLConditionCode.InnerText = det.EstadoEnvio.ToString();
                SDLStatus.AppendChild(SDLConditionCode);


                XmlNode TotalAmount = doc.CreateElement("sac:TotalAmount", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
                XmlAttribute currencyID100 = doc.CreateAttribute("currencyID");
                currencyID100.Value = "PEN";
                TotalAmount.Attributes.Append(currencyID100);
                TotalAmount.InnerText = det.ImporteTotalVenta;
                InvoiceLine.AppendChild(TotalAmount);

                if (det.TotalValorVentaOperGravadas != "0.00")
                {
                    XmlNode BillingPayment0 = doc.CreateElement("sac:BillingPayment", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
                    InvoiceLine.AppendChild(BillingPayment0);

                    XmlNode PaidAmount0 = doc.CreateElement("cbc:PaidAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    PaidAmount0.InnerText = det.TotalValorVentaOperGravadas;
                    XmlAttribute currencyID101 = doc.CreateAttribute("currencyID");
                    currencyID101.Value = "PEN";
                    PaidAmount0.Attributes.Append(currencyID101);
                    BillingPayment0.AppendChild(PaidAmount0);

                    XmlNode InstructionID0 = doc.CreateElement("cbc:InstructionID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    InstructionID0.InnerText = det.CodigoTipoMontoRDTVVOperGravadas;
                    BillingPayment0.AppendChild(InstructionID0);
                }

                if (det.TotalValorVentaOperInafectadas != "0.00")
                {
                    XmlNode BillingPayment1 = doc.CreateElement("sac:BillingPayment", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
                    InvoiceLine.AppendChild(BillingPayment1);

                    XmlNode PaidAmount1 = doc.CreateElement("cbc:PaidAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    XmlAttribute currencyID102 = doc.CreateAttribute("currencyID");
                    currencyID102.Value = "PEN";
                    PaidAmount1.Attributes.Append(currencyID102);
                    PaidAmount1.InnerText = det.TotalValorVentaOperInafectadas;
                    BillingPayment1.AppendChild(PaidAmount1);

                    XmlNode InstructionID1 = doc.CreateElement("cbc:InstructionID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    InstructionID1.InnerText = det.CodigoTipoMontoRDTVVOperInafectadas;
                    BillingPayment1.AppendChild(InstructionID1);
                }

                if (det.TotalValorVentaOperExoneradas != "0.00")
                {
                    XmlNode BillingPayment2 = doc.CreateElement("sac:BillingPayment", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
                    InvoiceLine.AppendChild(BillingPayment2);

                    XmlNode PaidAmount2 = doc.CreateElement("cbc:PaidAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    XmlAttribute currencyID103 = doc.CreateAttribute("currencyID");
                    currencyID103.Value = "PEN";
                    PaidAmount2.Attributes.Append(currencyID103);
                    PaidAmount2.InnerText = det.TotalValorVentaOperExoneradas;
                    BillingPayment2.AppendChild(PaidAmount2);

                    XmlNode InstructionID2 = doc.CreateElement("cbc:InstructionID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    InstructionID2.InnerText = det.CodigoTipoMontoRDTVVOperExoneradas;
                    BillingPayment2.AppendChild(InstructionID2);
                }

                if (det.TotalValorVentaOperGratuitas != "0.00")
                {
                    XmlNode BillingPayment3 = doc.CreateElement("sac:BillingPayment", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
                    InvoiceLine.AppendChild(BillingPayment3);

                    XmlNode PaidAmount3 = doc.CreateElement("cbc:PaidAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    XmlAttribute currencyID104 = doc.CreateAttribute("currencyID");
                    currencyID104.Value = "PEN";
                    PaidAmount3.Attributes.Append(currencyID104);
                    PaidAmount3.InnerText = det.TotalValorVentaOperGratuitas;
                    BillingPayment3.AppendChild(PaidAmount3);

                    XmlNode InstructionID3 = doc.CreateElement("cbc:InstructionID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    InstructionID3.InnerText = det.CodigoTipoMontoRDTVVOperGratuitas;
                    BillingPayment3.AppendChild(InstructionID3);
                }

                if (det.SumatoriaOtrosCargos != "0.00")
                {
                    XmlNode Allowancecharge = doc.CreateElement("cac:AllowanceCharge", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                    InvoiceLine.AppendChild(Allowancecharge);

                    XmlNode ChargeIndicator = doc.CreateElement("cbc:ChargeIndicator", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    ChargeIndicator.InnerText = det.IndicadorSumatoriaOtrosCargos;
                    Allowancecharge.AppendChild(ChargeIndicator);

                    XmlNode Amount = doc.CreateElement("cbc:Amount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    XmlAttribute currencyID98 = doc.CreateAttribute("currencyID");
                    currencyID98.Value = "PEN";
                    Amount.Attributes.Append(currencyID98);
                    Amount.InnerText = det.SumatoriaOtrosCargos;
                    Allowancecharge.AppendChild(Amount);
                }

                XmlNode TaxTotalIs = doc.CreateElement("cac:TaxTotal", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                InvoiceLine.AppendChild(TaxTotalIs);

                XmlNode TaxAmountIs = doc.CreateElement("cbc:TaxAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID91 = doc.CreateAttribute("currencyID");
                currencyID91.Value = "PEN";
                TaxAmountIs.Attributes.Append(currencyID91);
                TaxAmountIs.InnerText = det.SumatoriaIsc;
                TaxTotalIs.AppendChild(TaxAmountIs);

                XmlNode TaxSubtotalIs = doc.CreateElement("cac:TaxSubtotal", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxTotalIs.AppendChild(TaxSubtotalIs);

                XmlNode TaxAmountSubIs = doc.CreateElement("cbc:TaxAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID109 = doc.CreateAttribute("currencyID");
                currencyID109.Value = "PEN";
                TaxAmountSubIs.Attributes.Append(currencyID109);
                TaxAmountSubIs.InnerText = det.SubTotalSumatoriaIsc;
                TaxSubtotalIs.AppendChild(TaxAmountSubIs);

                XmlNode TaxCategoryIs = doc.CreateElement("cac:TaxCategory", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxSubtotalIs.AppendChild(TaxCategoryIs);

                XmlNode TaxSchemeIs = doc.CreateElement("cac:TaxScheme", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxCategoryIs.AppendChild(TaxSchemeIs);


                XmlNode IdTaxIs = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                IdTaxIs.InnerText = det.CodigoSUNATTipoTributoIsc;
                TaxSchemeIs.AppendChild(IdTaxIs);

                XmlNode NameTaxIs = doc.CreateElement("cbc:Name", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                NameTaxIs.InnerText = det.NombreTipoTributoIsc;
                TaxSchemeIs.AppendChild(NameTaxIs);

                XmlNode TaxTypeCodeIs = doc.CreateElement("cbc:TaxTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                TaxTypeCodeIs.InnerText = det.CodigoUneceTipoTributoIsc;
                TaxSchemeIs.AppendChild(TaxTypeCodeIs);


                XmlNode TaxTotalI = doc.CreateElement("cac:TaxTotal", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                InvoiceLine.AppendChild(TaxTotalI);

                XmlNode TaxAmountI = doc.CreateElement("cbc:TaxAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID9 = doc.CreateAttribute("currencyID");
                currencyID9.Value = "PEN";
                TaxAmountI.Attributes.Append(currencyID9);
                TaxAmountI.InnerText = det.SumatoriaIgv;
                TaxTotalI.AppendChild(TaxAmountI);

                XmlNode TaxSubtotalI = doc.CreateElement("cac:TaxSubtotal", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxTotalI.AppendChild(TaxSubtotalI);

                XmlNode TaxAmountSubI = doc.CreateElement("cbc:TaxAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID10 = doc.CreateAttribute("currencyID");
                currencyID10.Value = "PEN";
                TaxAmountSubI.Attributes.Append(currencyID10);
                TaxAmountSubI.InnerText = det.SubTotalSumatoriaIgv;
                TaxSubtotalI.AppendChild(TaxAmountSubI);

                XmlNode TaxCategoryI = doc.CreateElement("cac:TaxCategory", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxSubtotalI.AppendChild(TaxCategoryI);

                XmlNode TaxSchemeI = doc.CreateElement("cac:TaxScheme", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxCategoryI.AppendChild(TaxSchemeI);


                XmlNode IdTaxI = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                IdTaxI.InnerText = det.CodigoSunatTipoTributoIgv;
                TaxSchemeI.AppendChild(IdTaxI);

                XmlNode NameTaxI = doc.CreateElement("cbc:Name", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                NameTaxI.InnerText = det.NombreTipoTributoIgv;
                TaxSchemeI.AppendChild(NameTaxI);

                XmlNode TaxTypeCodeI = doc.CreateElement("cbc:TaxTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                TaxTypeCodeI.InnerText = det.CodigoUneceTipoTributoIgv;
                TaxSchemeI.AppendChild(TaxTypeCodeI); 
            }
             
            return doc.InnerXml.ToString();

        }
    }
}
