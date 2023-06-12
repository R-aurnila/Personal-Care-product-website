using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalCare.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Windows;

namespace PersonalCare.Controllers
{
    public class RegistrationController : Controller
    {
        public string value = "";

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Registration e)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);
            if (Request.HttpMethod == "POST")
            {
                Registration er = new Registration();
                using (SqlCommand cmd = new SqlCommand("SP_RegistrationDetail", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", e.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", e.LastName);
                    cmd.Parameters.AddWithValue("@Password", e.Password);
                    cmd.Parameters.AddWithValue("@Gender", e.Gender);
                    cmd.Parameters.AddWithValue("@Email", e.Email);
                    cmd.Parameters.AddWithValue("@Phone", e.PhoneNumber);
                    cmd.Parameters.AddWithValue("@status", "INSERT");
                    con.Open();
                    ViewData["result"] = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            MessageBox.Show("Registration Successful");
            return Redirect("../UserLogin/Index");
        }
    }
}