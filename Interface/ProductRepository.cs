using RefactorThis.Models;
using RefactorThis.Context;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace RefactorThis.Interface
{
    public class ProductRepositoty: IProductRepositoty
    {
        RefactorThisContext context = new RefactorThisContext();
        public Products GetAllProducts() 
        {
            Products products = new Products();
            products.Items = context.LoadProducts(null);
            return products;
        }

        public Products GetProductsByName(string name) 
        {
            Products products = new Products();
            products.Items=context.LoadProducts($"where lower(name) like '%{name.ToLower()}%'");
            return products;
        }

        public Product GetProductById(Guid id) 
        {
            return context.LoadProducts($"where id = '{id}' collate nocase").First();
        }

        public bool AddOrUpdateProduct(Product product)
        {
            if (product.IsNew)
            {
                context.AddProduct(product);
            }
            else
            {
                context.UpdateProduct(product);
            }
            return true;
        }

        public bool DeleteProduct(Guid id)
        {
            context.DeleteOptionsForProduct(id);
            context.DeleteProduct(id);
            return true;
        }
    }
}
