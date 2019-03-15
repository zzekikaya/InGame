using InGame.Core.Entities;
using InGame.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InGame.Core.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IAsyncRepository<SubCategory> _subCategoryRepository;
        private readonly IAppLogger<SubCategoryService> _logger;
        public SubCategoryService(IAsyncRepository<SubCategory> subCategoryRepository,
            IAppLogger<SubCategoryService> logger)
        {
            _logger = logger;
            _subCategoryRepository = subCategoryRepository;
        }

        public IQueryable<SubCategory> Set => throw new NotImplementedException();

        public async Task<SubCategory> AddAsync(SubCategory entity)
        {
            var result = await _subCategoryRepository.AddAsync(entity);
            return result;
        }
        public async Task CreateSubCategory(SubCategory subCategory)
        {
            await _subCategoryRepository.AddAsync(subCategory);
        }

        public async Task DeleteAsync(SubCategory entity)
        {
            await _subCategoryRepository.DeleteAsync(entity);
        }

        public SubCategory Get(Expression<Func<SubCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SubCategory> GetAll(Expression<Func<SubCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<SubCategory> GetByIdAsync(int id)
        {
            var subCategory = await _subCategoryRepository.GetByIdAsync(id);
            return subCategory;
        }

        public Task<IReadOnlyList<SubCategory>> GetListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public SubCategory GetSubCategoryId(int subCategoryId)
        {
            var result = _subCategoryRepository.GetByIdAsync(subCategoryId);
            return result.Result;
        }

        public bool IsAny(Expression<Func<SubCategory, bool>> predicate)
        {
            return _subCategoryRepository.IsAny(predicate);
        }

        public async Task<IReadOnlyList<SubCategory>> ListAllAsync()
        {
            return await _subCategoryRepository.ListAllAsync();
        }

        public Task<IReadOnlyList<SubCategory>> ListAsync(ISpecification<SubCategory> spec)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<SubCategory, TResult>> selector)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(SubCategory entity)
        {
            await _subCategoryRepository.UpdateAsync(entity);
        }

        public async Task UpdateSubCategory(SubCategory subCategory)
        {
            await _subCategoryRepository.UpdateAsync(subCategory);
        }

        public IQueryable<SubCategory> Where(Expression<Func<SubCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }


    }
}
