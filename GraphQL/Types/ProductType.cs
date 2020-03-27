using CarvedRock.Data.Entities;
using CarvedRock.Repositories;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace CarvedRock.GraphQL.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(ProductReviewRepository reviewRepository, IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(t=> t.Id);
            Field(t=> t.Name);
            Field(t=> t.Description);
            Field(t=> t.IntroducedAt).Description("When the product was first introduced in the database");
            Field(t=> t.PhotoFileName).Description("The file name of the photo so the client can");
            Field(t=> t.Price);
            Field(t=> t.Rating).Description("Rating given from users");
            Field<ProductTypeEnumType>("Type", "The type of product");
            Field<ListGraphType<ProductReviewType>>(
                "reviews",
                resolve: context =>{
                    var loader = dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int,ProductReview>(
                            "GetReviewsByProductId", reviewRepository.GetForProducts
                            );
                    return loader.LoadAsync(context.Source.Id);
                }  
            );
        }
    }
}