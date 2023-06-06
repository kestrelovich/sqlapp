using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {

        private static string db_source = "appserverprasko.database.windows.net";
        private static string db_user = "sqladmin";
        private static string db_password = "Brez0vsk!";
        private static string db_database = "appdb";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
           SqlConnection _connection = GetConnection();

            List<Product> _products_lst = new List<Product>();

            string statement = "SELECT ProductID,ProductName,Quantity from Products";

            _connection.Open();

            SqlCommand _sqlCommand =new SqlCommand(statement, _connection);

            using (SqlDataReader _reader = _sqlCommand.ExecuteReader())
            {
                while(_reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = _reader.GetInt32(0),
                        ProductName = _reader.GetString(1),
                        Quantity = _reader.GetInt32(2)
                    };

                    _products_lst.Add(_product);

                }
            }
            _connection.Close();
            return _products_lst;
        }
    }
}