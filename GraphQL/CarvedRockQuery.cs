using CarvedRock.GraphQL.Types;
using CarvedRock.Repositories;
using GraphQL.Types;

namespace CarvedRock.GraphQL
{
    public class CarvedRockQuery : ObjectGraphType
    {
        public CarvedRockQuery(ProductRepository productRepository)
        {
            Field<ListGraphType<ProductType>>(
                "products",
                resolve: context => productRepository.GetAll()
            );
        }
        
    }
}