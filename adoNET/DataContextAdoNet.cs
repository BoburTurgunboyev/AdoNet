using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net.Quic;

namespace adoNET
{
    public class DataContextAdoNet
    {
       
        public static void GetById(int userId)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=StudyCenterDB;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;

                connection.Open();

                string query = $"select * from userTable where userId = {userId}";

                SqlCommand command = new SqlCommand(query, connection);

                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Console.WriteLine($"{reader["userId"]} {reader["firstName"]} {reader["lastName"]}");
                    }
                }
            }
        }

       
        public static void GetAll()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=StudyCenterDB;Trusted_Connection=True;";

            using(SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;

                connection.Open();

                string query = "select * from userTable";

                SqlCommand command = new SqlCommand(@query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Console.WriteLine($"{reader["userId"]} {reader["firstName"]} {reader["lastName"]}");
                    }
                }
            }
        }

      
        public static void DeleteById(int userId)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=StudyCenterDB;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;

                connection.Open();

                string query = $"delete from userTable where userId = {userId}";

                SqlCommand command = new SqlCommand(query , connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {

                }
            }
        }

       
        public static void Create(int userId,string firstName,string lastName)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=StudyCenterDB;Trusted_Connection=True;";
            
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;

                connection.Open();

                string query = $"insert into userTable(userId,firstName,lastName) values({userId}, '{firstName}', '{lastName}')";

                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader()) { }
            }
        }

      
        public static void Update(int userId, string firstName, string lastName)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=StudyCenterDB;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;

                connection.Open();

                string query = $"update userTable set firstName = '{firstName}',lastName = '{lastName}' where userId = {userId};";

                SqlCommand command = new SqlCommand(query,connection);

                using(SqlDataReader reader = command.ExecuteReader()) { }
            }
        }

      
        public static List<User> GetUsers()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=StudyCenterDB;Trusted_Connection=True;";

            List<User> users = new List<User>();

            using(SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;

                connection.Open();

                string query = "select * from userTable";

                SqlCommand command = new SqlCommand(query,connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            userId = (int)reader[0],
                            firstName = (string)reader[1],
                            lastName = (string)reader[2]
                        });
                    }
                }

                return users;
            }
        }


        /// <summary>
        /// This is a generic Function
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="tableName"></param>
        /// <param name="SQlcommand"></param>
        public static void GetByGeneric(string databaseName, string tableName, string SQlcommand)
        {
            string connectionString = $"Server=(localdb)\\MSSQLLocalDB;Database={databaseName};Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;

                connection.Open();

                string query = $"select * from {tableName} {SQlcommand}";

                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int j = 0; j < reader.FieldCount; j++)
                        {
                            Console.Write($"{reader[j]} ");
                        }
                        Console.WriteLine();
                    }
                }
            }

        }

        /// <summary>
        /// Generic Insert
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="tableName"></param>
        /// <param name="values"></param>
        public static void InsertGeneric(string databaseName, string tableName,string values)
        {
            string connectionString = $"Server=(localdb)\\MSSQLLocalDB;Database={databaseName};Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;

                connection.Open();

                string Columns = $"select * from {tableName};";

                SqlCommand ColumnCommond = new SqlCommand(Columns, connection);

                dynamic result;

                using (SqlDataReader reader = ColumnCommond.ExecuteReader())
                {
                    result = reader.GetColumnSchema();
                }

                string stringResult = string.Empty;

                foreach (var i in result)
                {
                    if (i.ColumnName.ToUpper() != "id".ToUpper())
                    {
                        stringResult += i.ColumnName + ",";
                    }
                }

                stringResult = stringResult.Substring(0,stringResult.Length - 1);

                string query = $"insert into {tableName} ({stringResult}) values {values}";

                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader2 = command.ExecuteReader())
                {

                }
            }
        }

        /// <summary>
        /// Generic Delete
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="tableName"></param>
        /// <param name="values"></param>
        public static void DeleteGeneric(string databaseName, string tableName, string condition)
        {
            string connectionString = $"Server=(localdb)\\MSSQLLocalDB;Database={databaseName};Trusted_Connection=True;";

            using(SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;

                connection.Open();  

                string query = $"delete from {tableName} {condition}";

                SqlCommand command = new SqlCommand(query,connection);

                command.ExecuteNonQuery();
            }
        }
    }
}
