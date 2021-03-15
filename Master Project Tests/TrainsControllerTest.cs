using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Master_Project;
using Master_Project.Models;
using System.Data.Entity;

namespace Master_Project.Controllers
{
    [TestClass]
    public class TrainsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            TrainsController controller = new TrainsController();
            ViewResult result = controller.Index(sortOrder: "", searchString: "", currentFilter: "", page: 1) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            TrainsController controller = new TrainsController();
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Add()
        {
            TrainsController controller = new TrainsController();
            ViewResult result = controller.Create() as ViewResult;
            Trains trains = new Trains { Id = 1, SeriaPociągu = "45WE", NumerPociągu = "001", Stacja = "KMKOL Warszawa Grochów", Rewizja = "P1: 30.11.2021 | Newag Nowy Sącz", Foto = "", Adnotacje = "Brak" };
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete()
        {
            TrainsController controller = new TrainsController();
            ViewResult result = controller.Delete(1) as ViewResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Details()
        {
            TrainsController controller = new TrainsController();
            ViewResult result = controller.Details(1) as ViewResult;
            Assert.IsNull(result);
        }
    }
}
