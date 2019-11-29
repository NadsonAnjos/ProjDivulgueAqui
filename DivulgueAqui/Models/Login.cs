using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DivulgueAqui.Models
{
    public class LoginAdm
    {
        public string Email { get; set; }
        public string Senha { get; set; }        
    }

    public class LoginAnunciante
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}