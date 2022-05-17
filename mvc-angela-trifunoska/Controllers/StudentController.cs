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
    public class StudentController : Controller
    {
        private readonly MVCUniversityContext _context;

        public StudentController(MVCUniversityContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index(string firstName, string lastName, int index)
        {
            IQueryable<Student> students = _context.Student.AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
            {
                students = students.Where(s => s.FirstName.Contains(firstName) || !!(s.FirstName == firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                students = students.Where(s => s.LastName.Contains(lastName) || !!(s.LastName == lastName));
            }

            if (string.IsNullOrEmpty(index.ToString()))
            {
                students = students.Where(s => s.Index.ToString() == index.ToString());
            }

            var studentModelView = new StudentFilterViewModel { Students = await students.Include(s => s.Subjects)
                .ThenInclude(s => s.Subject).ToListAsync() };

            return View(studentModelView);

        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s=>s.Subjects)
                .ThenInclude(s => s.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,FullName,Index")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Subjects)
                .ThenInclude(s => s.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            
            var subjects = _context.Subject.AsEnumerable();
            subjects = subjects.OrderBy(s => s.SubjectName);

            var subjectStudentEditViewModel = new SubjectStudentEditViewModel
            {
                Id = student.Id,
                Student = student,
                SubjectList = new MultiSelectList(subjects, "Id", "SubjectName"),
                SelectedSubjects = student.Subjects.Select(sa => sa.SubjectId)
            };

            return View(subjectStudentEditViewModel);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubjectStudentEditViewModel viewModel)
        {
            if (id != viewModel.Student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Student);
                    await _context.SaveChangesAsync();

                    IEnumerable<int> listSubjects = viewModel.SelectedSubjects;
                    IQueryable<StudentSubject> toBeRemoved = _context.StudentSubject.Where(s => !listSubjects.Contains(s.SubjectId) && s.StudentId == id);

                    IEnumerable<int> existSubjects = _context.StudentSubject.Where(s => listSubjects.Contains(s.SubjectId) && s.StudentId == id).Select(s => s.SubjectId);
                    IEnumerable<int> newSubjects = listSubjects.Where(s => !existSubjects.Contains(s));

                    foreach (int subjectId in newSubjects)
                    {
                        _context.StudentSubject.Add(new StudentSubject { StudentId = id, SubjectId = subjectId });
                    }

                    await _context.SaveChangesAsync();
                }   
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(viewModel.Student.Id))
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

            return View(viewModel);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
