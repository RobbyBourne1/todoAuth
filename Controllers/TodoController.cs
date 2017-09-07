using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todoAuth.Models;
using todoAuth.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace todoAuth.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ApplicationDbContext context, UserManager<ApplicationUser> um)
        {
            _context = context;
            _userManager = um;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Todos.Include(p => p.UserId);
            return View(await applicationDbContext.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string newToDoName)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var currentToDo = new TodoModel
            {
                TaskName = newToDoName
            };

            currentToDo.UserId = user;

            _context.Todos.Add(currentToDo);
            _context.SaveChanges();

            return View(_context.Todos.Where(w => w.UserId == user.Id).ToList());
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
