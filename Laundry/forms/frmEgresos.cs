using Lavanderia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lavanderia.util;
using Lavanderia.Persistencia;

namespace Lavanderia.forms
{
    public partial class frmEgresos : Form
    {
        Validacion v = new Validacion();
        public frmEgresos()
        {
            InitializeComponent();
            
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCantidad.Text)) {

                Egreso egreso = new Egreso();
                egreso.Usuario = varGlobales.sessionUsuario;
                DateTime Hoy = DateTime.Now;

                 decimal porcentajeDescuento = varGlobales.sessionUsuario;
                 if (porcentajeDescuento == 1)
                 {
                     egreso.Usuario = 3;
                     string fecha_actual = this.dtFechaEgreso.Value.ToString("yyyy-MM-dd HH:mm:ss");
                     egreso.FechaEgreso = fecha_actual;
                 }
                 else {
                     egreso.Usuario = varGlobales.sessionUsuario;
                     string fecha_actual = Hoy.ToString("yyyy-MM-dd HH:mm:ss");
                     egreso.FechaEgreso = fecha_actual;
                 }
                
                
                egreso.Monto = Convert.ToDecimal(txtCantidad.Text);
                egreso.Motivo = txtMotivo.Text;
                egreso.Estado = 1;
                EgresoDao edao = new EgresoDao();
                edao.Agregar(egreso);
                MessageBox.Show("Gasto registrado correctamente","Aviso",MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMotivo.Text = "";
                txtCantidad.Text = "";
            }
            
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            v.soloNumeros(e);
        }

        private void frmEgresos_Load(object sender, EventArgs e)
        {
            decimal porcentajeDescuento = varGlobales.sessionUsuario;
            if (porcentajeDescuento == 1)
            {
                this.labelFecha.Visible = true;
                this.dtFechaEgreso.Visible = true;
            }

        }
    }
}
