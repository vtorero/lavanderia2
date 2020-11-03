using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lavanderia.util;
using System.Windows.Forms;

namespace Lavanderia
{
    public class ConexBD
    {
        MySqlConnection con;
        public void Conectar()
        {
            try
            {
                //string enlace ="Server=kvconsult.com; Port=3306 ;Database=kvconsul_lavan; uid=kvconsul_lavan; pwd=vji2002;";
              //string enlace = "Server=107.190.129.66; Port=3306 ;Database=" + varGlobales.baseDeDatos + "; uid=cualesmi_web; pwd=vji2002;";
                string enlace = "Server=35.231.78.51; Port=3306 ;Database=dashboard; uid=marife; pwd=libido16;";
               //string enlace = "Server=localhost; Port=3306 ;Database=cualesmi_lavanderia; uid=root; pwd=;";
                this.con = new MySqlConnection(enlace);
                this.con.Open();
            }
            catch (MySqlException e)
            {
                 Console.WriteLine(e);
            }

        }

        public MySqlConnection ObtenerConexion()
        {
            return this.con;
        }

        public void cerrarConexion()
        {
            this.con.Close();
        }
    }
}
