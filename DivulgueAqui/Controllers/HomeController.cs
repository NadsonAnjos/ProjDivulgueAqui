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
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult LoginAdm()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("IndexAdministracao", "DashboardAdministracao");
            return View();            
        }

        [AllowAnonymous]
        public ActionResult LoginAnunciante()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("IndexAnunciante", "DashboardAdministracao");
            return View();
        }

        [AllowAnonymous]
        public ActionResult HomeDivulgueAqui()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogarAdm(LoginAdm l)
        {
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string cmd = "SELECT COUNT(1) FROM LoginAdm Where Email=@email and Senha=@senha ";

                    SqlCommand command = new SqlCommand(cmd, connection);
                    command.Parameters.AddWithValue("@email", l.Email);
                    command.Parameters.AddWithValue("@senha", l.Senha);
                    command.Connection.Open();
                    var autenticou = int.Parse(command.ExecuteScalar().ToString());
                    if (autenticou > 0)
                    {
                        FormsAuthentication.SetAuthCookie(l.Email, false);
                        return RedirectToAction("IndexAdministracao", "DashboardAdministracao");
                    }
                    else
                    {
                        return RedirectToAction("LoginAdm");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }
        public ActionResult LogoutAdm()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginAdm", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogarAnunciante(LoginAnunciante l)
        {
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string cmd = "SELECT COUNT(1) FROM Anunciante Where Email=@email and Senha=@senha ";

                    SqlCommand command = new SqlCommand(cmd, connection);
                    command.Parameters.AddWithValue("@email", l.Email);
                    command.Parameters.AddWithValue("@senha", l.Senha);
                    command.Connection.Open();
                    var autenticou = int.Parse(command.ExecuteScalar().ToString());
                    if (autenticou > 0)
                    {
                        FormsAuthentication.SetAuthCookie(l.Email, false);
                        return RedirectToAction("IndexAnunciante", "DashboardAnunciante");
                    }
                    else
                    {
                        return RedirectToAction("LoginAdm");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }
        public ActionResult LogoutAnunciante()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginAnunciante", "Home");
        }

    }
}