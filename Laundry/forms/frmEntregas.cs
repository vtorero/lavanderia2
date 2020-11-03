using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lavanderia.forms.busquedas;
using Lavanderia.Persistencia;
using Lavanderia.util;

namespace Lavanderia.forms
{
    public partial class frmEntregas : Form
    {
        int pos;
        public frmEntregas()
        {
            InitializeComponent();
        }

        private void btnSrcCliente_Click(object sender, EventArgs e)
        {
            if(txtNumero.Text.Equals("")) {
            llenarDatos();
            llenarDetalles(0);
            }else{
            
                llenarDatosId(Convert.ToInt32(txtNumero.Text));
                llenarDetalles(0);
            }
        }

        public void ejecutar(string id, string nombre, string dni, string telefono)
        {
            txtCliente.Text = nombre;
            txtDni.Text = dni;
            txtIdCliente.Text = id;

        }

        private void llenarDatosId(int id) { 
           
           dgvOrdenes.DataSource = OrdenDao.buscarOrdenId(id);
           dgvOrdenes.Columns[0].HeaderText = "Código";
           dgvOrdenes.Columns[0].Width = 50;
           dgvOrdenes.Columns[1].Visible = false;
           dgvOrdenes.Columns[2].HeaderText = "Nombre cliente";
           dgvOrdenes.Columns[2].Width = 200;
           dgvOrdenes.Columns[3].Visible = false;
           dgvOrdenes.Columns[4].HeaderText = "Sucursal";
           dgvOrdenes.Columns[4].Width = 100;
           dgvOrdenes.Columns[5].HeaderText = "Fecha Orden";
           dgvOrdenes.Columns[5].Width = 120;
           dgvOrdenes.Columns[6].HeaderText = "Fecha Entrega";
           dgvOrdenes.Columns[6].Width = 120;
           dgvOrdenes.Columns[7].HeaderText = "Pago 1";
           dgvOrdenes.Columns[7].DefaultCellStyle.Format = "C2";
           dgvOrdenes.Columns[7].Width = 70;
           dgvOrdenes.Columns[8].HeaderText = "Pago 2";
           dgvOrdenes.Columns[8].DefaultCellStyle.Format = "C2";
           dgvOrdenes.Columns[8].Width = 70;
           dgvOrdenes.Columns[9].HeaderText = "Monto Orden";
           dgvOrdenes.Columns[9].DefaultCellStyle.Format = "C2";
           dgvOrdenes.Columns[9].Width = 100;
           dgvOrdenes.Columns[10].HeaderText = "Monto Pendiente";
           dgvOrdenes.Columns[10].DefaultCellStyle.Format = "C2";
           dgvOrdenes.Columns[10].Width = 100;
           dgvOrdenes.Columns[11].HeaderText = "Cuotas";
        
        }


        private void llenarDatos() {
            dgvOrdenes.DataSource = OrdenDao.buscarOrden("%" + txtCliente.Text + "%", txtDni.Text, dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00", dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59",0);
            dgvOrdenes.Columns[0].HeaderText = "Código";
            dgvOrdenes.Columns[0].Width = 50;
            dgvOrdenes.Columns[1].Visible = false;
            dgvOrdenes.Columns[2].HeaderText = "Nombre cliente";
            dgvOrdenes.Columns[2].Width = 200;
            dgvOrdenes.Columns[3].Visible = false;
            dgvOrdenes.Columns[4].HeaderText = "Sucursal";
            dgvOrdenes.Columns[4].Width = 100;
            dgvOrdenes.Columns[5].HeaderText = "Fecha Orden";
            dgvOrdenes.Columns[5].Width = 120;
            dgvOrdenes.Columns[6].HeaderText = "Fecha Entrega";
            dgvOrdenes.Columns[6].Width = 120;
            dgvOrdenes.Columns[7].HeaderText = "Pago 1";
            dgvOrdenes.Columns[7].DefaultCellStyle.Format = "C2";
            dgvOrdenes.Columns[7].Width = 70;
            dgvOrdenes.Columns[8].HeaderText = "Pago 2";
            dgvOrdenes.Columns[8].DefaultCellStyle.Format = "C2";
            dgvOrdenes.Columns[8].Width = 70;
            dgvOrdenes.Columns[9].HeaderText = "Monto Orden";
            dgvOrdenes.Columns[9].DefaultCellStyle.Format = "C2";
            dgvOrdenes.Columns[9].Width = 100;
            dgvOrdenes.Columns[10].HeaderText = "Monto Pendiente";
            dgvOrdenes.Columns[10].DefaultCellStyle.Format = "C2";
            dgvOrdenes.Columns[10].Width = 100;
            dgvOrdenes.Columns[11].HeaderText = "Cuotas";
            
        }

        private void llenarDetalles(int id)
        {
            dgvDetalles.DataSource = OrdenDao.consultarOrden(id);
            dgvDetalles.Columns[0].HeaderText = "Código";
              dgvDetalles.Columns[2].Visible = false;
              dgvDetalles.Columns[5].DefaultCellStyle.Format = "C2";
              dgvDetalles.Columns[9].DefaultCellStyle.Format = "C2";
              dgvDetalles.Columns[10].Visible = false;
              dgvDetalles.Columns[11].Visible = false;
              
              /*dgvDetalles.Columns[2].HeaderText = "Nombre cliente";
              dgvDetalles.Columns[2].Width = 250;
              dgvDetalles.Columns[3].HeaderText = "DNI";
              dgvDetalles.Columns[3].Visible = false;
              dgvDetalles.Columns[4].HeaderText = "Fecha Orden";
              dgvDetalles.Columns[4].Width = 200;
              dgvDetalles.Columns[5].HeaderText = "Monto Orden";*/
            
        }

        private void btnAddPrenda_Click(object sender, EventArgs e)
        {
            frmBuscarCliente childForm = new frmBuscarCliente();
            childForm.enviado += new frmBuscarCliente.enviar(ejecutar);
            childForm.ShowDialog();
        }

        private void cargaItems() {
            pos = dgvOrdenes.CurrentRow.Index;

            txtMonto.Text = Convert.ToString(dgvOrdenes[9, pos].Value);
            if (Convert.ToInt32(dgvOrdenes[11, pos].Value) == 2)
            {
                txtCodigo.Text = Convert.ToString(dgvOrdenes[0, pos].Value);
                btnEntregar.Enabled = true;
                txtDebe.Visible = true;
                lblDebe.Visible = true;
                lblsimdebe.Visible = true;
                txtDebe.Text = Convert.ToString(dgvOrdenes[8, pos].Value);
                chkVisa.Visible = true;
                llenarDetalles(Convert.ToInt32(dgvOrdenes[0, pos].Value));
            }
            else
            {
                txtCodigo.Text = Convert.ToString(dgvOrdenes[0, pos].Value);
                txtDebe.Visible = false;
                lblDebe.Visible = false;
                btnEntregar.Enabled = true;
                lblsimdebe.Visible = false;
                txtDebe.Text = Convert.ToString(0);
                chkVisa.Visible = false;
                llenarDetalles(Convert.ToInt32(dgvOrdenes[0, pos].Value));
            }

        
        }

        private void dgvOrdenes_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            cargaItems();

        }

        private void btnEntregar_Click(object sender, EventArgs e)
        {
            int pago2=0;
            int v_delivery;
            v_delivery = 1;
            if (chkDelivery.Checked) { }
            if (chkVisa.Checked)
                pago2 = 1;
            int re = 0;

            re= OrdenDao.entregaOrden(Convert.ToInt32(txtCodigo.Text),pago2,txtObs.Text,v_delivery);
            llenarDatos();
            llenarDetalles(0);
            chkVisa.Visible = false;
            txtMonto.Text = "0.00";
            txtDebe.Text = "0.00";
            txtDebe.Visible = false;
            lblDebe.Visible = false;
            lblsimdebe.Visible = false;
            MessageBox.Show("Orden Nro: " + txtCodigo.Text + " actualizada correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnEntregar.Enabled = false;
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {

                if (txtNumero.Text.Equals(""))
                {
                    llenarDatos();
                }
                else
                {
                
                    try
                        {
                            int result = int.Parse(txtNumero.Text);
                            llenarDatosId(Convert.ToInt32(txtNumero.Text));
                        }
                        catch
                        {
                         MessageBox.Show("No es un código válido","Error",MessageBoxButtons.OK, MessageBoxIcon.Information);
}
                        }


                    llenarDatosId(Convert.ToInt32(txtNumero.Text));
                }
            
            }
        

        private void dgvOrdenes_MouseClick(object sender, MouseEventArgs e)
        {
            cargaItems();
        }

       }
    }
    
