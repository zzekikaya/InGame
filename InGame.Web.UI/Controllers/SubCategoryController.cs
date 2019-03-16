using InGame.Core.Entities;
using InGame.Core.Interfaces;
using InGame.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Web.UI.Controllers
{
    public class SubCategoryController : Controller
    {
        
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;
        public SubCategoryController(InGameContext context, ISubCategoryService subCategoryService,
            ICategoryService categoryService)
        {
          
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }

        // GET: SubCategories
        public async Task<IActionResult> Index()
        {
            List<SubCategory> result = new List<SubCategory>();

            result = _subCategoryService.ListAllAsync().Result.ToList();
            if (result != null)
            {
                var subCategories = result.Select(p => new SubCategory
                {
                    Id = p.Id,
                    SubCategoryName = p.SubCategoryName,
                    Description = p.Description,
                    CategoryID = p.CategoryID,
                    Category = _categoryService.GetCagetoryById(p.CategoryID.Value)
                }).ToList();
            }
            //var inGameContext = _context.SubCategories.Include(s => s.Category);
            //return View(await inGameContext.ToListAsync());

            return View(result);
        }

        // GET: SubCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = _subCategoryService.GetSubCategoryId(id.Value);
            subCategory.Category = _categoryService.GetCagetoryById(subCategory.CategoryID.Value);
            //var subCategory = await _context.SubCategories
            //    .Include(s => s.Category)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // GET: SubCategories/Create
        public IActionResult Create()
        {
            var categories = _categoryService.ListAllAsync().Result.ToList();
            ViewData["CategoryID"] = new SelectList(categories, "Id", "Id");
            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubCategoryName,Description,CategoryID,Id")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                await _subCategoryService.AddAsync(subCategory);
                //_context.Add(subCategory);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var categories = _categoryService.ListAllAsync().Result.ToList();
            ViewData["CategoryID"] = new SelectList(categories, "Id", "Id", subCategory.CategoryID);
            return View(subCategory);
        }

        // GET: SubCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = _subCategoryService.GetSubCategoryId(id.Value);
            var categoryResult = _categoryService.GetListByIdAsync(subCategory.Id);
            var categories = _categoryService.ListAllAsync().Result.ToList();
            //var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            //ViewData["CategoryID"] = new SelectList(_context.Cagetories, "Id", "Id", subCategory.CategoryID);
            ViewData["CategoryID"] = new SelectList(categories, "Id", "Id", subCategory.CategoryID);
            return View(subCategory);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubCategoryName,Description,CategoryID,Id")] SubCategory subCategory)
        {
            if (id != subCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _subCategoryService.UpdateSubCategory(subCategory);
                    //_context.Update(subCategory);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubCategoryExists(subCategory.Id))
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
            var categories = _categoryService.ListAllAsync().Result.ToList();
            ViewData["CategoryID"] = new SelectList(categories, "Id", "Id", subCategory.CategoryID);
            return View(subCategory);
        }

        // GET: SubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var sub =  _subCategoryService.GetSubCategoryId(id.Value);
            //   _categoryService.GetCagetoryById(sub.CategoryID.Value);
            var subCategory = _subCategoryService.GetSubCategoryId(id.Value);
            subCategory.Category = await _categoryService.GetByIdAsync(subCategory.Id);

            //subCategory2.Category = categoryResult.Result;
            //var subCategory = await _context.SubCategories
            //    .Include(s => s.Category)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // POST: SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategory = _subCategoryService.GetSubCategoryId(id);
            await _subCategoryService.DeleteAsync(subCategory);
            //var subCategory = await _context.SubCategories.FindAsync(id);
            //_context.SubCategories.Remove(subCategory);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubCategoryExists(int id)
        {
            return _subCategoryService.IsAny(e => e.Id == id);
        }
    }
}
