using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO; 
using System.Net; 
using System.Xml;
using System.Configuration;

namespace SIS.Principal.Models
{
    public class SunatServices
    {
        string ValidSunatServices = ConfigurationManager.AppSettings["ValidSunatServices"];
        string inputsPath = "";
        string outputPath = "";

        public SunatServices(string inputsPath, string outputPath)
        {

            ServicePointManager.UseNagleAlgorithm = true;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.CheckCertificateRevocationList = true;


            this.inputsPath = inputsPath; // XML a Enviar
            this.outputPath = outputPath; // Reciben los XML de respuesta (Aceptados/Rechazados)


            //string folder = inputsPath + NameOfFileZip;
            //NameOfFileZip = NameOfFileZip.Split('\\').Last();
            //byte[] allbytes = File.ReadAllBytes(folder);
        } 
    }
}