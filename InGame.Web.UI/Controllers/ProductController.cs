using InGame.Core.Entities;
using InGame.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace InGame.Web.UI.Controllers
{
    [Authorize(Roles = "Product_view")]
    public class ProductController : Controller
    {
        //private readonly InGameContext _context;
        private readonly IProductService _productService;
        private readonly ISubCategoryService _subCategoryService;

        public ProductController(IProductService productService, ISubCategoryService subCategoryService)
        {
            //_context = context;
            _productService = productService;
            _subCategoryService = subCategoryService;
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

            //var inGameContext = _context.Products.Include(p => p.Subcategory);
            //return View(await inGameContext.ToListAsync());
            return View(producties);
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

            //var product = await _context.Products
            //    .Include(p => p.Subcategory)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            var subcategories = _subCategoryService.ListAllAsync().Result;
            ViewData["SubCategoryID"] = new SelectList(subcategories, "Id", "Id");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,PictureUri,IsActive,SubCategoryID,Id")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(product);
                return RedirectToAction(nameof(Index));
            }

            var subCategories = _subCategoryService.ListAllAsync().Result;
            ViewData["SubCategoryID"] = new SelectList(subCategories, "Id", "Id", product.SubCategoryID);
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
            //var product = await _context.Products.FindAsync(id);
            var subCategories = _subCategoryService.ListAllAsync().Result;
            if (product == null)
            {
                return NotFound();
            }
            ViewData["SubCategoryID"] = new SelectList(subCategories, "Id", "SubCategoryName", product.SubCategoryID);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Price,PictureUri,IsActive,SubCategoryID,Id")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _productService.UpdateProduct(product);

                    //_context.Update(product);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["SubCategoryID"] = new SelectList(subCategories, "Id", "Id", product.SubCategoryID);
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
            //var product = await _context.Products
            //    .Include(p => p.Subcategory)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var product = await _context.Products.FindAsync(id);
            //_context.Products.Remove(product);
            //await _context.SaveChangesAsync();
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
