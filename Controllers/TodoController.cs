using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todoAuth.Models;
using todoAuth.Data;

namespace todoAuth.Controllers
{
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Todos.ToList()); 
        }
 
        [HttpPost]
        public IActionResult Index(string NewItem)
        {
            var currentToDo = new TodoModel{
                TaskName = NewItem
            }; 

            _context.Add(currentToDo);
            _context.SaveChanges();

            return View(_context.Todos.ToList());
        }

        [HttpPost]
        public IActionResult IsComplete(int Id)
        {
            var finished = _context.Todos.FirstOrDefault(m => m.Id == Id);
            finished.Complete();
            _context.SaveChanges();
            return RedirectToAction(nameof(Index), nameof(TodoController));
        } 

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
