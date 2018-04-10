using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BO;

namespace SolutionPersonnelleTemplate.Controllers
{
    public class totoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public totoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: toto
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Messages.Include(m => m.Histoire);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: toto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Histoire)
                .SingleOrDefaultAsync(m => m.MessageID == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: toto/Create
        public IActionResult Create()
        {
            // ViewData["HistoireID"] = new SelectList(_context.Histoires, "HistoireID", "Titre");
            ViewBag.JobTitle = new SelectList(_context.Histoires, "HistoireID", "Titre");

            return View();
        }

        // POST: toto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HistoireID"] = new SelectList(_context.Histoires, "HistoireID", "Synopsis", message.HistoireID);
            return View(message);
        }

        // GET: toto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.SingleOrDefaultAsync(m => m.MessageID == id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["HistoireID"] = new SelectList(_context.Histoires, "HistoireID", "Synopsis", message.HistoireID);
            return View(message);
        }

        // POST: toto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageID,Titre,Contenu,UrlMedia,MessageEnfant1,NumeroMessageEnfant1,MessageEnfant2,NumeroMessageEnfant2,MessageEnfant3,NumeroMessageEnfant3,HistoireID")] Message message)
        {
            if (id != message.MessageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MessageID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HistoireID"] = new SelectList(_context.Histoires, "HistoireID", "Synopsis", message.HistoireID);
            return View(message);
        }

        // GET: toto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Histoire)
                .SingleOrDefaultAsync(m => m.MessageID == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: toto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Messages.SingleOrDefaultAsync(m => m.MessageID == id);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageID == id);
        }
    }
}
