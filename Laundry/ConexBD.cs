﻿using System;
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
                string enlace ="Server=kvconsult.com; Port=3306 ;Database=kvconsul_lavanderia; uid=kvconsul_lavanderia; pwd=Drupal2024$;";
                //string enlace = "Server=107.190.129.66; Port=3306 ;Database=" + varGlobales.baseDeDatos + "; uid=cualesmi_web; pwd=vji2002;";
                //string enlace = "Server=35.231.78.51; Port=3306 ;Database=dashboard2; uid=marife; pwd=libido16;";
               // string enlace = "Server=lh-cjm.com; Port=3306 ;Database=aprendea_lavanderia; uid=aprendea_lavanderia; pwd=lavanderia2024;";
                //string enlace = "Server=localhost; Port=3306 ;Database=lavanderia; uid=root; pwd=";
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
