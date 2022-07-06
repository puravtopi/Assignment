using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;
using AutoMapper;

namespace ExpenseTracker.Controllers
{
    public class UserCategoryMapController : Controller
    {
        private readonly ExpenseTrackerContextDB _context;
        private readonly IMapper _mapper;

        public UserCategoryMapController(ExpenseTrackerContextDB context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: UserCategoryMap
        public IActionResult Index()
        {
            var userCategoryMap = _context.UserCategoryMaps
                                        .Include(u => u.Category)
                                        .Include(u => u.User)
                                        .ToList();

            var result = _mapper.Map<List<UserCategoryMapModel>>(userCategoryMap);

            return View(result);
        }

        // GET: UserCategoryMap/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCategoryMapModel = _context.UserCategoryMaps
                .Include(u => u.Category)
                .Include(u => u.User)
                .FirstOrDefault(m => m.UserCategoryId == id);
            if (userCategoryMapModel == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<UserCategoryMapModel>(userCategoryMapModel);

            return View(userCategoryMapModel);
        }

        // GET: UserCategoryMap/Create
        public IActionResult Create()
        {
            var User = _context.Users.FirstOrDefault();
            var UserCategoryMap = _context.UserCategoryMaps.Where(e => e.UserId == User.UserId).ToList();

            if(UserCategoryMap.Count == 0)
            {
                ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            }
            else
            {
                var category = _context.Category.ToList();

                var categoryRemain = category.Where(t => UserCategoryMap.All(p => p.CategoryId != t.CategoryId));
                ViewData["CategoryId"] = new SelectList(categoryRemain, "CategoryId", "CategoryName");
            }

            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName");
            return View();
        }

        // POST: UserCategoryMap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserCategoryId,UserId,CategoryId")] UserCategoryMapModel userCategoryMapModel)
        {
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<UserCategoryMap>(userCategoryMapModel);
                _context.Add(result);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", userCategoryMapModel.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName", userCategoryMapModel.UserId);
            return View(userCategoryMapModel);
        }

        // GET: UserCategoryMap/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCategoryMapModel = _context.UserCategoryMaps.Find(id);
            if (userCategoryMapModel == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<UserCategoryMapModel>(userCategoryMapModel);

            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", userCategoryMapModel.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName", userCategoryMapModel.UserId);
            return View(result);
        }

        // POST: UserCategoryMap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("UserCategoryId,UserId,CategoryId")] UserCategoryMapModel userCategoryMapModel)
        {
            if (id != userCategoryMapModel.UserCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<UserCategoryMap>(userCategoryMapModel);

                try
                {
                    _context.Update(result);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCategoryMapModelExists(userCategoryMapModel.UserCategoryId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", userCategoryMapModel.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName", userCategoryMapModel.UserId);
            return View(userCategoryMapModel);
        }

        // GET: UserCategoryMap/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCategoryMapModel = _context.UserCategoryMaps
                .Include(u => u.Category)
                .Include(u => u.User)
                .FirstOrDefault(m => m.UserCategoryId == id);
            if (userCategoryMapModel == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<UserCategoryMapModel>(userCategoryMapModel);

            return View(userCategoryMapModel);
        }

        // POST: UserCategoryMap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userCategoryMapModel = _context.UserCategoryMaps.Find(id);
            _context.UserCategoryMaps.Remove(userCategoryMapModel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCategoryMapModelExists(int id)
        {
            return _context.UserCategoryMaps.Any(e => e.UserCategoryId == id);
        }
    }
}
