using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ReadingIsGood.MongoDB.Abstractions;

namespace ReadingIsGood.MongoDB
{
    public class MongoDbRepositoryQueryBuilder<T> : IGenericRepositoryQueryBuilder<T>
    {
        private IMongoQueryable<T> mongoQueryable;

        internal MongoDbRepositoryQueryBuilder(IMongoQueryable<T> mongoQueryabl)
        {
            this.mongoQueryable = mongoQueryabl;
        }

        public async Task<T> FirstOrDefaultAsync()
        {
            return await this.mongoQueryable.FirstOrDefaultAsync();
        }

        public T FirstOrDefault()
        {
            return this.mongoQueryable.FirstOrDefault();
        }


        public IGenericRepositoryQueryBuilder<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        {
            return new MongoDbRepositoryQueryBuilder<TResult>(this.mongoQueryable.Select(selector));
        }

        public async Task<IReadOnlyList<T>> ToListAsync()
        {
            return await this.mongoQueryable.ToListAsync();
        }

        public IGenericRepositoryQueryBuilder<T> Where(Expression<Func<T, bool>> expression)
        {
            this.mongoQueryable = this.mongoQueryable.Where(expression);
            return this;
        }

        public IGenericRepositoryQueryBuilder<T> Take(int count)
        {
            this.mongoQueryable = this.mongoQueryable.Take(count);
            return this;
        }

        public IGenericRepositoryQueryBuilder<T> Skip(int count)
        {
            this.mongoQueryable = this.mongoQueryable.Skip(count);
            return this;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.mongoQueryable.AnyAsync(predicate);
        }
    }
}
