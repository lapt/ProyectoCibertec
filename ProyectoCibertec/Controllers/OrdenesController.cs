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
            ViewBag.Producto_idProducto = db.Producto;
            ViewData["fechaActual"] = DateTime.Now.ToString("yyyy-MM-dd");
            RegistroOrderDto registroOrderDto = new RegistroOrderDto();
            TempData.Clear();
            ViewBag.Show = true;
            return View(registroOrderDto);
        }

    
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(RegistroOrderDto vm)
        {
           
            // Inicializamos contador y lista
            List<Detalle> lstMatricula1;
            if (!TempData.ContainsKey("count"))
            {
                
                TempData["count"] = 0;
                TempData["listDetalle"] = new List<Detalle>();
                lstMatricula1 = new List<Detalle>();
            }
            else
            {
               
                TempData["count"] = Convert.ToInt32(TempData["count"] as string) + 1;
                TempData.Keep("count");
                lstMatricula1 = TempData["listDetalle"] as List<Detalle>;
                TempData.Keep("listDetalle");
            }
            //FIN
            // Identificar que submit se activo
            if (Request.Form["btnAdd"] != null)
            {
                ViewBag.Show = false;
                ViewBag.Producto_idProducto = db.Producto;
                ViewData["fechaActual"] = vm.DtFecha.ToString("yyyy-MM-dd");
                ViewBag.listDetalle = new List<Detalle>();
                try
                {
                    vm.LstOrder.Add(new Detalle()
                    {
                        dblCantidad = vm.DblCantidad,
                        DtFecha = vm.DtFecha,
                        Producto_idProducto = vm.IdProducto,
                        StrNroDoc = vm.StrNroDoc
                    });

                    lstMatricula1.Add(vm.LstOrder[0]);
                    vm.LstOrder = lstMatricula1;
                    TempData["listDetalle"] = lstMatricula1;

                    return View(vm);
                }
                catch (Exception ex)
                {
                    // Info  
                    Console.Write(ex);
                }
                // Info  
                return View(vm);
            }
            else if (Request.Form["btnSet"] != null)
            {
                ViewBag.Show = true;
                var orden = new Orden()
                {
                    strNroDoc = vm.StrNroDoc,
                    dtFecha = vm.DtFecha
                };

                //var producto = db.Producto.Find(vm.IdProducto);           


                foreach (var orderDetalle in lstMatricula1)
                {

                    orden.OrdenDetalle.Add(new OrdenDetalle() { 
                    dblCantidad= orderDetalle.dblCantidad,                    
                    Producto_idProducto=vm.IdProducto                    
                    });
                }
                vm.StrNroDoc = "";
                vm.DblCantidad = 0;
                db.Orden.Add(orden);
                db.SaveChanges();
                ViewBag.Producto_idProducto = db.Producto;
                ViewData["fechaActual"] = DateTime.Now.ToString("yyyy-MM-dd");

            }
            ModelState.Clear();
            TempData.Clear();
            return View(vm);
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
