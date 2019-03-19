using InGame.Core.Entities;
using InGame.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InGame.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IAsyncRepository<Category> _categoryRepository; 
        private readonly IAppLogger<CategoryService> _logger;
        public CategoryService(IAsyncRepository<Category> categoryRepository, IAppLogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<Category> AddAsync(Category entity)
        {
            var result = await _categoryRepository.AddAsync(entity);
            return result;
        }

        public  async Task CreateCagetory(Category cagetory)
        {
            var result = await _categoryRepository.AddAsync(cagetory);
        }

        public async Task DeleteAsync(Category entity)
        {
            await _categoryRepository.DeleteAsync(entity);
        }

        public Category Get(Expression<Func<Category, bool>> predicate)
        {
            return _categoryRepository.Get(predicate);
        }

        public IQueryable<Category> GetAll(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            var result = _categoryRepository.GetByIdAsync(id);
            return result;
        }

        public Category GetCagetoryById(int cagetoryId)
        {
            var result = _categoryRepository.GetByIdAsync(cagetoryId);
            return result.Result;
        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Category>> GetListByIdAsync(int id)
        {
            var result = _categoryRepository.GetListByIdAsync(id);
            return result.Result;
        }

        public bool IsAny(Expression<Func<Category, bool>> predicate)
        {
            return _categoryRepository.IsAny(predicate);
        }

        public Task<IReadOnlyList<Category>> ListAllAsync()
        {
           return _categoryRepository.ListAllAsync();
        }

        public Task<IReadOnlyList<Category>> ListAsync(ISpecification<Category> spec)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCagetory(Category cagetory)
        {
            await _categoryRepository.UpdateAsync(cagetory);
        }
    }
}
