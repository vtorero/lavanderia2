using CrystalDecisions.CrystalReports.Engine;
using Lavanderia.Persistencia;
using Lavanderia.util;
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

namespace Lavanderia.forms
{
    public partial class frmReporteAdmin : Form
    {
        public frmReporteAdmin()
        {
            InitializeComponent();
        }

        
        private void btnSrcCliente_Click(object sender, EventArgs e)
        {
            // If so, loop through all checked items and print results.  
            string s = "";
            for (int x = 0; x <= chKSucursal.CheckedItems.Count - 1; x++)
            {
                s +=  "'"+ chKSucursal.CheckedItems[x].ToString() + "',";
            }
            s = s.TrimEnd(',');

            if (s != "")
            {
                ConexBD cnx = new ConexBD();
                cnx.Conectar();
                ReportDocument cryrep = new ReportDocument();
                MySqlDataAdapter myadap = new MySqlDataAdapter(String.Format("(SELECT pg.idOrden,UPPER(c.nombreCliente) nombreCliente,SUBSTRING(o.fechaCreado,1,10) AS fechaCreado,pg.fechaPago,o.idUsuario, u.sucursal,pg.pagoTotal,pg.pago1 AS pago," +
" IF(tipoPago1=0,'EFECTIVO','TARJETA') modoPago,IF(o.Estado=0,'ENTREGA','RECOJO') Movimiento FROM " +
" (SELECT * FROM Pago WHERE fechaPago BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' " +
" AND pago1>0) pg INNER JOIN Orden o ON o.idOrden=pg.idOrden AND o.tipoPago IN(1) AND o.`estado`in(0,1) INNER JOIN usuario u ON o.idUsuario=u.id " +
" INNER JOIN Cliente c ON o.idCliente=c.idCliente AND u.sucursal IN(" + s + ") ORDER BY modoPago) UNION ALL " +
" (SELECT pg.idOrden,UPPER(c.nombreCliente) nombreCliente,SUBSTRING(o.fechaCreado,1,10) AS fechaCreado,pg.fechaActualizado AS fechaPago,o.idUsuario, u.sucursal,pg.pagoTotal,pg.pago1 AS pago,IF(pg.tipoPago2=0,'EFECTIVO','TARJETA') modoPago," +
" IF(o.Estado=0,'ENTREGA','RECOJO') Movimiento FROM (SELECT * FROM Pago WHERE fechaPago BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' " +
 " ) pg INNER JOIN Orden o ON o.idOrden=pg.idOrden AND o.tipoPago IN(2) AND o.`estado` IN(0,1) INNER JOIN usuario u ON o.idUsuario=u.id " +
  " INNER JOIN Cliente c ON o.idCliente=c.idCliente AND  u.sucursal IN(" + s + ") ORDER BY modoPago) " +
            "UNION ALL " +
" (SELECT pg.idOrden,UPPER(c.nombreCliente) nombreCliente,SUBSTRING(o.fechaCreado,1,10) AS fechaCreado,pg.fechaActualizado AS fechaPago,o.idUsuario, u.sucursal,pg.pagoTotal,pg.pago2 AS pago ,IF(pg.tipoPago2=0,'EFECTIVO','TARJETA') modoPago, " +
" IF(o.Estado=0,'ENTREGA','RECOJO') Movimiento FROM (SELECT * FROM Pago WHERE fechaActualizado BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' " +
 " ) pg INNER JOIN Orden o ON o.idOrden=pg.idOrden AND o.tipoPago IN(2) AND o.`estado` IN(1) INNER JOIN usuario u ON o.idUsuario=u.id " +
  " INNER JOIN Cliente c ON o.idCliente=c.idCliente AND u.sucursal IN(" + s + ")  ORDER BY modoPago) ORDER BY modopago,idOrden"), cnx.ObtenerConexion());
                DataSet ds = new DataSet();




                myadap.Fill(ds, "dsCierrePagos");


                cryrep.Load(varGlobales.rutaReportes+"\\Reportes\\crSucursales.rpt");

                cryrep.SetDataSource(ds);

                cnx.cerrarConexion();
                frmReporte rt = new frmReporte();
                rt.Text = "Reporte de Cierre";
                rt.crystalReportViewer1.ReportSource = cryrep;
                rt.Show();﻿
            }
            else {
                MessageBox.Show("Debe seleccionar al menos una sucursal","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
        }

        private void fillSusursal()
        {
            chKSucursal.Items.Clear();
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand("SELECT id, sucursal FROM usuario where id<> 1 order by sucursal", cnx.ObtenerConexion());
             MySqlDataReader _readerS = _comando.ExecuteReader();
            while (_readerS.Read())
            {
                string name = _readerS.GetString("sucursal");
                string id = _readerS.GetString("id");
                chKSucursal.Items.Add(name);
                chKSucursal.DisplayMember = name;
                chKSucursal.ValueMember = id;
            }
            cnx.cerrarConexion();

        }

        private void frmReporteAdmin_Load(object sender, EventArgs e)
        {
            fillSusursal();
        }
    }
}
