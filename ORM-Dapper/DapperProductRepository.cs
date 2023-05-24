﻿using System;
using System.Data;
using Dapper;

namespace ORM_Dapper
{
	public class DapperProductRepository : IProductRepository
	{
        private readonly IDbConnection _connecton;

		public DapperProductRepository(IDbConnection connection)
		{
            _connecton = connection;
		}

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connecton.Execute("INSERT INTO products (Name, Price, CategoryID)"
                + "VALUES (@name, @price, @categoryID);",
                new { name = name, price = price, categoryID = categoryID });
                
        }

        public void DeleteProduct(int productID)
        {
            _connecton.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID = productID });

            _connecton.Execute("DELETE FROM sales WHERE ProductID = @productID;",
                new { productID = productID });

            _connecton.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = productID });
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connecton.Query<Product>("SELECT * FROM products;");
        }

        public void UpdateProduct(int productID, string updatedName)
        {
            _connecton.Excute("UPDATE products SET Name = @updatedName WHERE productID = @productID;",
                new { productID = productID, updatedName = updatedName });
        }
    }
}

