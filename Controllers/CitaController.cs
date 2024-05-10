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
    public class CitaController : Controller
    {
        private SupplyPetEntities4 db = new SupplyPetEntities4();

        // GET: Cita
        public ActionResult Index()
        {
            var cita = db.Cita.Include(c => c.Horario).Include(c => c.Mascota);
            return View(cita.ToList());
        }

        // GET: Cita/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            var horario = $"{cita.Horario.dia.ToShortDateString()} {cita.Horario.hora.ToString(@"hh\:mm")}";

            ViewBag.Horario = horario;
            return View(cita);
        }

        // GET: Cita/Create
        public ActionResult Create()
        {
            var horarios = db.Horario.ToList().Select(h => new { Id = h.Id, DiaHora = $"{h.dia.ToShortDateString()} {h.hora.ToString(@"hh\:mm")}" }).ToList();
            ViewBag.Id_Horario = new SelectList(horarios, "Id", "DiaHora");
            ViewBag.Id_Mascota = new SelectList(db.Mascota, "Id", "Nombre");
            return View();
        }

        // POST: Cita/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Mascota,Descripcion_Servicio,Precio,Id_Horario")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Cita.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Horario = new SelectList(db.Horario, "Id", "Id", cita.Id_Horario);
            ViewBag.Id_Mascota = new SelectList(db.Mascota, "Id", "Nombre", cita.Id_Mascota);
            return View(cita);
        }

        // GET: Cita/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Horario = new SelectList(db.Horario, "Id", "Id", cita.Id_Horario);
            ViewBag.Id_Mascota = new SelectList(db.Mascota, "Id", "Nombre", cita.Id_Mascota);
            return View(cita);
        }

        // POST: Cita/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Mascota,Descripcion_Servicio,Precio,Id_Horario")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Horario = new SelectList(db.Horario, "Id", "Id", cita.Id_Horario);
            ViewBag.Id_Mascota = new SelectList(db.Mascota, "Id", "Nombre", cita.Id_Mascota);
            return View(cita);
        }

        // GET: Cita/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            var horario = $"{cita.Horario.dia.ToShortDateString()} {cita.Horario.hora.ToString(@"hh\:mm")}";

            ViewBag.Horario = horario;
            return View(cita);
        }

        // POST: Cita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cita cita = db.Cita.Find(id);
            db.Cita.Remove(cita);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Index2()
        {
            var cita = db.Cita.Include(c => c.Horario).Include(c => c.Mascota);
            return View(cita.ToList());
        }

        // GET: Cita/Details/5
        public ActionResult Details2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            var horario = $"{cita.Horario.dia.ToShortDateString()} {cita.Horario.hora.ToString(@"hh\:mm")}";

            ViewBag.Horario = horario;
            return View(cita);
        }

        // GET: Cita/Create
        public ActionResult Create2()
        {
            var horarios = db.Horario.ToList().Select(h => new { Id = h.Id, DiaHora = $"{h.dia.ToShortDateString()} {h.hora.ToString(@"hh\:mm")}" }).ToList();
            ViewBag.Id_Horario = new SelectList(horarios, "Id", "DiaHora");
            ViewBag.Id_Mascota = new SelectList(db.Mascota, "Id", "Nombre");
            return View();
        }

        // POST: Cita/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2([Bind(Include = "Id,Id_Mascota,Descripcion_Servicio,Precio,Id_Horario")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Cita.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index2");
            }

            ViewBag.Id_Horario = new SelectList(db.Horario, "Id", "Id", cita.Id_Horario);
            ViewBag.Id_Mascota = new SelectList(db.Mascota, "Id", "Nombre", cita.Id_Mascota);
            return View(cita);
        }

        // GET: Cita/Edit/5
        public ActionResult Edit2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Horario = new SelectList(db.Horario, "Id", "Id", cita.Id_Horario);
            ViewBag.Id_Mascota = new SelectList(db.Mascota, "Id", "Nombre", cita.Id_Mascota);
            return View(cita);
        }

        // POST: Cita/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "Id,Id_Mascota,Descripcion_Servicio,Precio,Id_Horario")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index2");
            }
            ViewBag.Id_Horario = new SelectList(db.Horario, "Id", "Id", cita.Id_Horario);
            ViewBag.Id_Mascota = new SelectList(db.Mascota, "Id", "Nombre", cita.Id_Mascota);
            return View(cita);
        }

        // GET: Cita/Delete/5
        public ActionResult Delete2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            var horario = $"{cita.Horario.dia.ToShortDateString()} {cita.Horario.hora.ToString(@"hh\:mm")}";

            ViewBag.Horario = horario;
            return View(cita);
        }

        // POST: Cita/Delete/5
        [HttpPost, ActionName("Delete2")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int id)
        {
            Cita cita = db.Cita.Find(id);
            db.Cita.Remove(cita);
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
    }
}
