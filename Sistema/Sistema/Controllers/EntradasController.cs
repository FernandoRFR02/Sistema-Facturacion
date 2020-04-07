using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class EntradasController : Controller
    {
        private Prog3FinalEntities db = new Prog3FinalEntities();

        // GET: Entradas
        public ActionResult Index()
        {
            var entradas = db.Entradas.Include(e => e.Producto).Include(e => e.Proveedor);
            return View(entradas.ToList());
        }

        // GET: Entradas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entradas entradas = db.Entradas.Find(id);
            if (entradas == null)
            {
                return HttpNotFound();
            }
            return View(entradas);
        }

        // GET: Entradas/Create
        public ActionResult Create()
        {
            ViewBag.id_producto = new SelectList(db.Producto, "id_producto", "Nombre");
            ViewBag.id_proveedor = new SelectList(db.Proveedor, "id_proveedor", "Nombre");
            return View();
        }

        // POST: Entradas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_entrada,id_producto,id_proveedor,Cantidad,Fecha")] Entradas entradas)
        {
            if (ModelState.IsValid)
            {
                db.Entradas.Add(entradas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_producto = new SelectList(db.Producto, "id_producto", "Nombre", entradas.id_producto);
            ViewBag.id_proveedor = new SelectList(db.Proveedor, "id_proveedor", "Nombre", entradas.id_proveedor);
            return View(entradas);
        }

        // GET: Entradas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entradas entradas = db.Entradas.Find(id);
            if (entradas == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_producto = new SelectList(db.Producto, "id_producto", "Nombre", entradas.id_producto);
            ViewBag.id_proveedor = new SelectList(db.Proveedor, "id_proveedor", "Nombre", entradas.id_proveedor);
            return View(entradas);
        }

        // POST: Entradas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_entrada,id_producto,id_proveedor,Cantidad,Fecha")] Entradas entradas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entradas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_producto = new SelectList(db.Producto, "id_producto", "Nombre", entradas.id_producto);
            ViewBag.id_proveedor = new SelectList(db.Proveedor, "id_proveedor", "Nombre", entradas.id_proveedor);
            return View(entradas);
        }

        // GET: Entradas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entradas entradas = db.Entradas.Find(id);
            if (entradas == null)
            {
                return HttpNotFound();
            }
            return View(entradas);
        }

        // POST: Entradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entradas entradas = db.Entradas.Find(id);
            db.Entradas.Remove(entradas);
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
