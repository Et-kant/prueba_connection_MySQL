using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

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
}
