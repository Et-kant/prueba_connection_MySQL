using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ZstdSharp.Unsafe;

public class Usuario //Objeto para crear el usuario
{
    public string Nombre { get; set; } //Propiedades
    public string Apellido { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    //Constructor 
    public Usuario(String nombre, String apellido, String email, String telefono)
    {
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.Email = email;
        this.Telefono = telefono;
    }
}


public class AccesoaDB
{
    private String _Stringdeconexion;
    public AccesoaDB(String conexionString)
    {
        _Stringdeconexion = conexionString;
    }

    public bool InsertarUsuario(Usuario usuario)
    {
        string consulta = "INSERT INTO Clientes (Nombre, Apellido, Email, Telefono) VALUES (@Nombre, @Apellido, @Email, @Telefono)";

        using(MySqlConnection con = new MySqlConnection(_Stringdeconexion))
        {
            try
            {
                con.Open();
                using(MySqlCommand cmd = new MySqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);

                    int filasAfect = cmd.ExecuteNonQuery();

                    return filasAfect > 0;
                }

            }catch(MySqlException e)
            {
                Console.WriteLine("ERROR" + e.Message);
                return false;
            }
            finally
            {
                con.Close();
                Console.WriteLine("Coneccion cerrada satisfactoriamente");
            }

        }
    }
}

public class ReadDB
{
    private String _Stringdeconexion;

    public ReadDB(String conexionString)
    {
        _Stringdeconexion = conexionString; 
    }

    public List<Usuario> GetUsers()
    {
        List<Usuario> users = new List<Usuario>();
        String consulta = "SELECT Nombre, Apellido, Email, Telefono FROM Clientes";
        using (MySqlConnection con = new MySqlConnection(_Stringdeconexion))
        {
            try
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(consulta, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuarios = new Usuario(
                                reader["Nombre"].ToString(),
                                reader["Apellido"].ToString(),
                                reader["Email"].ToString(),
                                reader["Telefono"].ToString()
                                );

                            users.Add(usuarios);
                        } 
                    }
                }
            } catch (MySqlException e)
            {
                Console.WriteLine("ERROR" + e);
            }
        }

        return users;
    }

}

public class UpdateDB
{
    private String _Stringdeconexion;

    public UpdateDB(String conexionString)
    {
        _Stringdeconexion = conexionString;
    }

    public void UpdateUser(int id, String newName, String newApellido, String newEmail, String newTelefono)
    {
        String consulta = "UPDATE Clientes SET Nombre = @name, Apellido = @Apellido, Email = @Email, Telefono = @Telefono WHERE id = @id";

        using (MySqlConnection con = new MySqlConnection(_Stringdeconexion))
        {
            MySqlCommand cmd = new MySqlCommand(consulta, con);
            cmd.Parameters.AddWithValue("@name", newName);
            cmd.Parameters.AddWithValue("@Apellido", newApellido);
            cmd.Parameters.AddWithValue("@Email", newEmail);
            cmd.Parameters.AddWithValue("@Telefono", newTelefono);
            cmd.Parameters.AddWithValue("@id", id);


            try
            {
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                Console.WriteLine($"{rowsAffected} filas actualizadas");
            }catch (MySqlException e)
            {
                Console.WriteLine("Error" + e.Message);
            }

        }
    }


}


public class DeleteDB
{
    private String _Stringconnection;

    public DeleteDB(String conexionString)
    {
        _Stringconnection = conexionString;
    }
    public void DeleteUser(int id)
    {
        String consulta = "DELETE FROM Clientes WHERE id = @Id";

        using(MySqlConnection con = new MySqlConnection(_Stringconnection))
        {
            MySqlCommand cmd = new MySqlCommand(consulta, con);
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                con.Open() ;
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"Total de filas afectadas {rowsAffected}");

            }catch (MySqlException e)
            {
                Console.WriteLine("Error" + e.Message);
            }
        }
    }
}

class program
{
    static void Main(string[] args)
    {
       string conexionString = "server= localhost;user id = root; password = 1234; database = users";

        //Usuario user = new Usuario("Ryan", "Trujillo", "Email@gmail.com", "3145481234");
        //Usuario user2 = new Usuario("Pedro", "Sanchez", "Emaail@gmail.com", "123456789");

        AccesoaDB data = new AccesoaDB(conexionString);
        //bool prueba = data.InsertarUsuario(user);
        //data.InsertarUsuario(user2);

        //if (prueba)
        //{
        //    Console.WriteLine("La prueba fue exitiosa");
        //}
        //else
        //{
        //    Console.WriteLine("El usuario no pudo se añadido");
        //}

        ReadDB readDB = new ReadDB(conexionString);
        List<Usuario> usuarios = readDB.GetUsers();

        Console.WriteLine("\nUsuario en la base de datos");

        foreach(var usr in usuarios)
        {
            Console.WriteLine($"Nombre: {usr.Nombre}, Apellido: {usr.Apellido}, Email: {usr.Email}, Telefono: {usr.Telefono}");  
        }

        UpdateDB updateDB = new UpdateDB(conexionString);
        updateDB.UpdateUser(1, "Roberto", "Lorenzo", "123@gmail.com", "1111111111");
        //updateDB.UpdateUser(10, "JoseAlonso");

        DeleteDB deleteDB = new DeleteDB(conexionString);
        deleteDB.DeleteUser(2);
    }

}

