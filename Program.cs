using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Security.Cryptography;

public class Usuario
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
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


class program
{
    static void Main(string[] args)
    {
       string conexionString = "server= localhost;user id = root; password = 1234; database = users";

        Usuario user = new Usuario("Ryan", "Trujillo", "Email@gmail.com", "3145481234");
        Usuario user2 = new Usuario("Pedro", "Sanchez", "Emaail@gmail.com", "123456789");

        AccesoaDB data = new AccesoaDB(conexionString);
        bool prueba = data.InsertarUsuario(user);
        data.InsertarUsuario(user2);

        if (prueba)
        {
            Console.WriteLine("La prueba fue exitiosa");
        }
        else
        {
            Console.WriteLine("El usuario no pudo se añadido");
        }

    }

}

