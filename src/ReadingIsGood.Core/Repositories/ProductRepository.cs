using MongoDB.Driver;
using ReadingIsGood.Core.Data.Abstractions;
using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IReadingIsGoodContext _context;

        public ProductRepository(IReadingIsGoodContext _readingIsGoodContext)
        {
            _context = _readingIsGoodContext ?? throw new ArgumentNullException(nameof(_readingIsGoodContext));
        }

        public async Task AddProductAsync(Product product)
        {
            await _context
                    .Product
                    .InsertOneAsync(product);
        }

        public async Task<Product?> GetProductAsync(string id)
        {
            return await _context
                            .Product
                            .Find(r => r.Id == id)
                            .FirstOrDefaultAsync();
        }

        //stock reconciliation olarak düşünülmüştür.
        //BulkQuery 
        public async Task<string> UpdateProductStockAsync(string id, int stock, DateTime updatedDateTime)
        {
            //TODO: or Builders ya da Bulk Query kullanmadan bütün modeli çekip stock sum field yapılabilir. 
            var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
            var update = Builders<Product>.Update.Set(x => x.Stock, stock)
                                                 .Set(x => x.UpdatedDateTime, updatedDateTime);

            await _context
                    .Product
                    .UpdateManyAsync(filter, update);

            return id;

        }
    }
}
