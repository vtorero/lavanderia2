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
using Lavanderia.Persistencia;
using Lavanderia.Models;

namespace Lavanderia.forms
{
    public partial class frmInicio : Form
    {
        //private int childFormNumber = 0;

        public frmInicio()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new frmPrendas();
            childForm.MdiParent = this;
            childForm.Text = "Mantenimiento de Prendas ";
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            Form childForm = new frmServicio();
            childForm.MdiParent = this;
            childForm.Text = "Mantenimiento de Servicios";
            childForm.Show();
            
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Form childForm = new frmClientes();
            childForm.MdiParent = this;
            childForm.Text = "Mantenimiento de Clientes";
            childForm.Show();
        
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

      

    

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void fileMenu_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new frmColor();
            childForm.MdiParent = this;
            childForm.Text = "Mantenimiento de Colores";
            childForm.Show();
        }

    

        private void prendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportDocument cryrep = new ReportDocument();
            MySqlDataAdapter myadap = new MySqlDataAdapter(String.Format(
         "SELECT idPrenda, NombrePrenda, DescripcionPrenda, PrecioServicio FROM Prenda"), BdComun.ObtenerConexion());
            DataSet ds = new DataSet();
         
            myadap.Fill(ds, "Prendas");

            cryrep.Load(@"D:\lavanderia\Laundry\Reportes\crPrendas.rpt");

            cryrep.SetDataSource(ds);
            
            frmReporte rt = new frmReporte();
            rt.Text = "Reporte de prendas";
            rt.crystalReportViewer1.ReportSource = cryrep;
            rt.Show();﻿
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            frmOrden childForm = new frmOrden();
            childForm.MdiParent = this;
            childForm.Text = "Orden de Servicio";
            childForm.toolStripStatusLabel1.Text = this.searchToolStripMenuItem.Text;
            childForm.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form childForm = new frmEntregas();
            childForm.MdiParent = this;
            childForm.Text = "Entrega de Ordenes";
            childForm.Show();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new frmReporteAdmin();
            childForm.MdiParent = this;
            childForm.Text = "Reporte de ingresos";
            childForm.Show();﻿
        }

     
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void imprimirTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmImprimir childForm = new frmImprimir();
            childForm.MdiParent = this;
            childForm.Text = "Imprimir Ticket";
            childForm.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form childForm = new frmClientes();
            childForm.MdiParent = this;
            childForm.Text = "Mantenimiento de Clientes";
            childForm.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form childForm = new frmFinalizados();
            childForm.MdiParent = this;
            childForm.Text = " Ordenes finalizadas";
            childForm.Show();
        }

        private void cierreCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportDocument cryrep = new ReportDocument();
            MySqlDataAdapter myadap = new MySqlDataAdapter(String.Format(
         "SELECT al.idOrden,al.fechaCreado,if(al.tipoServicio=1, 'Al seco','Al Peso') tipo,al.pago1,al.pago2,(al.precio*al.cantidad) total from (SELECT l.tipoServicio,o.idOrden,o.fechaCreado,l.precio,l.cantidad,total,p.pago1,p.pago2 FROM Orden o inner join OrdenLinea l on o.idOrden=l.idOrden inner join Pago p on o.idOrden=p.idOrden where p.tipoPago in(1,2) and tipoPago1 in(0,1) and tipoPago2 in (0,1)) al where al.tipoServicio in(1,2);"), BdComun.ObtenerConexion());
            DataSet ds = new DataSet();



            myadap.Fill(ds, "sdAlseco");
           

            cryrep.Load(@"D:\lavanderia\Laundry\Reportes\crCierre.rpt");

            cryrep.SetDataSource(ds);
           

            frmReporte rt = new frmReporte();
            rt.Text = "Reporte de Cierre al Seco";
            rt.crystalReportViewer1.ReportSource = cryrep;
            rt.Show();﻿
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Form childForm = new frmCierre();
            childForm.MdiParent = this;
            childForm.Text = "Reporte de Cierre";
            childForm.Show();
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
           
            //label1.Text = Convert.ToString(OrdenDao.consultaPendientes(varGlobales.sessionUsuario)) + " Ordenes para entrega hoy ";
            this.btn_pendientes.Text = Convert.ToString(OrdenDao.consultaPendientes(varGlobales.sessionUsuario)) + " Ordenes para entrega hoy ";
                      
            }

        private void detalleOrdenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new frmReporteDetalle();
            childForm.MdiParent = this;
            childForm.Text = "Detalle de Ordenes";
            childForm.Show();﻿
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportDocument cryrep = new ReportDocument();
            MySqlDataAdapter myadap = new MySqlDataAdapter(String.Format(
         "SELECT upper(nombreCliente) nombreCliente,dniCliente,direccionCliente,correoCliente,telefonoCliente,u.sucursal FROM Cliente c inner join usuario u on c.usuarioCreador=u.id order by sucursal,nombreCliente"), BdComun.ObtenerConexion());
            DataSet ds = new DataSet();

            myadap.Fill(ds, "Clientes");

            cryrep.Load(varGlobales.rutaReportes + "\\Reportes\\crClientes.rpt");

            cryrep.SetDataSource(ds);

            frmReporte rt = new frmReporte();
            rt.Text = "Reporte de Clientes";
            rt.crystalReportViewer1.ReportSource = cryrep;
            rt.Show();﻿
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new frmInventario();
            childForm.MdiParent = this;
            childForm.Text = "Reporte de inventarios";
            childForm.Show();

        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Form childForm = new frmEgresos();
          //  childForm.MdiParent = this;
            childForm.Text = "Registro de egresos";
            childForm.ShowDialog();

        }

        private void egresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new frmRptEgresos();
            childForm.MdiParent = this;
            childForm.Text = "Reporte de Egresos";
            childForm.Show();﻿
        }

        private void historicoClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new frmClientesHistorico();
            childForm.MdiParent = this;
            childForm.Text = "Historico Clientes";
            childForm.Show();﻿
        }

        private void btn_pendientes_Click(object sender, EventArgs e)
        {
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            ReportDocument cryrep = new ReportDocument();
            MySqlDataAdapter myadap = new MySqlDataAdapter(String.Format("SELECT idOrden,fechaEntrega,nombreCliente,totalOrden FROM Orden o,Cliente c WHERE o.idCliente=c.idCliente and  SUBSTRING(o.fechaEntrega,1,10)=SUBSTRING(NOW(),1,10) AND o.estado=0 AND o.idUsuario="+varGlobales.sessionUsuario+""), cnx.ObtenerConexion());
            DataSet ds = new DataSet();
            myadap.Fill(ds, "pendientes");
            cryrep.Load(varGlobales.rutaReportes + "\\Reportes\\crPendientes.rpt");
            cryrep.SetDataSource(ds);

            cnx.cerrarConexion();
            frmReporte rt = new frmReporte();
            rt.Text = "Reporte de Entregas";
            rt.crystalReportViewer1.ReportSource = cryrep;
            rt.Show();﻿
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Form childForm = new AnularForm();
            //childForm.MdiParent = this;
            childForm.ShowDialog();
        }

     
   
    }
}
