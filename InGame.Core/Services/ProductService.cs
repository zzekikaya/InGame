using InGame.Core.Entities;
using InGame.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InGame.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IAsyncRepository<Product> _productRepository;
        private readonly IAsyncRepository<SubCategory> _subCategoryRepository;
        private readonly IAppLogger<ProductService> _logger;

        public IQueryable<Product> Set => throw new NotImplementedException();

        public ProductService(IAsyncRepository<Product> productRepository,
            IAppLogger<ProductService> logger,
            IAsyncRepository<SubCategory> subCategoryRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
            _subCategoryRepository = subCategoryRepository;
        }
        public Task<Product> GetByIdAsync(int id)
        {
            var result = _productRepository.GetByIdAsync(id);
            return result;
        }
        public async Task CreateProduct(Product product)
        {
            var result = await _productRepository.AddAsync(product);
        }
        public async Task UpdateProduct(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            var result = await _productRepository.GetByIdAsync(id.Value);

            return result;
        }

        public async Task<IReadOnlyList<Product>> ListAllAsync()
        {
            var products = await _productRepository.ListAllAsync();

            return products.ToList();
        }

        public async Task<Product> AddAsync(Product entity)
        {
            var result = await _productRepository.AddAsync(entity);
            return result;
        }

        public async Task UpdateAsync(Product entity)
        {
            await _productRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Product entity)
        {
            await _productRepository.DeleteAsync(entity);
        }

        public Task<int> CountAsync(ISpecification<Product> spec)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Product>> ListAsync(ISpecification<Product> spec)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int productId)
        {
            var result = _productRepository.GetByIdAsync
                (productId);
            return result.Result;
        }

        public Product Get(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetAll(Expression<Func<Product, bool>> predicate)
        {
          return  _productRepository.GetAll(predicate);
        }

        public Task<IReadOnlyList<Product>> GetListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsAny(Expression<Func<Product, bool>> predicate)
        {
            return _productRepository.IsAny(predicate);
        }
    }
}
