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
    public partial class frmBuscarServicio : Form
    {
        public delegate void enviar(string id, string nombre, decimal precio,string tipo,string tipooferta);
        public event enviar enviado;
        public frmBuscarServicio()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos() {

            dgvServicios.DataSource = ServicioDao.Buscar(txtNombres.Text);
            dgvServicios.RowHeadersVisible = false;
            dgvServicios.Columns[0].HeaderText = "Código";
            dgvServicios.Columns[0].Width = 50;
            dgvServicios.Columns[0].Visible = false;
            dgvServicios.Columns[1].HeaderText = "Nombres";
            dgvServicios.Columns[1].Width = 190;
            dgvServicios.Columns[2].HeaderText = "Precio";
            dgvServicios.Columns[2].DefaultCellStyle.Format = "C2";
            dgvServicios.Columns[2].Width = 100;
            dgvServicios.Columns[3].Width = 100;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            int pos;
            pos = dgvServicios.CurrentRow.Index;
            enviado(Convert.ToString(dgvServicios[0,pos].Value), Convert.ToString(dgvServicios[1,pos].Value), Convert.ToDecimal(dgvServicios[2,pos].Value), Convert.ToString(dgvServicios[3,pos].Value),Convert.ToString(dgvServicios[4,pos].Value));
            this.Close();
        }

        private void frmBuscarServicio_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void dgvServicios_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int pos;
            pos = dgvServicios.CurrentRow.Index;
            enviado(Convert.ToString(dgvServicios[0, pos].Value), Convert.ToString(dgvServicios[1, pos].Value), Convert.ToDecimal(dgvServicios[2, pos].Value), Convert.ToString(dgvServicios[3, pos].Value), Convert.ToString(dgvServicios[4, pos].Value));
            this.Close();
        }
    }
}
