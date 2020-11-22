using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoCibertec.Models;
using ProyectoCibertec.Models.Dto;

namespace ProyectoCibertec.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdenesController : Controller
    {
        private cibertec_dbEntities db = new cibertec_dbEntities();

        // GET: Ordenes
        public async Task<ActionResult> Index()
        {
            return View(await db.Orden.ToListAsync());
        }

        // GET: Ordenes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = await db.Orden.FindAsync(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            return View(orden);
        }

        // GET: Ordenes/Create
        public ActionResult Create()
        {
            ViewBag.Producto_idProducto = new SelectList(db.Producto, "idProducto", "strDescripcion");
            ViewData["fechaActual"] = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        // POST: Ordenes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idOrden,strNroDoc,dtFecha")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                db.Orden.Add(orden);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(orden);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult IngresarOrden(Orden vm)
        {

            try
            {
                // Verification  
                if (ModelState.IsValid)
                {
                    // Info.  
                    var orden = new Orden()
                    {
                        strNroDoc = vm.strNroDoc,
                        dtFecha = vm.dtFecha
                    };

                    var producto = db.Producto.Find(vm.OrdenDetalle.First().Producto_idProducto);

                    foreach (var orderDetalle in vm.OrdenDetalle)
                    {
                        orderDetalle.Producto = producto;
                        orden.OrdenDetalle.Add(orderDetalle);
                    }
                    db.Orden.Add(orden);
                    db.SaveChanges();

                    return this.Json(new
                    {
                        EnableSuccess = true,
                        SuccessTitle = "Success",
                        SuccessMsg = "Exitos MSJ"
                    });
                }
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }
            // Info  
            return this.Json(new
            {
                EnableError = true,
                ErrorTitle = "Error",
                ErrorMsg = "Something goes wrong, please try again later"
            });
          
        }
        // GET: Ordenes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = await db.Orden.FindAsync(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            return View(orden);
        }

        // POST: Ordenes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idOrden,strNroDoc,dtFecha")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orden).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(orden);
        }

        // GET: Ordenes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = await db.Orden.FindAsync(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            return View(orden);
        }

        // POST: Ordenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Orden orden = await db.Orden.FindAsync(id);
            db.Orden.Remove(orden);
            await db.SaveChangesAsync();
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
