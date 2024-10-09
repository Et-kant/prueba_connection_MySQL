using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    //Objeto
    public class AccesoaDB
    {
        private String _Stringdeconexion;
        public AccesoaDB(String conexionString)
        {
            _Stringdeconexion = conexionString;
        }

        //Metodo para agregar informacion a la base de datos
        public bool InsertarUsuario(Usuario usuario)
        {
            string consulta = "INSERT INTO Clientes (Nombre, Apellido, Email, Telefono) VALUES (@Nombre, @Apellido, @Email, @Telefono)";

            using (MySqlConnection con = new MySqlConnection(_Stringdeconexion))
            {
                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(consulta, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                        cmd.Parameters.AddWithValue("@Email", usuario.Email);
                        cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);

                        int filasAfect = cmd.ExecuteNonQuery();

                        return filasAfect > 0;
                    }

                }
                catch (MySqlException e)
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

        //Metodo que recib una lista para el disply de la informacion del objeto 
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
                }
                catch (MySqlException e)
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

        //Metodo para la Actualizacion de datos en la base de datos segun su ID
        //Actualmente actualiza toda la informacion de la fila, tengo que cambiarlo para que actualize no mas email y telefono
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
                }
                catch (MySqlException e)
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

        //Metodo para el Borrado de datos segun su ID
        public void DeleteUser(int id)
        {
            String consulta = "DELETE FROM Clientes WHERE id = @Id";

            using (MySqlConnection con = new MySqlConnection(_Stringconnection))
            {
                MySqlCommand cmd = new MySqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Total de filas afectadas {rowsAffected}");

                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Error" + e.Message);
                }
            }
        }
    }
}
