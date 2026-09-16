using Microsoft.Data.SqlClient;
using System.Text;

namespace _1ADONet
{
    internal class Program
    {

        private static readonly string _connectionString = "Server=LUKA-PC;Database=UNIVERSITY;Trusted_Connection=True; TrustServerCertificate=True";

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            ReadStudentsAllData();
        }

        static void ReadStudentsAllData()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Students", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if(reader.HasRows)
                            {
                                int id = (int)reader["ID"];
                                string name = (string)reader["Name"];

                                Console.WriteLine($"ID: {id}, Name: {name}");
                            }
                        }
                    }
                }
            }
        }
    }
}
