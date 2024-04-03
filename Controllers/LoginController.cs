using Proyecto_PetSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_PetSupply.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private SupplyPetEntities1 dbA = new SupplyPetEntities1();
        private SupplyPetEntities2 dbE = new SupplyPetEntities2();

        // GET: Login
        public ActionResult Index(string Usuario, string Contraseña)
        {
            // Verificar si los datos de inicio de sesión existen en la tabla de Empleados
            var Empleado = dbE.Empleado.FirstOrDefault(e => e.Usuario == Usuario && e.Contraseña == Contraseña);

            if (Empleado != null)
            {
                // Usuario válido, asignar el valor de la cédula a ViewBag y redireccionar al layout de usuario
                return RedirectToAction("Index2", "Home"); ;//, new { area = "", layout = "~/Views/Shared/_LayoutUsuario.cshtml" });
            }

            // Verificar si los datos de inicio de sesión existen en la tabla de administradores
            var admin = dbA.Administrador.FirstOrDefault(a => a.Usuario == Usuario && a.Contraseña == Contraseña);

            if (admin != null)
            {
                // Administrador válido, asignar el valor de la cédula a ViewBag y redireccionar al layout de administrador
                return RedirectToAction("Index", "Home");//, new { area = "", layout = "~/Views/Shared/_LayoutAdmin.cshtml" });
            }

            // Datos de inicio de sesión inválidos o cédula no proporcionada, mostrar mensaje de error
            ModelState.AddModelError("", "Usuario o contraseña incorrecta.");


            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbE.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}