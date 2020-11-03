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

namespace Lavanderia.forms
{
    public partial class frmInventario : Form
    {
        public frmInventario()
        {
            InitializeComponent();
        }

        private void dtFechaInicial_ValueChanged(object sender, EventArgs e)
        {

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

        private void frmInventario_Load(object sender, EventArgs e)
        {
            fillSusursal();
        }

        private void btnSrcCliente_Click(object sender, EventArgs e)
        {
            string s = "";
            for (int x = 0; x <= chKSucursal.CheckedItems.Count - 1; x++)
            {
                s += "'" + chKSucursal.CheckedItems[x].ToString() + "',";
            }
            s = s.TrimEnd(',');

            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            ReportDocument cryrep = new ReportDocument();
            MySqlDataAdapter myadap = new MySqlDataAdapter(String.Format(
         "SELECT o.idOrden,UPPER(nombreCliente) nombreCliente, SUBSTRING(fechaCreado,1,10) fechaCreado,SUBSTRING(fechaEntrega,1,10) fechaEntrega,u.sucursal,totalOrden,pago1,o.aplicaDscto,DATEDIFF(NOW(),o.fechaEntrega) diasAtraso,IF(o.`estado`=0, 'En tienda','Entregado') estado, (SELECT IF(tipoServicio=1, 'Al Seco','Al Agua') from OrdenLinea where idOrden=o.idOrden Limit 1) tipo FROM Orden o INNER JOIN Cliente c ON o.idCliente=c.idCliente INNER JOIN Pago p ON o.idOrden=p.idOrden INNER JOIN usuario u ON u.id=o.idUsuario AND u.sucursal IN(" + s + ") WHERE o.estado=0 AND u.id<>1 AND o.fechaEntrega BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' " + "order by o.idOrden DESC"), cnx.ObtenerConexion());
            DataSet ds = new DataSet();

            myadap.Fill(ds, "dsInventario");
            cnx.cerrarConexion();
            cryrep.Load(@"D:\lavanderia\Laundry\Reportes\crInventario.rpt");

            cryrep.SetDataSource(ds);

            frmReporte rt = new frmReporte();
            rt.Text = "Reporte de Inventario";
            rt.crystalReportViewer1.ReportSource = cryrep;
            rt.Show();﻿﻿
        }
    }
}
