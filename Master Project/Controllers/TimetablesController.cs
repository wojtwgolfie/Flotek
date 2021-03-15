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

namespace Master_Project.Controllers
{
    public class TimetablesController : Controller
    {
        private MasterProjectContext db = new MasterProjectContext();

        // GET: Timetables
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "kategoria" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "numer" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "stacjapoczątkowa" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "stacjakońcowa" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "obsługa" : "";
            var Timetables = from tb in db.Timetables
                         select tb;
            if (!String.IsNullOrEmpty(searchString))
            {
                Timetables = Timetables.Where(tb => tb.Kategoria.Contains(searchString)
                                       || tb.Numer.Contains(searchString)
                                       || tb.StacjaPoczątkowa.Contains(searchString)
                                       || tb.StacjaKońcowa.Contains(searchString)
                                       || tb.Obsługa.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "kategoria":
                    Timetables = Timetables.OrderByDescending(t => t.Kategoria);
                    break;
                case "numerskładu":
                    Timetables = Timetables.OrderByDescending(t => t.Numer);
                    break;
                case "stacjapoczątkowa":
                    Timetables = Timetables.OrderByDescending(t => t.StacjaPoczątkowa);
                    break;
                case "stacjakońcowa":
                    Timetables = Timetables.OrderByDescending(t => t.StacjaKońcowa);
                    break;
                case "obsługa":
                    Timetables = Timetables.OrderByDescending(t => t.Obsługa);
                    break;
                default:
                    Timetables = Timetables.OrderBy(t => t.Kategoria);
                    break;
            }
            return View(db.Timetables.ToList());
        }

        // GET: Timetables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetables timetables = db.Timetables.Find(id);
            if (timetables == null)
            {
                return HttpNotFound();
            }
            return View(timetables);
        }

        // GET: Timetables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Timetables/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Kategoria,Numer,Nazwa,StacjaPoczątkowa,StacjaKońcowa,Odjazd,Przyjazd,Obsługa,Uwagi")] Timetables timetables)
        {
            if (ModelState.IsValid)
            {
                db.Timetables.Add(timetables);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetables);
        }

        // GET: Timetables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetables timetables = db.Timetables.Find(id);
            if (timetables == null)
            {
                return HttpNotFound();
            }
            return View(timetables);
        }

        // POST: Timetables/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Kategoria,Numer,Nazwa,StacjaPoczątkowa,StacjaKońcowa,Odjazd,Przyjazd,Obsługa,Uwagi")] Timetables timetables)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetables).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetables);
        }

        // GET: Timetables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetables timetables = db.Timetables.Find(id);
            if (timetables == null)
            {
                return HttpNotFound();
            }
            return View(timetables);
        }

        // POST: Timetables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Timetables timetables = db.Timetables.Find(id);
            db.Timetables.Remove(timetables);
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
