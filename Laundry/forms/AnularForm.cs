using Lavanderia.Rest;
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
using Newtonsoft;
using Newtonsoft.Json.Schema;
using System.Net;
using Newtonsoft.Json;


namespace Lavanderia.forms
{
    public partial class AnularForm : Form
    {
        public AnularForm()
        {
            InitializeComponent();
        }

        #region Consumir Api
        private void btnanular_Click(object sender, EventArgs e)
        {

            WebClient rClient = new WebClient();
            
            string rawJson = rClient.DownloadString(textBox1.Text);
            Ordenes ordenes = JsonConvert.DeserializeObject<Ordenes>(rawJson);

                Console.WriteLine(ordenes.Ordene.Count);

            TextApiAout("Rest Client Created");
            
           

        }

        private void TextApiAout(string text) {
            try {
                System.Diagnostics.Debug.Write(text + Environment.NewLine);
                textBox2.Text = textBox2.Text + text + Environment.NewLine;
                textBox2.SelectionStart=textBox2.TextLength;
                textBox2.ScrollToCaret();
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.Write(ex.Message, ToString() + Environment.NewLine);

            }
        }
        #endregion
    }
}
