using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Data;
using SmartUni.Models;
using SmartUni.Services;

namespace SmartUni.Controllers
{
    [Authorize]
    public class TokensController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRandomStringGenerator _randomString;

        public TokensController(ApplicationDbContext context, IRandomStringGenerator randomString)
        {
            _context = context;
            _randomString = randomString;
        }

        // GET: Tokens
        public async Task<IActionResult> Index()
        {
              return _context.Tokens != null ? 
                          View(await _context.Tokens.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tokens'  is null.");
        }

        // GET: Tokens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tokens == null)
            {
                return NotFound();
            }

            var token = await _context.Tokens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (token == null)
            {
                return NotFound();
            }

            return View(token);
        }

        // GET: Tokens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tokens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Token token)
        {
            if (ModelState.IsValid)
            {
                string value = _randomString.GenerateRandomString(4);
                var token_check = await _context.Tokens.Where(x => x.Value.Contains(value)).FirstOrDefaultAsync();
                var referenceno_check = await _context.Tokens.Where(x => x.ReferenceNo.Contains(token.ReferenceNo)).FirstOrDefaultAsync();
                if (token_check == null && referenceno_check == null)
                {
                    token.Value = $"SUC-{value}-{DateTime.Now.Year}";
                    token.HasEntered = false;
					_context.Add(token);
					await _context.SaveChangesAsync();
					TempData["Message"] = "Token Created Successfully";
					TempData["Type"] = "success";
					return RedirectToAction(nameof(Index));
				}
                TempData["Message"] = "Token Exist or Receipt has already been inserted";
                TempData["Type"] = "warning";
                return View(token);
            }

			TempData["Message"] = "Could Create Token";
			TempData["Type"] = "danger";
			return View(token);
        }

        // GET: Tokens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tokens == null)
            {
                return NotFound();
            }

            var token = await _context.Tokens.FindAsync(id);
            if (token == null)
            {
                return NotFound();
            }
            return View(token);
        }

        // POST: Tokens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReferenceNo,Value,HasEntered,DateEntered,ExpirationDate,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Token token)
        {
            if (id != token.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(token);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TokenExists(token.Id))
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
            return View(token);
        }

        // GET: Tokens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tokens == null)
            {
                return NotFound();
            }

            var token = await _context.Tokens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (token == null)
            {
                return NotFound();
            }

            return View(token);
        }

        // POST: Tokens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tokens == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tokens'  is null.");
            }
            var token = await _context.Tokens.FindAsync(id);
            if (token != null)
            {
                _context.Tokens.Remove(token);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TokenExists(int id)
        {
          return (_context.Tokens?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
