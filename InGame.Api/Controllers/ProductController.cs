using InGame.Core.Entities;
using InGame.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace InGame.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    //[Authorize]
    //[ApiController]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trusted")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ISubCategoryService subCategoryService, ICategoryService categoryService)
        {
            //_context = context;
            _productService = productService;
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }


        [HttpGet]
        public IEnumerable<Product> GetProductList()
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

            return producties;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                product.Subcategory = _subCategoryService.GetSubCategoryId(product.SubCategoryID.Value);

                product.Subcategory.Category = _categoryService.GetCagetoryById(product.Subcategory.CategoryID.Value);
            }

            return product;
        }

        // POST api/values
        [HttpPost]
        public void AddProduct([FromBody] Product model)
        {
            if (model != null)
            {
                _productService.CreateProduct(model);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void UpdateProduct(int id, [FromBody] Product model)
        {
            var existProduct = ProductExists(id);
            if (existProduct && model != null)
            {
                _productService.UpdateProduct(model);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
            var product = _productService.GetProductById(id);
            _productService.DeleteAsync(product);
        }

        private bool ProductExists(int id)
        {
            return _subCategoryService.IsAny(x => x.Id == id);
        }
    }
}
