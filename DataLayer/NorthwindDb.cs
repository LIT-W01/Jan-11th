using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class NorthwindDb
    {
        private readonly string _connectionString;

        public NorthwindDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Category> GetAll()
        {
            List<Category> result = new List<Category>();
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Categories";
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Category c = new Category();
                    c.Id = (int) reader["CategoryId"];
                    c.Name = (string) reader["CategoryName"];
                    c.Description = (string) reader["Description"];
                    result.Add(c);
                }
            }

            return result;
        }

        public Category GetCategoryById(int categoryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Categories WHERE CategoryId = @categoryId";
                connection.Open();
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                var reader = cmd.ExecuteReader();
                reader.Read();
                Category c = new Category();
                c.Id = (int) reader["CategoryId"];
                c.Name = (string) reader["CategoryName"];
                c.Description = (string) reader["Description"];
                return c;
            }
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            List<Product> products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Products WHERE CategoryId = @categoryId";
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product p = new Product();
                    p.Id = (int) reader["ProductId"];
                    p.Name = (string) reader["ProductName"];
                    p.QuantityPerUnit = (string) reader["QuantityPerUnit"];
                    p.UnitPrice = (decimal) reader["UnitPrice"];
                    products.Add(p);
                }
            }

            return products;
        }
    }
}

