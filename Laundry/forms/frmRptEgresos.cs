using CrystalDecisions.CrystalReports.Engine;
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
    public partial class frmRptEgresos : Form
    {
        public frmRptEgresos()
        {
            InitializeComponent();
        }

        private void btnSrcCliente_Click(object sender, EventArgs e)
        {
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            ReportDocument cryrep = new ReportDocument();
            MySqlDataAdapter myadap = new MySqlDataAdapter(String.Format("SELECT idegresos,idUsuario,sucursal,fechaEgreso,motivo,monto FROM egresos e  INNER JOIN usuario u ON e.idUsuario=u.id WHERE FechaEgreso BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:00'"), cnx.ObtenerConexion());
            DataSet ds = new DataSet();
            myadap.Fill(ds, "dsEgreso");
            cryrep.Load(varGlobales.rutaReportes+"\\Reportes\\crEgresos.rpt");
            cryrep.SetDataSource(ds);

            cnx.cerrarConexion();
            frmReporte rt = new frmReporte();
            rt.Text = "Reporte de Egresos";
            rt.crystalReportViewer1.ReportSource = cryrep;
            rt.Show();﻿
        }
    }
}
