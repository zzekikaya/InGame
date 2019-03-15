using System;
using InGame.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InGame.Core.Interfaces
{
    public interface IProductService: IAsyncRepository<Product>
    {
        Product GetProductById(int productId);

        Task CreateProduct(Product product);

        Task UpdateProduct(Product product);
    }
}
