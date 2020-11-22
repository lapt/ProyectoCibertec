using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ProyectoCibertec.Models;

[assembly: OwinStartupAttribute(typeof(ProyectoCibertec.Startup))]
namespace ProyectoCibertec
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CrearAdminUser();
        }
        public void CrearAdminUser()
        {
            ApplicationDbContext contexto = new ApplicationDbContext();

            var AdministradorDeRoles = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(contexto));
            var AdministradorDeUsuarios = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(contexto));

            if (!AdministradorDeRoles.RoleExists("Admin"))
            {
                //creamos rol
                var rol = new IdentityRole("Admin");
                AdministradorDeRoles.Create(rol);
                //es lo mismo que crear rol desde mi modelo custom
                //Rol rol = new Rol();
                //contexto.Rol.add(rol);
                var usuario = new ApplicationUser();
                usuario.UserName = "admin@cibertec.edu.pe";
                usuario.Email = "admin@cibertec.edu.pe";
                string password = "Cibertec2020!";
                var exito = AdministradorDeUsuarios.Create(usuario, password);
                if (exito.Succeeded)
                {
                    AdministradorDeUsuarios.AddToRole(usuario.Id, rol.Name);
                }

            }
        }
    }
}
