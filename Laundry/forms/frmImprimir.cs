using Crearticket;
using CrystalDecisions.CrystalReports.Engine;
using Lavanderia.Persistencia;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lavanderia.util;

namespace Lavanderia.forms
{
    public partial class frmImprimir : Form
    {
        public frmImprimir()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            ReportDocument cryrep = new ReportDocument();
            MySqlDataAdapter myadap = new MySqlDataAdapter(String.Format(
         "SELECT o.idOrden,c.dniCliente,c.nombreCliente,o.fechaCreado,o.fechaEntrega, o.totalOrden,l.cantidad,l.precio,l.descripcion,l.total,l.colorPrenda,l.marca,l.defecto,p.pago1,p.pago2,u.direccion,u.telefono FROM Orden o INNER JOIN Cliente c ON o.idCliente=c.idCliente INNER JOIN Pago p ON o.idOrden=p.idOrden INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN usuario u ON u.id=o.idUsuario WHERE o.idOrden={0}", txtTicket.Text), cnx.ObtenerConexion());
            DataSet ds = new DataSet();
            myadap.Fill(ds, "Ticket");
            cryrep.Load(varGlobales.rutaReportes+"\\Reportes\\crTicket.rpt");
            cryrep.SetDataSource(ds);
            //cryrep.PrintToPrinter(1, true, 0, 0);
            cnx.cerrarConexion();
            frmReporte rt = new frmReporte();
            rt.Text = "Ticket";
            rt.crystalReportViewer1.ReportSource = cryrep;
            rt.Show();﻿
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(txtTicket.Text))
            {
                CrearTicket ticket = new CrearTicket();

                ConexBD cn1 = new ConexBD();
                cn1.Conectar();

                ticket.TextoCentro("LAVANDERIA SAN ISIDRO S.A");
                ticket.TextoIzquierda("");
                MySqlCommand _comando1 = new MySqlCommand(String.Format(
                "SELECT o.idOrden,c.dniCliente,c.nombreCliente,o.fechaCreado,o.fechaEntrega, o.totalOrden,o.descuento,(l.total-o.`totalOrden`) dscto ,o.aplicaDscto,l.cantidad,l.precio,l.descripcion,l.total,l.colorPrenda,l.marca,l.defecto,p.pago1,p.pago2,u.direccion,u.telefono,u.impresora,p.tipoPago1,o.express,o.delivery FROM Orden o INNER JOIN Cliente c ON o.idCliente=c.idCliente INNER JOIN Pago p ON o.idOrden=p.idOrden INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN usuario u ON u.id=o.idUsuario WHERE o.idOrden={0}", txtTicket.Text), cn1.ObtenerConexion());
                ConexBD cn2 = new ConexBD();
                cn2.Conectar();
                MySqlCommand _comando = new MySqlCommand(String.Format(
                "SELECT o.idOrden,c.dniCliente,c.nombreCliente,o.fechaCreado,o.fechaEntrega, o.totalOrden,l.cantidad,l.precio,l.descuento,l.descripcion,l.total,l.colorPrenda,l.marca,l.defecto,p.pago1,p.pago2,u.direccion,u.telefono,u.impresora,p.tipoPago1,o.garantia,o.express,o.delivery FROM Orden o INNER JOIN Cliente c ON o.idCliente=c.idCliente INNER JOIN Pago p ON o.idOrden=p.idOrden INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN usuario u ON u.id=o.idUsuario WHERE o.idOrden={0}", txtTicket.Text), cn2.ObtenerConexion());
                MySqlDataReader _reader1 = _comando1.ExecuteReader();
                MySqlDataReader _reader = _comando.ExecuteReader();
                _reader1.Read();
                ticket.TextoIzquierda("DIRECCION: " + _reader1.GetString(18).ToUpper());
                ticket.TextoIzquierda("HORARIO: LUNES A VIERNES DE 8:00AM");
                ticket.TextoIzquierda(" A 8:00PM Y SABADO DE 8:00AM A 7:00PM");
                ticket.TextoIzquierda("TELEF: " + _reader1.GetString(19));
                ticket.TextoIzquierda("WEB:LAVANDERIASANISIDRO.COM");
                ticket.lineasIgual();
                ticket.TextoIzquierda("CLIENTE: " + _reader1.GetString(2).ToUpper());
                if (!_reader1.GetString(1).Equals(""))
                {
                    ticket.TextoIzquierda("DNI: " + _reader1.GetString(1));
                }
                ticket.TextoIzquierda("FECHA DE ORDEN: " + _reader1.GetString(3));
                ticket.TextoIzquierda("FECHA DE ENTREGA: " + _reader1.GetString(4).Substring(0, 10));
                ticket.TextoExtremos("NRO DE ORDEN:", "Ticket # " + _reader1.GetString(0));

                ticket.lineasAsteriscos();
                ticket.EncabezadoVenta();
                ticket.lineasAsteriscos();
                decimal totalSinDescuento = 0;
                decimal cargoVisa = 0;
               
                if (_reader1.GetDecimal(21) == 1)
                {
                    cargoVisa = 5;
                }
                while (_reader.Read())
                {
                    ticket.AgregaArticulo(_reader.GetString(9), _reader.GetDecimal(6), _reader.GetDecimal(7), _reader.GetDecimal(10));
                    if (!_reader.GetString(11).Equals("") || !_reader.GetString(12).Equals("") || !_reader.GetString(13).Equals(""))
                    {
                        ticket.TextoExtremos(_reader.GetString(11) + " " + _reader.GetString(12), _reader.GetString(13));
                    }
                    if (_reader1.GetDecimal(8) > 0 && _reader.GetDecimal(8) > 0)
                    {
                        ticket.TextoExtremos("PROM DSCTO -" + _reader1.GetDecimal(6) + "%  ", " - " + (Decimal.Round(Decimal.Round((Convert.ToDecimal(_reader.GetDecimal(10))), 2) * _reader1.GetDecimal(6) / 100, 2)));
                    }
                    totalSinDescuento += _reader.GetDecimal(10);
                    ticket.lineasGuio();
                }

                ticket.lineasAsteriscos();

                if (_reader1.GetDecimal(6) > 0)
                {
                    if (_reader1.GetDecimal(8) > 0 )  {

                        ticket.AgregarTotales("            TOTAL..........S/.", _reader.GetDecimal(14));
                        
                        }else {

                        ticket.AgregarTotales("            TOTAL..........S/.", totalSinDescuento);
                        }

                  //  ticket.AgregarTotales("            DESCUENTO.." + (_reader1.GetDecimal(6) - cargoVisa) + "%.", (totalSinDescuento - _reader1.GetDecimal(5)));
                }
                else
                {
                    if (_reader1.GetDecimal(23) > 0)
                    {
                        ticket.TextoDerecha("              SERVICIO DELIVERY");
                    }
                    if (_reader1.GetDecimal(22) > 0)
                    {
                        ticket.TextoDerecha("              SERVICIO EXPRESS 50% +");
                    }
                    ticket.AgregarTotales("            TOTAL..........S/.", _reader.GetDecimal(5));
                }

                

                ticket.AgregarTotales("            A CUENTA.......S/.", _reader.GetDecimal(14));//La M indica que es un decimal en C#
                ticket.AgregarTotales("            SALDO..........S/.", _reader.GetDecimal(15));
                ticket.TextoIzquierda("");
                if (_reader.GetInt32(19) == 1)
                {
                    ticket.TextoCentro("Prendas sin garantía");
                }
                ticket.TextoCentro("¡GRACIAS POR SU PREFERENCIA!");
                ticket.CortaTicket();
                ticket.ImprimirTicket(_reader1.GetString(20));

                _comando.Connection.Close();
                _comando1.Connection.Close();
                cn1.cerrarConexion();
                cn2.cerrarConexion();
            }
            else {

                MessageBox.Show("Debe ingresar un Numero de Orden", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CrearTicket ticket = new CrearTicket();
            ticket.CortaPapel();
            ticket.ImprimirTicket("ticketera");
        }
    }
}
