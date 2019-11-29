using DivulgueAqui.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DivulgueAqui.Controllers
{
    public class CadastroController : Controller
    {
        // GET: Cadastro
        public ActionResult CadastroAnunciante()
        {
            ViewBag.Message = "Cadastro de Anunciantes";
            return View();
        }

        public ActionResult EsqueceuSenha()
        {
            ViewBag.Message = "Cadastro de Anunciantes";
            return View();
        }

        [HttpPost]
        public ActionResult CadastroAnunciante(Anunciante a) {
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string cmd = "INSERT INTO Anunciante(nomeAnunciante,nomeComercio,email,tipoComercio,valorPacote,senha,anunciosinseridosQtd,latlong) VALUES @NomeAnunciante,@NomeComercio,@Email,@TipoComercio,@ValorPacote,@Senha,@AnunciosinseridosQtd,@LatLong,@Limite";

                    SqlCommand command = new SqlCommand(cmd, connection);
                    command.Parameters.AddWithValue("@NomeAnunciante", a.NomeAnunciante);
                    command.Parameters.AddWithValue("@NomeComercio", a.NomeComercio);
                    command.Parameters.AddWithValue("@Email", a.Email);
                    command.Parameters.AddWithValue("@TipoComercio", a.TipoComercio);
                    command.Parameters.AddWithValue("@ValorPacote", a.ValorPacote);
                    command.Parameters.AddWithValue("@Senha", a.Senha);
                    command.Parameters.AddWithValue("@AnunciosinseridosQtd ", 0);
                    command.Parameters.AddWithValue("@LatLong", a.LatLong);
                    if (a.ValorPacote == 9.90) {
                        command.Parameters.AddWithValue("@Limite", 4);
                    }
                    if (a.ValorPacote == 19.90)
                    {
                        command.Parameters.AddWithValue("@Limite", 8);
                    }
                    if (a.ValorPacote == 39.90)
                    {
                        command.Parameters.AddWithValue("@Limite", 0);
                    }
                    command.Connection.Open();
                    return View();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }
    }
}