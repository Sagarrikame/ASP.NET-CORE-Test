using Dapper;
using QHRMProject.Models;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class ProductRepository
{
    private readonly string connectionString;

    public ProductRepository()
    {
        connectionString = @"Data Source=DESKTOP-BU3COTI\SQLEXPRESS;Initial Catalog=ProductsDapper;Integrated Security=True";
    }

    public IDbConnection Connection => new SqlConnection(connectionString);

    public IEnumerable<Product> GetAllProducts()
    {
        using (IDbConnection dbConnection = Connection)
        {
            string query = "SELECT * FROM products";
            return dbConnection.Query<Product>(query).ToList();
        }
    }

    public void AddProduct(Product product)
    {
        using (IDbConnection dbConnection = Connection)
        {
            product.Created = DateTime.Now;
            string query = "INSERT INTO products (ProductName, Description, Created, Price) VALUES (@ProductName, @Description, @Created, @Price)";
            dbConnection.Execute(query, product);
        }
    }


    public Product GetProductById(int id)
    {
        using (IDbConnection dbConnection = Connection)
        {
            string query = "SELECT * FROM products WHERE SN = @SN";
            var product = dbConnection.Query<Product>(query, new { SN = id }).FirstOrDefault();
            return product ?? new Product();
        }
    }

  
    public void UpdateProduct(Product product)
    {
        using (IDbConnection dbConnection = Connection)
        {
            string query = "UPDATE products SET ProductName = @ProductName, Description = @Description, Price = @Price WHERE SN = @SN";
            dbConnection.Execute(query, product);
        }
    }

    public void DeleteProduct(int id)
    {
        using (IDbConnection dbConnection = Connection)
        {
            string query = "DELETE FROM products WHERE SN = @SN";
            dbConnection.Execute(query, new { SN = id });
        }
    }
}
