using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sistema.Models;
using System.Data.SqlClient;

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
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string name = fc["entrega"];
            var entradas = db.Entradas.Include(e => e.Producto).Include(e => e.Proveedor);
            if (name != "")
            {
                entradas = (from e in db.Entradas where e.Fecha.ToString() == name select e);
            }
            return View(entradas.ToList());
        }

        public void AddStock(Entradas entradas)
        {
            if (ModelState.IsValid)
            {
                var cantidad = entradas.Cantidad;

            }
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
            var idpro = entradas.id_producto;
            var canti = entradas.Cantidad;
            string strConString = @"data source=FERNANDORFR;initial catalog=Prog3Final;integrated security=True";
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string comando = "Insert into Stock(id_producto,cantidad) values (" + idpro + "," + canti + ")";
                SqlCommand cmd = new SqlCommand(comando, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
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
