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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trusted")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        { 
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IEnumerable<Product> GetProductList()
        {
            var producResult = _productService.ListAllAsync().Result.ToList();

            return producResult;
        }

        //ürün detay sayfasında Id kullanıldı.
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _productService.GetProductById(id);
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

        //update işlemi için put kullanıldı.
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
            return _categoryService.IsAny(x => x.Id == id);
        }
    }
}
