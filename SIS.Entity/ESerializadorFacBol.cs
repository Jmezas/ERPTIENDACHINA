using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace SIS.Entity
{
    public class ESerializadorFacBol
    {
        public string ArmarXML(List<EFactBolSunatDetalle> loObj)
        {

            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", "no");
            doc.AppendChild(docNode);

            XmlNode Invoice = doc.CreateElement("Invoice");
            doc.AppendChild(Invoice);

            //----------------------------------------------------------
            //                  Invoice Attributes
            //----------------------------------------------------------

            XmlAttribute xmlns = doc.CreateAttribute("xmlns");
            xmlns.Value = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
            Invoice.Attributes.Append(xmlns);

            XmlAttribute cac = doc.CreateAttribute("xmlns:cac");
            cac.Value = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
            Invoice.Attributes.Append(cac);

            XmlAttribute cbc = doc.CreateAttribute("xmlns:cbc");
            cbc.Value = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
            Invoice.Attributes.Append(cbc);

            XmlAttribute ccts = doc.CreateAttribute("xmlns:ccts");
            ccts.Value = "urn:un:unece:uncefact:documentation:2";
            Invoice.Attributes.Append(ccts);

            XmlAttribute ds = doc.CreateAttribute("xmlns:ds");
            ds.Value = "http://www.w3.org/2000/09/xmldsig#";
            Invoice.Attributes.Append(ds);

            XmlAttribute ext = doc.CreateAttribute("xmlns:ext");
            ext.Value = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";
            Invoice.Attributes.Append(ext);

            XmlAttribute qdt = doc.CreateAttribute("xmlns:qdt");
            qdt.Value = "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2";
            Invoice.Attributes.Append(qdt);

            XmlAttribute sac = doc.CreateAttribute("xmlns:sac");
            sac.Value = "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1";
            Invoice.Attributes.Append(sac);

            XmlAttribute udt = doc.CreateAttribute("xmlns:udt");
            udt.Value = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2";
            Invoice.Attributes.Append(udt);

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


            XmlNode UBLExtension1 = doc.CreateElement("ext:UBLExtension", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            UBLExtensions.AppendChild(UBLExtension1);

            var nodoExtension1 = doc.CreateElement("ext:ExtensionContent", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            UBLExtension1.AppendChild(nodoExtension1);

            #region Datos Adicionales WolfMax
            XmlNode loAdditionalWolfMaxInformation = doc.CreateElement("sac:DatosAdicionalesWolfMax", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
            nodoExtension.AppendChild(loAdditionalWolfMaxInformation);

            XmlNode loSucursalNombre = doc.CreateElement("cbc:sucursalNombre", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            // Agregar nombre de sucursal
            loSucursalNombre.InnerText = loObj[0].FactBolSunat.SucursalNombre;
            loAdditionalWolfMaxInformation.AppendChild(loSucursalNombre);

            XmlNode loSucursalDireccion = doc.CreateElement("cbc:sucursalDireccion", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            // Agregar direccion de sucursal
            loSucursalDireccion.InnerText = loObj[0].FactBolSunat.SucursalDireccion;
            loAdditionalWolfMaxInformation.AppendChild(loSucursalDireccion);

            XmlNode loObservaciones = doc.CreateElement("cbc:observaciones", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            // Agregar observaciones
            loObservaciones.InnerText = loObj[0].FactBolSunat.Observaciones;
            loAdditionalWolfMaxInformation.AppendChild(loObservaciones);

            XmlNode loPlaca = doc.CreateElement("cbc:placa", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            // Agregar placa
            loPlaca.InnerText = loObj[0].FactBolSunat.Placa;
            loAdditionalWolfMaxInformation.AppendChild(loPlaca);

            XmlNode loVendedor = doc.CreateElement("cbc:vendedor", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            // Agregar vendedor
            loVendedor.InnerText = loObj[0].FactBolSunat.Vendedor;
            loAdditionalWolfMaxInformation.AppendChild(loVendedor);

            XmlNode loPaymentDueDate = doc.CreateElement("cbc:PaymentDueDate", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            // Agregar fecha de vencimiento
            loPaymentDueDate.InnerText = loObj[0].FactBolSunat.FechaEmision;
            loAdditionalWolfMaxInformation.AppendChild(loPaymentDueDate);

            XmlNode loCondicionPago = doc.CreateElement("cbc:condicionPago", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            // Agregar condicion de pago
            loCondicionPago.InnerText = loObj[0].FactBolSunat.CondicionPago;
            loAdditionalWolfMaxInformation.AppendChild(loCondicionPago);

            XmlNode loEmisorDireccion = doc.CreateElement("cbc:emisorDireccion", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            loEmisorDireccion.InnerText = loObj[0].FactBolSunat.DomicilioFiscalDireccion;
            loAdditionalWolfMaxInformation.AppendChild(loEmisorDireccion);

            XmlNode loClienteDireccion = doc.CreateElement("cbc:clienteDireccion", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            loClienteDireccion.InnerText = loObj[0].FactBolSunat.DireccionCliente;
            loAdditionalWolfMaxInformation.AppendChild(loClienteDireccion);
            #endregion

            XmlNode UBLVersionID = doc.CreateElement("cbc:UBLVersionID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            UBLVersionID.InnerText = loObj[0].FactBolSunat.VersionUBL;
            Invoice.AppendChild(UBLVersionID);

            XmlNode CustomizationID = doc.CreateElement("cbc:CustomizationID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            CustomizationID.InnerText = loObj[0].FactBolSunat.VersionEstructuraDocumento;
            Invoice.AppendChild(CustomizationID);

            XmlNode ID = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            ID.InnerText = loObj[0].FactBolSunat.SerieNumeroDocumento;
            Invoice.AppendChild(ID);

            XmlNode IssueDate = doc.CreateElement("cbc:IssueDate", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            IssueDate.InnerText = loObj[0].FactBolSunat.FechaEmision;
            Invoice.AppendChild(IssueDate);

            XmlNode IssueTime = doc.CreateElement("cbc:IssueTime", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            IssueTime.InnerText = loObj[0].FactBolSunat.HoraEmision;
            Invoice.AppendChild(IssueTime);

            XmlNode InvoiceTypeCode = doc.CreateElement("cbc:InvoiceTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

            XmlAttribute listIDTC = doc.CreateAttribute("listID");
            listIDTC.Value = "0101";
            InvoiceTypeCode.Attributes.Append(listIDTC);

            XmlAttribute listAgencyNameDC = doc.CreateAttribute("listAgencyName");
            listAgencyNameDC.Value = "PE:SUNAT";
            InvoiceTypeCode.Attributes.Append(listAgencyNameDC);

            XmlAttribute listNameDC = doc.CreateAttribute("listName");
            listNameDC.Value = "SUNAT:Identificador de Tipo de Documento";
            InvoiceTypeCode.Attributes.Append(listNameDC);

            XmlAttribute listURIDC = doc.CreateAttribute("listURI");
            listURIDC.Value = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo01";
            InvoiceTypeCode.Attributes.Append(listURIDC);

            InvoiceTypeCode.InnerText = loObj[0].FactBolSunat.CodigoTipoDocumento;
            Invoice.AppendChild(InvoiceTypeCode);

            XmlNode DocumentCurrencyCode = doc.CreateElement("cbc:DocumentCurrencyCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

            XmlAttribute listIDDC = doc.CreateAttribute("listID");
            listIDDC.Value = "ISO 4217 Alpha";
            DocumentCurrencyCode.Attributes.Append(listIDDC);

            XmlAttribute listNameDC1 = doc.CreateAttribute("listName");
            listNameDC1.Value = "Currency";
            DocumentCurrencyCode.Attributes.Append(listNameDC1);

            XmlAttribute listAgencyNameDC1 = doc.CreateAttribute("listAgencyName");
            listAgencyNameDC1.Value = "United Nations Economic Commission for Europe";
            DocumentCurrencyCode.Attributes.Append(listAgencyNameDC1);

            DocumentCurrencyCode.InnerText = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
            Invoice.AppendChild(DocumentCurrencyCode);

            XmlNode LineCountNumeric = doc.CreateElement("cbc:LineCountNumeric", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            LineCountNumeric.InnerText = loObj.Count.ToString();
            Invoice.AppendChild(LineCountNumeric);

            if (loObj[0].FactBolSunat.SerieNumeroAnticipo.Length > 0)
            {
                XmlNode AdditionalDocumentReference = doc.CreateElement("cac:AdditionalDocumentReference", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                Invoice.AppendChild(AdditionalDocumentReference);

                XmlNode IDAdditionalDocumentReference = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute schemeIDAdditionalDocumentReference = doc.CreateAttribute("schemeID");
                schemeIDAdditionalDocumentReference.Value = "01";
                IDAdditionalDocumentReference.Attributes.Append(schemeIDAdditionalDocumentReference);

                IDAdditionalDocumentReference.InnerText = loObj[0].FactBolSunat.SerieNumeroAnticipo;

                AdditionalDocumentReference.AppendChild(IDAdditionalDocumentReference);

                XmlNode DocumentTypeCode = doc.CreateElement("cbc:DocumentTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute listName = doc.CreateAttribute("listName");
                listName.Value = "Documento Relacionado";
                DocumentTypeCode.Attributes.Append(listName);

                XmlAttribute listAgencyName = doc.CreateAttribute("listAgencyName");
                listAgencyName.Value = "PE:SUNAT";
                DocumentTypeCode.Attributes.Append(listAgencyName);

                XmlAttribute listURI = doc.CreateAttribute("listURI");
                listURI.Value = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo12";
                DocumentTypeCode.Attributes.Append(listURI);

                DocumentTypeCode.InnerText = "02";
                AdditionalDocumentReference.AppendChild(DocumentTypeCode);

                XmlNode DocumentType = doc.CreateElement("cbc:DocumentType", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                DocumentType.InnerText = "ANTICIPO";
                AdditionalDocumentReference.AppendChild(DocumentType);

                XmlNode DocumentStatusCode = doc.CreateElement("cbc:DocumentStatusCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute listNameDocumentStatusCode = doc.CreateAttribute("listName");
                listNameDocumentStatusCode.Value = "Anticipo";
                DocumentStatusCode.Attributes.Append(listNameDocumentStatusCode);

                XmlAttribute listAgencyNameDocumentStatusCode = doc.CreateAttribute("listAgencyName");
                listAgencyNameDocumentStatusCode.Value = "PE:SUNAT";
                DocumentStatusCode.Attributes.Append(listAgencyNameDocumentStatusCode);

                DocumentStatusCode.InnerText = "1";
                AdditionalDocumentReference.AppendChild(DocumentStatusCode);

                XmlNode IssuerParty = doc.CreateElement("cac:IssuerParty", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                AdditionalDocumentReference.AppendChild(IssuerParty);

                XmlNode PartyIdentificationAD = doc.CreateElement("cac:PartyIdentification", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                IssuerParty.AppendChild(PartyIdentificationAD);


                XmlNode IDPartyIdentificationAD = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute schemeAgencyNamePAD = doc.CreateAttribute("schemeAgencyName");
                schemeAgencyNamePAD.Value = "PE:SUNAT";
                IDPartyIdentificationAD.Attributes.Append(schemeAgencyNamePAD);

                XmlAttribute schemeIDPAD = doc.CreateAttribute("schemeID");
                schemeIDPAD.Value = "6";
                IDPartyIdentificationAD.Attributes.Append(schemeIDPAD);

                XmlAttribute schemeNamePAD = doc.CreateAttribute("schemeName");
                schemeNamePAD.Value = "Documento de Identidad";
                IDPartyIdentificationAD.Attributes.Append(schemeNamePAD);

                XmlAttribute schemeURIPAD = doc.CreateAttribute("schemeURI");
                schemeURIPAD.Value = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
                IDPartyIdentificationAD.Attributes.Append(schemeURIPAD);

                IDPartyIdentificationAD.InnerText = loObj[0].FactBolSunat.NumeroDocumentoEmpresa;
                PartyIdentificationAD.AppendChild(IDPartyIdentificationAD);
            }

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
            IdPartyIdentification.InnerText = loObj[0].FactBolSunat.NumeroDocumentoEmpresa;
            PartyIdentification.AppendChild(IdPartyIdentification);

            XmlNode PartyName0 = doc.CreateElement("cac:PartyName", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            SignatoryParty.AppendChild(PartyName0);

            XmlNode Name0 = doc.CreateElement("cbc:Name", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            var cdataRazonSoc = doc.CreateCDataSection(loObj[0].FactBolSunat.RazonSocialEmpresa);
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


            XmlNode Party = doc.CreateElement("cac:Party", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            AccountingSupplierParty.AppendChild(Party);

            XmlNode PartyIdentification1 = doc.CreateElement("cac:PartyIdentification", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            Party.AppendChild(PartyIdentification1);

            XmlNode IdPartyIdentification1 = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

            XmlAttribute schemeID3 = doc.CreateAttribute("schemeID");
            schemeID3.Value = loObj[0].FactBolSunat.CodigoDocumentoEmpresa;
            IdPartyIdentification1.Attributes.Append(schemeID3);

            XmlAttribute schemeName3 = doc.CreateAttribute("schemeName");
            schemeName3.Value = "SUNAT:Identificador de Documento de Identidad";
            IdPartyIdentification1.Attributes.Append(schemeName3);

            XmlAttribute schemeAgencyName3 = doc.CreateAttribute("schemeAgencyName");
            schemeAgencyName3.Value = "PE:SUNAT";
            IdPartyIdentification1.Attributes.Append(schemeAgencyName3);

            XmlAttribute schemeURI3 = doc.CreateAttribute("schemeURI");
            schemeURI3.Value = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            IdPartyIdentification1.Attributes.Append(schemeURI3);


            IdPartyIdentification1.InnerText = loObj[0].FactBolSunat.NumeroDocumentoEmpresa;
            PartyIdentification1.AppendChild(IdPartyIdentification1);


            XmlNode PartyName = doc.CreateElement("cac:PartyName", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            Party.AppendChild(PartyName);

            XmlNode Name = doc.CreateElement("cbc:Name", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            var cdataRazonSoc0 = doc.CreateCDataSection(loObj[0].FactBolSunat.RazonSocialEmpresa);
            Name.AppendChild(cdataRazonSoc0);
            PartyName.AppendChild(Name);

            XmlNode PartyLegalEntity = doc.CreateElement("cac:PartyLegalEntity", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            Party.AppendChild(PartyLegalEntity);

            XmlNode RegistrationName1 = doc.CreateElement("cbc:RegistrationName", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            var cRegistrationName1 = doc.CreateCDataSection(loObj[0].FactBolSunat.RazonSocialEmpresa);
            RegistrationName1.AppendChild(cRegistrationName1);
            PartyLegalEntity.AppendChild(RegistrationName1);

            XmlNode RegistrationAddress1 = doc.CreateElement("cac:RegistrationAddress", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            PartyLegalEntity.AppendChild(RegistrationAddress1);

            XmlNode AddressTypeCode1 = doc.CreateElement("cbc:AddressTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            AddressTypeCode1.InnerText = "0000";
            RegistrationAddress1.AppendChild(AddressTypeCode1);



            XmlNode AccountingCustomerParty = doc.CreateElement("cac:AccountingCustomerParty", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            Invoice.AppendChild(AccountingCustomerParty);


            XmlNode ClienteParty = doc.CreateElement("cac:Party", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            AccountingCustomerParty.AppendChild(ClienteParty);


            XmlNode PartyIdentification2 = doc.CreateElement("cac:PartyIdentification", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            ClienteParty.AppendChild(PartyIdentification2);

            XmlNode IdPartyIdentification2 = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

            XmlAttribute schemeID4 = doc.CreateAttribute("schemeID");
            schemeID4.Value = loObj[0].FactBolSunat.CodigoDocumentoCliente;
            IdPartyIdentification2.Attributes.Append(schemeID4);

            XmlAttribute schemeName4 = doc.CreateAttribute("schemeName");
            schemeName4.Value = "SUNAT:Identificador de Documento de Identidad";
            IdPartyIdentification2.Attributes.Append(schemeName4);

            XmlAttribute schemeAgencyName4 = doc.CreateAttribute("schemeAgencyName");
            schemeAgencyName4.Value = "PE:SUNAT";
            IdPartyIdentification2.Attributes.Append(schemeAgencyName4);

            XmlAttribute schemeURI4 = doc.CreateAttribute("schemeURI");
            schemeURI4.Value = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            IdPartyIdentification2.Attributes.Append(schemeURI4);

            IdPartyIdentification2.InnerText = loObj[0].FactBolSunat.NumeroDocumentoCliente;
            PartyIdentification2.AppendChild(IdPartyIdentification2);



            XmlNode PartyNameClient = doc.CreateElement("cac:PartyName", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            ClienteParty.AppendChild(PartyNameClient);

            XmlNode NameClient = doc.CreateElement("cbc:Name", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            var cdataRazonSoc4 = doc.CreateCDataSection(loObj[0].FactBolSunat.ApellidoNombreRazonSocialCliente);
            NameClient.AppendChild(cdataRazonSoc4);
            PartyNameClient.AppendChild(NameClient);



            XmlNode PartyLegalEntity2 = doc.CreateElement("cac:PartyLegalEntity", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            ClienteParty.AppendChild(PartyLegalEntity2);

            XmlNode RegistrationName2 = doc.CreateElement("cbc:RegistrationName", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            var cRegistrationName2 = doc.CreateCDataSection(loObj[0].FactBolSunat.ApellidoNombreRazonSocialCliente);
            RegistrationName2.AppendChild(cRegistrationName2);
            PartyLegalEntity2.AppendChild(RegistrationName2);

            XmlNode RegistrationAddress2 = doc.CreateElement("cac:RegistrationAddress", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            PartyLegalEntity2.AppendChild(RegistrationAddress2);

            XmlNode AddressTypeCode2 = doc.CreateElement("cbc:AddressTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            AddressTypeCode2.InnerText = "0001";
            RegistrationAddress2.AppendChild(AddressTypeCode2);

            if (loObj[0].FactBolSunat.LeyendaRetencion != null)
            {
                if (loObj[0].FactBolSunat.LeyendaRetencion.Length > 0)
                {
                    XmlNode PaymentMeans = doc.CreateElement("cac:PaymentMeans", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                    Invoice.AppendChild(PaymentMeans);

                    XmlNode PaymentMeansCode = doc.CreateElement("cbc:PaymentMeansCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    PaymentMeansCode.InnerText = "001";
                    PaymentMeans.AppendChild(PaymentMeansCode);

                    XmlNode PayeeFinancialAccount = doc.CreateElement("cac:PayeeFinancialAccount", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                    PaymentMeans.AppendChild(PayeeFinancialAccount);

                    XmlNode IdCtaDet = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    IdCtaDet.InnerText = loObj[0].FactBolSunat.LeyendaRetencion;
                    PayeeFinancialAccount.AppendChild(IdCtaDet);

                    XmlNode PaymentTerms = doc.CreateElement("cac:PaymentTerms", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                    Invoice.AppendChild(PaymentTerms);

                    XmlNode IdDet = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    XmlAttribute schemeNameDet = doc.CreateAttribute("schemeName");
                    schemeNameDet.Value = "SUNAT:Codigo de detraccion";
                    IdDet.Attributes.Append(schemeNameDet);
                    XmlAttribute schemeAgencyNameDet = doc.CreateAttribute("schemeAgencyName");
                    schemeAgencyNameDet.Value = "PE:SUNAT";
                    IdDet.Attributes.Append(schemeAgencyNameDet);
                    XmlAttribute schemeURIDet = doc.CreateAttribute("schemeURI");
                    schemeURIDet.Value = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo54";
                    IdDet.Attributes.Append(schemeURIDet);
                    IdDet.InnerText = loObj[0].FactBolSunat.ServicioRetencion.Trim();
                    PaymentTerms.AppendChild(IdDet);

                    XmlNode PaymentPercentDet = doc.CreateElement("cbc:PaymentPercent", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    PaymentPercentDet.InnerText = loObj[0].FactBolSunat.PorcentajeRetencion;
                    PaymentTerms.AppendChild(PaymentPercentDet);

                    XmlNode AmountDet = doc.CreateElement("cbc:Amount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    XmlAttribute currencyIDDet = doc.CreateAttribute("currencyID");
                    currencyIDDet.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                    AmountDet.Attributes.Append(currencyIDDet);
                    AmountDet.InnerText = loObj[0].FactBolSunat.MontoRetencion;
                    PaymentTerms.AppendChild(AmountDet);

                }
            }

            if (loObj[0].FactBolSunat.SerieNumeroAnticipo.Length > 0)
            {

                XmlNode PrepaidPayment = doc.CreateElement("cac:PrepaidPayment", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                Invoice.AppendChild(PrepaidPayment);

                XmlNode IDPrepaidPayment = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");



                IDPrepaidPayment.InnerText = "1";

                PrepaidPayment.AppendChild(IDPrepaidPayment);


                decimal dPrepaidAmount2 = 0;
                foreach (EFactBolSunatDetalle d in loObj)
                {
                    dPrepaidAmount2 += Decimal.Parse(d.PrecioVenta) * Decimal.Parse(d.CantidadItem);

                }

                XmlNode PaidAmount = doc.CreateElement("cbc:PaidAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyIDPaidAmount = doc.CreateAttribute("currencyID");
                currencyIDPaidAmount.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                PaidAmount.Attributes.Append(currencyIDPaidAmount);
                PaidAmount.InnerText = dPrepaidAmount2.ToString("0.00");
                PrepaidPayment.AppendChild(PaidAmount);




            }



            XmlNode TaxTotal = doc.CreateElement("cac:TaxTotal", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            Invoice.AppendChild(TaxTotal);

            XmlNode TaxAmount = doc.CreateElement("cbc:TaxAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            XmlAttribute currencyID1 = doc.CreateAttribute("currencyID");
            currencyID1.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
            TaxAmount.Attributes.Append(currencyID1);
            TaxAmount.InnerText = loObj[0].FactBolSunat.SumatoriaIGV;
            TaxTotal.AppendChild(TaxAmount);

            /* Total Operaciones Gravadas */
            XmlNode TaxSubtotal = doc.CreateElement("cac:TaxSubtotal", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            TaxTotal.AppendChild(TaxSubtotal);

            XmlNode TaxableAmount = doc.CreateElement("cbc:TaxableAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            XmlAttribute currencyID22 = doc.CreateAttribute("currencyID");
            currencyID22.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
            TaxableAmount.Attributes.Append(currencyID22);
            TaxableAmount.InnerText = loObj[0].FactBolSunat.TotalValorVentaOperGravadas;
            TaxSubtotal.AppendChild(TaxableAmount);

            XmlNode TaxAmountSub = doc.CreateElement("cbc:TaxAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            XmlAttribute currencyID2 = doc.CreateAttribute("currencyID");
            currencyID2.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
            TaxAmountSub.Attributes.Append(currencyID2);
            TaxAmountSub.InnerText = loObj[0].FactBolSunat.SubTotalSumatoriaIGV;
            TaxSubtotal.AppendChild(TaxAmountSub);

            XmlNode TaxCategory = doc.CreateElement("cac:TaxCategory", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            TaxSubtotal.AppendChild(TaxCategory);

            XmlNode TaxCategoryIID0 = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

            XmlAttribute schemeIDTaxCategory0 = doc.CreateAttribute("schemeID");
            schemeIDTaxCategory0.Value = "UN/ECE 5305";
            TaxCategoryIID0.Attributes.Append(schemeIDTaxCategory0);

            XmlAttribute schemeNameTaxCategory0 = doc.CreateAttribute("schemeName");
            schemeNameTaxCategory0.Value = "Tax Category Identifier";
            TaxCategoryIID0.Attributes.Append(schemeNameTaxCategory0);

            XmlAttribute schemeAgencyNameTaxCategory0 = doc.CreateAttribute("schemeAgencyName");
            schemeAgencyNameTaxCategory0.Value = "United Nations Economic Commission for Europe";
            TaxCategoryIID0.Attributes.Append(schemeAgencyNameTaxCategory0);

            TaxCategoryIID0.InnerText = "S";
            TaxCategory.AppendChild(TaxCategoryIID0);

            XmlNode TaxScheme = doc.CreateElement("cac:TaxScheme", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            TaxCategory.AppendChild(TaxScheme);

            XmlNode IdTax = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

            XmlAttribute schemeIDIdTax = doc.CreateAttribute("schemeID");
            schemeIDIdTax.Value = "UN/ECE 5153";
            IdTax.Attributes.Append(schemeIDIdTax);

            XmlAttribute schemeAgencyIDIdTax = doc.CreateAttribute("schemeAgencyID");
            schemeAgencyIDIdTax.Value = "6";
            IdTax.Attributes.Append(schemeAgencyIDIdTax);

            IdTax.InnerText = loObj[0].FactBolSunat.CodigoSUNATTipoTributoIGV;
            TaxScheme.AppendChild(IdTax);

            XmlNode NameTax = doc.CreateElement("cbc:Name", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            NameTax.InnerText = loObj[0].FactBolSunat.NombreTipoTributoIGV;
            TaxScheme.AppendChild(NameTax);

            XmlNode TaxTypeCode = doc.CreateElement("cbc:TaxTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            TaxTypeCode.InnerText = loObj[0].FactBolSunat.CodigoUNECETipoTributoIGV;
            TaxScheme.AppendChild(TaxTypeCode);

            /* END: Total Operaciones Gravadas */

            if (loObj[0].FactBolSunat.TotalValorVentaOperExoneradas != "0.00")
            {
                /* Total Operaciones Exoneradas */

                XmlNode TaxSubtotalEx = doc.CreateElement("cac:TaxSubtotal", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxTotal.AppendChild(TaxSubtotalEx);

                XmlNode TaxableAmountEx = doc.CreateElement("cbc:TaxableAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID22Ex = doc.CreateAttribute("currencyID");
                currencyID22Ex.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                TaxableAmountEx.Attributes.Append(currencyID22Ex);
                TaxableAmountEx.InnerText = "0.00";
                TaxSubtotalEx.AppendChild(TaxableAmountEx);

                XmlNode TaxAmountSubEx = doc.CreateElement("cbc:TaxAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID2Ex = doc.CreateAttribute("currencyID");
                currencyID2Ex.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                TaxAmountSubEx.Attributes.Append(currencyID2Ex);
                TaxAmountSubEx.InnerText = "0.00";
                TaxSubtotalEx.AppendChild(TaxAmountSubEx);

                XmlNode TaxCategoryEx = doc.CreateElement("cac:TaxCategory", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxSubtotalEx.AppendChild(TaxCategoryEx);

                XmlNode TaxCategoryIID0Ex = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute schemeIDTaxCategory0Ex = doc.CreateAttribute("schemeID");
                schemeIDTaxCategory0Ex.Value = "UN/ECE 5305";
                TaxCategoryIID0Ex.Attributes.Append(schemeIDTaxCategory0Ex);

                XmlAttribute schemeNameTaxCategory0Ex = doc.CreateAttribute("schemeName");
                schemeNameTaxCategory0Ex.Value = "Tax Category Identifier";
                TaxCategoryIID0Ex.Attributes.Append(schemeNameTaxCategory0Ex);

                XmlAttribute schemeAgencyNameTaxCategory0Ex = doc.CreateAttribute("schemeAgencyName");
                schemeAgencyNameTaxCategory0Ex.Value = "United Nations Economic Commission for Europe";
                TaxCategoryIID0Ex.Attributes.Append(schemeAgencyNameTaxCategory0Ex);

                TaxCategoryIID0Ex.InnerText = "E";
                TaxCategoryEx.AppendChild(TaxCategoryIID0Ex);

                XmlNode TaxSchemeEx = doc.CreateElement("cac:TaxScheme", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxCategoryEx.AppendChild(TaxSchemeEx);

                XmlNode IdTaxEx = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute schemeIDIdTaxEx = doc.CreateAttribute("schemeID");
                schemeIDIdTaxEx.Value = "UN/ECE 5153";
                IdTaxEx.Attributes.Append(schemeIDIdTaxEx);

                XmlAttribute schemeAgencyIDIdTaxEx = doc.CreateAttribute("schemeAgencyID");
                schemeAgencyIDIdTaxEx.Value = "6";
                IdTaxEx.Attributes.Append(schemeAgencyIDIdTaxEx);

                IdTaxEx.InnerText = "9997";
                TaxSchemeEx.AppendChild(IdTaxEx);

                XmlNode NameTaxEx = doc.CreateElement("cbc:Name", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                NameTaxEx.InnerText = "EXO";
                TaxSchemeEx.AppendChild(NameTaxEx);

                XmlNode TaxTypeCodeEx = doc.CreateElement("cbc:TaxTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                TaxTypeCodeEx.InnerText = "VAT";
                TaxSchemeEx.AppendChild(TaxTypeCodeEx);

                /* END: Total Operaciones Exoneradas */
            }

            if (loObj[0].FactBolSunat.TotalValorVentaOperInafectadas != "0.00")
            {
                /* Total Operaciones Inafectas */

                XmlNode TaxSubtotalIna = doc.CreateElement("cac:TaxSubtotal", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxTotal.AppendChild(TaxSubtotalIna);

                XmlNode TaxableAmountIna = doc.CreateElement("cbc:TaxableAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID22Ina = doc.CreateAttribute("currencyID");
                currencyID22Ina.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                TaxableAmountIna.Attributes.Append(currencyID22Ina);
                TaxableAmountIna.InnerText = loObj[0].FactBolSunat.TotalValorVentaOperInafectadas;
                TaxSubtotalIna.AppendChild(TaxableAmountIna);

                XmlNode TaxAmountSubIna = doc.CreateElement("cbc:TaxAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID2Ina = doc.CreateAttribute("currencyID");
                currencyID2Ina.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                TaxAmountSubIna.Attributes.Append(currencyID2Ina);
                TaxAmountSubIna.InnerText = "0.00";
                TaxSubtotalIna.AppendChild(TaxAmountSubIna);

                XmlNode TaxCategoryIna = doc.CreateElement("cac:TaxCategory", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxSubtotalIna.AppendChild(TaxCategoryIna);

                XmlNode TaxCategoryIID0Ina = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute schemeIDTaxCategory0Ina = doc.CreateAttribute("schemeID");
                schemeIDTaxCategory0Ina.Value = "UN/ECE 5305";
                TaxCategoryIID0Ina.Attributes.Append(schemeIDTaxCategory0Ina);

                XmlAttribute schemeNameTaxCategory0Ina = doc.CreateAttribute("schemeName");
                schemeNameTaxCategory0Ina.Value = "Tax Category Identifier";
                TaxCategoryIID0Ina.Attributes.Append(schemeNameTaxCategory0Ina);

                XmlAttribute schemeAgencyNameTaxCategory0Ina = doc.CreateAttribute("schemeAgencyName");
                schemeAgencyNameTaxCategory0Ina.Value = "United Nations Economic Commission for Europe";
                TaxCategoryIID0Ina.Attributes.Append(schemeAgencyNameTaxCategory0Ina);

                TaxCategoryIID0Ina.InnerText = "O";
                TaxCategoryIna.AppendChild(TaxCategoryIID0Ina);

                XmlNode TaxSchemeIna = doc.CreateElement("cac:TaxScheme", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxCategoryIna.AppendChild(TaxSchemeIna);

                XmlNode IdTaxIna = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute schemeIDIdTaxIna = doc.CreateAttribute("schemeID");
                schemeIDIdTaxIna.Value = "UN/ECE 5153";
                IdTaxIna.Attributes.Append(schemeIDIdTaxIna);

                XmlAttribute schemeAgencyIDIdTaxIna = doc.CreateAttribute("schemeAgencyID");
                schemeAgencyIDIdTaxIna.Value = "6";
                IdTaxIna.Attributes.Append(schemeAgencyIDIdTaxIna);

                IdTaxIna.InnerText = "9998";
                TaxSchemeIna.AppendChild(IdTaxIna);

                XmlNode NameTaxIna = doc.CreateElement("cbc:Name", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                NameTaxIna.InnerText = "INA";
                TaxSchemeIna.AppendChild(NameTaxIna);

                XmlNode TaxTypeCodeIna = doc.CreateElement("cbc:TaxTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                TaxTypeCodeIna.InnerText = "FRE";
                TaxSchemeIna.AppendChild(TaxTypeCodeIna);

                /* END: Total Operaciones Inafectas */
            }

            if (loObj[0].FactBolSunat.TotalValorVentaOperGratuitas != "0.00")
            {
                /* Total Operaciones Gratuitas */

                XmlNode TaxSubtotalGr = doc.CreateElement("cac:TaxSubtotal", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxTotal.AppendChild(TaxSubtotalGr);

                XmlNode TaxableAmountGr = doc.CreateElement("cbc:TaxableAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID22Gr = doc.CreateAttribute("currencyID");
                currencyID22Gr.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                TaxableAmountGr.Attributes.Append(currencyID22Gr);
                TaxableAmountGr.InnerText = loObj[0].FactBolSunat.TotalValorVentaOperGratuitas;
                TaxSubtotalGr.AppendChild(TaxableAmountGr);

                XmlNode TaxAmountSubGr = doc.CreateElement("cbc:TaxAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID2Gr = doc.CreateAttribute("currencyID");
                currencyID2Gr.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                TaxAmountSubGr.Attributes.Append(currencyID2Gr);
                TaxAmountSubGr.InnerText = "0.00";
                TaxSubtotalGr.AppendChild(TaxAmountSubGr);

                XmlNode TaxCategoryGr = doc.CreateElement("cac:TaxCategory", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxSubtotalGr.AppendChild(TaxCategoryGr);

                XmlNode TaxCategoryIID0Gr = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute schemeIDTaxCategory0Gr = doc.CreateAttribute("schemeID");
                schemeIDTaxCategory0Gr.Value = "UN/ECE 5305";
                TaxCategoryIID0Gr.Attributes.Append(schemeIDTaxCategory0Gr);

                XmlAttribute schemeNameTaxCategory0Gr = doc.CreateAttribute("schemeName");
                schemeNameTaxCategory0Gr.Value = "Tax Category Identifier";
                TaxCategoryIID0Gr.Attributes.Append(schemeNameTaxCategory0Gr);

                XmlAttribute schemeAgencyNameTaxCategory0Gr = doc.CreateAttribute("schemeAgencyName");
                schemeAgencyNameTaxCategory0Gr.Value = "United Nations Economic Commission for Europe";
                TaxCategoryIID0Gr.Attributes.Append(schemeAgencyNameTaxCategory0Gr);

                TaxCategoryIID0Gr.InnerText = "Z";
                TaxCategoryGr.AppendChild(TaxCategoryIID0Gr);

                XmlNode TaxSchemeGr = doc.CreateElement("cac:TaxScheme", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxCategoryGr.AppendChild(TaxSchemeGr);

                XmlNode IdTaxGr = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute schemeIDIdTaxGr = doc.CreateAttribute("schemeID");
                schemeIDIdTaxGr.Value = "UN/ECE 5153";
                IdTaxGr.Attributes.Append(schemeIDIdTaxGr);

                XmlAttribute schemeAgencyIDIdTaxGr = doc.CreateAttribute("schemeAgencyID");
                schemeAgencyIDIdTaxGr.Value = "6";
                IdTaxGr.Attributes.Append(schemeAgencyIDIdTaxGr);

                IdTaxGr.InnerText = "9996";
                TaxSchemeGr.AppendChild(IdTaxGr);

                XmlNode NameTaxGr = doc.CreateElement("cbc:Name", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                NameTaxGr.InnerText = "GRATUITO";
                TaxSchemeGr.AppendChild(NameTaxGr);

                XmlNode TaxTypeCodeGr = doc.CreateElement("cbc:TaxTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                TaxTypeCodeGr.InnerText = "FRE";
                TaxSchemeGr.AppendChild(TaxTypeCodeGr);

                /* END: Total Operaciones Gratuitas */
            }

            XmlNode LegalMonetaryTotal = doc.CreateElement("cac:LegalMonetaryTotal", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            Invoice.AppendChild(LegalMonetaryTotal);

            XmlNode LineExtensionAmount = doc.CreateElement("cbc:LineExtensionAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            XmlAttribute currencyID666 = doc.CreateAttribute("currencyID");
            currencyID666.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
            LineExtensionAmount.Attributes.Append(currencyID666);
            LineExtensionAmount.InnerText = loObj[0].FactBolSunat.TotalValorVentaOperGravadas;
            LegalMonetaryTotal.AppendChild(LineExtensionAmount);

            XmlNode AllowanceTotalAmount = doc.CreateElement("cbc:AllowanceTotalAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            XmlAttribute currencyID66 = doc.CreateAttribute("currencyID");
            currencyID66.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
            AllowanceTotalAmount.Attributes.Append(currencyID66);
            AllowanceTotalAmount.InnerText = loObj[0].FactBolSunat.DescuentosGlobales;
            LegalMonetaryTotal.AppendChild(AllowanceTotalAmount);


            if (loObj[0].FactBolSunat.SerieNumeroAnticipo.Length > 0)
            {
                decimal dPrepaidAmount = 0;
                foreach (EFactBolSunatDetalle d in loObj)
                {
                    dPrepaidAmount += Decimal.Parse(d.PrecioVenta) * Decimal.Parse(d.CantidadItem);

                }


                XmlNode PrepaidAmount = doc.CreateElement("cbc:PrepaidAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID7_1 = doc.CreateAttribute("currencyID");
                currencyID7_1.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                PrepaidAmount.Attributes.Append(currencyID7_1);
                PrepaidAmount.InnerText = dPrepaidAmount.ToString("0.00");
                LegalMonetaryTotal.AppendChild(PrepaidAmount);
            }

            XmlNode PayableAmount = doc.CreateElement("cbc:PayableAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            XmlAttribute currencyID7 = doc.CreateAttribute("currencyID");
            currencyID7.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
            PayableAmount.Attributes.Append(currencyID7);
            PayableAmount.InnerText = loObj[0].FactBolSunat.ImporteTotalVenta;
            LegalMonetaryTotal.AppendChild(PayableAmount);


            foreach (EFactBolSunatDetalle det in loObj)
            {
                XmlNode InvoiceLine = doc.CreateElement("cac:InvoiceLine", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                Invoice.AppendChild(InvoiceLine);

                XmlNode IDI = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                IDI.InnerText = det.NumeroOrdenItem.Replace(",", ".");
                InvoiceLine.AppendChild(IDI);

                XmlNode InvoicedQuantity = doc.CreateElement("cbc:InvoicedQuantity", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute unitCode = doc.CreateAttribute("unitCode");
                unitCode.Value = det.UnidadMedidaItem;
                InvoicedQuantity.Attributes.Append(unitCode);

                XmlAttribute unitCodeListID = doc.CreateAttribute("unitCodeListID");
                unitCodeListID.Value = "UN/ECE rec 20";
                InvoicedQuantity.Attributes.Append(unitCodeListID);

                XmlAttribute unitCodeListAgencyName = doc.CreateAttribute("unitCodeListAgencyName");
                unitCodeListAgencyName.Value = "United Nations Economic Commission forEurope";
                InvoicedQuantity.Attributes.Append(unitCodeListAgencyName);

                InvoicedQuantity.InnerText = det.CantidadItem.Replace(",", ".");

                InvoiceLine.AppendChild(InvoicedQuantity);

                XmlNode LineExtensionAmountI = doc.CreateElement("cbc:LineExtensionAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID13 = doc.CreateAttribute("currencyID");
                currencyID13.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                LineExtensionAmountI.Attributes.Append(currencyID13);
                LineExtensionAmountI.InnerText = (Decimal.Parse(det.ValorVentaUnitario.Replace(",", ".")) < 0 ? (Decimal.Parse(det.ValorVentaUnitario.Replace(",", ".")) * -1).ToString("0.00") : det.ValorVentaUnitario.Replace(",", "."));

                //   LineExtensionAmountI.InnerText = det.ValorVentaItem;
                InvoiceLine.AppendChild(LineExtensionAmountI);

                XmlNode PricingReference = doc.CreateElement("cac:PricingReference", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                InvoiceLine.AppendChild(PricingReference);

                XmlNode AlternativeConditionPrice = doc.CreateElement("cac:AlternativeConditionPrice", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                PricingReference.AppendChild(AlternativeConditionPrice);

                XmlNode PriceAmountA = doc.CreateElement("cbc:PriceAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID8 = doc.CreateAttribute("currencyID");
                currencyID8.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                PriceAmountA.Attributes.Append(currencyID8);
                //PriceAmountA.InnerText = det.PrecioVenta;
                PriceAmountA.InnerText = (Decimal.Parse(det.Importe.Replace(",", ".")) < 0 ? (Decimal.Parse(det.Importe.Replace(",", ".")) * -1).ToString("0.00") : det.Importe).Replace(",", ".");
                AlternativeConditionPrice.AppendChild(PriceAmountA);

                XmlNode PriceTypeCode = doc.CreateElement("cbc:PriceTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute listName = doc.CreateAttribute("listName");
                listName.Value = "SUNAT:Indicador de Tipo de Precio";
                PriceTypeCode.Attributes.Append(listName);

                XmlAttribute listAgencyName = doc.CreateAttribute("listAgencyName");
                listAgencyName.Value = "PE:SUNAT";
                PriceTypeCode.Attributes.Append(listAgencyName);

                XmlAttribute listURI = doc.CreateAttribute("listURI");
                listURI.Value = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo16";
                PriceTypeCode.Attributes.Append(listURI);

                PriceTypeCode.InnerText = det.CodigoTipoPrecioVenta;
                AlternativeConditionPrice.AppendChild(PriceTypeCode);


                XmlNode TaxTotalI = doc.CreateElement("cac:TaxTotal", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                InvoiceLine.AppendChild(TaxTotalI);

                XmlNode TaxAmountI = doc.CreateElement("cbc:TaxAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID9 = doc.CreateAttribute("currencyID");
                currencyID9.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                TaxAmountI.Attributes.Append(currencyID9);
                //TaxAmountI.InnerText = "0.00";
                TaxAmountI.InnerText = (Decimal.Parse(det.igv.Replace(",", ".")) < 0 ? (Decimal.Parse(det.igv.Replace(",", ".")) * -1).ToString("0.00") : det.igv).Replace(",", ".");
                TaxTotalI.AppendChild(TaxAmountI);

                XmlNode TaxSubtotalI = doc.CreateElement("cac:TaxSubtotal", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxTotalI.AppendChild(TaxSubtotalI);

                XmlNode TaxableAmountI = doc.CreateElement("cbc:TaxableAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID20 = doc.CreateAttribute("currencyID");
                currencyID20.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                TaxableAmountI.Attributes.Append(currencyID20);
                TaxableAmountI.InnerText = (Decimal.Parse(det.ValorVentaUnitario.Replace(",", ".")) < 0 ? (Decimal.Parse(det.ValorVentaUnitario.Replace(",", ".")) * -1).ToString("0.00") : det.ValorVentaUnitario.Replace(",", "."));
                //TaxableAmountI.InnerText = "0.00";
                TaxSubtotalI.AppendChild(TaxableAmountI);

                XmlNode TaxAmountSubI = doc.CreateElement("cbc:TaxAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID10 = doc.CreateAttribute("currencyID");
                currencyID10.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                TaxAmountSubI.Attributes.Append(currencyID10);
                //TaxAmountSubI.InnerText = "0.00";
                TaxAmountSubI.InnerText = (Decimal.Parse(det.igv.Replace(",", ".")) < 0 ? (Decimal.Parse(det.igv.Replace(",", ".")) * -1).ToString("0.00") : det.igv).Replace(",", ".");

                TaxSubtotalI.AppendChild(TaxAmountSubI);

                XmlNode TaxCategoryI = doc.CreateElement("cac:TaxCategory", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxSubtotalI.AppendChild(TaxCategoryI);

                XmlNode TaxCategoryIID = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute schemeIDTaxCategory = doc.CreateAttribute("schemeID");
                schemeIDTaxCategory.Value = "UN/ECE 5305";
                TaxCategoryIID.Attributes.Append(schemeIDTaxCategory);

                XmlAttribute schemeNameTaxCategory = doc.CreateAttribute("schemeName");
                schemeNameTaxCategory.Value = "Tax Category Identifier";
                TaxCategoryIID.Attributes.Append(schemeNameTaxCategory);

                XmlAttribute schemeAgencyNameTaxCategory = doc.CreateAttribute("schemeAgencyName");
                schemeAgencyNameTaxCategory.Value = "United Nations Economic Commission for Europe";
                TaxCategoryIID.Attributes.Append(schemeAgencyNameTaxCategory);

                TaxCategoryIID.InnerText = "S";
                TaxCategoryI.AppendChild(TaxCategoryIID);

                XmlNode PercentTax = doc.CreateElement("cbc:Percent", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                PercentTax.InnerText = "18.00";
                TaxCategoryI.AppendChild(PercentTax);

                XmlNode TaxExemptionReasonCode = doc.CreateElement("cbc:TaxExemptionReasonCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");


                XmlAttribute listAgencyNameTE = doc.CreateAttribute("listAgencyName");
                listAgencyNameTE.Value = "PE:SUNAT";
                TaxExemptionReasonCode.Attributes.Append(listAgencyNameTE);

                XmlAttribute listNameTE = doc.CreateAttribute("listName");
                listNameTE.Value = "SUNAT:Codigo de Tipo de Afectación del IGV";
                TaxExemptionReasonCode.Attributes.Append(listNameTE);

                XmlAttribute listURITE = doc.CreateAttribute("listURI");
                listURITE.Value = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07";
                TaxExemptionReasonCode.Attributes.Append(listURITE);

                TaxExemptionReasonCode.InnerText = det.CodigoTipoAfectacionIGV;
                TaxCategoryI.AppendChild(TaxExemptionReasonCode);

                XmlNode TaxSchemeI = doc.CreateElement("cac:TaxScheme", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                TaxCategoryI.AppendChild(TaxSchemeI);

                //--
                XmlNode IdTaxI = doc.CreateElement("cbc:ID", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

                XmlAttribute schemeIDIdTaxI = doc.CreateAttribute("schemeID");
                schemeIDIdTaxI.Value = "UN/ECE 5153";
                IdTaxI.Attributes.Append(schemeIDIdTaxI);

                XmlAttribute schemeNameIdTaxI = doc.CreateAttribute("schemeName");
                schemeNameIdTaxI.Value = "Tax Scheme Identifier";
                IdTaxI.Attributes.Append(schemeNameIdTaxI);

                XmlAttribute schemeAgencyNameIdTaxI = doc.CreateAttribute("schemeAgencyName");
                schemeAgencyNameIdTaxI.Value = "United Nations Economic Commission for Europe";
                IdTaxI.Attributes.Append(schemeAgencyNameIdTaxI);

                IdTaxI.InnerText = det.CodigoSUNATTipoTributoIGV;
                TaxSchemeI.AppendChild(IdTaxI);

                XmlNode NameTaxI = doc.CreateElement("cbc:Name", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                NameTaxI.InnerText = det.NombreTipoTributoIGV;
                TaxSchemeI.AppendChild(NameTaxI);

                XmlNode TaxTypeCodeI = doc.CreateElement("cbc:TaxTypeCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                TaxTypeCodeI.InnerText = det.CodigoUNECETipoTributoIGV;
                TaxSchemeI.AppendChild(TaxTypeCodeI);

                XmlNode Item = doc.CreateElement("cac:Item", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                InvoiceLine.AppendChild(Item);

                XmlNode Description = doc.CreateElement("cbc:Description", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                var cdataDescription = doc.CreateCDataSection(det.DescripcionDetalladaProducto);
                Description.AppendChild(cdataDescription);
                Item.AppendChild(Description);

                // Agregar placa
                if (loObj[0].FactBolSunat.Placa != "-")
                {
                    XmlNode AdditionalItemPropertyItem = doc.CreateElement("cac:AdditionalItemProperty", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                    Item.AppendChild(AdditionalItemPropertyItem);

                    XmlNode NameItem = doc.CreateElement("cbc:Name", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    NameItem.InnerText = "Gastos Art. 37 Renta: Número de Placa";
                    AdditionalItemPropertyItem.AppendChild(NameItem);

                    XmlNode NameCodeItem = doc.CreateElement("cbc:NameCode", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    XmlAttribute listNameItem = doc.CreateAttribute("listName");
                    listNameItem.Value = "SUNAT:Identificador de la propiedad del ítem";
                    NameCodeItem.Attributes.Append(listNameItem);
                    XmlAttribute listAgencyNameItem = doc.CreateAttribute("listAgencyName");
                    listAgencyNameItem.Value = "PE:SUNAT";
                    NameCodeItem.Attributes.Append(listAgencyNameItem);
                    NameCodeItem.InnerText = "7000";
                    AdditionalItemPropertyItem.AppendChild(NameCodeItem);

                    XmlNode ValueItem = doc.CreateElement("cbc:Value", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    ValueItem.InnerText = loObj[0].FactBolSunat.Placa;
                    AdditionalItemPropertyItem.AppendChild(ValueItem);
                }

                XmlNode Price = doc.CreateElement("cac:Price", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                InvoiceLine.AppendChild(Price);

                XmlNode PriceAmount = doc.CreateElement("cbc:PriceAmount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID77 = doc.CreateAttribute("currencyID");
                currencyID77.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                PriceAmount.Attributes.Append(currencyID77);
                //PriceAmount.InnerText = det.ValorVentaUnitario;
                PriceAmount.InnerText = (Decimal.Parse(det.ValorVentaUnitario.Replace(",", ".")) < 0 ? (Decimal.Parse(det.ValorVentaUnitario.Replace(",", ".")) * -1).ToString("0.00") : det.ValorVentaUnitario.Replace(",", "."));
                Price.AppendChild(PriceAmount);

                XmlNode AllowancechargeI = doc.CreateElement("cac:AllowanceCharge", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                Price.AppendChild(AllowancechargeI);

                XmlNode ChargeIndicatorI = doc.CreateElement("cbc:ChargeIndicator", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                ChargeIndicatorI.InnerText = det.IndicadorDescuentosItem;
                AllowancechargeI.AppendChild(ChargeIndicatorI);

                XmlNode AmountI = doc.CreateElement("cbc:Amount", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                XmlAttribute currencyID99 = doc.CreateAttribute("currencyID");
                currencyID99.Value = loObj[0].FactBolSunat.CodigoISOTipoMoneda;
                AmountI.Attributes.Append(currencyID99);
                AmountI.InnerText = det.DescuentosItem;
                AllowancechargeI.AppendChild(AmountI);

            }

            return doc.InnerXml.ToString();
        }
    }
}
