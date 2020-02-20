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
    public class PessoasDocumentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PessoasDocumentos
        public async Task<ActionResult> Index()
        {
            //var PessoasDocumentos = db.PessoasDocumentos.Include(p => p.Documento).Include(p => p.Pessoa);
            return View();//await PessoasDocumentos.ToListAsync());
        }

        // GET: PessoasDocumentos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //PessoaDocumento pessoaDocumento = await db.PessoasDocumentos.FindAsync(id);
            //if (pessoaDocumento == null)
            //{
            //    return HttpNotFound();
            //}
            return View();//pessoaDocumento);
        }

        // GET: PessoasDocumentos/Create
        public ActionResult Create()
        {
            //ViewBag.DocumentosId = new SelectList(db.Documentos, "Id", "Descricao");
            //ViewBag.PessoasId = new SelectList(db.Pessoas, "Id", "Nome");
            return View();
        }

        // POST: PessoasDocumentos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Numero,PessoasId,DocumentosId")] object pessoaDocumento)//PessoaDocumento pessoaDocumento)
        {
            if (ModelState.IsValid)
            {
                //db.PessoasDocumentos.Add(pessoaDocumento);
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.DocumentosId = new SelectList(db.Documentos, "Id", "Descricao", pessoaDocumento.DocumentosId);
            //ViewBag.PessoasId = new SelectList(db.Pessoas, "Id", "Nome", pessoaDocumento.PessoasId);
            //return View(pessoaDocumento);
            return View();
        }

        // GET: PessoasDocumentos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //PessoaDocumento pessoaDocumento = await db.PessoasDocumentos.FindAsync(id);
            //if (pessoaDocumento == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.DocumentosId = new SelectList(db.Documentos, "Id", "Descricao", pessoaDocumento.DocumentosId);
            //ViewBag.PessoasId = new SelectList(db.Pessoas, "Id", "Nome", pessoaDocumento.PessoasId);
            //return View(pessoaDocumento);
            return View();
        }

        // POST: PessoasDocumentos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Numero,PessoasId,DocumentosId")] object pessoaDocumento)//PessoaDocumento pessoaDocumento)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(pessoaDocumento).State = EntityState.Modified;
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.DocumentosId = new SelectList(db.Documentos, "Id", "Descricao", pessoaDocumento.DocumentosId);
            //ViewBag.PessoasId = new SelectList(db.Pessoas, "Id", "Nome", pessoaDocumento.PessoasId);
            //return View(pessoaDocumento);
            return View();
        }

        // GET: PessoasDocumentos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //PessoaDocumento pessoaDocumento = await db.PessoasDocumentos.FindAsync(id);
            //if (pessoaDocumento == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(pessoaDocumento);
            return View();
        }

        // POST: PessoasDocumentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //PessoaDocumento pessoaDocumento = await db.PessoasDocumentos.FindAsync(id);
            //db.PessoasDocumentos.Remove(pessoaDocumento);
            //await db.SaveChangesAsync();
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
