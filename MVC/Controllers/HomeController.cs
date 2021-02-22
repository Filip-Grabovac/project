using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using PagedList;
using PagedList.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private AutaDBContext db = new AutaDBContext();

        // GET: Home
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.SortModelParameter = sortBy == "Model" ? "Model desc" : "Model";

            var cars = db.Cars.AsQueryable();

            if (searchBy == "Model")
            {
                cars = cars.Where(x => x.Model.StartsWith(search) || search == null);
            }
            else
            {
                cars = cars.Where(x => x.Name.StartsWith(search) || search == null);
            }

            switch(sortBy)
            {
                case "Name desc":
                    cars = cars.OrderByDescending(x => x.Name);
                    break;
                case "Model desc":
                    cars = cars.OrderByDescending(x => x.Model);
                    break;
                case "Model":
                    cars = cars.OrderBy(x => x.Model);
                    break;
                default:
                    cars = cars.OrderBy(x => x.Name);
                    break;
            }
            return View(cars.ToPagedList(page ?? 1, 7));
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = db.Cars.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Model,Abrv")] Cars cars)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(cars);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cars);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = db.Cars.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Model,Abrv")] Cars cars)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cars).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cars);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = db.Cars.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cars cars = db.Cars.Find(id);
            db.Cars.Remove(cars);
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
