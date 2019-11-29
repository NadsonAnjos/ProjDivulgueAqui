using DivulgueAqui.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DivulgueAqui.Controllers
{
    public class DashboardAdministracaoController : Controller
    {
        // GET: DashboardAdministracao
        public ActionResult Faturamento()
        {
            //float faturamentoPrevisto = this.getFaturamento();
            //Faturamento fat = new Faturamento();
            //fat.ValorEstimado = faturamentoPrevisto;
            return View();
        }

        public ActionResult IndexAdministracao()
        {
            return View();
        }

        public ActionResult Anunciantes()
        {
            //List<Models.Anunciante> lista = teste1();
            List<Models.Anunciante> lista = GetAnunciantes();
            return View(lista);
        }

        public ActionResult Detalhes(String email)
        {
            Anunciante anunciante = this.getAnunciante(email);
            return View(anunciante);
        }

        public Anunciante teste()
        {
            List<Models.Anunciante> lista = new List<Models.Anunciante>();
            Models.Anunciante fiel = new Models.Anunciante();
            fiel.LatLong = "(-13.0010719, -38.530658)";
            fiel.Email = "oi@gmai.com";
            fiel.NomeAnunciante = "Adenilson";
            fiel.NomeComercio = "mercadinho";

            return fiel;
        }

        public List<Models.Anunciante> teste1()
        {
            List<Models.Anunciante> lista = new List<Models.Anunciante>();
            Models.Anunciante fiel = new Models.Anunciante();
            fiel.LatLong = "(-12.96265216518016, -38.50096648473988)";
            fiel.Email = "oi@gmai.com";
            fiel.NomeAnunciante = "Adenilson";
            fiel.NomeComercio = "mercadinho";
            lista.Add(fiel);


            Models.Anunciante fiel1 = new Models.Anunciante();
            fiel1.Email = "abesvaldo@gmai.com";
            fiel1.NomeAnunciante = "Abesvaldo";
            fiel1.NomeComercio = "Churros";
            fiel.LatLong = "(-13.0010719, -38.530658)";
            lista.Add(fiel1);

            Models.Anunciante fiel2 = new Models.Anunciante();
            fiel2.Email = "fulano@gmai.com";
            fiel2.NomeAnunciante = "fulano";
            fiel2.NomeComercio = "1,99";
            fiel.LatLong = "(-12.986687, -38.4958537)";
            lista.Add(fiel2);

            return lista;
        }

        public Anunciante getAnunciante(String Email) {
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string cmd = "SELECT * FROM Anunciante WHERE email=@Email";

                    SqlCommand command = new SqlCommand(cmd, connection);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Connection.Open();
                    var reader = command.ExecuteReader();
                    Models.Anunciante anun = new Models.Anunciante();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            anun.NomeAnunciante = (string)reader["nomeAnunciante"];
                            anun.NomeComercio = (string)reader["nomeComercio"];
                            anun.ValorPacote = (float)reader["valorPacote"];
                            anun.Email = (string)reader["email"];
                            anun.LatLong = (string)reader["latLong "];
                        }
                    }
                    return anun;
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }

        

        public float getFaturamento()
        {
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            float faturamentoPrevisto = 0;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try
                {
                    string cmd = "SELECT SUM(valorPacote) FROM Anunciante";
                    SqlCommand command = new SqlCommand(cmd, connection);
                    command.Connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            faturamentoPrevisto += (float)reader["valorPacote"];
                        }
                    }
                    return faturamentoPrevisto;
                }
                catch(Exception ex) {
                    throw new Exception("Erro: " + ex.Message);
                }
                
            }
        }

        public List<Models.Anunciante> GetAnunciantes()
        {
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string cmd = "SELECT nomeAnunciante, nomeComercio, email, valorPacote FROM Anunciante";

                    SqlCommand command = new SqlCommand(cmd, connection);
                    command.Connection.Open();
                    var reader = command.ExecuteReader();              
                    List<Models.Anunciante> lista = new List<Models.Anunciante>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Models.Anunciante anun = new Models.Anunciante();
                            anun.NomeAnunciante = (string)reader["nomeAnunciante"];
                            anun.NomeComercio = (string)reader["nomeComercio"];
                            anun.Email = (string)reader["email"];
                            anun.ValorPacote = (double)reader["valorPacote"];
                            lista.Add(anun);
                        }
                        return lista;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
                return null;
            }
        }

        public List<Models.Faturamento> getFatura()
        {
            List<Models.Faturamento> lista = new List<Faturamento>();
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try
                {
                    string cmd = "SELECT p.Mes, p.ValorArrecadado FROM Pagamentos AS p GROUP BY p.Mes";
                    SqlCommand command = new SqlCommand(cmd, connection);
                    command.Connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Models.Faturamento fat = new Models.Faturamento();
                            fat.Mes = (string)reader["Mes"];
                            fat.ValorEstimado = (float)reader["ValorEstimado"];
                            fat.ValorArrecadado = (float)reader["ValorArrecadado"];

                            lista.Add(fat);
                        }
                        return lista;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
            return lista;
        }
    }
}