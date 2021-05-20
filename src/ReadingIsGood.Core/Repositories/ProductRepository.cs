using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Repositories.Abstractions;
using ReadingIsGood.MongoDB.Abstractions;
using System;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IGenericRepository<Product, string> genericRepository;
        public ProductRepository(
            IGenericRepository<Product, string> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public async Task AddProductAsync(Product product)
        {
            await this.genericRepository
                              .AddOneAsync(product);
        }

        public async Task<Product?> GetProductAsync(string id)
        {
            return await this.genericRepository
                                .GetByIdAsync(id);
        }

        //stock reconciliation olarak düşünülebilir ve stock değerinin sıfırlanmasıda yapılabilir.
        public async Task<string> UpdateProductStockAsync(string id, int stock, DateTime updatedDateTime)
        {
            var product = await this.genericRepository.GetByIdAsync(id);

            if (product == null)
            {
                //TODO: Thow yapılacak.
            }

            product!.Stock += stock;

            await this.genericRepository.UpdateAsync(product);

            return id;

        }
    }
}
