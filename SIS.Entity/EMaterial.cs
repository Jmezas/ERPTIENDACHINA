using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Entity
{
    public class EMaterial : EGeneral
    {
        public EMaterial()
        {
            Unidad = new EUnidad();
            Marca = new EMarca();
            Categoria = new ECategoria();
            SubCateoria = new ESubCateoria();
            Empresa = new EEmpresa();
            Modelo = new EModelo();
            genero = new EGenero();
            Etipo = new Etipo();
            EColor = new EColor();
            Talla = new ETalla();
            Temporada = new ETemporda();
        }
        public int IdMaterial { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public EUnidad Unidad { get; set; }
        public EMarca Marca { get; set; }
        public float PrecioVenta { get; set; }
        public float PrecioCompra { get; set; }
        public ECategoria Categoria { get; set; }
        public ESubCateoria SubCateoria { get; set; }
        public double Descuento { get; set; }

        public EEmpresa Empresa { get; set; }
        public EModelo Modelo { get; set; }
        public EGenero genero { get; set; }
        public Etipo Etipo { get; set; }
        public EColor EColor { get; set; }
        public ETalla Talla { get; set; }
        public ETemporda Temporada { get; set; }
        public float PrecioUnidad { get; set; }
        public float PrecioDocena { get; set; }
        public float PrecioCaja { get; set; }
        public long CantCaja { get; set; }
        public int tipoEmpresa { get; set; }
        public string stipoEmpresa { get; set; }
    }
    public class EModelo
    {

        public int IdModelo { get; set; }
        public string Nombre { get; set; }


    }
    public class EGenero
    {
        public int IdGenero { get; set; }
        public string Nombre { get; set; }

    }
    public class Etipo
    {
        public int IdTipo { get; set; }
        public string Nombre { get; set; }
    }
    public class EColor
    {

        public int IdColor { get; set; }
        public string Nombre { get; set; }

    }

    public class ETalla
    {
        public int IdTalla { get; set; }
        public string Nombre { get; set; }

    }
    public class ETemporda
    {
        public int IdTemporada { get; set; }
        public string Nombre { get; set; }
    }

}
