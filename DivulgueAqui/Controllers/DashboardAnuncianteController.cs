using DivulgueAqui.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DivulgueAqui.Controllers
{
    public class DashboardAnuncianteController : Controller
    {
        private String emailLogin;
        // GET: Dashboard
        public ActionResult IndexAnunciante()
        {
            return View();
        }

        public ActionResult Pagamentos()
        {
            return View();
        }

        public ActionResult MeusAnuncios(Anunciante a)
        {
            //a.ListaAnuncios = getMeusAnuncios(a);
            return View();
        }

        [HttpPost]
        public ActionResult InserirAnuncio(HttpPostedFileBase file, Anunciante a)
        {
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                string cmd = "Select limite,email, anunciosinseridosQtd from Anunciante as a where a.Email=@email";
                SqlCommand command = new SqlCommand(cmd, connection);
                command.Parameters.AddWithValue("@email", a.Email);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                int limite = 0, inseridos = 0;
                while (reader.Read())
                {
                    limite = (int)reader["limite"];
                    inseridos = (int)reader["anunciosinseridosQtd"];
                }
                if (limite == 0)
                {
                    var path = "";
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {
                            if (Path.GetExtension(file.FileName).ToLower() == ".png" ||
                                Path.GetExtension(file.FileName).ToLower() == ".jpg" ||
                                Path.GetExtension(file.FileName).ToLower() == ".jpeg" ||
                                Path.GetExtension(file.FileName).ToLower() == ".gif")
                            {
                                path = Path.Combine(Server.MapPath("~/Content/Images/"), file.FileName);
                                file.SaveAs(path);
                                ViewBag.UploadSucess = true;
                                SalvarCaminho(path, a);
                                AtualizarQuantidade(a);
                            }
                        }
                    }
                }
                else {
                    if (inseridos < limite)
                    {
                        var path = "";
                        if (file != null)
                        {
                            if (file.ContentLength > 0)
                            {
                                if (Path.GetExtension(file.FileName).ToLower() == ".png" ||
                                    Path.GetExtension(file.FileName).ToLower() == ".jpg" ||
                                    Path.GetExtension(file.FileName).ToLower() == ".jpeg" ||
                                    Path.GetExtension(file.FileName).ToLower() == ".gif")
                                {
                                    path = Path.Combine(Server.MapPath("~/Content/Images/"), file.FileName);
                                    file.SaveAs(path);
                                    ViewBag.UploadSucess = true;
                                    SalvarCaminho(path, a);
                                    AtualizarQuantidade(a);
                                }
                            }
                        }
                    }
                }
                
            }
            return RedirectToAction("IndexAnunciante");
        }

        private void SalvarCaminho(string path, Anunciante a)
        {
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmd = "INSERT INTO Anuncio(diretorio) VALUES @Path";
                using(SqlCommand command = new SqlCommand(cmd, connection))
                {
                    command.Parameters.AddWithValue("@Path", path);
                    command.ExecuteNonQuery();
                }
                string cmd1 = "INSERT INTO Anunciante_Anuncios(email,id_anuncio) VALUES @Email,(SELECT id_anuncio FROM Anuncios WHERE diretorio=@Path)";
                using(SqlCommand command = new SqlCommand(cmd1, connection))
                {
                    command.Parameters.AddWithValue("@Path", a.Email);
                    command.Parameters.AddWithValue("@Path", path);
                    command.ExecuteNonQuery();
                }
            }
        }

        private List<string> getMeusAnuncios(Anunciante a)
        {
            List<string> lista = new List<string>();
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                string cmd = "SELECT ao.diretorio  FROM anuncios AS ao INNER JOIN  anunciante_anuncios AS aa ON (ao.id_anuncio =aa.id_anuncio) where aa.email=@Email;";
                using (SqlCommand command = new SqlCommand(cmd, connection)) {
                    command.Parameters.AddWithValue("@Email", a.Email);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add((string)reader["diretorio"]);
                    }
                }
            }
            return lista;
        }

        private void AtualizarQuantidade(Anunciante a)
        {
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string cmd = "UPDATE anunciante SET anunciosinseridosQtd=anunciosinseridosQtd+1 WHERE email=@Email;";

                    SqlCommand command = new SqlCommand(cmd, connection);
                    command.Parameters.AddWithValue("@Email", a.Email);
                    command.Connection.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }

    }
}