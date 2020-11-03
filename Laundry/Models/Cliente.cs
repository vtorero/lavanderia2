using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia.Models
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string Nombres{ get; set; }
        public string DNI { get; set; }
        public string Email{ get; set; }
        public string Dirección { get; set; }
        public string Teléfono { get; set; }
        public int usuarioCreador { get; set; }



        public Cliente() { }

        public Cliente(int idCliente, string NombreC, string dni, string correo,string direccion,string telefono,int usuario)
        {
            this.idCliente = idCliente;
            this.Nombres = NombreC;
            this.DNI= dni;
            this.Email= correo;
            this.Dirección= direccion;
            this.Teléfono= telefono;
            this.usuarioCreador = usuario;
            
        }
    }
}
