using RefactorThis.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RefactorThis.Interface
{
    public interface IProductRepositoty
    {
        Products GetAllProducts();
        Products GetProductsByName(string name);
        Product GetProductById(Guid id);
        bool AddOrUpdateProduct(Product product);
        bool DeleteProduct(Guid id);
    }
}
