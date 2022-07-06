using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;
using AutoMapper;

namespace ExpenseTracker.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpenseTrackerContextDB _context;
        private readonly IMapper _mapper;

        public ExpensesController(ExpenseTrackerContextDB context,
                                                IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Expenses
        public IActionResult Index()
        {
            var expenses = _context.Expenses
                            .Include(e => e.UserCategoryMap)
                            .Include(e => e.UserCategoryMap.Category)
                            .Include(e => e.UserCategoryMap.User)
                            .OrderByDescending(e => e.ExpenseDate)
                            .ToList();

            var result = _mapper.Map<List<ExpensesModel>>(expenses);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string startdate, string enddate)
        {
            

            var expenses = _context.Expenses
                            .Include(e => e.UserCategoryMap)
                            .Include(e => e.UserCategoryMap.Category)
                            .Include(e => e.UserCategoryMap.User)
                            .ToList();

            //var filter = expenses.Where(e => (e.ExpenseDate >= Convert.ToDateTime(startdate) && e.ExpenseDate <= Convert.ToDateTime(enddate)) || (e.ExpenseDate >= Convert.ToDateTime(startdate)) || e.ExpenseDate <= Convert.ToDateTime(enddate)).ToList();

            var filter = expenses.Where(e => e.ExpenseDate >= Convert.ToDateTime(startdate) && e.ExpenseDate <= Convert.ToDateTime(enddate)).ToList();

            if (filter.Count == 0)
            {
                filter = expenses;
            }

            var result = _mapper.Map<List<ExpensesModel>>(filter);

            return View(result);
        }

        // GET: Expenses/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expensesModel = _context.Expenses
                .Include(e => e.UserCategoryMap)
                .Include(e => e.UserCategoryMap.User)
                .Include(e => e.UserCategoryMap.Category)
                .FirstOrDefault(m => m.ExpenseId == id);

            if (expensesModel == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<ExpensesModel>(expensesModel);

            return View(result);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            ViewData["UserCategoryId"] = new SelectList(_context.UserCategoryMaps.Include(e => e.Category), "UserCategoryId", "Category.CategoryName","- Select -");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ExpenseId,UserCategoryId,Amount,ExpenseDate")] ExpensesModel expensesModel)
        {
            if (ModelState.IsValid)
            {

                var result = _mapper.Map<Expenses>(expensesModel);

                _context.Add(result);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserCategoryId"] = new SelectList(_context.UserCategoryMaps.Include(e => e.Category), "UserCategoryId", "Category.CategoryName", expensesModel.UserCategoryId);
            return View(expensesModel);
        }

        // GET: Expenses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expensesModel = _context.Expenses.Find(id);
            if (expensesModel == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<ExpensesModel>(expensesModel);

            ViewData["UserCategoryId"] = new SelectList(_context.UserCategoryMaps.Include(e => e.Category), "UserCategoryId", "Category.CategoryName", expensesModel.UserCategoryId);
            return View(result);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ExpenseId,UserCategoryId,Amount,ExpenseDate")] ExpensesModel expensesModel)
        {
            if (id != expensesModel.ExpenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var result = _mapper.Map<Expenses>(expensesModel);

                try
                {
                    _context.Update(result);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpensesModelExists(expensesModel.ExpenseId))
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
            ViewData["UserCategoryId"] = new SelectList(_context.UserCategoryMaps.Include(e => e.Category), "UserCategoryId", "Category.CategoryName", expensesModel.UserCategoryId);
            return View(expensesModel);
        }

        // GET: Expenses/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expensesModel = _context.Expenses
                .Include(e => e.UserCategoryMap)
                .FirstOrDefault(m => m.ExpenseId == id);
            if (expensesModel == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<ExpensesModel>(expensesModel);

            return View(result);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var expensesModel = _context.Expenses.Find(id);
            _context.Expenses.Remove(expensesModel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpensesModelExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpenseId == id);
        }
    }
}
