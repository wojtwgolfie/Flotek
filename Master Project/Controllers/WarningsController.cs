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
    public class WarningsController : Controller
    {
        private MasterProjectContext db = new MasterProjectContext();

        // GET: Warnings
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nazwa" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "od" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "do" : "";
            var Warnings = from w in db.Warningslist
                             select w;
            if (!String.IsNullOrEmpty(searchString))
            {
                Warnings = Warnings.Where(w => w.Nazwa.Contains(searchString)
                                       || w.Od.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nazwa":
                    Warnings = Warnings.OrderByDescending(t => t.Nazwa);
                    break;
                case "od":
                    Warnings = Warnings.OrderByDescending(t => t.Od);
                    break;
                case "do":
                    Warnings = Warnings.OrderByDescending(t => t.Do);
                    break;
                default:
                    Warnings = Warnings.OrderBy(t => t.Nazwa);
                    break;
            }
            return View(db.Warningslist.ToList());
        }

        // GET: Warnings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WarningsList warningsList = db.Warningslist.Find(id);
            if (warningsList == null)
            {
                return HttpNotFound();
            }
            return View(warningsList);
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // GET: Warnings/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // POST: Warnings/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa,Od,Do,Odcinek,Szczegóły")] WarningsList warningsList)
        {
            if (ModelState.IsValid)
            {
                db.Warningslist.Add(warningsList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(warningsList);
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // GET: Warnings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WarningsList warningsList = db.Warningslist.Find(id);
            if (warningsList == null)
            {
                return HttpNotFound();
            }
            return View(warningsList);
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // POST: Warnings/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa,Od,Do,Odcinek,Szczegóły")] WarningsList warningsList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(warningsList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(warningsList);
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // GET: Warnings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WarningsList warningsList = db.Warningslist.Find(id);
            if (warningsList == null)
            {
                return HttpNotFound();
            }
            return View(warningsList);
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // POST: Warnings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WarningsList warningsList = db.Warningslist.Find(id);
            db.Warningslist.Remove(warningsList);
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
