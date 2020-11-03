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
    public partial class frmServicio : Form
    {
        public frmServicio()
        {
            InitializeComponent();
            if (varGlobales.sessionUsuario == 1)
            {
                button1.Enabled = true;

            }
            else
            {
                button1.Enabled = false;
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Servicio servicio = new Servicio();
            int resultado = 0;
            string msj = "";


            if ((!string.IsNullOrWhiteSpace(txtNombre.Text))
                && (!string.IsNullOrWhiteSpace(txtPrecio.Text))
                )
            {
                servicio.NombreServicio = txtNombre.Text.Trim();
                servicio.precioServicio = float.Parse(txtPrecio.Text);

                if (button1.Text.Equals("&Registrar"))
                {
                    msj = "Servicio registrado con éxito!!";
                    resultado = ServicioDao.Agregar(servicio);
                }
                if (button1.Text.Equals("&Actualizar"))
                {
                    msj = "Servicio actualizado con éxito!!";
                    servicio.idServicio = Convert.ToInt32(txtCodigo.Text.Trim());
                    resultado = ServicioDao.Modificar(servicio);

                }

            }
            else
            {
                resultado = 0;
                MessageBox.Show("Debe ingresar los valores");
            }

            if (resultado > 0)
            {
                dgvServicios.DataSource = ServicioDao.Buscar();
                dgvServicios.Columns[0].HeaderText = "Código";
                dgvServicios.Columns[0].Width = 60;
                dgvServicios.Columns[1].HeaderText = "Nombre";
                dgvServicios.Columns[1].Width = 230;
                dgvServicios.Columns[2].HeaderText = "Precio";
                dgvServicios.Columns[2].DefaultCellStyle.Format = "C2";
                tabControl1.SelectedTab = tabPage2;
                button1.Text = "&Registrar";
                resetValores();

                MessageBox.Show(msj, "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo guardar el servicio", "Fallo!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void resetValores()
        {
            lblCodigo.Text = "";
            txtCodigo.Text = "";
            lblCodigo.Visible = false;
            txtCodigo.Visible = false;
            txtNombre.Text = "";
            txtPrecio.Text = "";

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int pos;
            pos = dgvServicios.CurrentRow.Index;
            lblCodigo.Visible = true;
            txtCodigo.Visible = true;
            txtCodigo.Text = Convert.ToString(dgvServicios[0, pos].Value);
            txtNombre.Text = Convert.ToString(dgvServicios[1, pos].Value);
            txtPrecio.Text = Convert.ToString(dgvServicios[2, pos].Value);
            tabControl1.SelectedTab = tabPage1;
            button1.Text = "&Actualizar";
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvServicios.RowCount == 0)
            {
                dgvServicios.DataSource = ServicioDao.Buscar();
                dgvServicios.Columns[0].HeaderText = "Código";
                dgvServicios.Columns[0].Width = 60;
                dgvServicios.Columns[1].HeaderText = "Nombre";
                dgvServicios.Columns[1].Width = 230;
                dgvServicios.Columns[2].DefaultCellStyle.Format = "C2";
                dgvServicios.Columns[2].HeaderText = "Precio";
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int pos;
            pos = dgvServicios.CurrentRow.Index;
            string id = Convert.ToString(dgvServicios[0, pos].Value);

            DialogResult result = MessageBox.Show("Eliminar el servicio: " + id, "Confirmar", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ServicioDao.Eliminar(Convert.ToInt32(id));
                dgvServicios.DataSource = ServicioDao.Buscar();
                dgvServicios.Columns[0].HeaderText = "Código";
                dgvServicios.Columns[0].Width = 60;
                dgvServicios.Columns[1].HeaderText = "Nombre";
                dgvServicios.Columns[1].Width = 230;
                dgvServicios.Columns[2].HeaderText = "Precio";
                dgvServicios.Columns[2].DefaultCellStyle.Format = "C2";
                MessageBox.Show("Servicio eliminado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
