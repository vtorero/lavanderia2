using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Lavanderia.util;

namespace Lavanderia
{
    public class BdComun
    {
        public static MySqlConnection ObtenerConexion()
        {
            //MySqlConnection conectar = new MySqlConnection("Server=kvconsult.com; Port=3306 ;Database=kvconsul_lavan; uid=kvconsul_lavan; pwd=vji2002;");
            //MySqlConnection conectar = new MySqlConnection("Server=107.190.129.66; Port=3306 ;Database=" + varGlobales.baseDeDatos + "; uid=cualesmi_web; pwd=vji2002;");
          MySqlConnection conectar = new MySqlConnection("Server=localhost; Port=3306 ;Database=" + varGlobales.baseDeDatos + "; uid=root; pwd=;");
                conectar.Open();
                return conectar;
       
        }
    }
}