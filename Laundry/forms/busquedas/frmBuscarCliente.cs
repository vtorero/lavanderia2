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
            int pos; 
            pos = dgvClientes.CurrentRow.Index;
            enviado(Convert.ToString(dgvClientes[0, pos].Value), Convert.ToString(dgvClientes[1, pos].Value), Convert.ToString(dgvClientes[2, pos].Value), Convert.ToString(dgvClientes[5, pos].Value));
            /*txtNombres.Text = Convert.ToString(dgvClientes[1, pos].Value);
            txtDNI.Text = Convert.ToString(dgvClientes[2, pos].Value);*/
            this.Close();
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            int pos;
            pos = dgvClientes.CurrentRow.Index;
            enviado(Convert.ToString(dgvClientes[0, pos].Value), Convert.ToString(dgvClientes[1, pos].Value), Convert.ToString(dgvClientes[2, pos].Value), Convert.ToString(dgvClientes[5, pos].Value));
            /*txtNombres.Text = Convert.ToString(dgvClientes[1, pos].Value);
            txtDNI.Text = Convert.ToString(dgvClientes[2, pos].Value);*/
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            dgvClientes.DataSource = ClienteDao.Buscar(txtNombres.Text, txtDni.Text);
            dgvClientes.Columns[0].HeaderText = "Código";
            dgvClientes.Columns[0].Width = 50;
            dgvClientes.Columns[1].HeaderText = "Nombres";
            dgvClientes.Columns[1].Width = 100;
            dgvClientes.Columns[2].Width = 80;
            dgvClientes.Columns[3].Visible = false;
            dgvClientes.Columns[4].Visible = false;
        }

  

      

    }
}
