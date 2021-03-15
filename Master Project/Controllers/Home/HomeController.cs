using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Master_Project.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        //Walidacja Google reCaptcha//
        [HttpPost]
        public ActionResult FormSubmit()
        {
            var response = Request["g-recaptcha-response"];
            string secretKey = "6Ldrc6YUAAAAAHoZ4-F2uoJ_dTG9XgQjvWg2uBBc";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            ViewBag.Message = status ? "Google reCaptcha validation success" : "Google reCaptcha validation failed";

            //When you will post form for save data, you should check both the model validation and google recaptcha validation
            //EX.
            /* if (ModelState.IsValid && status)
             {
 
             }*/

            //Here I am returning to Index page for demo perpose, you can use your view here
            return View();
        }
    }
}