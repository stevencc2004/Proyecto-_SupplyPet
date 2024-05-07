using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Proyecto_PetSupply.Models;

namespace Proyecto_PetSupply.Controllers
{
    public class Detalle_VentaController : Controller
    {
        private SupplyPetEntities3 db = new SupplyPetEntities3();

        // GET: Detalle_Venta
        public ActionResult Index(int? id)
        {
            var detalle_Venta = db.Detalle_Venta.Include(d => d.Producto).Include(d => d.Venta).Where(d => d.Id_Venta == id);

            if (!detalle_Venta.Any())
            {
                // Si no hay detalles de venta, redirigir a la acción Create con el id de venta especificado
                return RedirectToAction("CreateFirst", new { id = id });
            }
            else 
            {
                // Si hay detalles de venta, mostrar la vista Index con los detalles de venta encontrados
                ViewBag.Id_Venta = id;
                return View(detalle_Venta.ToList());
            }

        }

        // GET: Detalle_Venta
        public ActionResult Index2(int? id)
        {
            var detalle_Venta = db.Detalle_Venta.Include(d => d.Producto).Include(d => d.Venta).Where(d => d.Id_Venta == id);

            if (!detalle_Venta.Any())
            {
                // Si no hay detalles de venta, redirigir a la acción Create con el id de venta especificado
                return RedirectToAction("CreateFirst2", new { id = id });
            }
            else
            {
                // Si hay detalles de venta, mostrar la vista Index con los detalles de venta encontrados
                ViewBag.Id_Venta = id;
                return View(detalle_Venta.ToList());
            }

        }

        // GET: Detalle_Venta/Create
        public ActionResult Create(int? id)
        {
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id", "Nombre");
            ViewBag.Id_Venta = id;//new SelectList(db.Venta, "Id", "Id");
            return View();
        }

        // POST: Detalle_Venta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Venta,Id_Producto,Cantidad,Subtotal")] Detalle_Venta detalle_Venta, int id)
        {

            Producto producto = db.Producto.Find(detalle_Venta.Id_Producto);
            Venta venta = db.Venta.Find(id);

            if (producto != null && venta != null)
            {
                // Calcular el subtotal multiplicando el precio del producto por la cantidad
                detalle_Venta.Subtotal = (int)CalcularSubtotal(producto.Id, detalle_Venta.Cantidad);
                detalle_Venta.Id_Venta = id;
                venta.Monto = (int)CalcularMontoTotal(venta.Id);

                if (ModelState.IsValid)
                {
                    db.Detalle_Venta.Add(detalle_Venta);
                    db.SaveChanges();

                    producto.Stock -= detalle_Venta.Cantidad;
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();

                    venta.Monto = (int)CalcularMontoTotal(venta.Id);
                    if (ModelState.IsValid)
                    {
                        db.Entry(venta).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", new { id = detalle_Venta.Id_Venta });
                    }
                    return RedirectToAction("Index", new { id = detalle_Venta.Id_Venta });
                }
            }

            ViewBag.Id_Producto = new SelectList(db.Producto, "Id", "Nombre", detalle_Venta.Id_Producto);
            ViewBag.Id_Venta = new SelectList(db.Venta, "Id", "Id", detalle_Venta.Id_Venta);
            return View(detalle_Venta);
        }

        // GET: Detalle_Venta/Create
        public ActionResult Create2(int? id)
        {
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id", "Nombre");
            ViewBag.Id_Venta = id;//new SelectList(db.Venta, "Id", "Id");
            return View();
        }

        // POST: Detalle_Venta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2([Bind(Include = "Id_Venta,Id_Producto,Cantidad,Subtotal")] Detalle_Venta detalle_Venta, int id)
        {

            Producto producto = db.Producto.Find(detalle_Venta.Id_Producto);
            Venta venta = db.Venta.Find(id);

            if (producto != null && venta != null)
            {
                // Calcular el subtotal multiplicando el precio del producto por la cantidad
                detalle_Venta.Subtotal = (int)CalcularSubtotal(producto.Id, detalle_Venta.Cantidad);
                detalle_Venta.Id_Venta = id;
                venta.Monto = (int)CalcularMontoTotal(venta.Id);

                if (ModelState.IsValid)
                {
                    db.Detalle_Venta.Add(detalle_Venta);
                    db.SaveChanges();

                    producto.Stock -= detalle_Venta.Cantidad;
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();

                    venta.Monto = (int)CalcularMontoTotal(venta.Id);
                    if (ModelState.IsValid)
                    {
                        db.Entry(venta).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index2", new { id = detalle_Venta.Id_Venta });
                    }
                    return RedirectToAction("Index2", new { id = detalle_Venta.Id_Venta });
                }
            }

            ViewBag.Id_Producto = new SelectList(db.Producto, "Id", "Nombre", detalle_Venta.Id_Producto);
            ViewBag.Id_Venta = new SelectList(db.Venta, "Id", "Id", detalle_Venta.Id_Venta);
            return View(detalle_Venta);
        }

        public ActionResult CreateFirst(int? id)
        {
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id", "Nombre");
            ViewBag.Id_Venta = new SelectList(db.Venta, "Id", "Id");
            return View();
        }

        // POST: Detalle_Venta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFirst([Bind(Include = "Id_Venta,Id_Producto,Cantidad,Subtotal")] Detalle_Venta detalle_Venta, int? id)
        {

            Producto producto = db.Producto.Find(detalle_Venta.Id_Producto);
            Venta venta = db.Venta.Find(id);

            if (producto != null && venta != null)
            {
                // Calcular el subtotal multiplicando el precio del producto por la cantidad
                detalle_Venta.Subtotal = (int)CalcularSubtotal(producto.Id, detalle_Venta.Cantidad);

                if (ModelState.IsValid)
                {
                    db.Detalle_Venta.Add(detalle_Venta);
                    db.SaveChanges();

                    producto.Stock -= detalle_Venta.Cantidad;
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();

                    venta.Monto = (int)CalcularMontoTotal(venta.Id);
                    if (ModelState.IsValid)
                    {
                        db.Entry(venta).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", new { id = detalle_Venta.Id_Venta });
                    }
                    return RedirectToAction("Index", new { id = detalle_Venta.Id_Venta });
                }
               
            }

            ViewBag.Id_Producto = new SelectList(db.Producto, "Id", "Nombre", detalle_Venta.Id_Producto);
            ViewBag.Id_Venta = new SelectList(db.Venta, "Id", "Id", detalle_Venta.Id_Venta);
            return View(detalle_Venta);
        }

        public ActionResult CreateFirst2(int? id)
        {
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id", "Nombre");
            ViewBag.Id_Venta = new SelectList(db.Venta, "Id", "Id");
            return View();
        }

        // POST: Detalle_Venta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFirst2([Bind(Include = "Id_Venta,Id_Producto,Cantidad,Subtotal")] Detalle_Venta detalle_Venta, int? id)
        {

            Producto producto = db.Producto.Find(detalle_Venta.Id_Producto);
            Venta venta = db.Venta.Find(id);

            if (producto != null && venta != null)
            {
                // Calcular el subtotal multiplicando el precio del producto por la cantidad
                detalle_Venta.Subtotal = (int)CalcularSubtotal(producto.Id, detalle_Venta.Cantidad);

                if (ModelState.IsValid)
                {
                    db.Detalle_Venta.Add(detalle_Venta);
                    db.SaveChanges();

                    producto.Stock -= detalle_Venta.Cantidad;
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();

                    venta.Monto = (int)CalcularMontoTotal(venta.Id);
                    if (ModelState.IsValid)
                    {
                        db.Entry(venta).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index2", new { id = detalle_Venta.Id_Venta });
                    }
                    return RedirectToAction("Index2", new { id = detalle_Venta.Id_Venta });
                }

            }

            ViewBag.Id_Producto = new SelectList(db.Producto, "Id", "Nombre", detalle_Venta.Id_Producto);
            ViewBag.Id_Venta = new SelectList(db.Venta, "Id", "Id", detalle_Venta.Id_Venta);
            return View(detalle_Venta);
        }

        // GET: Detalle_Venta/Edit/5
        public ActionResult Edit(int? iid, int? iid2)
        {
            if (iid == null || iid2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Detalle_Venta detalle_Venta = db.Detalle_Venta.FirstOrDefault(d => d.Id_Venta == iid && d.Id_Producto == iid2);


            if (detalle_Venta == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id_Producto = new SelectList(db.Producto, "Id", "Nombre", detalle_Venta.Id_Producto);
            ViewBag.Id_Venta = iid;//new SelectList(db.Venta, "Id", "Id", detalle_Venta.Id_Venta);
            return View(detalle_Venta);
        }

        // POST: Detalle_Venta/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Venta,Id_Producto,Cantidad,Subtotal")] Detalle_Venta detalle_Venta)
        {

            Producto producto = db.Producto.Find(detalle_Venta.Id_Producto);
            Venta venta = db.Venta.Find(detalle_Venta.Id_Venta);


            if (producto != null && venta != null)
            {
                // Calcular el subtotal multiplicando el precio del producto por la cantidad
                detalle_Venta.Subtotal = (int)CalcularSubtotal(producto.Id, detalle_Venta.Cantidad);

                if (ModelState.IsValid)
                {
                    Detalle_Venta originalDetalleVenta = db.Detalle_Venta.Find(detalle_Venta.Id_Venta, detalle_Venta.Id_Producto);

                    if (originalDetalleVenta == null)
                    {
                        return HttpNotFound();
                    }

                    // Calcular la diferencia entre la cantidad original y la nueva cantidad
                    int diferenciaCantidad = detalle_Venta.Cantidad - originalDetalleVenta.Cantidad;

                    // Actualizar las propiedades de la entidad original con los valores de la entidad modificada
                    originalDetalleVenta.Cantidad = detalle_Venta.Cantidad;
                    originalDetalleVenta.Subtotal = detalle_Venta.Subtotal;

                    // Guardar los cambios en la base de datos
                    db.Entry(originalDetalleVenta).State = EntityState.Modified;
                    db.SaveChanges();

                    venta.Monto = (int)CalcularMontoTotal(venta.Id);

                    // Ajustar el stock del producto
                    producto.Stock -= diferenciaCantidad;
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();

                    if (ModelState.IsValid)
                    {
                        db.Entry(venta).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", new { id = detalle_Venta.Id_Venta });
                    }
                    return RedirectToAction("Index", new { id = detalle_Venta.Id_Venta });
                }
            }
                ViewBag.Id_Producto = new SelectList(db.Producto, "Id", "Nombre", detalle_Venta.Id_Producto);
                ViewBag.Id_Venta = new SelectList(db.Venta, "Id", "Id", detalle_Venta.Id_Venta);
                return View(detalle_Venta);
            
        }

        // GET: Detalle_Venta/Edit/5
        public ActionResult Edit2(int? iid, int? iid2)
        {
            if (iid == null || iid2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Detalle_Venta detalle_Venta = db.Detalle_Venta.FirstOrDefault(d => d.Id_Venta == iid && d.Id_Producto == iid2);


            if (detalle_Venta == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id_Producto = new SelectList(db.Producto, "Id", "Nombre", detalle_Venta.Id_Producto);
            ViewBag.Id_Venta = iid;//new SelectList(db.Venta, "Id", "Id", detalle_Venta.Id_Venta);
            return View(detalle_Venta);
        }

        // POST: Detalle_Venta/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "Id_Venta,Id_Producto,Cantidad,Subtotal")] Detalle_Venta detalle_Venta)
        {

            Producto producto = db.Producto.Find(detalle_Venta.Id_Producto);
            Venta venta = db.Venta.Find(detalle_Venta.Id_Venta);


            if (producto != null && venta != null)
            {
                // Calcular el subtotal multiplicando el precio del producto por la cantidad
                detalle_Venta.Subtotal = (int)CalcularSubtotal(producto.Id, detalle_Venta.Cantidad);

                if (ModelState.IsValid)
                {
                    Detalle_Venta originalDetalleVenta = db.Detalle_Venta.Find(detalle_Venta.Id_Venta, detalle_Venta.Id_Producto);

                    if (originalDetalleVenta == null)
                    {
                        return HttpNotFound();
                    }

                    // Calcular la diferencia entre la cantidad original y la nueva cantidad
                    int diferenciaCantidad = detalle_Venta.Cantidad - originalDetalleVenta.Cantidad;

                    // Actualizar las propiedades de la entidad original con los valores de la entidad modificada
                    originalDetalleVenta.Cantidad = detalle_Venta.Cantidad;
                    originalDetalleVenta.Subtotal = detalle_Venta.Subtotal;

                    // Guardar los cambios en la base de datos
                    db.Entry(originalDetalleVenta).State = EntityState.Modified;
                    db.SaveChanges();

                    venta.Monto = (int)CalcularMontoTotal(venta.Id);

                    // Ajustar el stock del producto
                    producto.Stock -= diferenciaCantidad;
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();

                    if (ModelState.IsValid)
                    {
                        db.Entry(venta).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index2", new { id = detalle_Venta.Id_Venta });
                    }
                    return RedirectToAction("Index2", new { id = detalle_Venta.Id_Venta });
                }
            }
            ViewBag.Id_Producto = new SelectList(db.Producto, "Id", "Nombre", detalle_Venta.Id_Producto);
            ViewBag.Id_Venta = new SelectList(db.Venta, "Id", "Id", detalle_Venta.Id_Venta);
            return View(detalle_Venta);

        }

        // GET: Detalle_Venta/Delete/5
        public ActionResult Delete(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle_Venta detalle_Venta = db.Detalle_Venta.FirstOrDefault(d => d.Id_Venta == id && d.Id_Producto == id2);
            ViewBag.Id_Venta = id;
            if (detalle_Venta == null)
            {
                return HttpNotFound();
            }
            return View(detalle_Venta);
        }

        // POST: Detalle_Venta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int id2)
        {
            Detalle_Venta detalle_Venta = db.Detalle_Venta.FirstOrDefault(d => d.Id_Venta == id && d.Id_Producto == id2);
            Producto producto = db.Producto.Find(detalle_Venta.Id_Producto);
            Venta venta = db.Venta.Find(detalle_Venta.Id_Venta);

            if (producto != null && venta != null)
            {
                if (ModelState.IsValid)
                {
                    db.Detalle_Venta.Remove(detalle_Venta);
                    db.SaveChanges();

                    producto.Stock += detalle_Venta.Cantidad;
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();

                    venta.Monto = (int)CalcularMontoTotal(venta.Id);
                    if (ModelState.IsValid)
                    {
                        db.Entry(venta).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", new { id = detalle_Venta.Id_Venta });
                    }
                    return RedirectToAction("Index", new { id = detalle_Venta.Id_Venta });
                }
            }
            
            return RedirectToAction("Index");
        }

        // GET: Detalle_Venta/Delete/5
        public ActionResult Delete2(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle_Venta detalle_Venta = db.Detalle_Venta.FirstOrDefault(d => d.Id_Venta == id && d.Id_Producto == id2);
            ViewBag.Id_Venta = id;
            if (detalle_Venta == null)
            {
                return HttpNotFound();
            }
            return View(detalle_Venta);
        }

        // POST: Detalle_Venta/Delete/5
        [HttpPost, ActionName("Delete2")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int id, int id2)
        {
            Detalle_Venta detalle_Venta = db.Detalle_Venta.FirstOrDefault(d => d.Id_Venta == id && d.Id_Producto == id2);
            Producto producto = db.Producto.Find(detalle_Venta.Id_Producto);
            Venta venta = db.Venta.Find(detalle_Venta.Id_Venta);

            if (producto != null && venta != null)
            {
                if (ModelState.IsValid)
                {
                    db.Detalle_Venta.Remove(detalle_Venta);
                    db.SaveChanges();

                    producto.Stock += detalle_Venta.Cantidad;
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();

                    venta.Monto = (int)CalcularMontoTotal(venta.Id);
                    if (ModelState.IsValid)
                    {
                        db.Entry(venta).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index2", new { id = detalle_Venta.Id_Venta });
                    }
                    return RedirectToAction("Index2", new { id = detalle_Venta.Id_Venta });
                }
            }

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

        private decimal CalcularSubtotal(int idProducto, int cantidad)
        {
            var producto = db.Producto.FirstOrDefault(p => p.Id == idProducto);
            if (producto != null)
            {
                return producto.Precio * cantidad;
            }
            return 0;
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

        private int CalcularStock (int idVenta)
        {
            int stock = 0;
            

            return stock;
        }
    }
}
