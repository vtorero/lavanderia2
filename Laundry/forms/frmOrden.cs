using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lavanderia.forms.busquedas;
using Lavanderia.Persistencia;
using Lavanderia.Models;
using Lavanderia.util;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using MySql.Data.MySqlClient;
using Crearticket;

namespace Lavanderia.forms
{
    public partial class frmOrden : Form
    {
        ConexBD cnx = new ConexBD();
        int i = 1;
        int idOrdenPrint = 0;
        decimal cantidadGeneral = 0;
        decimal cantidadGeneralcama = 0;
        int garantia = 0;
        Validacion v = new Validacion();
        decimal totalOrden = 0;
        decimal totalGeneral = 0;
        decimal totalDescuento = 0;
        decimal totalOfertaRopa = 0;
        decimal totalOfertaCama = 0;
        decimal porcentajeDescuento = varGlobales.porcentajeOferta;
        

         public frmOrden()
        {
            InitializeComponent();
        }



        private void btnSrcCliente_Click(object sender, EventArgs e)
        {
            frmBuscarCliente childForm = new frmBuscarCliente();
            childForm.enviado += new frmBuscarCliente.enviar(ejecutar);
            childForm.ShowDialog();

        }

        public void ejecutar(string id, string nombre, string dni, string telefono)
        {
            txtNombreCliente.Text = nombre;
            txtDni.Text = dni;
            lblCodigoCliente.Text = id;
            txtTelefono.Text = telefono;

        }

        public void ejecutar2(string id, string nombre, decimal precio, string tipo,string tipooferta)
        {
            LblId.Text = id;
            txtNombrePrenda.Text = nombre;
            txtPrecio.Text = Convert.ToString(Decimal.Round(precio, 2));
            txttipo.Text = tipo;
            txt_tipo_oferta.Text = tipooferta;
        }

        public void ejecutar3(string colores)
        {




        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nroCantidad.Value > 0)
            {
                int tipoServ = 0;
                string id, detalle, defecto, colores, marca,tipooferta,globalOferta;
                id = LblId.Text;
                detalle = (rdPrenda.Checked) ? txtNombrePrenda.Text : cmbServicios.Text;
                decimal cantidad, precio, total;
                total = 0;
                 decimal MontoDecuento = 0;
                  cantidad = nroCantidad.Value;


                marca = cmbMarca.Text;
                precio = Decimal.Round(Convert.ToDecimal(txtPrecio.Text), 2);
                tipooferta = txt_tipo_oferta.Text;

                defecto = "";
                colores = "";
                globalOferta="";

                if (rdPrenda.Checked) { tipoServ = 1; }
                if (rdServicio.Checked) { tipoServ = 2; }

                //cantidadGeneral = 0;
                string[] separators = {","};
                string value = varGlobales.PrendasDia;
                string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                 totalGeneral+= Decimal.Round((cantidad * precio),2);
                 foreach (var word in words)
                     if (tipooferta == word && tipoServ == 1)
                     {
                         cantidadGeneral += cantidad;
                         globalOferta = word;
                         totalOfertaRopa += Decimal.Round((cantidad * precio), 2);
                         //MessageBox.Show("hay una oferta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                         total = Decimal.Round((cantidad * precio), 2);
                     }
                   
                   if (tipoServ == 1 && cantidadGeneral>=varGlobales.CantidadDia &&  tipooferta.Equals(globalOferta))
                    {
                        //cantidadGeneral += cantidad;
                        //totalOfertaRopa += Decimal.Round((cantidad * precio), 2);
                        MontoDecuento= (Decimal.Round(Decimal.Round((total), 2) * varGlobales.porcentajeOferta / 100, 2));
                       totalDescuento= MontoDecuento;
                       total = Decimal.Round((cantidad * precio), 2)-MontoDecuento;
                       //total = totalOfertaRopa - MontoDecuento;

                    }
                   else
                   {
                       total = (cantidad * Decimal.Round(precio,2));

                   }

             /*   if (tipoServ == 1 && cantidad>=varGlobales.CantidadDia &&  tipooferta.Equals(globalOferta))
                {
                    cantidadGeneralcama += cantidad;
                    totalOfertaCama += Decimal.Round((cantidad * precio), 2);
                }
                else
                {
                    total = Decimal.Round((cantidad * precio), 2);
                }*/

                foreach (Object def in chkDefecto.CheckedItems)
                {
                    defecto += def.ToString() + ",";
                }

                colores = cmbColor.Text;
                /*foreach (Object col in chkColores.CheckedItems)
                {
                    colores += col.ToString() + ",";
                }*/



                if (chkDscto.Checked)
                {
                    total = (total - precio);
                    detalle = detalle + " Dscto + 5k";

                }
               

                dgvOrden.Rows.Add(i, id, detalle, cantidad, precio, Decimal.Round((precio*cantidad),2), defecto, colores, marca, tipoServ, tipooferta,MontoDecuento);
                i = i + 1;
                totalOrden += Decimal.Round(total, 2);
               // totalOrden = totalGeneral-MontoDecuento;
                PrendaDao.agregarMarca(cmbMarca.Text);
                txtTotal.Text = Convert.ToString(Decimal.Round(totalOrden,2));
                //txtIgv.Text = "S/." + Convert.ToString(Decimal.Round((totalOrden *igv) / 100,2));
                restablecer();
                cantidad = 0;
                chkDscto.Checked = false;
                chkGarantia.Enabled = false;
                MontoDecuento = 0;
                globalOferta = "";
                total = 0;

                //if (cantidadGeneralcama >= 1)
                //{

                //    MessageBox.Show("Aplica el descuento Ropa de Cama: " + aplicaDescuento + " por la cantidad " + cantidadGeneralcama + " items "+ totalOfertaCama   , "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //}

                //if(cantidadGeneral>=3 ){

                //    MessageBox.Show("Aplica el descuento: " + aplicaDescuento  + " por la cantidad " + cantidadGeneral+ " items "+ totalOfertaRopa, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //}
            }
            else
            {
                MessageBox.Show("Debe indicar el nro de prendas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

        public void restablecer()
        {
            decimal porcentajeDescuento =  varGlobales.porcentajeOferta;
            decimal porcentajeDescuentoVisa = varGlobales.porcentajeVisa;
            txtNombrePrenda.Enabled = false;
            txtNombrePrenda.Text = "";
            btnBuscaprenda.Enabled = false;
            btnBuscaprenda.Visible = false;
            cmbServicios.Enabled = false;
            cbDescuento.Enabled = false;
            //cmbMarca.Enabled = false;
            cmbMarca.Text = "";
            cmbColor.Enabled = false;
            cmbColor.Text = "";
            cmbPrenda.Text = "";
            cmbServicios.Text = "";
            //rdPrenda.Checked = false;
            //rdServicio.Checked = false;
            chkVisa.Checked = false;
            chkVisa.Enabled = false;
            chkDescuento.Checked = false;
            chkDescuento.Enabled = false;
            chkExpress.Checked = false;
            chkExpress.Visible = false;
            nroDscto.Enabled = false;
            imgDelivery.Visible = false;
            chkDelivery.Checked = false;
            chkDelivery.Visible = false;
            nroDscto.Text = "0";
            txtPrecio.Text = "";
            nroCantidad.Value = nroCantidad.Minimum;

            foreach (int w in chkDefecto.CheckedIndices)
            {
                chkDefecto.SetItemCheckState(w, CheckState.Unchecked);
            }
            foreach (int e in chkColores.CheckedIndices)
            {
                chkColores.SetItemCheckState(e, CheckState.Unchecked);
            }


            btnAdd.Enabled = false;
            nroCantidad.Enabled = false;
            chkDefecto.Enabled = false;
            chkColores.Enabled = false;
            cmbColor.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (rdPrenda.Checked)
            {
                frmBuscarPrendas childForm = new frmBuscarPrendas();
                childForm.enviado += new frmBuscarPrendas.enviar(ejecutar2);
                childForm.ShowDialog();
            }
            else if (rdServicio.Checked)
            {
                frmBuscarServicio childForm = new frmBuscarServicio();
                childForm.enviado += new frmBuscarServicio.enviar(ejecutar2);
                childForm.ShowDialog();

            }


        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            frmBuscarColor childForm = new frmBuscarColor();
            childForm.enviad += new frmBuscarColor.enviar(ejecutar3);
            childForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Orden ord = new Orden();
            Pago pago = new Pago();
            int tipo_pago = 0;
            int nrodia = 0;
            int tipo_pago1 = 0;
            int tipo_doc = 0;
            int tipo_descuento = 0;
            int nro_descuento = 0;
            int v_delivery = 0;

            if (rdTotal.Checked)
            {
                tipo_pago = 1;
            }
            if (rdParcial.Checked)
            {
                tipo_pago = 2;
            }

            if (chkVisa.Checked)
            {
                tipo_pago1 = 1;
            }
            if (chkGarantia.Checked)
            {
                garantia = 1;
            }

            if (chkDelivery.Checked) {
                v_delivery = 1;
            }

            if (Convert.ToDecimal(nroDscto.Text) > 0 && !rdParcial.Checked)
            {
                tipo_descuento = 1;
                nro_descuento = Convert.ToInt32(nroDscto.Text);
            }

            int status = 0;
            string s = dtFechaEntrega.Value.ToString("yyyy-MM-dd HH:mm:ss").Replace("/", "-").Substring(0, 10);
            string h = dtHoraEntrega.Value.ToString("hh:mm:ss").Replace("a.m.", "").Replace("p.m.", "").Replace("/", "-");
            ord.idCliente = Convert.ToInt32(lblCodigoCliente.Text);
            ord.fechaEntrega = s + " " + h;
            ord.idUsuario = Convert.ToInt32(toolStripStatusLabel1.Text);
            ord.observacion = txtObservacion.Text;
            ord.estado = 0;
            ord.tipoPago = tipo_pago;
            ord.Descuento = tipo_descuento;
            ord.pDelivery = v_delivery;
            //ord.pDescuento = nro_descuento;
            
            ord.pGarantia = garantia;



            // if (status > 0)
            //{
            if (tipo_pago == 1)
            {
                DateTime Hoy = DateTime.Now;
                string fecha_actual = Hoy.ToString("yyyy-MM-dd HH:mm:ss");

                if (chkVisa.Checked && totalDescuento > 0)
                {

                    //porcentajeDescuento = Convert.ToDecimal(cbDescuento.Text) - 10;
                    porcentajeDescuento = varGlobales.porcentajeVisa;
                    ord.pDescuento = Convert.ToDecimal(porcentajeDescuento);
                }
                else
                {

                    if (varGlobales.porcentajeOferta > 0 && cbDescuento.Text=="")
                    {
                        porcentajeDescuento = varGlobales.porcentajeOferta;
                    }
                    else
                    {
                        if (cbDescuento.Text != "")
                        {
                            porcentajeDescuento = Convert.ToDecimal(cbDescuento.Text);
                        }
                        else {
                            porcentajeDescuento = 0;
                        }
                    }
                    ord.pDescuento = Convert.ToDecimal(porcentajeDescuento);
                }

                //if (varGlobales.porcentajeOferta > 0 && (cantidadGeneralcama >= 1 || cantidadGeneral >= 1))
                //{
                //    //if (varGlobales.porcentajeOferta > 0 && varGlobales.OfertaDia.Equals("Ropa de Cama") && cantidadGeneralcama >= 1)
                    //{

                    //    decimal nuevototal = Convert.ToDecimal(txtPago.Text) - (totalOfertaCama);
                    //    decimal descuento = totalOfertaCama * (porcentajeDescuento / 100);
                    //    // MessageBox.Show(descuento + " :Porcentate:" + of.Porcentaje, "aviso");
                    //    pago.PagoTotal = nuevototal + (totalOfertaCama - descuento);
                    //    ord.totalOrden = nuevototal + (totalOfertaCama - descuento);
                    //    ord.observacion = txtObservacion.Text + "Oferta:" + varGlobales.OfertaDia;
                    //    pago.Pago1 = nuevototal + (totalOfertaCama - descuento);
                    //    ord.Descuento = 1;
                    //    ord.pDescuento = Convert.ToDecimal(nroDscto.Text);

                    //}


                    //if (varGlobales.porcentajeOferta > 0 && varGlobales.CantidadDia >= 1 && totalOfertaRopa>0)
                    //{
                    //    decimal nuevototal = Convert.ToDecimal(txtPago.Text) - (totalOfertaRopa);
                    //    decimal descuento = totalOfertaRopa * (porcentajeDescuento / 100);
                    //    // MessageBox.Show(descuento + " :Porcentate:" + of.Porcentaje, "aviso");
                    //    pago.PagoTotal = nuevototal + (totalOfertaRopa - descuento);
                    //    ord.totalOrden = nuevototal + (totalOfertaRopa - descuento);
                    //    pago.Pago1 = nuevototal + (totalOfertaRopa - descuento);
                    //    ord.Descuento = 1;
                    //    ord.observacion = txtObservacion.Text + "Oferta:" + varGlobales.OfertaDia;
                    //    ord.pDescuento = Convert.ToDecimal(nroDscto.Text);
                    //}
                //}
                //else
               // {

                decimal montoDescuento=0;
                decimal montoTotal;
                if (cbDescuento.Text == "")
                {
                    montoDescuento = (Decimal.Round(Decimal.Round((Convert.ToDecimal(totalOfertaRopa)), 2) * porcentajeDescuento / 100, 2));
                }
                else {
                    montoDescuento = (Decimal.Round(Decimal.Round((Convert.ToDecimal(totalGeneral)), 2) * porcentajeDescuento / 100, 2));
                }

                montoTotal = Convert.ToDecimal(totalGeneral) - montoDescuento;
                  if (chkExpress.Checked) {
                      ord.pExpress = 1;
                      montoTotal = Convert.ToDecimal(totalGeneral) + (Convert.ToDecimal(totalGeneral) * 50) / 100;
                  
                  }

                if(rdServicio.Checked){
                //montoTotal = Convert.ToDecimal(txtPago.Text);
                }

                 pago.PagoTotal = montoTotal;
                 ord.totalOrden = montoTotal;
                 pago.Pago1 = montoTotal;

                 montoTotal = 0;
                 tipo_descuento = 0;
                 nro_descuento = 0;
/*                    pago.PagoTotal = Convert.ToDecimal(txtPago.Text);
                    ord.totalOrden = Convert.ToDecimal(txtPago.Text);
                    pago.Pago1 = Convert.ToDecimal(txtPago.Text);*/
                //}

                status = OrdenDao.Agregar(ord);
                pago.idOrden = status;

                pago.Pago2 = 0;

                pago.TipoPago = tipo_pago;
                pago.TipoPago1 = tipo_pago1;
                pago.TipoDocumento = tipo_doc;
                //pago.Igv = Convert.ToDecimal(txtIg.Text);
                pago.Estado = 0;
                pago.fechaPago = fecha_actual;
                pago.fechaActualizado = fecha_actual;
                PagoDao.Agregar(pago);

            }

            if (tipo_pago == 2)
            {

                DateTime Hoy = DateTime.Now;
                string fecha_actual = Hoy.ToString("yyyy-MM-dd HH:mm:ss");
                nrodia = (int)Hoy.DayOfWeek;
                ord.totalOrden = (Convert.ToDecimal(txtPago.Text) + Convert.ToDecimal(txtPendiente.Text));
                status = OrdenDao.Agregar(ord);
                pago.idOrden = status;
                pago.Pago1 = Convert.ToDecimal(txtPago.Text);
                pago.Pago2 = Convert.ToDecimal(txtPendiente.Text); ;
                pago.PagoTotal = (Convert.ToDecimal(totalOrden+totalDescuento));
                pago.TipoPago = tipo_pago;
                pago.TipoPago1 = tipo_pago1;
                pago.TipoDocumento = tipo_doc;
                //pago.Igv = pago.Igv = Convert.ToDecimal(txtIg.Text);
                pago.Estado = 0;
                pago.fechaPago = fecha_actual;
                pago.fechaActualizado = fecha_actual;
                PagoDao.Agregar(pago);

            }
            string sqlAddLinea = "INSERT INTO OrdenLinea (idOrden,item,idPrenda,Descripcion,cantidad,precio,descuento,defecto,colorPrenda,marca,total,tipoServicio,estado) VALUES ";
            try
            {
                foreach (DataGridViewRow data in dgvOrden.Rows)            
                {
                    /*OrdenLinea ordline = new OrdenLinea();
                    ordline.idOrden = status;
                    ordline.Item = Convert.ToInt32(data.Cells["clNumero"].Value);
                    ordline.idPrenda = Convert.ToInt32(data.Cells["clPrenda"].Value);
                    ordline.Descripcion = data.Cells["clDescripcion"].Value.ToString();
                    ordline.Cantidad = Convert.ToDecimal(data.Cells["clCantidad"].Value);
                    ordline.Precio = Decimal.Round(Convert.ToDecimal(data.Cells["clPrecio"].Value.ToString()), 2);
                    ordline.Defecto = Convert.ToString(data.Cells["clDefecto"].Value);
                    ordline.Colores = Convert.ToString(data.Cells["clColores"].Value);
                    ordline.Total = Convert.ToDecimal(data.Cells["clTotal"].Value);
                    ordline.Marca = Convert.ToString(data.Cells["cLmarca"].Value);
                    ordline.TipoServicio = Convert.ToInt32(data.Cells["clTipo"].Value);
                    ordline.Estado = 0;
                    */
                    if (tipo_pago == 2)
                    {
                       // sqlAddLinea += "(" + status + "," + Convert.ToInt32(data.Cells["clNumero"].Value) + "," + Convert.ToInt32(data.Cells["clPrenda"].Value) + ",'" + data.Cells["clDescripcion"].Value.ToString() + "'," + Convert.ToDecimal(data.Cells["clCantidad"].Value) + "," + Decimal.Round(Convert.ToDecimal(data.Cells["clPrecio"].Value.ToString()), 2) + ",'" + Convert.ToString(data.Cells["clDefecto"].Value) + "','" + Convert.ToString(data.Cells["clColores"].Value) + "','" + Convert.ToString(data.Cells["cLmarca"].Value) + "'," + (Convert.ToDecimal(data.Cells["clTotal"].Value) + Convert.ToDecimal(data.Cells["clDescuento"].Value)) + "," + Convert.ToInt32(data.Cells["clTipo"].Value) + ",0),";
                        //sqlAddLinea += String.Format("({0},{1},{2},'{3}',{4},{5},{6},'{7}','{8}','{9}',10},{11},0),", status, Convert.ToInt32(data.Cells["clNumero"].Value), Convert.ToInt32(data.Cells["clPrenda"].Value), data.Cells["clDescripcion"].Value.ToString(), Convert.ToDecimal(data.Cells["clCantidad"].Value), Decimal.Round(Convert.ToDecimal(data.Cells["clPrecio"].Value.ToString()),2), Convert.ToString(data.Cells["clDefecto"].Value), Convert.ToString(data.Cells["clColores"].Value), Convert.ToString(data.Cells["cLmarca"].Value), (Convert.ToDecimal(data.Cells["clTotal"].Value) + Convert.ToDecimal(data.Cells["clDescuento"].Value)), Convert.ToInt32(data.Cells["clTipo"].Value), 0);
                        sqlAddLinea += String.Format("({0},{1},{2},'{3}',{4},{5},{6},'{7}','{8}','{9}',{10},{11},0),", status, Convert.ToInt32(data.Cells["clNumero"].Value), Convert.ToInt32(data.Cells["clPrenda"].Value), data.Cells["clDescripcion"].Value.ToString(), Convert.ToDecimal(data.Cells["clCantidad"].Value), Convert.ToDecimal(data.Cells["clPrecio"].Value), Decimal.Round(Convert.ToDecimal(data.Cells["clDescuento"].Value.ToString()), 2), Convert.ToString(data.Cells["clDefecto"].Value), Convert.ToString(data.Cells["clColores"].Value), Convert.ToString(data.Cells["cLmarca"].Value), Convert.ToDecimal(data.Cells["clTotal"].Value), Convert.ToInt32(data.Cells["clTipo"].Value), 0);
                    }
                    else {
                        sqlAddLinea += String.Format("({0},{1},{2},'{3}',{4},{5},{6},'{7}','{8}','{9}',{10},{11},0),", status, Convert.ToInt32(data.Cells["clNumero"].Value), Convert.ToInt32(data.Cells["clPrenda"].Value), data.Cells["clDescripcion"].Value.ToString(), Convert.ToDecimal(data.Cells["clCantidad"].Value), Convert.ToDecimal(data.Cells["clPrecio"].Value), Decimal.Round(Convert.ToDecimal(data.Cells["clDescuento"].Value.ToString()), 2), Convert.ToString(data.Cells["clDefecto"].Value), Convert.ToString(data.Cells["clColores"].Value), Convert.ToString(data.Cells["cLmarca"].Value), Convert.ToDecimal(data.Cells["clTotal"].Value), Convert.ToInt32(data.Cells["clTipo"].Value), 0);
                        //sqlAddLinea += "(" + status + "," + Convert.ToInt32(data.Cells["clNumero"].Value) + "," + Convert.ToInt32(data.Cells["clPrenda"].Value) + ",'" + data.Cells["clDescripcion"].Value.ToString() + "'," + Convert.ToDecimal(data.Cells["clCantidad"].Value) + "," + Decimal.Round(Convert.ToDecimal(data.Cells["clPrecio"].Value.ToString()), 2) + ",'" + Convert.ToString(data.Cells["clDefecto"].Value) + "','" + Convert.ToString(data.Cells["clColores"].Value) + "','" + Convert.ToString(data.Cells["cLmarca"].Value) + "'," + Convert.ToDecimal(data.Cells["clTotal"].Value) + "," + Convert.ToInt32(data.Cells["clTipo"].Value) + ",0),";
                    
                    }
                    //OrdenDao.AgregarLinea(ordline);
                }
                //Console.WriteLine(sqlAddLinea);
            }
            catch (Exception)
            
            {


            }

       

            OrdenDao.Agregarsql(sqlAddLinea.TrimEnd(','));
            sqlAddLinea = "";

            MessageBox.Show(string.Format("Se grabó correctamente la orden con el número: {0}", status), "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            idOrdenPrint = status;
            desHabilitaServicio();
            
            btnImprimir.Enabled = true;
            i = 1;

            //}


        }

        private void rdParcial_CheckedChanged(object sender, EventArgs e)
        {
            lblMontopagar.Text = "Adelanto";
            lblPendiente.Visible = true;
            chkVisa.Enabled = true;
            lblSimbolopendiente.Visible = true;
            txtPendiente.Visible = true;
            txtPendiente.Text = Convert.ToString(totalGeneral);
            txtPago.Enabled = true;
            txtObservacion.Enabled = true;
            txtPago.Text = "0.00";
            dtFechaEntrega.Enabled = true;
            dtHoraEntrega.Enabled = true;
            btnGuardar.Enabled = true;
            txtIg.Visible = false;
            lblPorcentaje.Visible = false;
            chkDescuento.Enabled = false;
            chkDescuento.Visible = false;
            lblDescuento.Visible = false;
            nroDscto.Visible = false;
            chkExpress.Visible = false;
            nroDscto.Enabled = false;
            cbDescuento.Enabled = false;
            cbDescuento.Visible = false;
           // labelDescuento.Visible = false;
            labelporcentaje.Visible = false;
        }

        private void rdTotal_Click(object sender, EventArgs e)
        {
            lblMontopagar.Text = "Monto";
            lblPendiente.Visible = false;
            txtPendiente.Visible = false;
            lblSimbolopendiente.Visible = false;
            txtPago.Text = Convert.ToString(totalOrden);
            
            txtPago.Enabled = false;
            txtObservacion.Enabled = true;
            dtFechaEntrega.Enabled = true;
            dtHoraEntrega.Enabled = true;
            cbDescuento.Enabled = true;
            cbDescuento.Visible = true;
            chkDelivery.Visible = true;
            imgDelivery.Visible = true;
            //labelDescuento.Visible = true;
            //labelporcentaje.Visible = true;
            chkExpress.Visible = true;
            chkVisa.Enabled = true;
            if (cantidadGeneral >= varGlobales.CantidadDia && totalOfertaRopa>0 && !chkVisa.Checked)
            {
                txtPago.Text = Convert.ToString(totalOrden);
               // lblPorcentaje.Visible = true;
                //nroDscto.Visible = true;
                //lblDescuento.Visible = true;
                //lblDescuento.Text = "Total a pagar: S/. " + Convert.ToString(Convert.ToDouble(txtPago.Text) - ((Convert.ToDouble(totalOfertaRopa) * Convert.ToDouble(nroDscto.Text)) / 100));

            }
            /*chkDescuento.Enabled = true;*/



        }

        private void txtPago_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPago.Text))
            {

                txtPendiente.Text = Convert.ToString(Convert.ToDecimal(totalGeneral-Convert.ToDecimal(txtPago.Text)));

            }
        }



        private void txtPago_KeyPress(object sender, KeyPressEventArgs e)
        {

            v.soloNumeros(e);
        }



        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {
            rdPrenda.Enabled = true;
            rdServicio.Enabled = true;
        }

        public void habilitaServicio()
        {


            nroCantidad.Enabled = true;
            chkDefecto.Enabled = true;
            chkColores.Enabled = true;
            cmbColor.Enabled = true;
            chkGarantia.Enabled = true;
            dgvOrden.Enabled = true;
            imgDelivery.Visible = true;
            chkDelivery.Visible = true;


        }

        public void desHabilitaServicio()
        {
            rdPrenda.Enabled = false;
            rdServicio.Enabled = false;
            nroCantidad.Enabled = false;
            chkDefecto.Enabled = false;
            chkColores.Enabled = false;
            cmbColor.Enabled = false;
            txtPendiente.Enabled = false;
            txtPago.Text = "";
            txtPago.Enabled = false;
            txtObservacion.Text = "";
            txtObservacion.Enabled = false;
            txtTotal.Text = "";
            dgvOrden.Rows.Clear();
            dgvOrden.Enabled = false;
            txtNombreCliente.Text = "";
            txtDni.Text = "";
            txtTelefono.Text = "";
            rdParcial.Checked = false;
            rdParcial.Enabled = false;
            rdTotal.Checked = false;
            rdTotal.Enabled = false;
            rdPrenda.Enabled = false;
            rdServicio.Enabled = false;
            dtFechaEntrega.Enabled = false;
            dtHoraEntrega.Enabled = false;
            txtPago.Enabled = false;
            lblPendiente.Visible = false;
            txtPendiente.Text = "0.00";
            txtPendiente.Visible = false;
            lblSimbolopendiente.Visible = false;
            txtObservacion.Enabled = false;
            btnQuitar.Enabled = false;
            btnGuardar.Enabled = false;
            btnImprimir.Enabled = false;
            //chkVisa.Checked = false;
            chkVisa.Enabled = false;
            chkGarantia.Checked = false;
            chkGarantia.Enabled = false;
            chkDescuento.Checked = false;
            chkDescuento.Enabled = false;
            chkDescuento.Visible = false;
            chkExpress.Checked = false;
            chkExpress.Visible = false;
            chkDelivery.Checked = false;
            chkDelivery.Visible = false;
            imgDelivery.Visible = false;
            nroDscto.Text = "0";
            nroDscto.Enabled = false;
            nroDscto.Visible = false;
            cargoVisa.Visible = false;
            lblPorcentaje.Visible = false;
            //porcentajeDescuento = varGlobales.porcentajeOferta;
            lblDescuento.Text = "0";
            lblDescuento.Visible = false;
            cbDescuento.Items[0] = "0";
            cbDescuento.SelectedIndex = 0;
            //labelDescuento.Visible = false;
            labelporcentaje.Visible = false;
            cbDescuento.Visible = false;

            cantidadGeneral = 0;
            garantia = 0;
            cantidadGeneralcama = 0;
            totalOrden = 0;
            totalOfertaRopa = 0;
            totalDescuento = 0;
            totalGeneral = 0;
            
             if (varGlobales.porcentajeOferta > 0)
            {
                decimal porcentajeDescuento = varGlobales.porcentajeOferta;
                nroDscto.Text = Convert.ToString(varGlobales.porcentajeOferta);

            }
            else {
                porcentajeDescuento = 0;
            }


        }


        private void txtNombrePrenda_TextChanged(object sender, EventArgs e)
        {
            habilitaServicio();
            //btnAdd.Enabled = true;

        }

        private void dgvOrden_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnQuitar.Enabled = true;
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            decimal totalDescontar = 0;
            Int32 selectedRowCount = dgvOrden.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount > 0)
            {

                for (int i = 0; i < selectedRowCount; i++)
                {
                    totalDescontar = Convert.ToDecimal(dgvOrden.Rows[dgvOrden.SelectedRows[i].Index].Cells["clTotal"].Value.ToString()) - Convert.ToDecimal(dgvOrden.Rows[dgvOrden.SelectedRows[i].Index].Cells["clDescuento"].Value.ToString()); ;
                    dgvOrden.Rows.RemoveAt(dgvOrden.SelectedRows[i].Index);
                }

            }
            totalGeneral = totalGeneral - totalDescontar;
            totalOrden = (totalOrden - totalDescontar);
            txtTotal.Text = Convert.ToString(totalOrden);
            txtPendiente.Text = Convert.ToString(totalOrden);
            btnQuitar.Enabled = false;
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            rdParcial.Enabled = true;
            rdTotal.Enabled = true;
            txtPago.Text = txtTotal.Text;


        }

        private void txtPago_Leave(object sender, EventArgs e)
        {
            if ((totalOrden - Convert.ToDecimal(txtPago.Text)) > 0)
            {
                txtPendiente.Text = Convert.ToString(totalGeneral - Convert.ToDecimal(txtPago.Text));

            }
            else
            {
                MessageBox.Show("El monto sobrepasa el total de la orden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPendiente.Text = "0.0";
                txtPago.Focus();
            }
        }




        private void rdTotal_CheckedChanged(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;

            //chkDescuento.Enabled = true;
            if (varGlobales.porcentajeOferta > 0 && cantidadGeneral >= varGlobales.CantidadDia || cantidadGeneralcama >= 1)
            {
                //lblPorcentaje.Visible = true;
                //nroDscto.Visible = true;

                if (chkVisa.Checked)
                {
                    nroDscto.Text = Convert.ToString(varGlobales.porcentajeOferta - 10);
                    lblDescuento.Visible = true;
                    if (!txtPago.Text.Equals(""))
                    {
                        //lblDescuento.Text = "Total a pagar: S/." + Convert.ToString(Convert.ToDouble(txtPago.Text) - ((Convert.ToDouble(totalOfertaRopa) * Convert.ToDouble(nroDscto.Text)) / 100));
                        // lblDescuento.Text =                        Convert.ToString((Convert.ToDouble(txtPago.Text) * Convert.ToDouble(nroDscto.Text)) / 100);
                    }
                }
                else
                {
                    nroDscto.Text = Convert.ToString(varGlobales.porcentajeOferta);
                    //lblDescuento.Visible = true;
                    if (!txtPago.Text.Equals(""))
                    {
                        //lblDescuento.Text = "Total a pagar: S/." + Convert.ToString(Convert.ToDouble(txtPago.Text) - ((Convert.ToDouble(totalOfertaRopa) * Convert.ToDouble(nroDscto.Text)) / 100));
                        //lblDescuento.Text = Convert.ToString((Convert.ToDouble(txtPago.Text) * Convert.ToDouble(nroDscto.Text)) / 100);
                    }
                }


            }


        }
        /*
        private void chkFactura_CheckStateChanged(object sender, EventArgs e)
        {
            
             if (chkVisa.Checked)
            {
                decimal igv = Decimal.Round((totalOrden * 18) / 100, 2);    
                MessageBox.Show("" + igv);
                txtIg.Visible = true;
                txtIg.Text = Convert.ToString(igv);

                txtPago.Text = Convert.ToString(totalOrden + igv);

            }
            else {
                txtIg.Visible = false;
                decimal quitar = Convert.ToDecimal(txtIg.Text);
                decimal t = Convert.ToDecimal(txtPago.Text);
                txtPago.Text = Convert.ToString(t- quitar);
                lbligv.Visible = false;
                TXTIGV.Visible = false;
                grpTotal.Left = 318;
                grpTotal.Width = 220;
                label9.Left = 15;
                txtTotal.Left = 106;
                txtTotal.Text = Convert.ToString(Decimal.Round(totalOrden, 2));

            
            }
        }*/

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ConexBD cn1 = new ConexBD();
            cn1.Conectar();
            CrearTicket ticket = new CrearTicket();
            ticket.TextoCentro("LAVANDERIA SAN ISIDRO S.A");
            ticket.TextoIzquierda("");
            MySqlCommand _comando1 = new MySqlCommand(String.Format(
          "SELECT o.idOrden,c.dniCliente,c.nombreCliente,o.fechaCreado,o.fechaEntrega, o.totalOrden,o.descuento,(l.total-o.`totalOrden`) dscto ,o.aplicaDscto,l.cantidad,l.precio,l.descripcion,l.total,l.colorPrenda,l.marca,l.defecto,p.pago1,p.pago2,u.direccion,u.telefono,u.impresora,p.tipoPago1,o.express,o.delivery FROM Orden o INNER JOIN Cliente c ON o.idCliente=c.idCliente INNER JOIN Pago p ON o.idOrden=p.idOrden INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN usuario u ON u.id=o.idUsuario WHERE o.idOrden={0}", idOrdenPrint), cn1.ObtenerConexion());
            ConexBD cn2 = new ConexBD();
            cn2.Conectar();
            MySqlCommand _comando = new MySqlCommand(String.Format(
          "SELECT o.idOrden,c.dniCliente,c.nombreCliente,o.fechaCreado,o.fechaEntrega, o.totalOrden,l.cantidad,l.precio,l.descuento,l.descripcion,l.total,l.colorPrenda,l.marca,l.defecto,p.pago1,p.pago2,u.direccion,u.telefono,u.impresora,p.tipoPago1,o.garantia,o.express,o.delivery  FROM Orden o INNER JOIN Cliente c ON o.idCliente=c.idCliente INNER JOIN Pago p ON o.idOrden=p.idOrden INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN usuario u ON u.id=o.idUsuario WHERE o.idOrden={0}", idOrdenPrint), cn2.ObtenerConexion());
            MySqlDataReader _reader1 = _comando1.ExecuteReader();
            MySqlDataReader _reader = _comando.ExecuteReader();
            _reader1.Read();
            ticket.TextoIzquierda("DIRECCION: " + _reader1.GetString(18).ToUpper());
            ticket.TextoIzquierda("HORARIO: LUNES A VIERNES DE 8:00AM");
            ticket.TextoIzquierda(" A 8:00PM Y SABADO DE 8:00AM A 7:00PM");
            ticket.TextoIzquierda("TELEF: " + _reader1.GetString(19));
            ticket.TextoIzquierda("WEB:LAVANDERIASANISIDRO.COM");
            ticket.lineasIgual();
            ticket.TextoIzquierda("CLIENTE: " + _reader1.GetString(2).ToUpper());
            if (!_reader1.GetString(1).Equals(""))
            {
                ticket.TextoIzquierda("DNI: " + _reader1.GetString(1));
            }
            ticket.TextoIzquierda("FECHA DE ORDEN: " + _reader1.GetString(3));
            ticket.TextoIzquierda("FECHA DE ENTREGA: " + _reader1.GetString(4).Substring(0, 10));
            ticket.TextoExtremos("NRO DE ORDEN:", "Ticket # " + _reader1.GetString(0));

            ticket.lineasAsteriscos();
            ticket.EncabezadoVenta();
            ticket.lineasAsteriscos();
            decimal totalSinDescuento = 0;
            decimal cargoVisa = 0;

            if (_reader1.GetDecimal(21) == 1)
            {
                cargoVisa = 10;
            }
            while (_reader.Read())
            {
                ticket.AgregaArticulo(_reader.GetString(9), _reader.GetDecimal(6), _reader.GetDecimal(7), _reader.GetDecimal(10));
                if (!_reader.GetString(11).Equals("") || !_reader.GetString(12).Equals("") || !_reader.GetString(13).Equals(""))
                {
                    ticket.TextoExtremos(_reader.GetString(11) + " " + _reader.GetString(12), _reader.GetString(13));
                }
                    if (_reader1.GetDecimal(8) > 0 && _reader.GetDecimal(8) > 0)
                {
                    ticket.TextoExtremos("PROM DSCTO -" + _reader1.GetDecimal(6) + "%  ", " - " + (Decimal.Round(Decimal.Round((Convert.ToDecimal(_reader.GetDecimal(10))), 2) * _reader1.GetDecimal(6) / 100, 2)));
                }

                totalSinDescuento += _reader.GetDecimal(10);
                ticket.lineasGuio();
            }

            ticket.lineasAsteriscos();

            if (_reader1.GetDecimal(6) > 0)
            {
                if (_reader1.GetDecimal(8) > 0)
                {

                    ticket.AgregarTotales("            TOTAL..........S/.", _reader.GetDecimal(14));

                }
                else
                {

                    ticket.AgregarTotales("            TOTAL..........S/.", totalSinDescuento);
                }

                //  ticket.AgregarTotales("            DESCUENTO.." + (_reader1.GetDecimal(6) - cargoVisa) + "%.", (totalSinDescuento - _reader1.GetDecimal(5)));
            }
            else
            {
                if (_reader1.GetDecimal(23) > 0)
                {
                    ticket.TextoDerecha("              SERVICIO DELIVERY");
                }
                if (_reader1.GetDecimal(22) > 0)
                {
                ticket.TextoDerecha("              SERVICIO EXPRESS 50% +");
                }
                ticket.AgregarTotales("            TOTAL..........S/.", _reader.GetDecimal(5));
            }


            //if (_reader1.GetDecimal(6) > 0 && porcentajeDescuento > 0)
            //{
            //    ticket.AgregarTotales("            TOTAL..........S/.", totalSinDescuento);
            //    if (chkVisa.Checked)
            //    { 
            //     ticket.AgregarTotales("            DESCUENTO....." + (cargoVisa) + "%.", (totalSinDescuento - _reader1.GetDecimal(5)));
            //    }
            //    else
            //    {
            //    ticket.AgregarTotales("            DESCUENTO.." + (_reader1.GetDecimal(6) - cargoVisa) + "%.", (totalSinDescuento - _reader1.GetDecimal(5)));
            //    }
            //}
            //else
            //{
            //    ticket.AgregarTotales("            TOTAL..........S/.", _reader.GetDecimal(5));
            //}

            ticket.AgregarTotales("            A CUENTA.......S/.", _reader.GetDecimal(14));//La M indica que es un decimal en C#
            ticket.AgregarTotales("            SALDO..........S/.", _reader.GetDecimal(15));
            ticket.TextoIzquierda("");
            if (_reader.GetInt32(20) == 1)
            {
                ticket.TextoCentro("Prendas sin garantía");
            }
            ticket.TextoCentro("¡GRACIAS POR SU PREFERENCIA!");
            ticket.CortaTicket();
            ticket.ImprimirTicket(_reader1.GetString(20));


            _comando.Connection.Close();
            _comando1.Connection.Close();

            cn1.cerrarConexion();
            cn2.cerrarConexion();

            totalOfertaCama = 0;
            totalOfertaRopa = 0;
            btnImprimir.Enabled = false;
            totalDescuento = 0;
            /*myadap.Fill(ds,"Ticket");

            cryrep.Load(@"D:\lavanderia\Laundry\Reportes\crTicket.rpt");

            cryrep.SetDataSource(ds);
            cryrep.PrintToPrinter(1, true, 0, 0);

            /*frmReporte rt = new frmReporte();
            rt.Text = "Ticket";
            rt.crystalReportViewer1.ReportSource = cryrep;
            rt.Show();﻿*/
        }


        private void fillMarcas()
        {
            cmbMarca.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbMarca.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection datosM = new AutoCompleteStringCollection();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand("marcasAll", cnx.ObtenerConexion());
            _comando.CommandType = CommandType.StoredProcedure;
            MySqlDataReader _reader = _comando.ExecuteReader(CommandBehavior.CloseConnection);

            while (_reader.Read())
            {
                string name = _reader.GetString("nombreMarca");
                datosM.Add(name);

            }
            cmbMarca.AutoCompleteCustomSource = datosM;
            cnx.cerrarConexion();

        }

        private void fillServicio()
        {
            cmbServicios.Items.Clear();
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand("serviciosAll", cnx.ObtenerConexion());
            _comando.CommandType = CommandType.StoredProcedure;
            MySqlDataReader _readerS = _comando.ExecuteReader(CommandBehavior.CloseConnection);

            while (_readerS.Read())
            {
                string name = _readerS.GetString("nombreServicio");
                string id = _readerS.GetString("idServicio");
                cmbServicios.Items.Add(name);
                cmbServicios.DisplayMember = name;
                cmbServicios.ValueMember = id;
            }
            _readerS.Close();
            cnx.cerrarConexion();
        }

        private void fillColores()
        {
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand("coloresAll", cnx.ObtenerConexion());
            _comando.CommandType = CommandType.StoredProcedure;
            MySqlDataReader _reader = _comando.ExecuteReader(CommandBehavior.CloseConnection);
            cmbColor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbColor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection datosM = new AutoCompleteStringCollection();
            while (_reader.Read())
            {
                string name = _reader.GetString("nombreColor");
                datosM.Add(name);

            }
            _reader.Close();
            cnx.cerrarConexion();
            cmbColor.AutoCompleteCustomSource = datosM;


        }

        private void frmOrden_Load(object sender, EventArgs e)
        {
            fillMarcas();
            fillColores();
            DateTime today = DateTime.Now;
            DateTime answer = today.AddDays(4);
            dtFechaEntrega.Value = answer;
            dtHoraEntrega.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0);

        }









        private void cmbPrenda_Leave(object sender, EventArgs e)
        {
            MySqlDataReader _reader = PrendaDao.fillPrendaSearch(cmbPrenda.Text);
            while (_reader.Read())
            {
                string name = _reader.GetString("idPrenda");
                txtPrecio.Text = Convert.ToString(Decimal.Round(_reader.GetDecimal("precioServicio"), 2));
                LblId.Text = name;
            }

            habilitaServicio();
            btnAdd.Enabled = true;
            fillMarcas();
        }

        private void cmbPrenda_MouseLeave(object sender, EventArgs e)
        {
            MySqlDataReader _reader = PrendaDao.fillPrendaSearch(cmbPrenda.Text);
            while (_reader.Read())
            {
                string name = _reader.GetString("idPrenda");

                LblId.Text = name;
            }
        }

        private void cmbPrenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbPrenda.Text.ToUpper();
                nroCantidad.Focus();
            }
        }

        private void cmbServicios_SelectedIndexChanged(object sender, EventArgs e)
        {

            Object selectedItem = cmbServicios.SelectedItem;
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand(String.Format(
             "SELECT idServicio, nombreServicio, precioServicio ,cantidadMinima FROM Servicio where nombreServicio = '{0}'", selectedItem.ToString()), cnx.ObtenerConexion());
            MySqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {

                string name = _reader.GetString("idServicio");
                nroCantidad.Minimum = _reader.GetInt32("cantidadMinima");
                txtPrecio.Text = Convert.ToString(Decimal.Round(_reader.GetDecimal("precioServicio"), 2));
                LblId.Text = name;
            }

            habilitaServicio();
            btnAdd.Enabled = true;
            _reader.Close();
            cnx.cerrarConexion();
        }

        private void nroCantidad_ValueChanged(object sender, EventArgs e)
        {
            if (rdServicio.Checked && nroCantidad.Value > 5)
            {
                labelOferta.Visible = true;
                chkDscto.Visible = true;
            }
            else
            {
                labelOferta.Visible = false;
                chkDscto.Visible = false;
            }
        }

        private void rdPrenda_Click(object sender, EventArgs e)   {
            //cmbPrenda.Enabled = true;
            txtNombrePrenda.Visible = true;
            // btnBuscaprenda.Visible = true;
            btnBuscaprenda.Enabled = true;
            nroCantidad.Enabled = true;
            //cmbPrenda.Visible = true;
            cmbServicios.Visible = false;
            cmbMarca.Enabled = true;
            cmbColor.Enabled = true;
            labelCantidad.Text = "Cantidad";
            nroCantidad.Minimum = 1;
            nroCantidad.Value = 1;

            frmBuscarPrendas childForm = new frmBuscarPrendas();
            childForm.enviado += new frmBuscarPrendas.enviar(ejecutar2);
            childForm.Show();
            //fillPrendas();
        }

        private void rdServicio_Click(object sender, EventArgs e)
        {
            txtNombrePrenda.Visible = false;
            btnBuscaprenda.Visible = false;
            cmbServicios.Visible = true;
            cmbServicios.Enabled = true;
            cmbMarca.Enabled = false;
            nroCantidad.Value = 2;
            nroCantidad.Minimum = 2;
            nroCantidad.Enabled = true;
            labelCantidad.Text = "Peso";
            fillServicio();
        }

        private void cmbMarca_Leave(object sender, EventArgs e)
        {
            cmbMarca.Text.ToUpper();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            frmBuscarPrendas childForm = new frmBuscarPrendas();
            childForm.enviado += new frmBuscarPrendas.enviar(ejecutar2);
            childForm.ShowDialog();
        }

        private void txtNombrePrenda_TextChanged_1(object sender, EventArgs e)
        {
            habilitaServicio();
            btnAdd.Enabled = true;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!nroDscto.Enabled)
            {
                nroDscto.Visible = false;
                nroDscto.Enabled = false;
                lblPorcentaje.Visible = false;
                txtPago.Text = Convert.ToString(totalOrden);

            }
            else
            {
                //nroDscto.Visible = true;
                //nroDscto.Enabled = true;
                //lblPorcentaje.Visible = true;


            }

        }


        private void nroDscto_KeyPress(object sender, KeyPressEventArgs e)
        {
            v.soloNumeros(e);
        }

        private void nroDscto_Leave(object sender, EventArgs e)
        {
            totalDescuento = Convert.ToDecimal(nroDscto.Text);
            if (!txtPago.Text.Equals(""))
            {
                decimal total = totalOrden;
                txtPago.Text = Convert.ToString(Decimal.Round(total - (Convert.ToDecimal(txtPago.Text) * totalDescuento) / 100, 2));
                //totalOrden = Decimal.Round(total - (Convert.ToDecimal(txtPago.Text) * Convert.ToDecimal(nroDscto.Text)) / 100, 2);
                //total = totalOrden;

            }
        }

        private void chkVisa_Click(object sender, EventArgs e)
        {
            

            if (chkVisa.Checked && !rdParcial.Checked && totalDescuento >0)
            {

                nroDscto.Text = Convert.ToString(varGlobales.porcentajeVisa);
                porcentajeDescuento = varGlobales.porcentajeVisa;

                if (!txtPago.Text.Equals("") && porcentajeDescuento > 0 && (totalOfertaRopa > 0 || totalOfertaCama > 0) && !rdParcial.Checked)
                {
                    lblDescuento.Visible = true;
                    cargoVisa.Visible = true;
                    cargoVisa.Text =  porcentajeDescuento + "% Visa";
                    if (!txtPago.Text.Equals("") && porcentajeDescuento > 0 )
                    {
                        lblDescuento.Text = "Total a pagar: S/." + Convert.ToString(Convert.ToDouble(totalGeneral) - ((Convert.ToDouble(totalOfertaRopa) * Convert.ToDouble(nroDscto.Text)) / 100));
                        txtPago.Text = Convert.ToString(Convert.ToDouble(totalGeneral) - ((Convert.ToDouble(totalOfertaRopa) * Convert.ToDouble(nroDscto.Text)) / 100));
                    }
                }
            }
            else
            {

                if (!txtPago.Text.Equals("")&& !rdParcial.Checked && totalDescuento > 0)
                {
                    cargoVisa.Visible = false;
                    cargoVisa.Text = "";
                    nroDscto.Text = Convert.ToString(porcentajeDescuento);
                   lblDescuento.Visible = false;
                   txtPago.Text = Convert.ToString(totalOrden);
                    //lblDescuento.Text = "Total a pagar: S/." + Convert.ToString(Convert.ToDouble(txtPago.Text) - ((Convert.ToDouble(txtPago.Text) * Convert.ToDouble(nroDscto.Text)) / 100));
                }
            }
        
        }

        private void chkVisa_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

     
       

      
    }
}
