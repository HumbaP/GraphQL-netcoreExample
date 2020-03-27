using CarvedRock.Data;
using CarvedRock.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarvedRock.Repositories
{
    public class ProductReviewRepository
    {
        private readonly CarvedRockDbContext _dbContext;

        public ProductReviewRepository(CarvedRockDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<ProductReview> GetForProduct(int id){
            return _dbContext.ProductReviews.Where(pr=> pr.Id==id).ToList();
        }

        public async Task<ILookup<int,ProductReview>> GetForProducts(IEnumerable<int> productsId){
            var reviews = await _dbContext.ProductReviews.Where(
                pr=> productsId.Contains(pr.ProductId)).ToListAsync();
            return reviews.ToLookup(r=>r.ProductId);
        }

    }


}