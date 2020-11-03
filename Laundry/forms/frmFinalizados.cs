using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lavanderia.Persistencia;
using Lavanderia.Models;
using MySql.Data.MySqlClient;
using Lavanderia.util;
using Lavanderia.forms.busquedas;

namespace Lavanderia.forms
{
    public partial class frmFinalizados : Form
    {
        int pos = 0;
        public frmFinalizados()
        {
            InitializeComponent();
           
        }

        private void btnSrcCliente_Click(object sender, EventArgs e)
        {
            if (txtNumero.Text.Equals(""))
            {
                llenarDatos();
                llenarDetalles(0);
            }
            else
            {

                llenarDatosId(Convert.ToInt32(txtNumero.Text));
                llenarDetalles(0);
            }
            
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

          

        }

        private void llenarDatos()
        {
            dgvOrdenes.DataSource = OrdenDao.buscarOrden("%" + txtCliente.Text + "%", txtDni.Text, dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00", dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59", 1);
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
            //dgvOrdenes.Columns[11].Visible = false;
            dgvOrdenes.Columns[11].HeaderText = "Cuotas";
           
       
        
        }

        private void llenarDatosId(int id)
        {

            dgvOrdenes.DataSource = OrdenDao.buscarOrdenIdFin(id);
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
            //dgvOrdenes.Columns[11].Visible = false;
            dgvOrdenes.Columns[11].HeaderText = "Cuotas";

        }

        private void dgvOrdenes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pos = dgvOrdenes.CurrentRow.Index;
            llenarDetalles(Convert.ToInt32(dgvOrdenes[0, pos].Value));
            llenaPago(Convert.ToInt32(dgvOrdenes[0, pos].Value));
        }

        private void llenaPago(int id) {

            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand("consultaPago", cnx.ObtenerConexion());
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.Add(new MySqlParameter("id", id));
            MySqlDataReader _reader = _comando.ExecuteReader(CommandBehavior.CloseConnection);

            while (_reader.Read())
            {

                string idPago = _reader.GetString("idPago");
                string tipopago = _reader.GetString("tipoPago");
                string tipopago1= _reader.GetString("tipoPago1");
                string tipopago2 = _reader.GetString("tipoPago2");
                txtIdPago.Text = Convert.ToString(idPago);

                //if (varGlobales.sessionUsuario.ToString().Equals("1")) {
                    btnCambiaModo.Enabled = true;
                    rdpago1E.Enabled = true;
                    rdpago1T.Enabled = true;
                    rdpago2E.Enabled = true;
                    rdpago2T.Enabled = true;
                
                //}

                if (tipopago.Equals("2"))
                {
                    grpPago2.Visible = true;
                    rdpago2E.Visible = true;
                    rdpago2T.Visible = true;
                }
                else {

                    grpPago2.Visible = false;
                    rdpago2E.Visible = false;
                    rdpago2T.Visible = false;
                }

                /*pago 1*/
                if(tipopago1.Equals("0")){
                rdpago1E.Checked=true;
                }if (tipopago1.Equals("1"))
                {
                    rdpago1T.Checked = true;
                }

                /*pago 2*/
                if (tipopago2.Equals("0"))
                {
                    rdpago2E.Checked = true;
                } 
                if (tipopago2.Equals("1"))
                {
                    rdpago2T.Checked = true;
                }
                
            }

            cnx.cerrarConexion();


        }

        private void btnCambiaModo_Click(object sender, EventArgs e)
        {
        int pago1 = (rdpago1E.Checked ==true) ? 0: 1 ;
        int pago2 = (rdpago2E.Checked == true) ? 0 : 1;

          PagoDao.modificaPago(Convert.ToInt32(txtIdPago.Text), pago1,pago2);

          MessageBox.Show("Cambio de tipo de Pago", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtIdPago_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                if (txtNumero.Text.Equals(""))
                {
            try
            {
        
            llenarDatos();
            }
        catch
        {
            MessageBox.Show("formato de Código inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

      
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

            }
        }

        private void dgvOrdenes_MouseClick(object sender, MouseEventArgs e)
        {
            pos = dgvOrdenes.CurrentRow.Index;
            llenarDetalles(Convert.ToInt32(dgvOrdenes[0, pos].Value));
            llenaPago(Convert.ToInt32(dgvOrdenes[0, pos].Value));
        }
        public void ejecutar(string id, string nombre, string dni, string telefono)
        {
            txtCliente.Text = nombre;
            txtDni.Text = dni;
            txtIdCliente.Text = id;

        }
        private void btnAddPrenda_Click(object sender, EventArgs e)
        {
            frmBuscarCliente childForm = new frmBuscarCliente();
            childForm.enviado += new frmBuscarCliente.enviar(ejecutar);
            childForm.ShowDialog();
        }




     
    }
}