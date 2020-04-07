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
    public class ClientesController : Controller
    {
        private Prog3FinalEntities db = new Prog3FinalEntities();

        // GET: Clientes
        public ActionResult Index()
        {
            var cliente = db.Cliente.Include(c => c.Categorias);
            return View(cliente.ToList());
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string name = fc["Nombre"];
            //string cat = fc["cat"].ToString();
            var cliente = db.Cliente.Include(c => c.Categorias);
            if (name != "")
            {
                cliente = (from c in db.Cliente where c.Nombre == name select c);
            }/*else if (cat != "")
            {
                int id = 0;
                if(cat == "Premium")
                {
                   id = 1;
                }else if(cat == "Regular")
                {
                    id = 2;
                }
                cliente = (from c in db.Cliente where c.id_categoria == id select c);
            }*/
            return View(cliente.ToList());
        }
        [HttpGet]
        public ActionResult Index(string cat)
        {
           
            var cliente = db.Cliente.Include(c => c.Categorias);
           if (cat != "")
            {
                int id = 0;
                if(cat == "Premium")
                {
                   id = 1;
                }else if(cat == "Regular")
                {
                    id = 2;
                }
                cliente = (from c in db.Cliente where c.id_categoria == id select c);
                ViewBag.Conteo = "Hay "+ cliente.Count() + " personas con esta categoria.";
            }
            else
            {
                ViewBag.Error = "Tiene que seleccionar una categoria para filtrar";
            }
            
            return View(cliente.ToList());
        }


        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "NombreCat");
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cliente,Cedula,Nombre,Telefono,Email,id_categoria")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "NombreCat", cliente.id_categoria);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "NombreCat", cliente.id_categoria);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cliente,Cedula,Nombre,Telefono,Email,id_categoria")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_categoria = new SelectList(db.Categorias, "id_categoria", "NombreCat", cliente.id_categoria);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
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
