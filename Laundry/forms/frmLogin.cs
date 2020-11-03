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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            login();
        }


        private void login() {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtUsuario.Text) && (!string.IsNullOrWhiteSpace(txtPassword.Text)))
            {
                Usuario result;
                if (cmbBase.Text.Equals("Test"))
                {
                    varGlobales.baseDeDatos = "cualesmi_lavan_test";

                }
                else
                {
                    varGlobales.baseDeDatos = "cualesmi_lavanderia";
                    
                }
                try
                {
                    result = UsuarioDao.Consultar(usuario, password);
                    if (result.nombreUsuario != null)
                    {
                        StatusBar mainStatusBar = new StatusBar();
                        frmInicio childForm = new frmInicio();
                        if (result.tipoUsuario == 1)
                        {
                            childForm.mnuAdmUsuarios.Visible = true;
                            childForm.optionsToolStripMenuItem.Visible = true;
                        }
                        childForm.searchToolStripMenuItem.Text = Convert.ToString(result.idUsuario);

                        varGlobales.sessionUsuario = result.idUsuario;

                        mainStatusBar.Panels.Add("t_usuario");
                        mainStatusBar.Panels.Add("Usuario");
                        mainStatusBar.Panels.Add("t_sucursal");
                        mainStatusBar.Panels.Add("sucursal");
                        mainStatusBar.Panels.Add("Fecha");
                        mainStatusBar.Panels.Add("tipo");
                        mainStatusBar.Panels.Add("Oferta del Día");
                        mainStatusBar.Panels[0].Width = 50;
                        mainStatusBar.Panels[0].Text = "Usuario:";
                        mainStatusBar.Panels[1].Text = result.nombreUsuario + " " + result.apellidoUsuario;
                        mainStatusBar.Panels[2].Width = 60;
                        mainStatusBar.Panels[2].Text = "Sucursal:";
                        mainStatusBar.Panels[3].Text = result.sucursalUsuario;
                        mainStatusBar.Panels[4].Width = 200;
                        mainStatusBar.Panels[4].Text = Convert.ToString(DateTime.Now);
                        mainStatusBar.Panels[5].Text = (result.tipoUsuario == 1) ? "Admin" : "Normal";
                        mainStatusBar.Panels[6].Width = 100;
                        mainStatusBar.Panels[6].Text = "Oferta del día:";
                        DateTime Hoy = DateTime.Now;
                        int nrodia = (int)Hoy.DayOfWeek;
                        Oferta of = new Oferta();
                        of = OfertaDao.Buscar(nrodia);
                        varGlobales.OfertaDia = of.Nombre;
                        varGlobales.porcentajeOferta = of.Porcentaje;
                        varGlobales.PrendasDia = of.Prendas;
                        varGlobales.CantidadDia = of.Cantidad;
                        varGlobales.porcentajeVisa = of.PorcentajeVisa;
                        

                     

                        mainStatusBar.Panels[6].Text = varGlobales.OfertaDia;

                        if (of.Porcentaje>0)
                        {
                            childForm.lblOferta.Text = "Oferta de Día: " + varGlobales.OfertaDia + " (" + varGlobales.porcentajeOferta + "%) Descuento";
                        }
                        else
                        {
                            childForm.lblOferta.Text = "";
                        }


                        mainStatusBar.ShowPanels = true;
                        childForm.Controls.Add(mainStatusBar);
                        childForm.Show();
                        this.Hide();
                    }
                    else
                    {

                        MessageBox.Show("Usuario y/o Password incorrectos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }catch(Exception e){
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                
                }
        

            }
            else
            {

                MessageBox.Show("Debe ingresar Usuario y Password", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {

            DialogResult x = MessageBox.Show("Desea salir del Sistema", "Programa", MessageBoxButtons.YesNo);
            if (x == DialogResult.Yes)
            {
                Close();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { login(); }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { txtPassword.Focus(); }
        }

        private void cmbBase_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     
    }
}
