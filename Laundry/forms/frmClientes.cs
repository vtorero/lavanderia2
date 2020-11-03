using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lavanderia.Models;
using Lavanderia.Persistencia;
using Lavanderia.util;

namespace Lavanderia.forms
{
    public partial class frmClientes : Form
    {
        int pos;
        Validacion v = new Validacion();

        public frmClientes()
        
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            Cliente cliente = new Cliente();
            int resultado = 0;
            string msj = "";


            if ((!string.IsNullOrWhiteSpace(txtNombres.Text)))
            {
                cliente.Nombres= txtNombres.Text.Trim();
                cliente.DNI= txtDNI.Text.Trim();
                cliente.Email= txtEmail.Text.Trim();
                cliente.Dirección= txtDireccion.Text.Trim();
                cliente.Teléfono=txtTelefono.Text.Trim();
                cliente.usuarioCreador = varGlobales.sessionUsuario;

                if(btnGuardar.Text.Equals("&Registrar")){
                 msj="Cliente registrado con Exito!!";
                resultado = ClienteDao.Agregar(cliente);
                }
                if (btnGuardar.Text.Equals("&Actualizar"))
                {
                     msj="Cliente actualizado con Exito!!";
                    cliente.idCliente = Convert.ToInt32(txtIDcliente.Text.Trim());
                    resultado = ClienteDao.Modificar(cliente);
                
                }
            }
            else
            {
                resultado = 0;
                MessageBox.Show("Debe ingresar los valores");
            }

            if (resultado > 0)
            {

                MessageBox.Show(msj, "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvClientes.DataSource = ClienteDao.Listar();
               /// tabControl1.SelectedTab = tabPage2;
                btnGuardar.Text = "&Registrar";
                resetValores();
            }
            else
            {
                MessageBox.Show("No se pudo guardar el cliente", "Fallo!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

     

       
        private void btnEditar_Click(object sender, EventArgs e)
        {
            pos = dgvClientes.CurrentRow.Index;
            LblId.Visible = true;
            txtIDcliente.Visible = true;
            txtIDcliente.Text = Convert.ToString(dgvClientes[0, pos].Value);
            txtNombres.Text = Convert.ToString(dgvClientes[1, pos].Value);
            txtDNI.Text = Convert.ToString(dgvClientes[2, pos].Value);
            txtEmail.Text = Convert.ToString(dgvClientes[3, pos].Value);
            txtDireccion.Text = Convert.ToString(dgvClientes[4, pos].Value);
            txtTelefono.Text = Convert.ToString(dgvClientes[5, pos].Value);


            tabControl1.SelectedTab = tabPage1;
            btnGuardar.Text = "&Actualizar";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            pos = dgvClientes.CurrentRow.Index;
            string id=Convert.ToString(dgvClientes[0, pos].Value);
             
             DialogResult result = MessageBox.Show("Eliminar al cliente: " + id +"?" ,"Confirmar",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
             if (result == DialogResult.Yes)
             {
                 ClienteDao.Eliminar(Convert.ToInt32(id));
                 dgvClientes.DataSource = ClienteDao.Listar();
                 MessageBox.Show("Cliente Eliminado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

             }
           
           
        }

        private void resetValores() {
            LblId.Text= "";
            txtIDcliente.Text= "";
            LblId.Visible = false;
            txtIDcliente.Visible = false;
            txtNombres.Text = "";
            txtDNI.Text = "";
            txtEmail.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
        
        }

        

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            v.soloNumeros(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            v.soloNumeros(e);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvClientes.RowCount == 0)
                dgvClientes.DataSource = ClienteDao.Listar();
                dgvClientes.Columns[0].HeaderText = "Código";
                dgvClientes.Columns[0].Width = 75;
                dgvClientes.Columns[1].HeaderText = "Nombres";
                dgvClientes.Columns[1].Width = 180;
                dgvClientes.Columns[2].Width = 90;
                dgvClientes.Columns[3].Width = 160;
                dgvClientes.Columns[6].Visible = false;
            
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            dgvClientes.DataSource = ClienteDao.Buscar(txtcriterio.Text,"", varGlobales.sessionUsuario);
            dgvClientes.Columns[0].HeaderText = "Código";
            dgvClientes.Columns[0].Width = 75;
            dgvClientes.Columns[1].HeaderText = "Nombres";
            dgvClientes.Columns[1].Width = 180;
            dgvClientes.Columns[2].Width = 90;
            dgvClientes.Columns[3].Width = 160;
            dgvClientes.Columns[6].Visible = false;
        }

        private void dgvClientes_DoubleClick(object sender, EventArgs e)
        {
            pos = dgvClientes.CurrentRow.Index;
            LblId.Visible = true;
            txtIDcliente.Visible = true;
            txtIDcliente.Text = Convert.ToString(dgvClientes[0, pos].Value);
            txtNombres.Text = Convert.ToString(dgvClientes[1, pos].Value);
            txtDNI.Text = Convert.ToString(dgvClientes[2, pos].Value);
            txtEmail.Text = Convert.ToString(dgvClientes[3, pos].Value);
            txtDireccion.Text = Convert.ToString(dgvClientes[4, pos].Value);
            txtTelefono.Text = Convert.ToString(dgvClientes[5, pos].Value);


            tabControl1.SelectedTab = tabPage1;
            btnGuardar.Text = "&Actualizar";
        }

    }
}
