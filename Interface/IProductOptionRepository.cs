using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Interface
{
    interface IProductOptionRepository
    {
        ProductOption GetProductOption(Guid productId, Guid id);
        ProductOptions GetProductOptionsForProduct(Guid productId);
        bool AddOrUpdateProductOption(Guid productId, ProductOption productOption);
        bool DeleteProductOption(Guid productId, Guid id);
    }
}
