using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia.Models
{
    public class OrdenLinea
    {
        public int idOrden { get; set; }
        public int Item { get; set; }
        public int idPrenda{ get; set; }
        public string Descripcion{ get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Defecto { get; set; }
        public string  Colores { get; set; }
        public string Marca { get; set; }
        public decimal Total { get; set; }
        public int TipoServicio { get; set; }
        public int Estado { get; set; }
      
        public OrdenLinea() { }
    
           public OrdenLinea(int idorden, int item, int idprenda,string descripcion, decimal cantidad,string defecto,decimal precio,string colores,string marca,decimal total,int tiposervicio,int estado)
        {
            this.idOrden=idorden;
            this.Item = item;
            this.idPrenda = idprenda;
            this.Descripcion = descripcion;
            this.Cantidad= cantidad;
            this.Precio = precio;
            this.Defecto = defecto;
            this.Colores = colores;
            this.Marca = marca;
            this.Total= total;
            this.TipoServicio = tiposervicio;
            this.Estado = estado;

            
        }
    
    }


}
