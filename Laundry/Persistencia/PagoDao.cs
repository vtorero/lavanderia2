using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lavanderia.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Lavanderia.Persistencia
{
    class PagoDao
    {
        public static int Agregar(Pago pago)
        {

            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(string.Format("Insert into Pago (idOrden, pago1,pago2,pagoTotal,tipoPago,tipoPago1,tipoDocumento,igv,Estado,fechaPago,fechaActualizado) values ({0},{1},{2},{3},{4},{5},{6},{7},{8},'{9}','{10}')",
                pago.idOrden,pago.Pago1,pago.Pago2,pago.PagoTotal,pago.TipoPago,pago.TipoPago1,pago.TipoDocumento,pago.Igv,pago.Estado,pago.fechaPago,pago.fechaActualizado), cnx.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            cnx.cerrarConexion();
            return retorno;
        
        
        
        }

        public static MySqlDataReader consultaPago(int id)
        {
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand("consultaPago", cnx.ObtenerConexion());
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.Add(new MySqlParameter("id", id));
            MySqlDataReader _reader = _comando.ExecuteReader(CommandBehavior.CloseConnection);
           
            return _reader;
        }

        public static int modificaPago(int id, int pago1, int pago2 )
        {
            int retorno = 1;
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand cmd = new MySqlCommand("modificaPago", cnx.ObtenerConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("id", id));
            cmd.Parameters.Add(new MySqlParameter("pago1", pago1));
            cmd.Parameters.Add(new MySqlParameter("pago2", pago2));
            cmd.ExecuteReader();
            cnx.cerrarConexion();
            return retorno;
        }


    }
}
