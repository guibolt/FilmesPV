using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FilmesPV.Data;
using FilmesPV.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FilmesPV.Controllers
{
    public class FilmesController : Controller
    {
        private readonly FilmesPVContext _context;

        public FilmesController(FilmesPVContext context)
        {
            _context = context;
        }

        // GET: Filmes
        public async Task<IActionResult> Index()
        {
            var filmesPVContext = _context.Filme.Include(f => f.Categoria);
            return View(await filmesPVContext.ToListAsync());
        }

        // GET: Filmes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .Include(f => f.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // GET: Filmes/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "Id", "Nome");
            return View();
        }

        // POST: Filmes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Filme filme)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "Id", "Id", filme.CategoriaId);
                return View(filme);
            }

            var imgPreFixo = $"{Guid.NewGuid()}_";

            if (!await UploadArquivo(filme.ImagemUpload, imgPreFixo))
            {
                ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "Id", "Id", filme.CategoriaId);
                return View(filme);
            }

            filme.Imagem = $"{imgPreFixo}{filme.ImagemUpload.FileName}";

            _context.Add(filme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Filmes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();


            var filme = await _context.Filme.FindAsync(id);

            if (filme == null)
                return NotFound();

            ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "Id", "Id", filme.CategoriaId);
            return View(filme);
        }

        // POST: Filmes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Filme filme)
        {
            if (id != filme.Id)
                return NotFound();
            

            if (!ModelState.IsValid)
            {
                ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "Id", "Id", filme.CategoriaId);
                return View(filme);
            }

            try
            {
                _context.Update(filme);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmeExists(filme.Id))
                    return NotFound();
                
                else
                    throw;
                
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Filmes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .Include(f => f.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // POST: Filmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filme = await _context.Filme.FindAsync(id);
            _context.Filme.Remove(filme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmeExists(int id)
        {
            return _context.Filme.Any(e => e.Id == id);
        }
        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}
