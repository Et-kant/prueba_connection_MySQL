using MySql.Data.MySqlClient;
using System;

class program
{
    static void Main(string[] args)
    {
        string connectionString = "server= localhost;user id = root; password = 1234; database = new_database_connection";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {

                conn.Open();
                Console.WriteLine("The connection to the data base was successfull");
            }
            catch(MySqlException ex)
            {
                Console.WriteLine("An error has ocurred" + ex.Message);
            }
        }
    } 
}