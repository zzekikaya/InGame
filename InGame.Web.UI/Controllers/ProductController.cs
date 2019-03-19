using AutoMapper;
using InGame.Core.Entities;
using InGame.Core.Interfaces;
using InGame.Web.UI.Models.ParentCategoryViewModels;
using InGame.Web.UI.Models.ProductViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var producties = _productService.ListAllAsync().Result.ToList();
            var ProductModelList = _mapper.Map<List<Product>, List<ProductViewModel>>(producties);
            return View(ProductModelList);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetProductById(id.Value);
            var category = _categoryService.Get(x => x.CategoryId == product.CategoryId);

            var ProductModel = _mapper.Map<Product, ProductViewModel>(product);
            ProductModel.Category = _mapper.Map<Category, CategoryViewModels>(category);

            if (ProductModel == null)
            {
                return NotFound();
            }

            return View(ProductModel);

        }

        // GET: Product/Create
        public IActionResult Create()
        {
            var categories = _categoryService.ListAllAsync().Result;
            var categoryModelList = _mapper.Map<List<Category>, List<CategoryViewModels>>(categories.ToList());
            ViewData["CategoryID"] = new SelectList(categoryModelList, "Id", "CategoryName");
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
            var categoryModelList = _mapper.Map<List<Category>, List<CategoryViewModels>>(categories.ToList());
            ViewData["CategoryID"] = new SelectList(categoryModelList, "Id", "CategoryName", product.CategoryID);
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
            var categoryViewModels = _mapper.Map<List<Category>, List<CategoryViewModels>>(categories.ToList());

            if (ProductModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(categoryViewModels, "Id", "CategoryName", product.CategoryId);
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

            var categorytModelList = _mapper.Map<List<Category>, List<CategoryViewModels>>(categories.ToList());
            ViewData["CategoryID"] = new SelectList(categorytModelList, "Id", "CategoryName", product.CategoryID);
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
            return _productService.IsAny(x => x.Id == id);
        }
    }
}
