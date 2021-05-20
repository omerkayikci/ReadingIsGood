using System;

namespace ReadingIsGood.MongoDB.Abstractions
{
    public interface IEntity<TId>
        where TId : IEquatable<TId>
    {
        TId Id { get; }
    }
}
