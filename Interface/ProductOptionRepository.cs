using RefactorThis.Context;
using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Interface
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        RefactorThisContext context = new RefactorThisContext();

        public bool AddOrUpdateProductOption(Guid productId, ProductOption productOption)
        {
            productOption.ProductId = productId;
            if (productOption.IsNew)
            {
                context.AddProductOption(productOption);
            }
            else
            {
                context.UpdateProductOption(productOption);
            }
            return true;
        }

        public bool DeleteProductOption(Guid productId, Guid id)
        {
            context.DeleteProductOption(productId, id);
            return true;
        }

        public ProductOption GetProductOption(Guid productId, Guid id)
        {
            List<ProductOption>  result =  context.LoadProductOptions($"where productId = {productId} and id = {id} collate nocase");
            return result == null || result.Count == 0 ? null : result.First(); 
        }

        public ProductOptions GetProductOptionsForProduct(Guid productId)
        {
            ProductOptions productOptions = new ProductOptions();
            productOptions.Item = context.LoadProductOptions($"where productId = {productId} collate nocase");
            return productOptions;
        }
    }
}
