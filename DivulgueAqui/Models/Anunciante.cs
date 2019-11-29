using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DivulgueAqui.Models
{
    public class Anunciante
    {
        public String NomeAnunciante { get; set; }
        public String NomeComercio { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public String TipoComercio { get; set; }
        public double ValorPacote { get; set; }
        public int AnunciosInseridosQtd { get; set; }
        public String LatLong { get; set; }
        public int Limite { get; set; }
        public List<string> ListaAnuncios = new List<string>();
    }
}