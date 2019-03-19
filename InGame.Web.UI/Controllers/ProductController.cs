using AutoMapper;
using InGame.Core.Entities;
using InGame.Core.Interfaces; 
using InGame.Web.UI.Models.ProductViewModels; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InGame.Web.UI.Models.ParentCategoryViewModels;

namespace InGame.Web.UI.Controllers
{
    [Authorize(Roles = "Product_view")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var producResult = _productService.ListAllAsync().Result.ToList();

            var producties = producResult.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                IsActive = p.IsActive,
                PictureUri = p.PictureUri,
                Price = p.Price,
                CategoryId = p.CategoryId,
                Category = _categoryService.Get(x=>x.CategoryId==p.CategoryId)
            }).ToList();

            var ProductModelList = _mapper.Map<List<Product>, List<ProductViewModel>>(producties);

            return View(ProductModelList);
            return null;
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetProductById(id.Value);
            product.Category = _categoryService.Get(x=>x.CategoryId==id);

            var ProductModel = _mapper.Map<Product, ProductViewModel>(product);
            ProductModel.Category = _mapper.Map<Category, CategoryViewModels>(product.Category);

            if (ProductModel == null)
            {
                return NotFound();
            }

            return View(ProductModel);

        }

        // GET: Product/Create
        public IActionResult Create()
        {
            var subcategories = _categoryService.ListAllAsync().Result;
            var subCategoryModelList = _mapper.Map<List<Category>, List<CategoryViewModels>>(subcategories.ToList());
            ViewData["CategoryID"] = new SelectList(subCategoryModelList, "Id", "CategoryName");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,PictureUri,IsActive,CategoryID,Id")] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<ProductViewModel, Product>(product);
                await _productService.CreateProduct(entity);
                return RedirectToAction(nameof(Index));
            }

            var categories = _categoryService.ListAllAsync().Result;
            var subCategoryModelList = _mapper.Map<List<Category>, List<CategoryViewModels>>(categories.ToList());
            ViewData["CategoryID"] = new SelectList(subCategoryModelList, "Id", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetProductById(id.Value);
            var categories = _categoryService.ListAllAsync().Result;

            var ProductModel = _mapper.Map<Product, ProductViewModel>(product);
            ProductModel.Category = _mapper.Map<Category, CategoryViewModels>(product.Category);
            var subCategoryModelList = _mapper.Map<List<Category>, List<CategoryViewModels>>(categories.ToList());


            if (ProductModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(subCategoryModelList, "Id", "CategoryName", product.CategoryId);
            return View(ProductModel);

        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Price,PictureUri,IsActive,CategoryID,Id")] ProductViewModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _mapper.Map<ProductViewModel, Product>(product);
                    await _productService.UpdateProduct(entity);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id.Value))
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

            var categories = _categoryService.ListAllAsync().Result;

            var SubCategorytModelList = _mapper.Map<List<Category>, List<CategoryViewModels>>(categories.ToList());
            ViewData["CategoryID"] = new SelectList(SubCategorytModelList, "Id", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _productService.GetProductById(id.Value);
            //product.Subcategory = _subCategoryService.GetSubCategoryId(product.SubCategoryID.Value);
            var ProductModel = _mapper.Map<Product, ProductViewModel>(product);
            if (ProductModel == null)
            {
                return NotFound();
            }

            return View(ProductModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = _productService.GetProductById(id);
            await _productService.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _categoryService.IsAny(x => x.Id == id);
        }
    }
}
