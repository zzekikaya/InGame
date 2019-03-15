using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using InGame.Core.Entities;

namespace InGame.Core.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
         
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetListByIdAsync(int id);

        Task<IReadOnlyList<T>> ListAllAsync();

        T Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
       
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        bool IsAny(Expression<Func<T, bool>> predicate);
    }
}
