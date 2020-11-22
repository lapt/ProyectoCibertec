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

namespace ProyectoCibertec.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MovimientosController : Controller
    {
        private cibertec_dbEntities db = new cibertec_dbEntities();

        // GET: Movimientos
        public async Task<ActionResult> Index()
        {
            var movimiento = db.Movimiento.Include(m => m.Producto);
            return View(await movimiento.ToListAsync());
        }

        // GET: Movimientos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimiento movimiento = await db.Movimiento.FindAsync(id);
            if (movimiento == null)
            {
                return HttpNotFound();
            }
            return View(movimiento);
        }

        // GET: Movimientos/Create
        public ActionResult Create()
        {
            ViewBag.Producto_idProducto = new SelectList(db.Producto, "idProducto", "strDescripcion");
            ViewData["fechaActual"] = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        // POST: Movimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idMovimiento,Producto_idProducto,dblCantidad,dtFecha")] Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                db.Movimiento.Add(movimiento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Producto_idProducto = new SelectList(db.Producto, "idProducto", "strDescripcion", movimiento.Producto_idProducto);
            return View(movimiento);
        }

        // GET: Movimientos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimiento movimiento = await db.Movimiento.FindAsync(id);
            if (movimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.Producto_idProducto = new SelectList(db.Producto, "idProducto", "strDescripcion", movimiento.Producto_idProducto);
            return View(movimiento);
        }

        // POST: Movimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idMovimiento,Producto_idProducto,dblCantidad,dtFecha")] Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimiento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Producto_idProducto = new SelectList(db.Producto, "idProducto", "strDescripcion", movimiento.Producto_idProducto);
            return View(movimiento);
        }

        // GET: Movimientos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimiento movimiento = await db.Movimiento.FindAsync(id);
            if (movimiento == null)
            {
                return HttpNotFound();
            }
            return View(movimiento);
        }

        // POST: Movimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Movimiento movimiento = await db.Movimiento.FindAsync(id);
            db.Movimiento.Remove(movimiento);
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
