﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace AparejosApp.Controllers
{
    public class PedidosController : Controller
    {
        private AparejosEntities db = new AparejosEntities();

        // GET: Pedidos
        public ActionResult Index()
        {
            var pedido = db.Pedido.Include(p => p.Clientes).Include(p => p.EstadoFabricacionPedido).Include(p => p.EstadoPagoPedido).Include(p => p.Productos).Include(p => p.TipoPedido).Include(p => p.TipoCarro);
            return View(pedido.ToList());
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Clientes, "ID", "NombreApellido");
            ViewBag.EstadoFabricacionPedidoID = new SelectList(db.EstadoFabricacionPedido, "ID", "Descripcion");
            ViewBag.EstadoPagoPedidoID = new SelectList(db.EstadoPagoPedido, "ID", "Descripcion");
            ViewBag.ProductoID = new SelectList(db.Productos, "ID", "Descripcion");
            ViewBag.TipoPedidoID = new SelectList(db.TipoPedido, "ID", "Descripcion");
            ViewBag.TipoCarroID = new SelectList(db.TipoCarro, "ID", "Descripcion");
            return View();
        }

        // POST: Pedidos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClienteID,ProductoID,EstadoPagoPedidoID,TipoPedidoID,EstadoFabricacionPedidoID,FechaEstimadaEntrega,FechaCreacion,FechaModificacion,Cantidad,TotalPrecio,Observaciones,Activo,TipoCarroID")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Pedido.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.Clientes, "ID", "NombreApellido", pedido.ClienteID);
            ViewBag.EstadoFabricacionPedidoID = new SelectList(db.EstadoFabricacionPedido, "ID", "Descripcion", pedido.EstadoFabricacionPedidoID);
            ViewBag.EstadoPagoPedidoID = new SelectList(db.EstadoPagoPedido, "ID", "Descripcion", pedido.EstadoPagoPedidoID);
            ViewBag.ProductoID = new SelectList(db.Productos, "ID", "Descripcion", pedido.ProductoID);
            ViewBag.TipoPedidoID = new SelectList(db.TipoPedido, "ID", "Descripcion", pedido.TipoPedidoID);
            ViewBag.TipoCarroID = new SelectList(db.TipoCarro, "ID", "Descripcion", pedido.TipoCarroID);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ID", "NombreApellido", pedido.ClienteID);
            ViewBag.EstadoFabricacionPedidoID = new SelectList(db.EstadoFabricacionPedido, "ID", "Descripcion", pedido.EstadoFabricacionPedidoID);
            ViewBag.EstadoPagoPedidoID = new SelectList(db.EstadoPagoPedido, "ID", "Descripcion", pedido.EstadoPagoPedidoID);
            ViewBag.ProductoID = new SelectList(db.Productos, "ID", "Descripcion", pedido.ProductoID);
            ViewBag.TipoPedidoID = new SelectList(db.TipoPedido, "ID", "Descripcion", pedido.TipoPedidoID);
            ViewBag.TipoCarroID = new SelectList(db.TipoCarro, "ID", "Descripcion", pedido.TipoCarroID);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClienteID,ProductoID,EstadoPagoPedidoID,TipoPedidoID,EstadoFabricacionPedidoID,FechaEstimadaEntrega,FechaCreacion,FechaModificacion,Cantidad,TotalPrecio,Observaciones,Activo,TipoCarroID")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ID", "NombreApellido", pedido.ClienteID);
            ViewBag.EstadoFabricacionPedidoID = new SelectList(db.EstadoFabricacionPedido, "ID", "Descripcion", pedido.EstadoFabricacionPedidoID);
            ViewBag.EstadoPagoPedidoID = new SelectList(db.EstadoPagoPedido, "ID", "Descripcion", pedido.EstadoPagoPedidoID);
            ViewBag.ProductoID = new SelectList(db.Productos, "ID", "Descripcion", pedido.ProductoID);
            ViewBag.TipoPedidoID = new SelectList(db.TipoPedido, "ID", "Descripcion", pedido.TipoPedidoID);
            ViewBag.TipoCarroID = new SelectList(db.TipoCarro, "ID", "Descripcion", pedido.TipoCarroID);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Pedido pedido = db.Pedido.Find(id);
            db.Pedido.Remove(pedido);
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
