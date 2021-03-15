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
    public class ServicePlansController : Controller
    {
        private MasterProjectContext db = new MasterProjectContext();

        [Authorize(Roles = "Administrator, Dispatcher, Worker")]
        // GET: ServicePlans
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "username" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nazwa" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "obsługa" : "";
            var ServicePlans = from s in db.ServicePlans
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                ServicePlans = ServicePlans.Where(s => s.UserName.Contains(searchString)
                                       || s.Nazwa.Contains(searchString)
                                       || s.Obsługa.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "username":
                    ServicePlans = ServicePlans.OrderByDescending(t => t.UserName);
                    break;
                case "nazwa":
                    ServicePlans = ServicePlans.OrderByDescending(t => t.Nazwa);
                    break;
                case "obsługa":
                    ServicePlans = ServicePlans.OrderByDescending(t => t.Obsługa);
                    break;
                default:
                    ServicePlans = ServicePlans.OrderBy(t => t.UserName);
                    break;
            }
            return View(db.ServicePlans.ToList());
        }

        [Authorize(Roles = "Administrator, Dispatcher, Worker")]
        // GET: ServicePlans/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePlan servicePlan = db.ServicePlans.Find(id);
            if (servicePlan == null)
            {
                return HttpNotFound();
            }
            return View(servicePlan);
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // GET: ServicePlans/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: ServicePlans/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Dispatcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Nazwa,NumerSkładu,SeriaPociągu,NumerPociągu,UwagiDyspozytora")] ServicePlan servicePlan)
        {
            if (ModelState.IsValid)
            {
                MasterProjectContext db = new MasterProjectContext();
                db.ServicePlans.Add(servicePlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(servicePlan);
        }

        // GET: ServicePlans/Edit/5
        [Authorize(Roles = "Administrator, Dispatcher")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePlan servicePlan = db.ServicePlans.Find(id);
            if (servicePlan == null)
            {
                return HttpNotFound();
            }
            return View(servicePlan);
        }

        // POST: ServicePlans/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Dispatcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Nazwa,PoczątekSłużby,ZakończenieSłużby,StartSłużby,KoniecSłużby,Obsługa,UwagiDyspozytora")] ServicePlan servicePlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicePlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servicePlan);
        }

        [Authorize(Roles = "Administrator, Dispatcher")]
        // GET: ServicePlans/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePlan servicePlan = db.ServicePlans.Find(id);
            if (servicePlan == null)
            {
                return HttpNotFound();
            }
            return View(servicePlan);
        }

        // POST: ServicePlans/Delete/5
        [Authorize(Roles = "Administrator, Dispatcher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ServicePlan servicePlan = db.ServicePlans.Find(id);
            db.ServicePlans.Remove(servicePlan);
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
