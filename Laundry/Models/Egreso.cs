using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia.Models
{
    class Egreso
    {
        public int idEgreso { get; set; }
        public int Usuario { get; set; }
        public decimal Monto { get; set; }
        public string FechaEgreso { get; set; }
        public string Motivo { get; set; }
        public int Estado { get; set; }


        public Egreso() { }

        public Egreso(int id, int usuario,decimal monto ,string fecha, string motivo, int estado)
           {
               this.idEgreso = id;
               this.Usuario = usuario;
               this.Monto = monto;
               this.FechaEgreso = fecha;
               this.Motivo = motivo;
               this.Estado = estado;
           }
    }
    }
