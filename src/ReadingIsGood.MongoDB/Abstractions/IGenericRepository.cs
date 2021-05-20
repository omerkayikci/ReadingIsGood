﻿using System;
using System.Threading.Tasks;

namespace ReadingIsGood.MongoDB.Abstractions
{
    public interface IGenericRepository<TEntity, TId>
         where TEntity : class
         where TId : IEquatable<TId>
    {

        string CollectionName { get; set; }

        bool CollectionExists { get; }

        bool CreateCollection();

        bool DropCollection();

        Task<TEntity?> GetByIdAsync(TId id);

        Task AddOneAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        IGenericRepositoryQueryBuilder<TEntity> Query();

    }
}
