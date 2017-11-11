using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lapscrap.DAL;
using lapscrap.Models;

namespace lapscrap.Controllers
{
    public class LaptopController : Controller
    {
        private LaptopContext db = new LaptopContext();

        // GET: Laptop
        public ActionResult Index()
        {
            return View(db.Laptops.ToList());
        }

        // GET: Laptop/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laptop laptop = db.Laptops.Find(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }
            return View(laptop);
        }

        // GET: Laptop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Laptop/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Title,Brand,Cpu,Ram,Size,Resolution,Gpu,Hd,Ssd,Width,Length,Height,Weight,Battery,Runtime,Price,Review_url,Shop_url,Ebay_url,Video_Url,Components")] Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                db.Laptops.Add(laptop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(laptop);
        }

        // GET: Laptop/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laptop laptop = db.Laptops.Find(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }
            return View(laptop);
        }

        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CpuSortParm = sortOrder == "cpu" ? "cpu_desc" : "cpu";
            ViewBag.CpuSortParm = sortOrder == "ram" ? "ram_desc" : "ram";

            var laptops = from l in db.Laptops
                           select l;
            switch (sortOrder)
            {
                case "name_desc":
                    laptops = laptops.OrderByDescending(l => l.Name);
                    break;
                case "cpu":
                    laptops = laptops.OrderBy(l => l.Cpu);
                    break;
                case "cpu_desc":
                    laptops = laptops.OrderByDescending(l => l.Cpu);
                    break;
                default:
                    laptops = laptops.OrderBy(l => l.Price);
                    break;
            }
            return View(laptops.ToList());
        }

        // POST: Laptop/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Title,Brand,Cpu,Ram,Size,Resolution,Gpu,Hd,Ssd,Width,Length,Height,Weight,Battery,Runtime,Price,Review_url,Shop_url,Ebay_url,Video_Url,Components")] Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laptop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(laptop);
        }

        // GET: Laptop/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laptop laptop = db.Laptops.Find(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }
            return View(laptop);
        }

        // POST: Laptop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Laptop laptop = db.Laptops.Find(id);
            db.Laptops.Remove(laptop);
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
