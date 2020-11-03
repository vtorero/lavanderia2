using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia.Models
{
    public class Servicio
    {
         public int idServicio { get; set; }
        public string NombreServicio{ get; set; }
        public float precioServicio { get; set; }
        

        public Servicio() { }

        public Servicio(int id, string nombre, float precio)
        {
            this.idServicio = id;
            this.NombreServicio = nombre;
            this.precioServicio= precio;
            
        }
    }
}
