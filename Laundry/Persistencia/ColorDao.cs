using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lavanderia.Models;
using System.Data;

namespace Lavanderia.Persistencia
{
    class ColorDao
    {
        public static int Agregar(Color color)
        {
            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(string.Format("Insert into Color (nombreColor, valorColor) values ('{0}','{1}')",
                color.nombreColor, color.valorColor), BdComun.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int Modificar(Color color)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(string.Format("UPDATE Color Set nombreColor='{0}',valorColor='{1}' where idColor='{2}'"
            ,color.nombreColor, color.valorColor, color.idColor), BdComun.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            return retorno;

        }

        public static int Eliminar(int idcolor)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(string.Format("DELETE FROM Color where idColor='{0}'", idcolor), BdComun.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static MySqlDataReader fillColor()
        {

            MySqlCommand _comando = new MySqlCommand("coloresAll", BdComun.ObtenerConexion());
            _comando.CommandType = CommandType.StoredProcedure;
            MySqlDataReader _reader = _comando.ExecuteReader(CommandBehavior.CloseConnection);
            return _reader;
        }

        public static List<Color> Listar()
        {
            List<Color> _lista = new List<Color>();
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand("coloresAll", cnx.ObtenerConexion());
            _comando.CommandType = CommandType.StoredProcedure;
            MySqlDataReader _reader = _comando.ExecuteReader(CommandBehavior.CloseConnection);

            while (_reader.Read())
            {
                Color color = new Color();
                color.idColor = Convert.ToInt32(_reader["idColor"]);
                color.nombreColor = Convert.ToString(_reader["nombreColor"]);
                color.valorColor = Convert.ToString(_reader["valorColor"]);
                _lista.Add(color);
            }
            cnx.cerrarConexion();
            return _lista;
        }

        public static List<Color> Buscar(string nombre)
        {
            List<Color> _lista = new List<Color>();

            MySqlCommand _comando = new MySqlCommand(String.Format(
           "SELECT *  FROM Color where nombreColor like '%{0}%' ", nombre), BdComun.ObtenerConexion());
            MySqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                Color color = new Color();
                color.idColor= _reader.GetInt32(0);
                color.nombreColor= _reader.GetString(1);
                color.valorColor= _reader.GetString(2);
                _lista.Add(color);
            }

            return _lista;
        }


    }
}
