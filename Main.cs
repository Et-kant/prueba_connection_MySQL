using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ZstdSharp.Unsafe;


namespace ConsoleApp1
{
class Program
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

}

//Pendiente por modularizar el codigo lo mas posible y aplicar MVC, integrar interfaz de usuario as well.