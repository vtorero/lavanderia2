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
using MySql.Data.MySqlClient;

namespace Lavanderia.forms.busquedas
{
    public partial class frmBuscarColor : Form
    {
        public delegate void enviar(string colores);
        public event enviar enviad;
        public frmBuscarColor()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int count = 0;
            MySqlCommand _comando = new MySqlCommand(String.Format(
        "SELECT *  FROM Color where nombreColor like '%{0}%' ", txtNombres.Text), BdComun.ObtenerConexion());
            MySqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {

                dgvColores.Rows.Add(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2));
                dgvColores[2, count].Style.BackColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(_reader.GetString(2)));
                count++;
            }


            dgvColores.RowHeadersVisible = false;
            dgvColores.Columns[0].HeaderText = "Código";
            dgvColores.Columns[0].Width = 50;
            dgvColores.Columns[1].HeaderText = "Nombre";
            dgvColores.Columns[1].Width = 190;
            dgvColores.Columns[2].HeaderText = "Valor";
            
 
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Int32 selectedRowCount =
        dgvColores.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                

                for (int i = 0; i < selectedRowCount; i++)
                {
                    sb.Append(dgvColores.Rows[dgvColores.SelectedRows[i].Index].Cells["Column2"].Value.ToString());
                    sb.Append(",");
                    sb.Append(Environment.NewLine);
                }

                
                
            }
            else {
                sb.Append("No especificado");
            }
            
            enviad(sb.ToString());
            this.Close();
        
         
        }

        private void frmBuscarColor_Load(object sender, EventArgs e)
        {
            txtNombres.Focus();
        }
    }
}
