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
using Lavanderia.util;

namespace Lavanderia.forms.busquedas
{
    public partial class frmBuscarCliente : Form
  {
        public delegate void enviar(string id,string nombre,string dni,string telefono);
        public event enviar enviado;
        public frmBuscarCliente()
        {
            InitializeComponent();
        }
     
     
        private void dgvClientes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            enviaDatos();
            
        }

        private void enviaDatos() {
            int pos = 0;
            

            Int32 selectedRowCount =
       dgvClientes.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount > 0)
            {
                pos = dgvClientes.CurrentRow.Index;
                enviado(Convert.ToString(dgvClientes[0, pos].Value), Convert.ToString(dgvClientes[1, pos].Value), Convert.ToString(dgvClientes[2, pos].Value), Convert.ToString(dgvClientes[5, pos].Value));
                this.Close();
            }
            else {
                MessageBox.Show("Debe seleccionar un cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            
            }
        
        
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            int pos = 0;
            
            
            Int32 selectedRowCount =
       dgvClientes.Rows.GetRowCount(DataGridViewElementStates.Selected);
           
            if (selectedRowCount > 0)
                    
            {
                pos = dgvClientes.CurrentRow.Index;               
                enviado(Convert.ToString(dgvClientes[0, pos].Value), Convert.ToString(dgvClientes[1, pos].Value), Convert.ToString(dgvClientes[2, pos].Value), Convert.ToString(dgvClientes[5, pos].Value));
                
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            dgvClientes.DataSource = ClienteDao.Buscar(txtNombres.Text, txtDni.Text,varGlobales.sessionUsuario);
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.Columns[0].HeaderText = "Código";
            dgvClientes.Columns[0].Width = 50;
            dgvClientes.Columns[0].Visible= false;
            dgvClientes.Columns[1].HeaderText = "Nombres";
            dgvClientes.Columns[1].Width = 200;
            dgvClientes.Columns[2].Width = 70;
           
            dgvClientes.Columns[3].Visible = false;
            dgvClientes.Columns[4].Visible = false;
          dgvClientes.Columns[6].Visible = false;
        }

        private void frmBuscarCliente_Load(object sender, EventArgs e)
        {

            dgvClientes.DataSource= ClienteDao.Listar();
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.Columns[0].HeaderText = "Código";
            dgvClientes.Columns[0].Width = 50;
            dgvClientes.Columns[0].Visible = false;
            dgvClientes.Columns[1].HeaderText = "Nombres";
            dgvClientes.Columns[1].Width = 200;
            dgvClientes.Columns[2].Width = 70;
            dgvClientes.Columns[3].Visible = false;
            dgvClientes.Columns[4].Visible = false;
            dgvClientes.Columns[6].Visible = false;
            dgvClientes.ClearSelection();
        }

       

  

      

    }
}
