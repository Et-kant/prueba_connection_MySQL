using MySql.Data.MySqlClient;
using System;

class program
{
    static void Main(string[] args)
    {
        string connectionString = "server= localhost;user id = root; password = 1234; database = new_database_connection.Clientes";

        String Nombre = "Ryan";
        String Apellido = "Lopez";
        String Email = "email@gmai.com";
        String Telefono = "3145471541";

        String query = "INSERT INTO Clientes (Nombre, Apellido, Email, Telefono) VALUES (@Nombre, @Apellido, @Email, @Telefono)";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {

                conn.Open();
                Console.WriteLine("The connection to the data base was successfull");

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", Apellido);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Telefono", Telefono);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    
                    if(filasAfectadas > 0)
                    {
                        Console.WriteLine("The user has been add it to the Data Base");
                    }
                    else
                    {
                        Console.WriteLine("The user was no possible to be added");
                    }
                }
            }
            catch(MySqlException ex)
            {
                Console.WriteLine("An error has ocurred" + ex.Message);
                
            }
            finally
            {
                conn.Close();
                Console.WriteLine("The Connection was closed Successfully");
            }
        }
    } 
}