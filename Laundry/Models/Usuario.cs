using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombreUsuario{ get; set; }
        public string apellidoUsuario { get; set; }
        public string emailUsuario { get; set; }
        public string sucursalUsuario { get; set; }
        public int tipoUsuario { get; set; }
        

    

      public Usuario() { }

        public Usuario(int idUsuario, string nombre, string apellido, string correo,string sucursal,int tipo)
        {
            this.idUsuario = idUsuario;
            this.nombreUsuario = nombre;
            this.apellidoUsuario = apellido;
            this.emailUsuario= correo;
            this.sucursalUsuario = sucursal;
            this.tipoUsuario= tipo;
            
        }
    }
}
