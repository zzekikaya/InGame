using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InGame.Core.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }

        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        IEnumerable<T> Select<TResult>(Expression<Func<T, TResult>> selector);

        int Take { get; }
        int Skip { get; }
        bool isPagingEnabled { get; }
    }
}
