using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Security;
using PersonalCare.Models;
using System.Web.Configuration;
using System.Web.UI;
using System.Windows;

namespace MvcApplication.Controllers
{
    public class UserLoginController : Controller
    {
        public string status;

        public object ClientScript { get; private set; }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Registration e)
        {
            String SqlCon = ConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString;
            SqlConnection con = new SqlConnection(SqlCon);
            string SqlQuery = "select Email,Password,FirstName from Registration where Email=@Email and Password=@Password";
            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
            cmd.Parameters.AddWithValue("@Email", e.Email);
            cmd.Parameters.AddWithValue("@Password", e.Password);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Session["UserName"] = sdr[2].ToString();
                con.Close();
                MessageBox.Show("Login Successful");
                return Redirect("../Home/Index");
            }
            else
            {
                ViewData["Message"] = "User Login Details Failed!!";
                con.Close();
                MessageBox.Show("Incorrect Credentials"); 
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "UserLogin");
        }

    }
}