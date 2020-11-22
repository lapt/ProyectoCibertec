using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProyectoCibertec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoCibertec.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext contexto = new ApplicationDbContext();
        
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CrearUsuario()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CrearUsuario(FormCollection elementosForm)
        {
            var AdministradorDeUsuarios = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(contexto));
            
            var usuario = new ApplicationUser();
            usuario.Email = elementosForm["txtEmail"];
            usuario.UserName = usuario.Email;
            string password = elementosForm["txtPassword"];
            var exito = AdministradorDeUsuarios.Create(usuario, password);
            if (exito.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.errorEnCreacion = "El usuario o contraseña no tienen el formato adecuado";
            }
            return View();
        }

        public ActionResult CrearRol()
        {

            return View();
        }

        [HttpPost]
        public ActionResult NuevoRol(FormCollection elementosForm)
        {
            var AdministradorDeRoles = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(contexto));
            string NombreRol = elementosForm["txtRol"];
            if (!AdministradorDeRoles.RoleExists(NombreRol))
            {
                //creamos rol
                var rol = new IdentityRole(NombreRol);
                AdministradorDeRoles.Create(rol);
            }
            return RedirectToAction("Index");
        }
        public ActionResult AsignarRol()
        {
            ViewBag.roles = contexto.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AsignarRol(FormCollection elementosForm)
        {
            string rolNombre = elementosForm["RolNombre"];
            string email = elementosForm["txtEmail"];
            var usuario = contexto.Users.Where(x => x.Email == email).FirstOrDefault();
            var AdministradorDeUsuarios = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(contexto));
            AdministradorDeUsuarios.AddToRole(usuario.Id, rolNombre);
            return RedirectToAction("Index");
        }
    }
}