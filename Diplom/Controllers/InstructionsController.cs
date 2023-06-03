﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Data;
using Diplom.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Diplom.Controllers
{
    public class InstructionsController : Controller
    {
        private readonly DiplomContext _context;

        public InstructionsController(DiplomContext context)
        {
            _context = context;
        }

        // GET: Instructions
        [Authorize(Roles = "Employee, Administrator")]
        public async Task<IActionResult> Index()
        {
              return _context.Instruction != null ? 
                          View(await _context.Instruction.ToListAsync()) :
                          Problem("Entity set 'DiplomContext.Instruction'  is null.");
        }

        // GET: Instructions/Details/5
        [Authorize(Roles = "Employee, Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Instruction == null)
            {
                return NotFound();
            }

            var instruction = await _context.Instruction
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instruction == null)
            {
                return NotFound();
            }

            return View(instruction);
        }

        // GET: Instructions/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Content")] Instruction instruction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instruction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instruction);
        }

        // GET: Instructions/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Instruction == null)
            {
                return NotFound();
            }

            var instruction = await _context.Instruction.FindAsync(id);
            if (instruction == null)
            {
                return NotFound();
            }
            return View(instruction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Content")] Instruction instruction)
        {
            if (id != instruction.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instruction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructionExists(instruction.ID))
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
            return View(instruction);
        }

        // GET: Instructions/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Instruction == null)
            {
                return NotFound();
            }

            var instruction = await _context.Instruction
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instruction == null)
            {
                return NotFound();
            }

            return View(instruction);
        }

        // POST: Instructions/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Instruction == null)
            {
                return Problem("Entity set 'DiplomContext.Instruction'  is null.");
            }
            var instruction = await _context.Instruction.FindAsync(id);
            if (instruction != null)
            {
                _context.Instruction.Remove(instruction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructionExists(int id)
        {
          return (_context.Instruction?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
