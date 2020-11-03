using Lavanderia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lavanderia.util
{
    public class varGlobales
    {
        public static int sessionUsuario;
        public static string baseDeDatos;
        public static string OfertaDia;
        public static string PrendasDia;
        public static int CantidadDia;
        public static decimal porcentajeOferta;
        public static decimal porcentajeVisa;
        public static string rutaReportes = Application.StartupPath.Replace("bin\\Debug","");

        
    }


}
