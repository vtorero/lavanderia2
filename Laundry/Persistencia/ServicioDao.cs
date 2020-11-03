using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lavanderia.Models;
using System.Data;
using Lavanderia.util;

namespace Lavanderia.Persistencia
    
{
    
 
    class ServicioDao
    {
            
        public static int Agregar(Servicio servicio)
        {
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(string.Format("Insert into Servicio (nombreServicio, precioServicio) values ('{0}','{1}')",
                servicio.NombreServicio, servicio.precioServicio),cnx.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            cnx.cerrarConexion();
            return retorno;
        }


        public static int Modificar(Servicio servicio)
        {
            int retorno = 0;
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand comando = new MySqlCommand(string.Format("UPDATE Servicio Set nombreServicio='{0}',precioServicio='{1}' where idServicio='{2}'"
            , servicio.NombreServicio, servicio.precioServicio, servicio.idServicio), cnx.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            cnx.cerrarConexion();
            return retorno;

        }

        public static int Eliminar(int idservicio)
        {
            int retorno = 0;
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand comando = new MySqlCommand(string.Format("DELETE FROM Servicio where idServicio='{0}'", idservicio), cnx.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            cnx.cerrarConexion();
            return retorno;
        }

        public static List<Servicio> Buscar()
        {
            List<Servicio> _lista = new List<Servicio>();
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand(String.Format(
           "SELECT * FROM Servicio ORDER BY nombreServicio"), cnx.ObtenerConexion());
            MySqlDataReader _reader = _comando.ExecuteReader();

            while (_reader.Read())
            {
                Servicio servicio = new Servicio();
                servicio.idServicio = _reader.GetInt32(0);
                servicio.NombreServicio = _reader.GetString(1);
                servicio.precioServicio = _reader.GetFloat(2);
                _lista.Add(servicio);
            }
            
            cnx.cerrarConexion();

            return _lista;
        }

        public static MySqlDataReader fillServicio()
        {
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand("serviciosAll", cnx.ObtenerConexion());
            _comando.CommandType = CommandType.StoredProcedure;
            MySqlDataReader _reader = _comando.ExecuteReader(CommandBehavior.CloseConnection);
            cnx.cerrarConexion();
           return _reader;
        }

        public static MySqlDataReader fillSucursales()
        {
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand("SELECT id, sucursal FROM usuario where id<> 1 order by sucursal", cnx.ObtenerConexion());
            MySqlDataReader _reader = _comando.ExecuteReader();
            cnx.cerrarConexion();
            return _reader;
        }



        public static MySqlDataReader fillServicioSearch(string criterio)
        {
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand(String.Format(
             "SELECT idServicio, nombreServicio, precioServicio ,cantidadMinima FROM Servicio where nombreServicio = '{0}'", criterio), cnx.ObtenerConexion());
            MySqlDataReader _reader = _comando.ExecuteReader();
            //_comando.Connection.Close();
            cnx.cerrarConexion();
            return _reader;
        }

        public static List<Servicio> Buscar(string nombre)
        {
            List<Servicio> _lista = new List<Servicio>();
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand(String.Format(
           "SELECT *  FROM Servicio where nombreServicio like '%{0}%' ", nombre), cnx.ObtenerConexion());
            MySqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                Servicio servicio = new Servicio();
                servicio.idServicio= _reader.GetInt32(0);
                servicio.NombreServicio= _reader.GetString(1);
                servicio.precioServicio = _reader.GetFloat(2);
                _lista.Add(servicio);
            }
            _comando.Connection.Close();
            cnx.cerrarConexion();
            return _lista;
        }

    }
}
