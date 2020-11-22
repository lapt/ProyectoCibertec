using ProyectoCibertec.Areas.Order.ViewModels;
using ProyectoCibertec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoCibertec.Areas.Order.Controllers
{
    public class OrdersController : Controller
    {
       
        private readonly cibertec_dbEntities contexto;
        // GET: Orden/Orders
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IngresarOrden(RegistroOrderViewModel vm)
        {

            var orden = new Orden()
            {
                strNroDoc = vm.StrNroDoc,
                dtFecha = vm.DtFecha
            };

            var producto = contexto.Producto.Find(vm.IdProducto);

            foreach (var orderDetalle in vm.LstOrder)
            {
                orderDetalle.Producto = producto;
                orden.OrdenDetalle.Add(orderDetalle);
            }
            contexto.Orden.Add(orden);
            contexto.SaveChanges();
            return View();
        }
    }
}