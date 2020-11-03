using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lavanderia.Models
{
    public class Oferta
    {
        public int idOferta { get; set; }
        public string Nombre { get; set; }
        public string DiasOferta { get; set; }
        public decimal Porcentaje {get;set;}
        public string Prendas { get; set; }
        public int Cantidad { get; set; }
        public int Estado {get;set;}
        public decimal PorcentajeVisa { get; set; }



        public Oferta() { }

        public Oferta(int id, string nombre, string dias, decimal oferta, string prendas, int cantidad, int estado, int visa)
        {
            this.idOferta = id;
            this.Nombre = nombre;
            this.DiasOferta= dias;
            this.Porcentaje = oferta;
            this.Prendas = prendas;
            this.Cantidad = cantidad;
            this.Estado = estado;
            this.PorcentajeVisa = visa;
        }
    }
}
