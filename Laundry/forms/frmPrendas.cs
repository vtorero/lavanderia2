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
using Lavanderia.Models;
using Lavanderia.Persistencia;
using Lavanderia.util;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Lavanderia.forms
{


    public partial class frmPrendas : Form
    {

        string url = "https://jsonplaceholder.typicode.com/posts";
        int pos;
        Validacion v = new Validacion();
        public frmPrendas()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

      
     

        private void button2_Click(object sender, EventArgs e)
        {
          //  MySqlDataAdapter myadap = new MySqlDataAdapter(String.Format(
          //"SELECT idPrenda, NombrePrenda, DescripcionPrenda, PrecioServicio FROM Prenda"), BdComun.ObtenerConexion());
          //  DataSet ds = new DataSet();
          //  myadap.Fill(ds, "Prendas");
          //  cryrep.Load(@"D:\laundry\Laundry\Laundry\Reportes\crPrendas.rpt");
          //  cryrep.SetDataSource(ds);
          //  frmReporte rt = new frmReporte();
          //  rt.crystalReportViewer1.ReportSource = cryrep;
          //  rt.Show();﻿

        }

        private async void frmPrendas_Load(object sender, EventArgs e)
        {
            string respuesta = await getHttp();
            List<Post> lst = JsonConvert.DeserializeObject<List<Post>>(respuesta);
/*dataGridView1.DataSource = lst;*/
            fillTipos();
            if (varGlobales.sessionUsuario == 1)
            {
                btnGuardar.Enabled = true;

            }
            else
            {
                btnGuardar.Enabled = false;
            }



        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            editar_prenda();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (dgvPrendas.RowCount == 0)
            {
                dgvPrendas.DataSource = PrendaDao.Listar();
                dgvPrendas.Columns[0].HeaderText = "Código";
                dgvPrendas.Columns[0].Width = 60;
                dgvPrendas.Columns[1].HeaderText = "Nombre";
                dgvPrendas.Columns[1].Width = 145;
                dgvPrendas.Columns[2].HeaderText = "Descripción";
                dgvPrendas.Columns[2].Width = 200;
                dgvPrendas.Columns[3].DefaultCellStyle.Format = "C2";
                dgvPrendas.Columns[3].HeaderText = "Precio";
                dgvPrendas.Columns[4].HeaderText = "Tipo";
                dgvPrendas.Columns[4].Width = 200;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Prenda prenda = new Prenda();
            int resultado = 0;
            string msj = "";



            if ((!string.IsNullOrWhiteSpace(txtNombre.Text)) && (!string.IsNullOrWhiteSpace(txtDescripcion.Text)) && (!string.IsNullOrWhiteSpace(cmbTipoPrenda.Text)))
            {
                prenda.NombrePrenda = txtNombre.Text.Trim();
                prenda.Descripcion = txtDescripcion.Text.Trim();
                prenda.precioServicio = Decimal.Round(Convert.ToDecimal(txtPrecio.Text),2);
                prenda.tipoPrenda = cmbTipoPrenda.Text;
                prenda.tipo_oferta = cmbTipoPrenda.ValueMember;

               

                if (btnGuardar.Text.Equals("&Registrar"))
                {
                    msj = "Prenda registrada con éxito!!";
                    resultado = PrendaDao.Agregar(prenda);
                }
                if (btnGuardar.Text.Equals("&Actualizar"))
                {
                    msj = "Prenda actualizada con éxito!!";
                    prenda.idPrenda = Convert.ToInt32(txtCodigo.Text.Trim());
                    resultado = PrendaDao.Modificar(prenda);

                }

               
            }
            else
            {
                resultado = 0;
                MessageBox.Show("Debe ingresar los valores");
            }

            if (resultado > 0)
            {

                
                dgvPrendas.DataSource = PrendaDao.Listar();
                dgvPrendas.Columns[0].HeaderText = "Código";
                dgvPrendas.Columns[0].Width = 60;
                dgvPrendas.Columns[1].HeaderText = "Nombre";
                dgvPrendas.Columns[1].Width = 145;
                dgvPrendas.Columns[2].HeaderText = "Descripción";
                dgvPrendas.Columns[2].Width = 200;
                dgvPrendas.Columns[3].DefaultCellStyle.Format = "C2";
                dgvPrendas.Columns[3].HeaderText = "Precio";
                dgvPrendas.Columns[4].HeaderText = "Tipo";
                dgvPrendas.Columns[4].Width = 200;
                tabControl1.SelectedTab = tabPage2;
                btnGuardar.Text = "&Registrar";
                resetValores();
                MessageBox.Show(msj, "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo guardar la prenda", "Fallo!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void resetValores()
        {
            lblCodigo.Text = "";
            txtCodigo.Text = "";
            lblCodigo.Visible = false;
            txtCodigo.Visible = false;
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            pos = dgvPrendas.CurrentRow.Index;
            string id = Convert.ToString(dgvPrendas[0, pos].Value);

            DialogResult result = MessageBox.Show("Eliminar la prenda: " + id, "Confirmar", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                PrendaDao.Eliminar(Convert.ToInt32(id));
                dgvPrendas.DataSource = PrendaDao.Listar();
                dgvPrendas.Columns[0].HeaderText = "Código";
                dgvPrendas.Columns[0].Width = 60;
                dgvPrendas.Columns[1].HeaderText = "Nombre";
                dgvPrendas.Columns[1].Width = 145;
                dgvPrendas.Columns[2].HeaderText = "Descripción";
                dgvPrendas.Columns[2].Width = 200;
                dgvPrendas.Columns[3].DefaultCellStyle.Format = "C2";
                dgvPrendas.Columns[3].HeaderText = "Precio";
                dgvPrendas.Columns[4].HeaderText = "Tipo";
                dgvPrendas.Columns[4].Width = 200;

                MessageBox.Show("Prenda eliminada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
           
        }


        private void  editar_prenda() {

            pos = dgvPrendas.CurrentRow.Index;
            lblCodigo.Visible = true;
            txtCodigo.Visible = true;
            txtCodigo.Text = Convert.ToString(dgvPrendas[0, pos].Value);
            txtNombre.Text = Convert.ToString(dgvPrendas[1, pos].Value);
            txtDescripcion.Text = Convert.ToString(dgvPrendas[2, pos].Value);
            txtPrecio.Text = Convert.ToString(dgvPrendas[3, pos].Value);
            cmbTipoPrenda.Text = Convert.ToString(dgvPrendas[4, pos].Value);
            tabControl1.SelectedTab = tabPage1;
            btnGuardar.Text = "&Actualizar";

         
        
        }

        private void dgvPrendas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editar_prenda();
        }

        private void cmbTipoPrenda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fillTipos()
        {
            cmbTipoPrenda.Items.Clear();
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand("prendasTipo", cnx.ObtenerConexion());
            _comando.CommandType = CommandType.StoredProcedure;
            MySqlDataReader _readerS = _comando.ExecuteReader(CommandBehavior.CloseConnection);

            while (_readerS.Read())
            {
                string name = _readerS.GetString("descripcion");
                string id = _readerS.GetString("id_tipo");
                cmbTipoPrenda.Items.Add(name);
                cmbTipoPrenda.DisplayMember = name;
                cmbTipoPrenda.ValueMember = id;
            }
            _readerS.Close();
            cnx.cerrarConexion();
        }



public async Task<string> getHttp() {
    WebRequest oRequest = WebRequest.Create(url);
    WebResponse oResponse = oRequest.GetResponse();
    StreamReader sr = new StreamReader(oResponse.GetResponseStream());
    return await sr.ReadToEndAsync();

}

     
    }
}
