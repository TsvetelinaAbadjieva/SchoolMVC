using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolMVC.Models;
using System.Data.Linq;
using System.Net.Mail;
using System.Net;

namespace SchoolMVC.Controllers
{
    public class LoginController : Controller
    {
        SchoolDataContext Context;
        public LoginController()
        {
            Context = new SchoolDataContext();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [ActionName("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [ActionName("Login")]
        public ActionResult Validate(Login model)
        {
            ViewBag.Message = "Your Login page.";
            if (ModelState.IsValid)
            {

                var result = Context.GetUser(model.UserName, model.Password).FirstOrDefault();
                
               
                    if (result == null)
                    {
                        TempData["msg"] = "Username or password are incorrect!";
                        Session["UserName"] = null;
                        return View("Login",model);
                    }

                    else
                    {
                        Session["UserName"] = model.UserName;
                        return RedirectToAction("UserDashboard");
                    }
                
                
            }
            else return View(model);

        }
        public ActionResult UserDashboard()
        {
            if (Session["UserName"] == null)
                return RedirectToAction("Login");
            else
            {
               // ViewBag.Message = "Wellcome, " + Session["UserName"] + "!  You are successfully connected to Your Dashboard!";
                //Session["UserName"] = UserName;

                return View();
            }
        }
        [HttpGet]
       [ActionName("RestorePassword")]
       //[ValidateAntiForgeryToken()]
        public ActionResult RestorePassword(string username)
        {
            if (username == null || username == "")

                //if ((Session["UserName"] == null || Session["UserName"].ToString()=="") && string.IsNullOrEmpty(Session["Password"].ToString()))

                return RedirectToAction("Login");
            else
            {
                var password = from m in Context.tbUsers.Where(m => m.UserName == Session["UserName"].ToString()) select m.Password;
                // var userName = Session["UserName"].ToString();
                var userName = username;
                if (password != null)
                {
                    MailMessage message = new MailMessage("sender@gmail.com", userName);
                    message.Subject = "Password recovery";
                    message.IsBodyHtml = true;
                    message.Body = string.Format("Hi { 0},< br />< br /> Your password is { 1}.< br />< br /> Thank You.<br/><a href="+ "localhost: 59157 / Login / Login"+">Confirm password</a>", userName, password);

                    SmtpClient mail = new SmtpClient();
                    mail.Host = "smtp.gmail.com";
                    mail.EnableSsl = true;

                    NetworkCredential netCredentials = new NetworkCredential();
                    netCredentials.UserName = userName;
                    netCredentials.Password = "<password>";
                    mail.UseDefaultCredentials = true;
                    mail.Credentials = netCredentials;
                    mail.Port = 587;
                    mail.Send(message);
                    return View();

                }
                else ViewBag.Message =string.Format( "Email address doesn't match!");

                return RedirectToAction("Login");
            }
        }
    }
}