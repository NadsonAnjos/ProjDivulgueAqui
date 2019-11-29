using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DivulgueAqui.Models
{
    public class Faturamento
    {
        public string Mes { get; set; }
        public float ValorEstimado { get; set; }
        public float ValorArrecadado { get; set; }
    }
}