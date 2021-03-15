using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Master_Project;
using System.Web.Mvc;

namespace Master_Project.Controllers
{
    [TestClass]
    public class ProfilesControllerTest
    {
        [TestMethod]
        public void Index()
        {
            ProfilesController controller = new ProfilesController();
            ViewResult result = controller.Index(sortOrder: "", searchString: "") as ViewResult;
            Assert.IsNotNull(result);
        }


    }
}
