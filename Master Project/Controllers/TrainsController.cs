using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Master_Project.Data_Access_Layer__DAL_;
using Master_Project.Models;
using PagedList;

namespace Master_Project.Controllers
{
    public class TrainsController : Controller
    {
        private MasterProjectContext db = new MasterProjectContext();

        // GET: Trains
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "seriapociągu" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "stacja" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var trains = from t in db.Trains
                         select t;
            if (!String.IsNullOrEmpty(searchString))
            {
                trains = trains.Where(t => t.SeriaPociągu.Contains(searchString)
                                       || t.Stacja.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "seriapociągu":
                    trains = trains.OrderByDescending(t => t.SeriaPociągu);
                    break;
                case "stacja":
                    trains = trains.OrderByDescending(t => t.Stacja);
                    break;
                default:
                    trains = trains.OrderBy(t => t.SeriaPociągu);
                    break;
            }
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(trains.ToPagedList(pageNumber, pageSize));
        }

        // GET: Trains/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trains trains = db.Trains.Find(id);
            if (trains == null)
            {
                return HttpNotFound();
            }
            return View(trains);
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // GET: Trains/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // POST: Trains/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SeriaPociągu,NumerPociągu,Stacja,Rewizja,Adnotacje")] Trains trains)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["loco"];
                if(file != null && file.ContentLength > 0)
                {
                    trains.Foto = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + trains.Foto);
                }

                db.Trains.Add(trains);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trains);
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // GET: Trains/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trains trains = db.Trains.Find(id);
            if (trains == null)
            {
                return HttpNotFound();
            }
            return View(trains);
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // POST: Trains/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Seria,Numer,Stacja,Rewizja,Adnotacje")] Trains trains)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["loco"];
                if (file != null && file.ContentLength > 0)
                {
                    trains.Foto = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + trains.Foto);
                }

                db.Entry(trains).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trains);
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // GET: Trains/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trains trains = db.Trains.Find(id);
            if (trains == null)
            {
                return HttpNotFound();
            }
            return View(trains);
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // POST: Trains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trains trains = db.Trains.Find(id);
            db.Trains.Remove(trains);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
