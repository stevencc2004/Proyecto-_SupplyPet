using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_PetSupply.Models;

namespace Proyecto_PetSupply.Controllers
{
    public class VentaController : Controller
    {
        private SupplyPetEntities3 db = new SupplyPetEntities3();

        // GET: Venta
        public ActionResult Index()
        {
            var venta = db.Venta.Include(v => v.Cliente);
            return View(venta.ToList());

        }

        // GET: Venta
        public ActionResult Index2()
        {
            var venta = db.Venta.Include(v => v.Cliente);
            return View(venta.ToList());

        }

        // GET: Venta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // GET: Venta/Details/5
        public ActionResult Details2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // GET: Venta/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Nombre");
            return View();
        }

        // POST: Venta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Cliente,Id_Empleado,Monto,Fecha")] Venta venta)
        {
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Nombre", venta.Id_Cliente);

            if (ModelState.IsValid)
            {
                db.Venta.Add(venta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(venta);
        }

        // GET: Venta/Create
        public ActionResult Create2()
        {
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Nombre");
            return View();
        }

        // POST: Venta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2([Bind(Include = "Id,Id_Cliente,Id_Empleado,Monto,Fecha")] Venta venta)
        {
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Nombre", venta.Id_Cliente);

            if (ModelState.IsValid)
            {
                db.Venta.Add(venta);
                db.SaveChanges();
                return RedirectToAction("Index2");
            }

            return View(venta);
        }

        // GET: Venta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Nombre", venta.Id_Cliente);
            return View(venta);
        }

        // POST: Venta/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Cliente,Id_Empleado,Monto,Fecha")] Venta venta)
        {
                
            venta.Monto = (int)CalcularMontoTotal(venta.Id);

            if (ModelState.IsValid)
            {
                   db.Entry(venta).State = EntityState.Modified;
                   db.SaveChanges();
                   return RedirectToAction("Index");
            }
              
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Nombre", venta.Id_Cliente);
            return View(venta);
        }

        // GET: Venta/Edit/5
        public ActionResult Edit2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Nombre", venta.Id_Cliente);
            return View(venta);
        }

        // POST: Venta/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "Id,Id_Cliente,Id_Empleado,Monto,Fecha")] Venta venta)
        {

            venta.Monto = (int)CalcularMontoTotal(venta.Id);

            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index2");
            }

            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Nombre", venta.Id_Cliente);
            return View(venta);
        }

        // GET: Venta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Venta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venta venta = db.Venta.Find(id);

            // Obtener todos los detalles de venta asociados a esta venta
            var detallesVenta = db.Detalle_Venta.Where(d => d.Id_Venta == id).ToList();

            // Eliminar cada detalle de venta encontrado
            foreach (var detalle in detallesVenta)
            {
                int idproducto = 0;
                idproducto = detalle.Id_Producto;
                Producto producto = db.Producto.Find(idproducto);
                if (producto != null) 
                {
                    producto.Stock += detalle.Cantidad;
                }
                db.Detalle_Venta.Remove(detalle);
            }
            db.Venta.Remove(venta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Venta/Delete/5
        public ActionResult Delete2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Venta/Delete/5
        [HttpPost, ActionName("Delete2")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int id)
        {
            Venta venta = db.Venta.Find(id);

            // Obtener todos los detalles de venta asociados a esta venta
            var detallesVenta = db.Detalle_Venta.Where(d => d.Id_Venta == id).ToList();

            // Eliminar cada detalle de venta encontrado
            foreach (var detalle in detallesVenta)
            {
                int idproducto = 0;
                idproducto = detalle.Id_Producto;
                Producto producto = db.Producto.Find(idproducto);
                if (producto != null)
                {
                    producto.Stock += detalle.Cantidad;
                }
                db.Detalle_Venta.Remove(detalle);
            }
            db.Venta.Remove(venta);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private decimal CalcularMontoTotal(int idVenta)
        {
            decimal montoTotal = 0;
            var detallesVenta = db.Detalle_Venta.Where(d => d.Id_Venta == idVenta).ToList();
            foreach (var detalle in detallesVenta)
            {
                montoTotal += (int)detalle.Subtotal;

            }
            return montoTotal;

        }
        
    }
}
