using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIS.Data;
using SIS.Factory;
using SIS.Entity;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace SIS.Business
{
    public class BFacturaElectronica
    {

        private static BFacturaElectronica Instancia;
        private DFacturaElectronica Data = DFacturaElectronica.ObtenerInstancia(DataBase.SqlServer);

        public static BFacturaElectronica ObtenerInstancia()
        {
            if (Instancia == null)
            {
                Instancia = new BFacturaElectronica();
            }
            return Instancia;
        }
        public List<EVenta> ListarComprobante(int Comienzo, int Medida, int empresa, int Sucursal, string FechaEmi)
        {
            try
            {
                return Data.ListarComprobante(Comienzo, Medida, empresa, Sucursal, FechaEmi);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public List<EVenta> ListarBoletaResumen(int Comienzo, int Medida, int empresa, int Sucursal, string FechaEmi)
        {
            try
            {
                return Data.ListarBoletaResumen(Comienzo, Medida, empresa, Sucursal, FechaEmi);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        public string GeneraResumen(int IdVenta, int empresa, int sucursal, string fecha, string usuario,string ruta, string rutaServidor,string claveCertificado)
        {
            try
            {
                return Data.GeneraResumen(IdVenta, empresa, sucursal, fecha, usuario, ruta, rutaServidor, claveCertificado);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
