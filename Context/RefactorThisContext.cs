using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace RefactorThis.Context
{
    public class RefactorThisContext : IRefactorThisContext
    {
        private const string ConnectionString = "Data Source=App_Data/products.db";

        private static SqliteConnection NewConnection()
        {
            return new SqliteConnection(ConnectionString);
        }

        private SqliteCommand SetUpCommand(string cmdText)
        {
            var conn = NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = cmdText;
            return cmd;
        }

        public List<Product> LoadProducts(string filter)
        {
            List<Product> Items = new List<Product>();
            SqliteCommand cmd = SetUpCommand($"select * from Products {filter}");

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Product matchingProduct = new Product(Guid.Parse(rdr["Id"].ToString()));
                matchingProduct.Name = rdr["Name"].ToString();
                matchingProduct.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();
                matchingProduct.Price = decimal.Parse(rdr["Price"].ToString());
                matchingProduct.DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString());
                Items.Add(matchingProduct);
            }
            return Items;
        }

        public bool AddProduct(Product product)
        {
            SqliteCommand cmd = SetUpCommand($"insert into Products (id, name, description, price, deliveryprice) values ('{product.Id}', '{product.Name}', '{product.Description}', {product.Price}, {product.DeliveryPrice})");
            cmd.ExecuteNonQuery();
            return true;
        }

        public bool UpdateProduct(Product product)
        {
            SqliteCommand cmd = SetUpCommand($"update Products set name = '{product.Name}', description = '{product.Description}', price = {product.Price}, deliveryprice = {product.DeliveryPrice} where id = '{product.Id}' collate nocase");
            cmd.ExecuteNonQuery();
            return true;
        }

        public bool DeleteProduct(Guid id)
        {
            SqliteCommand cmd = SetUpCommand($"delete from products where id = '{id}' collate nocase");
            cmd.ExecuteNonQuery();
            return true;
        }

        public bool DeleteProductOption(Guid productId, Guid id)
        {
            SqliteCommand cmd = SetUpCommand($"delete from productOptions where id = '{id}' and productId = '{productId}' collate nocase");
            cmd.ExecuteNonQuery();
            return true;
        }

        public bool DeleteOptionsForProduct(Guid productId)
        {
            SqliteCommand cmd = SetUpCommand($"delete from productOptions where productId = '{productId}' collate nocase");
            cmd.ExecuteNonQuery();
            return true;
        }

        public List<ProductOption> LoadProductOptions(string filter)
        {
            List<ProductOption> Items = new List<ProductOption>();
            SqliteCommand cmd = SetUpCommand($"select * from productoptions {filter}");

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ProductOption matchingProductOption = new ProductOption(Guid.Parse(rdr["Id"].ToString()));
                matchingProductOption.Name = rdr["Name"].ToString();
                matchingProductOption.ProductId = Guid.Parse(rdr["ProductId"].ToString());
                matchingProductOption.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();
                Items.Add(matchingProductOption);
            }
            return Items;
        }

        public bool AddProductOption(ProductOption productOption)
        {
            SqliteCommand cmd = SetUpCommand($"insert into productoptions (id, productid, name, description) values ('{productOption.Id}', '{productOption.ProductId}', '{productOption.Name}', '{productOption.Description}')");
            cmd.ExecuteNonQuery();
            return true;
        }

        public bool UpdateProductOption(ProductOption productOption)
        {
            SqliteCommand cmd = SetUpCommand($"update productoptions set name = '{productOption.Name}', description = '{productOption.Description}' where id = '{productOption.Id}' collate nocase");
            cmd.ExecuteNonQuery();
            return true;
        }
    }
}