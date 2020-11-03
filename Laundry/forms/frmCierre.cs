using CrystalDecisions.CrystalReports.Engine;
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
    public partial class frmCierre : Form
    {
        public frmCierre()
        {
            InitializeComponent();
        }

        private void btnSrcCliente_Click(object sender, EventArgs e)
        {
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
ReportDocument cryrep = new ReportDocument();


MySqlDataAdapter myadap = new MySqlDataAdapter(String.Format("(SELECT pg.idOrden,UPPER(c.nombreCliente) nombreCliente,SUBSTRING(o.fechaCreado,1,10) AS fechaCreado,pg.fechaPago,o.idUsuario,IF(delivery = 1, 'SI', 'NO') as delivery, u.sucursal,pg.pagoTotal,pg.pago1 AS pago," +
"(SELECT IFNULL(SUM(monto),0) FROM egresos  WHERE fechaEgreso BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND idUsuario=" + varGlobales.sessionUsuario + ") egreso, " +
" IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,IF(o.Estado=0,'Entrega','Recojo') Movimiento FROM " +
" (SELECT * FROM Pago WHERE fechaPago BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 23:59:59' " +
" AND pago1>0) pg INNER JOIN Orden o ON o.idOrden=pg.idOrden AND o.tipoPago IN(1) AND o.`estado` in(0,1) INNER JOIN usuario u ON o.idUsuario=u.id " +
" INNER JOIN Cliente c ON o.idCliente=c.idCliente AND u.id="+varGlobales.sessionUsuario+" ORDER BY modoPago) UNION ALL " +
" (SELECT pg.idOrden,UPPER(c.nombreCliente) nombreCliente,SUBSTRING(o.fechaCreado,1,10) AS fechaCreado,pg.fechaActualizado AS fechaPago,o.idUsuario,IF(delivery = 1, 'SI', 'NO') as delivery, u.sucursal,pg.pagoTotal,pg.pago1 AS pago, " +
"(SELECT IFNULL(SUM(monto),0) FROM egresos  WHERE fechaEgreso BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND idUsuario=" + varGlobales.sessionUsuario + ") egreso, " +
            " IF(pg.tipoPago1=0,'Efectivo','Tarjeta') modoPago," +
" IF(o.Estado=0,'Entrega','Recojo') Movimiento FROM (SELECT * FROM Pago WHERE fechaPago BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 23:59:59' " +
 " ) pg INNER JOIN Orden o ON o.idOrden=pg.idOrden AND o.tipoPago IN(2) AND o.`estado` IN(0,1) INNER JOIN usuario u ON o.idUsuario=u.id " +
  " INNER JOIN Cliente c ON o.idCliente=c.idCliente AND u.id="+varGlobales.sessionUsuario+" ORDER BY modoPago) " +
            "UNION ALL " +
" (SELECT pg.idOrden,UPPER(c.nombreCliente) nombreCliente,SUBSTRING(o.fechaCreado,1,10) AS fechaCreado,pg.fechaActualizado AS fechaPago,o.idUsuario,IF(delivery = 1, 'SI', 'NO') as delivery,u.sucursal,pg.pagoTotal, pg.pago2 AS pago ," +
"(SELECT IFNULL(SUM(monto),0) FROM egresos  WHERE fechaEgreso BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND idUsuario=" + varGlobales.sessionUsuario + ") egreso, " +
" IF(pg.tipoPago2=0,'Efectivo','Tarjeta') modoPago, " +
" IF(o.Estado=0,'Entrega','Recojo') Movimiento FROM (SELECT * FROM Pago WHERE fechaActualizado BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 23:59:59' " +
 " ) pg INNER JOIN Orden o ON o.idOrden=pg.idOrden AND o.tipoPago IN(2) AND o.`estado` IN(1) INNER JOIN usuario u ON o.idUsuario=u.id " +
  " INNER JOIN Cliente c ON o.idCliente=c.idCliente AND u.id=" + varGlobales.sessionUsuario + " ORDER BY modoPago) ORDER BY modopago,idOrden"), cnx.ObtenerConexion());

           
        DataSet ds = new DataSet();
            myadap.Fill(ds,"dsCierrePagos");
            cryrep.Load(varGlobales.rutaReportes+"\\Reportes\\cierreDiario.rpt");
            cryrep.SetDataSource(ds);

            cnx.cerrarConexion();
            frmReporte rt = new frmReporte();
            rt.Text = "Reporte de Cierre";
            rt.crystalReportViewer1.ReportSource = cryrep;
            rt.Show();﻿

        }

        private void dtFechaFin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dtFechaInicial_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblFecha_Click(object sender, EventArgs e)
        {

        }

    
    }
}
