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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IEnumerable<Category> GetCategoryList()
        {
            var categoryResult = _categoryService.ListAllAsync().Result.ToList();

            return categoryResult;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            var category = _categoryService.GetCagetoryById(id);

            return category;
        }

        // POST api/values
        [HttpPost]
        public void AddCategory([FromBody] Category model)
        {
            if (model != null)
            {
                _categoryService.CreateCagetory(model);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void UpdateCategory(int id, [FromBody] Category model)
        {
            var existCategory= CategoryExists(id);
            if (existCategory && model != null)
            {
                _categoryService.UpdateCagetory(model);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void DeleteCategory(int id)
        {
            var category = _categoryService.GetCagetoryById(id);
            _categoryService.DeleteAsync(category);
        }

        private bool CategoryExists(int id)
        {
            return _categoryService.IsAny(x => x.Id == id);
        }
    }
}
