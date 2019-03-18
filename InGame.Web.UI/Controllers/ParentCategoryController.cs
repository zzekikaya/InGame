using InGame.Core.Entities;
using InGame.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Web.UI.Controllers
{
    public class ParentCategoryController : Controller
    {
        private readonly IParentCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;
        public ParentCategoryController(IParentCategoryService subCategoryService,
            ICategoryService categoryService)
        {
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }

        // GET: SubCategories
        public async Task<IActionResult> Index()
        {
            List<ParentCategory> result = new List<ParentCategory>();

            result = _subCategoryService.ListAllAsync().Result.ToList();
            if (result != null)
            {
                var subCategories = result.Select(p => new ParentCategory
                {
                    Id = p.Id,
                    ParentCategoryName = p.ParentCategoryName,
                    Description = p.Description,
                    CategoryID = p.CategoryID,
                    Category = _categoryService.GetCagetoryById(p.CategoryID.Value)
                }).ToList();
            }

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
            ViewData["CategoryID"] = new SelectList(categories, "Id", "CategoryName");
            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubCategoryName,Description,CategoryID,Id")] ParentCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                await _subCategoryService.AddAsync(subCategory);
                return RedirectToAction(nameof(Index));
            }

            var categories = _categoryService.ListAllAsync().Result.ToList();
            ViewData["CategoryID"] = new SelectList(categories, "Id", "CategoryName", subCategory.CategoryID);
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
            if (subCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(categories, "Id", "CategoryName", subCategory.CategoryID);
            return View(subCategory);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubCategoryName,Description,CategoryID,Id")] ParentCategory subCategory)
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
            ViewData["CategoryID"] = new SelectList(categories, "Id", "CategoryName", subCategory.CategoryID);
            return View(subCategory);
        }

        // GET: SubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var subCategory = _subCategoryService.GetSubCategoryId(id.Value);
            subCategory.Category = await _categoryService.GetByIdAsync(subCategory.Id);

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
            return RedirectToAction(nameof(Index));
        }

        private bool SubCategoryExists(int id)
        {
            return _subCategoryService.IsAny(e => e.Id == id);
        }
    }
}
