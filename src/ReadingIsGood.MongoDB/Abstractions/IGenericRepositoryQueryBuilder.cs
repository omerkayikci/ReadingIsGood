using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReadingIsGood.MongoDB.Abstractions
{
    public interface IGenericRepositoryQueryBuilder<T>
    {

        Task<IReadOnlyList<T>> ToListAsync();
        T FirstOrDefault();
        Task<T> FirstOrDefaultAsync();
        IGenericRepositoryQueryBuilder<T> Take(int count);
        IGenericRepositoryQueryBuilder<T> Skip(int count);
        IGenericRepositoryQueryBuilder<TResult> Select<TResult>(Expression<Func<T, TResult>> selector);
        IGenericRepositoryQueryBuilder<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
