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


namespace Lavanderia.forms
{
    public partial class frmColor : Form
    {
        int pos;
        public frmColor()
        {
            
            InitializeComponent();
        }

     

           

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (dgvColores.RowCount == 0)
            {
                dgvColores.DataSource = ColorDao.Listar();
                dgvColores.Columns[0].HeaderText= "Código";
                dgvColores.Columns[0].Width = 100;
                dgvColores.Columns[1].HeaderText = "Nombre Color";
                dgvColores.Columns[1].Width = 150;
                dgvColores.Columns[2].HeaderText = "Valor";
                dgvColores.Columns[2].Width = 150;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            pos = dgvColores.CurrentRow.Index;
            lblCodigo.Visible = true;
            txtCodigo.Visible = true;
            txtCodigo.Text = Convert.ToString(dgvColores[0, pos].Value);
            txtNombreColor.Text = Convert.ToString(dgvColores[1, pos].Value);
            txtValorColor.Text = Convert.ToString(dgvColores[2, pos].Value);
            lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(dgvColores[2,pos].Value));
            
            tabControl1.SelectedTab = tabPage1;
            btnGuardar.Text = "&Actualizar";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                txtValorColor.Text = colorDialog1.Color.Name;
                if (colorDialog1.Color.IsKnownColor)
                {
                    lblColor.BackColor = colorDialog1.Color;
                    txtValorColor.Text = colorDialog1.Color.Name;
                }
                else {
                    lblColor.BackColor = colorDialog1.Color;
                    txtValorColor.Text = "#"+colorDialog1.Color.Name;
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Lavanderia.Models.Color color = new Lavanderia.Models.Color();
            int resultado = 0;
            string msj = "";

            if ((!string.IsNullOrWhiteSpace(txtNombreColor.Text)) && (!string.IsNullOrWhiteSpace(txtValorColor.Text)))
            {
                color.nombreColor = txtNombreColor.Text.Trim();
                color.valorColor = txtValorColor.Text.Trim();

                if (btnGuardar.Text.Equals("&Registrar"))
                {
                    msj = "Color registrado con éxito!!";
                    resultado = ColorDao.Agregar(color);
                }
                if (btnGuardar.Text.Equals("&Actualizar"))
                {
                    msj = "Color actualizado con éxito!!";
                    color.idColor= Convert.ToInt32(txtCodigo.Text.Trim());
                    resultado = ColorDao.Modificar(color);

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
                dgvColores.DataSource = ColorDao.Listar();
                tabControl1.SelectedTab = tabPage2;
                btnGuardar.Text = "&Registrar";
                resetValores();
            }
            else
            {
                MessageBox.Show("No se pudo guardar el Color", "Fallo!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void resetValores()
        {
            lblCodigo.Text = "";
            txtCodigo.Text = "";
            lblCodigo.Visible = false;
            txtCodigo.Visible = false;
            txtNombreColor.Text = "";
            txtValorColor.Text = "";
            lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("White");
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            pos = dgvColores.CurrentRow.Index;
            string id = Convert.ToString(dgvColores[0, pos].Value);

            DialogResult result = MessageBox.Show("Eliminar el color: " + id, "Confirmar", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ColorDao.Eliminar(Convert.ToInt32(id));
                dgvColores.DataSource = ColorDao.Listar();
                MessageBox.Show("Color eliminado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

    }
}
