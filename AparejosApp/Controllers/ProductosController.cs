using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using System.Data.Entity.Validation;

namespace AparejosApp.Controllers
{
    public class ProductosController : Controller
    {
        private AparejosEntities db = new AparejosEntities();

        // GET: Productos
        public ActionResult Index()
        {
            List<Productos> productosActivos = new List<Productos>();
            productosActivos = db.Productos.Where(x => x.Activo.Value).ToList();
            return View(productosActivos);
        }

        // GET: Productos/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos producto = db.Productos.Where(x => x.Activo.Value && x.ID == id).FirstOrDefault();
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Descripcion,Especificacion,Precio,MetrosCadena")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                productos.Activo = true;
                db.Productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Where(x => x.Activo.Value && x.ID == id).FirstOrDefault();
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Descripcion,Especificacion,Precio,MetrosCadena")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productos);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Where(x => x.Activo.Value && x.ID == id).FirstOrDefault();
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Productos producto = db.Productos.Where(x => x.Activo.Value && x.ID == id).FirstOrDefault();
            if (producto != null)
            {
                producto.Activo = false;
            }
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
