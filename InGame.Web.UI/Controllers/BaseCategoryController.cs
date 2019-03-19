using AutoMapper;
using InGame.Core.Entities;
using InGame.Core.Interfaces;
using InGame.Web.UI.Models.ParentCategoryViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Web.UI.Controllers
{
    public class BaseCategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public BaseCategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET: BaseCategory
        public IActionResult Index()
        {
            var categories = _categoryService.ListAllAsync();
            var categoryModelList = _mapper.Map<List<Category>, List<CategoryViewModels>>(categories.Result.ToList());
            List<CategoryViewModels> result = new List<CategoryViewModels>();
            if (categoryModelList != null)
            {
                result = categoryModelList.Select(p => new CategoryViewModels
                {
                    Id = p.Id,
                    CategoryId = p.CategoryId,
                    CategoryName = p.CategoryName,
                    ParentCategoryId = p.ParentCategoryId, 
                    Uri = p.Uri,
                    PictureUri = p.PictureUri,
                    Description = p.Description
                }).ToList();
            }
            return View(result);
        }


        // GET: BaseCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.Get(x => x.Id == id.Value);

            var categoryModel = _mapper.Map<Category, CategoryViewModels>(category);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // GET: BaseCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BaseCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,ParentCategoryId,CategoryName,Uri,PictureUri,Description,Id")] CategoryViewModels category)
        {
            if (ModelState.IsValid)
            {
                //Aynı anda Hem AnaCategory hem ParentCategory insert edilemez.
                if (category.CategoryId == 0 && category.ParentCategoryId == 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                var entity = _mapper.Map<CategoryViewModels, Category>(category);
                await _categoryService.AddAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: BaseCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.Get(x => x.Id == id.Value);
            var categoryModel = _mapper.Map<Category, CategoryViewModels>(category);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // POST: BaseCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,ParentCategoryId,CategoryName,Uri,PictureUri,Description,Id")] CategoryViewModels category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Aynı anda Hem AnaCategory hem ParentCategory insert edilemez.
                    if (category.CategoryId == 0 && category.ParentCategoryId == 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    var entity = _mapper.Map<CategoryViewModels, Category>(category);
                    await _categoryService.UpdateCagetory(entity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: BaseCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _categoryService.Get(x => x.Id == id.Value);
            var categoryModel = _mapper.Map<Category, CategoryViewModels>(category);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // POST: BaseCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = _categoryService.Get(x => x.Id == id);
            await _categoryService.DeleteAsync(category);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _categoryService.IsAny(x => x.Id == id);
        }

        //recursive Category Listesi 
        public IActionResult CategoryTreeView()
        {
            var TreeViewList = GetDOMTreeView();
            ViewData["CategoryTreeList"] = TreeViewList;
            return View();
        }
        private string GetDOMTreeView()
        {
            var CategoryTreeList = _categoryService.ListAllAsync().Result.ToList();
            string DOMTreeView = string.Empty;

            DOMTreeView += CreateTreeViewOuterParent(CategoryTreeList);
            DOMTreeView += CreateTreeViewMenu(CategoryTreeList, "0");

            DOMTreeView += "</ul>";

            return DOMTreeView;
        }

        private string CreateTreeViewOuterParent(List<Category> dt)
        {
            //parentID 0 olanlar root yani AnaMenü olarak kabul edilir.
            string DOMDataList = string.Empty;
            var menuList = dt.Where(x => x.CategoryId.ToString() == "0");
            foreach (var itemCat in menuList)
            {
                DOMDataList = "<ul><li class='header'>" + itemCat.CategoryName + "</li>";

            }
            return DOMDataList;
        }

        private string CreateTreeViewMenu(List<Category> dt, string ParentCategory)
        {
            string DOMDataList = string.Empty;
            string CategoryId = string.Empty;
            string categoryName = string.Empty; ;
            string uri = string.Empty; ;
            string urlPicture = string.Empty; ;

            var parentList = dt.Where(x => x.ParentCategoryId.ToString() == ParentCategory).ToList();
            foreach (var item in parentList)
            {
                CategoryId = item.CategoryId.ToString();
                categoryName = item.CategoryName;
                uri = item.Uri;
                urlPicture = item.PictureUri;

                DOMDataList += "<li class='treeview'>";
                DOMDataList += "<i class='" + urlPicture + "'></i><span>  "
                               + categoryName + "</span>";

                if (CategoryId.Length != 0)
                {
                    DOMDataList += "<ul class='treeview-menu'>";
                    DOMDataList += CreateTreeViewMenu(dt, CategoryId).Replace("<li class='treeview'>", "<li>");
                    DOMDataList += "</ul></li>";
                }
                else
                {
                    DOMDataList += "</li>";
                }
            }
            return DOMDataList;
        }
    }
}
