using Lavanderia.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia.Persistencia
{
    class EgresoDao
    {
        public void Agregar(Egreso egreso)
        {

            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand cmd = new MySqlCommand("addEgreso", cnx.ObtenerConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("pIdUsuario", egreso.Usuario));
            cmd.Parameters.Add(new MySqlParameter("pMonto", egreso.Monto));
            cmd.Parameters.Add(new MySqlParameter("pFecha", egreso.FechaEgreso));
            cmd.Parameters.Add(new MySqlParameter("pMotivo", egreso.Motivo));
            cmd.Parameters.Add(new MySqlParameter("pEstado", egreso.Estado));
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            cnx.cerrarConexion();
            
        }
    }
}
