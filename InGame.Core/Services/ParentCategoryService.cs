using InGame.Core.Entities;
using InGame.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InGame.Core.Services
{
    public class ParentCategoryService : IParentCategoryService
    {
        private readonly IAsyncRepository<ParentCategory> _subCategoryRepository;
        private readonly IAppLogger<ParentCategoryService> _logger;
        public ParentCategoryService(IAsyncRepository<ParentCategory> subCategoryRepository,
            IAppLogger<ParentCategoryService> logger)
        {
            _logger = logger;
            _subCategoryRepository = subCategoryRepository;
        }

        public IQueryable<ParentCategory> Set => throw new NotImplementedException();

        public async Task<ParentCategory> AddAsync(ParentCategory entity)
        {
            var result = await _subCategoryRepository.AddAsync(entity);
            return result;
        }
        public async Task CreateSubCategory(ParentCategory subCategory)
        {
            await _subCategoryRepository.AddAsync(subCategory);
        }

        public async Task DeleteAsync(ParentCategory entity)
        {
            await _subCategoryRepository.DeleteAsync(entity);
        }

        public ParentCategory Get(Expression<Func<ParentCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ParentCategory> GetAll(Expression<Func<ParentCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<ParentCategory> GetByIdAsync(int id)
        {
            var subCategory = await _subCategoryRepository.GetByIdAsync(id);
            return subCategory;
        }

        public Task<IReadOnlyList<ParentCategory>> GetListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public ParentCategory GetSubCategoryId(int subCategoryId)
        {
            var result = _subCategoryRepository.GetByIdAsync(subCategoryId);
            return result.Result;
        }

        public bool IsAny(Expression<Func<ParentCategory, bool>> predicate)
        {
            return _subCategoryRepository.IsAny(predicate);
        }

        public async Task<IReadOnlyList<ParentCategory>> ListAllAsync()
        {
            return await _subCategoryRepository.ListAllAsync();
        }

        public Task<IReadOnlyList<ParentCategory>> ListAsync(ISpecification<ParentCategory> spec)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<ParentCategory, TResult>> selector)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ParentCategory entity)
        {
            await _subCategoryRepository.UpdateAsync(entity);
        }

        public async Task UpdateSubCategory(ParentCategory subCategory)
        {
            await _subCategoryRepository.UpdateAsync(subCategory);
        }

        public IQueryable<ParentCategory> Where(Expression<Func<ParentCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }


    }
}
