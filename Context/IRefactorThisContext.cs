using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Context
{
    public interface IRefactorThisContext
    {
        List<Product> LoadProducts(string filter);

        bool AddProduct(Product product);

        bool UpdateProduct(Product product);

        List<ProductOption> LoadProductOptions(string filter);

        bool DeleteProduct(Guid id);

        bool DeleteProductOption(Guid productId, Guid id);

        bool DeleteOptionsForProduct(Guid productId);

        bool AddProductOption(ProductOption productOption);

        bool UpdateProductOption(ProductOption productOption);
    }
}
