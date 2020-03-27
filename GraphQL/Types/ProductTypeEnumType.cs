using GraphQL.Types;

namespace CarvedRock.GraphQL.Types
{
    public class ProductTypeEnumType: EnumerationGraphType<Data.ProductType>
    {
        public ProductTypeEnumType()
        {  
            Name = "Type";
            Description = "The type of product";
        }
    }
}