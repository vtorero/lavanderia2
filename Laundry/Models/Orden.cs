using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia.Models
{
    public class Orden
    {
        public int idOrden { get; set; }
        public int idCliente { get; set; }
        public string fechaEntrega{ get; set; }
        public decimal totalOrden{ get; set; }
        public string fechaCreado { get; set; }
        public int idUsuario{ get; set; }
        public string observacion { get; set; }
        public int estado { get; set; }
        public int tipoPago { get; set;}
        public int Descuento { get; set; }
        public decimal pDescuento {get; set;}
        public int pGarantia { get; set; }
        public int pExpress { get; set; }
        public int pDelivery { get; set; } 

        public Orden() { }

        public Orden(int idorden, int idcliente, string fechaentrega, decimal totalorden, string fechacreado, int idusuario, string Observacion, int Estado, int tipopago, int descuento, decimal pdescuento, int pgarantia,int pexpress,int pdelivery)
        {
            this.idOrden=idorden;
            this.idCliente = idcliente;
            this.fechaEntrega=fechaentrega;
            this.totalOrden = totalorden;
            this.fechaCreado = fechacreado;
            this.idUsuario = idusuario;
            this.observacion = Observacion;
            this.estado = Estado;
            this.tipoPago = tipopago;
            this.Descuento = descuento;
            this.pDescuento = pdescuento;
            this.pGarantia = pgarantia;
            this.pExpress = pexpress;
            this.pDelivery = pdelivery;
            
        }

    }
}
