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
    public class ProfilesController : Controller
    {
        private MasterProjectContext db = new MasterProjectContext();

        [Authorize(Roles ="Administrator, Dispatcher")]
        // GET: Profiles
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "username" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "imię" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nazwisko" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "miasto" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "zakład" : "";
            var Profiles = from p in db.Profiles
                               select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                Profiles = Profiles.Where(p => p.UserName.Contains(searchString)
                                       || p.Imię.Contains(searchString)
                                       || p.Nazwisko.Contains(searchString)
                                       || p.Miasto.Contains(searchString)
                                       || p.Zakład.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "username":
                    Profiles = Profiles.OrderByDescending(t => t.UserName);
                    break;
                case "imię":
                    Profiles = Profiles.OrderByDescending(t => t.Imię);
                    break;
                case "nazwisko":
                    Profiles = Profiles.OrderByDescending(t => t.Nazwisko);
                    break;
                case "miasto":
                    Profiles = Profiles.OrderByDescending(t => t.Miasto);
                    break;
                case "zakład":
                    Profiles = Profiles.OrderByDescending(t => t.Zakład);
                    break;
                default:
                    Profiles = Profiles.OrderBy(t => t.UserName);
                    break;
            }
            return View(Profiles.ToList());
        }


        // GET: Profiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Profiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Imię,Nazwisko,Miasto,Zakład")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Profiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        [Authorize(Roles = "Administrator")]
        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
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
