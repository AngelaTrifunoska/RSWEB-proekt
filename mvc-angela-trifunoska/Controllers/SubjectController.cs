using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_angela_trifunoska.Data;
using mvc_angela_trifunoska.Models;
using mvc_angela_trifunoska.ViewModel;

namespace mvc_angela_trifunoska.Controllers
{
    public class SubjectController : Controller
    {
        private readonly MVCUniversityContext _context;

        public SubjectController(MVCUniversityContext context)
        {
            _context = context;
        }

        // GET: Subject
        public async Task<IActionResult> Index(string subjectName, string program, string semester)
        {
            var mVCUniversityContext = _context.Subject.Include(s => s.FirstProfessor).Include(s => s.SecondProfessor);

            IQueryable<Subject> subjects = _context.Subject.AsQueryable();

            if (!string.IsNullOrEmpty(subjectName))
            {
                subjects = subjects.Where(s => s.SubjectName.Contains(subjectName) || !!(s.SubjectName == subjectName));
            }

            if (!string.IsNullOrEmpty(program))
            {
                subjects = subjects.Where(s => s.Program.Contains(program) || !!(s.Program == program));
            }

            if (!string.IsNullOrEmpty(semester))
            {
                subjects = subjects.Where(s => s.Semester == semester);
            }

            subjects = subjects.Include(m => m.FirstProfessor).Include(m => m.SecondProfessor);

            var subjectModelView = new SubjectFilterViewModel { Subjects = await subjects.ToListAsync() };

            return View(subjectModelView);

        }

        // GET: Subject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                .Include(s => s.FirstProfessor)
                .Include(s => s.SecondProfessor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subject/Create
        public IActionResult Create()
        {
            ViewData["FirstProfessorId"] = new SelectList(_context.Professor, "Id", "FirstName");
            ViewData["SecondProfessorId"] = new SelectList(_context.Professor, "Id", "FirstName");
            return View();
        }

        // POST: Subject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubjectName,Program,Semester,FirstProfessorId,SecondProfessorId")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstProfessorId"] = new SelectList(_context.Professor, "Id", "FirstName", subject.FirstProfessorId);
            ViewData["SecondProfessorId"] = new SelectList(_context.Professor, "Id", "FirstName", subject.SecondProfessorId);
            return View(subject);
        }

        // GET: Subject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }
            ViewData["FirstProfessorId"] = new SelectList(_context.Professor, "Id", "FirstName", subject.FirstProfessorId);
            ViewData["SecondProfessorId"] = new SelectList(_context.Professor, "Id", "FirstName", subject.SecondProfessorId);
            return View(subject);
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectName,Program,Semester,FirstProfessorId,SecondProfessorId")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.Id))
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
            ViewData["FirstProfessorId"] = new SelectList(_context.Professor, "Id", "FirstName", subject.FirstProfessorId);
            ViewData["SecondProfessorId"] = new SelectList(_context.Professor, "Id", "FirstName", subject.SecondProfessorId);
            return View(subject);
        }

        // GET: Subject/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                .Include(s => s.FirstProfessor)
                .Include(s => s.SecondProfessor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subject.FindAsync(id);
            _context.Subject.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
            return _context.Subject.Any(e => e.Id == id);
        }
    }
}
