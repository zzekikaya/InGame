using AutoMapper;
using InGame.Core.Entities;
using InGame.Core.Interfaces;
using InGame.Web.UI.Models.CategoryViewModels;
using InGame.Web.UI.Models.ProductViewModels;
using InGame.Web.UI.Models.SubCategoryViewModels;
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
        private readonly ISubCategoryService _subCategoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, ISubCategoryService subCategoryService, IMapper mapper)
        {
            _productService = productService;
            _subCategoryService = subCategoryService;
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
                SubCategoryID = p.SubCategoryID,
                Subcategory = _subCategoryService.GetSubCategoryId(p.SubCategoryID.Value)
            }).ToList();

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
            product.Subcategory = _subCategoryService.GetSubCategoryId(product.SubCategoryID.Value);

            var ProductModel = _mapper.Map<Product, ProductViewModel>(product);
            ProductModel.Subcategory = _mapper.Map<SubCategory, SubCategoryViewModel>(product.Subcategory);

            if (ProductModel == null)
            {
                return NotFound();
            }

            return View(ProductModel);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            var subcategories = _subCategoryService.ListAllAsync().Result;
            var subCategoryModelList = _mapper.Map<List<SubCategory>, List<SubCategoryViewModel>>(subcategories.ToList());
            ViewData["SubCategoryID"] = new SelectList(subCategoryModelList, "Id", "SubCategoryName");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,PictureUri,IsActive,SubCategoryID,Id")] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<ProductViewModel, Product>(product);
                await _productService.CreateProduct(entity);
                return RedirectToAction(nameof(Index));
            }

            var subCategories = _subCategoryService.ListAllAsync().Result;
            var subCategoryModelList = _mapper.Map<List<SubCategory>, List<SubCategoryViewModel>>(subCategories.ToList());
            ViewData["SubCategoryID"] = new SelectList(subCategoryModelList, "Id", "SubCategoryName", product.SubCategoryID);
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
            var subCategories = _subCategoryService.ListAllAsync().Result;

            var ProductModel = _mapper.Map<Product, ProductViewModel>(product);
            ProductModel.Subcategory = _mapper.Map<SubCategory, SubCategoryViewModel>(product.Subcategory);
            var subCategoryModelList = _mapper.Map<List<SubCategory>, List<SubCategoryViewModel>>(subCategories.ToList());


            if (ProductModel == null)
            {
                return NotFound();
            }
            ViewData["SubCategoryID"] = new SelectList(subCategoryModelList, "Id", "SubCategoryName", product.SubCategoryID);
            return View(ProductModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Price,PictureUri,IsActive,SubCategoryID,Id")] ProductViewModel product)
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

            var subCategories = _subCategoryService.ListAllAsync().Result;

            var SubCategorytModelList = _mapper.Map<List<SubCategory>, List<SubCategoryViewModels>>(subCategories.ToList());
            ViewData["SubCategoryID"] = new SelectList(SubCategorytModelList, "Id", "SubCategoryName", product.SubCategoryID);
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
            product.Subcategory = _subCategoryService.GetSubCategoryId(product.SubCategoryID.Value);
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
            return _subCategoryService.IsAny(x => x.Id == id);
        }
    }
}
