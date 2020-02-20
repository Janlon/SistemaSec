using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaSec.Models;
using SistemaSec;

namespace SistemaSec.Controllers
{
    public class CargosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cargos
        public async Task<ActionResult> Index()
        {
            return View();//await db.Cargos.ToListAsync());
        }

        // GET: Cargos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Cargo cargo = await db.Cargos.FindAsync(id);
            //if (cargo == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cargo);
            return View();
        }

        // GET: Cargos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cargos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Descricao,Sigla")]object cargo)// Cargo cargo)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Cargos.Add(cargo);
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}
            //return View(cargo);
            return View();
        }

        // GET: Cargos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Cargo cargo = await db.Cargos.FindAsync(id);
            //if (cargo == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cargo);
            return View();
        }

        // POST: Cargos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Descricao,Sigla")]object cargo)// Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cargo);
        }

        // GET: Cargos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Cargo cargo = await db.Cargos.FindAsync(id);
            //if (cargo == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cargo);
            return View();
        }

        // POST: Cargos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //Cargo cargo = await db.Cargos.FindAsync(id);
            //db.Cargos.Remove(cargo);
            //await db.SaveChangesAsync();
            //return RedirectToAction("Index");
            return View();
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
