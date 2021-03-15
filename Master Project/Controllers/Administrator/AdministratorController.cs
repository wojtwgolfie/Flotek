using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Master_Project.Controllers
{
    // Obowiązują tylko dla zalogowanego administratora
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        // GET: /Administrator/AdminPage
        public ActionResult AdminPage()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

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