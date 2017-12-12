using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using AparejosApp.Models;
using static AparejosApp.Models.Enumeradores;

namespace AparejosApp.Controllers
{
    public class PedidosController : Controller
    {

        private AparejosEntities db = new AparejosEntities();

        // GET: Pedidos
        public ActionResult Index(string rangoFechas = "",
                                  TipoFiltroPedidos tipoFiltro = TipoFiltroPedidos.Normal)
        {
            List<Pedido> pedidos = new List<Pedido>();
            DateTime fechaDesde = new DateTime(1000000);
            DateTime fechaHasta = new DateTime();
            bool fechaDesdeCorrecta = false;
            bool fechaHastaCorrecta = false;
            
            switch (tipoFiltro)
            {
                case TipoFiltroPedidos.Normal:
                    pedidos = db.Pedido.Include(p => p.Clientes)
                        .Include(p => p.EstadoFabricacionPedido)
                        .Include(p => p.EstadoPagoPedido)
                        .Include(p => p.Productos)
                        .Include(p => p.TipoPedido)
                        .Include(p => p.TipoCarro)
                        .ToList();
                    break;
                case TipoFiltroPedidos.PorRangoFechaYTerminados:
                    if (!String.IsNullOrEmpty(rangoFechas))
                    {
                        fechaDesdeCorrecta = DateTime.TryParse(rangoFechas.Split('-')[0].Trim(), out fechaDesde);
                        fechaHastaCorrecta = DateTime.TryParse(rangoFechas.Split('-')[1].Trim(), out fechaHasta);
                    }
                    if (fechaDesdeCorrecta && fechaHastaCorrecta)
                    {
                        pedidos = db.Pedido.Include(p => p.Clientes)
                        .Include(p => p.EstadoFabricacionPedido)
                        .Include(p => p.EstadoPagoPedido)
                        .Include(p => p.Productos)
                        .Include(p => p.TipoPedido)
                        .Include(p => p.TipoCarro)
                        .Where(p => p.EstadoFabricacionPedidoID == (int)Enumeradores.EstadoFabricacionPedido.Terminado
                            && p.FechaModificacion >= fechaDesde && p.FechaModificacion <= fechaHasta)
                        .OrderByDescending(p => p.FechaModificacion).ToList();
                    }
                    break;
                case TipoFiltroPedidos.Terminados:
                    pedidos.AddRange(db.Pedido.Include(p => p.Clientes)
                        .Include(p => p.EstadoFabricacionPedido)
                        .Include(p => p.EstadoPagoPedido)
                        .Include(p => p.Productos)
                        .Include(p => p.TipoPedido)
                        .Include(p => p.TipoCarro)
                        .Where(p => p.EstadoFabricacionPedidoID == (int)Enumeradores.EstadoFabricacionPedido.Terminado
                            && p.TipoPedidoID == (int)Enumeradores.TipoPedido.Urgente)
                        .OrderByDescending(p => p.FechaModificacion).ToList());

                    pedidos.AddRange(db.Pedido.Include(p => p.Clientes)
                        .Include(p => p.EstadoFabricacionPedido)
                        .Include(p => p.EstadoPagoPedido)
                        .Include(p => p.Productos)
                        .Include(p => p.TipoPedido)
                        .Include(p => p.TipoCarro)
                        .Where(p => p.EstadoFabricacionPedidoID == (int)Enumeradores.EstadoFabricacionPedido.Terminado
                            && p.TipoPedidoID == (int)Enumeradores.TipoPedido.Reclamo)
                        .OrderByDescending(p => p.FechaModificacion).ToList());

                    pedidos.AddRange(db.Pedido.Include(p => p.Clientes)
                        .Include(p => p.EstadoFabricacionPedido)
                        .Include(p => p.EstadoPagoPedido)
                        .Include(p => p.Productos)
                        .Include(p => p.TipoPedido)
                        .Include(p => p.TipoCarro)
                        .Where(p => p.EstadoFabricacionPedidoID == (int)Enumeradores.EstadoFabricacionPedido.Terminado
                            && p.TipoPedidoID == (int)Enumeradores.TipoPedido.Normal)
                        .OrderByDescending(p => p.FechaModificacion).ToList());
                    break;
            }

            return View(pedidos);
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
            ViewBag.ClienteID = new SelectList(db.Clientes.Where(x => x.Activo != null && x.Activo.Value), "ID", "NombreApellido");
            //ViewBag.EstadoFabricacionPedidoID = new SelectList(db.EstadoFabricacionPedido.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion");
            ViewBag.EstadoPagoPedidoID = new SelectList(db.EstadoPagoPedido.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion");
            ViewBag.ProductoID = new SelectList(db.Productos.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion");
            ViewBag.ListProductos = GetListProductos();
            ViewBag.TipoPedidoID = new SelectList(db.TipoPedido.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion");
            ViewBag.TipoCarroID = new SelectList(db.TipoCarro.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion");
            return View();
        }

        private List<SelectProducto> GetListProductos()
        {
            List<SelectProducto> listProductos = new List<SelectProducto>();
            listProductos.AddRange(db.Productos.Where(x => x.Activo != null && x.Activo.Value).Select(y => new SelectProducto() {
                ID = y.ID,
                Precio = y.Precio
            }).ToList());

            return listProductos;
        }
        // POST: Pedidos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.Activo = true;
                pedido.FechaCreacion = DateTime.Now;
                pedido.EstadoFabricacionPedidoID = (int)Enumeradores.EstadoFabricacionPedido.Pedido;
                db.Pedido.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.Clientes.Where(x => x.Activo != null && x.Activo.Value), "ID", "NombreApellido");
            ViewBag.EstadoFabricacionPedidoID = new SelectList(db.EstadoFabricacionPedido.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion");
            ViewBag.EstadoPagoPedidoID = new SelectList(db.EstadoPagoPedido.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion");
            ViewBag.ProductoID = new SelectList(db.Productos.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion");
            ViewBag.ListProductos = GetListProductos();
            ViewBag.TipoPedidoID = new SelectList(db.TipoPedido.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion");
            ViewBag.TipoCarroID = new SelectList(db.TipoCarro.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion");
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
            ViewBag.ClienteID = new SelectList(db.Clientes.Where(x => x.Activo != null && x.Activo.Value), "ID", "NombreApellido", pedido.ClienteID);
            ViewBag.EstadoFabricacionPedidoID = new SelectList(db.EstadoFabricacionPedido.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion", pedido.EstadoFabricacionPedidoID);
            ViewBag.EstadoPagoPedidoID = new SelectList(db.EstadoPagoPedido.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion", pedido.EstadoPagoPedidoID);
            ViewBag.ProductoID = new SelectList(db.Productos.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion",pedido.ProductoID);
            ViewBag.ListProductos = GetListProductos();
            ViewBag.TipoPedidoID = new SelectList(db.TipoPedido.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion", pedido.TipoPedidoID);
            ViewBag.TipoCarroID = new SelectList(db.TipoCarro.Where(x => x.Activo != null && x.Activo.Value), "ID", "Descripcion", pedido.TipoCarroID);
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
                pedido.FechaModificacion = DateTime.Now;
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
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(long id)
        {
            bool seBorro = false;
            try
            {
                Pedido pedido = db.Pedido.Find(id);
                db.Pedido.Remove(pedido);
                db.SaveChanges();
                seBorro = true;
            }
            catch (Exception)
            {
                seBorro = false;
            }
            
            return Json(seBorro, JsonRequestBehavior.AllowGet);
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
