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
    class ClienteDao
    {
        public static int Agregar(Cliente cliente)
        {
            int retorno = 0;
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand comando = new MySqlCommand(string.Format("Insert into Cliente (nombreCliente, dniCliente, correoCliente,direccionCliente,telefonoCliente,usuarioCreador) values ('{0}','{1}','{2}','{3}','{4}',{5})",
                cliente.Nombres, cliente.DNI, cliente.Email,cliente.Dirección,cliente.Teléfono,cliente.usuarioCreador), cnx.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            comando.Connection.Close();
            cnx.cerrarConexion();
            return retorno;
        }

        public static int Modificar(Cliente cliente) {
            int retorno = 0;
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand comando = new MySqlCommand(string.Format("UPDATE Cliente Set nombreCliente='{0}',dniCliente='{1}',correoCliente='{2}',direccionCliente='{3}',telefonoCliente='{4}' where idCliente={5}"
            , cliente.Nombres, cliente.DNI, cliente.Email, cliente.Dirección, cliente.Teléfono, cliente.idCliente), cnx.ObtenerConexion());
            retorno= comando.ExecuteNonQuery();
            comando.Connection.Close();
            cnx.cerrarConexion();
            return retorno;

        
        }

        public static int Eliminar(int idcliente) {
            int retorno = 0;
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand comando = new MySqlCommand(string.Format("DELETE FROM Cliente where idCliente='{0}'", idcliente),cnx.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            comando.Connection.Close();
            cnx.cerrarConexion();
            return retorno;
        }

        public static List<Cliente> Listar()
        {
            List<Cliente> _lista = new List<Cliente>();
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand cmd= new MySqlCommand("clientesAll", cnx.ObtenerConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("idUsuario", varGlobales.sessionUsuario));
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           

            while (dr.Read())
            {
            Cliente cliente= new Cliente();
                cliente.idCliente = Convert.ToInt32(dr["idCliente"]);
                cliente.Nombres= Convert.ToString(dr["nombreCliente"]);
                cliente.DNI = Convert.ToString(dr["dniCliente"]);
                cliente.Email = Convert.ToString(dr["correoCliente"]);
                cliente.Dirección= Convert.ToString(dr["direccionCliente"]);
                cliente.Teléfono = Convert.ToString(dr["telefonoCliente"]);
                _lista.Add(cliente);
            }
            cmd.Connection.Close();
            cnx.cerrarConexion();
            return _lista;
        }

        public static List<Cliente> Buscar(string nombre,string dni,int usuarioCreador)
        {
            List<Cliente> _lista = new List<Cliente>();
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand(String.Format(
           "SELECT idCliente,nombreCliente,dniCliente,correoCliente,telefonoCliente,direccionCliente FROM Cliente where nombreCliente like '%{0}%' and dniCliente like '%{1}%' and usuarioCreador={2}",nombre,dni,usuarioCreador), cnx.ObtenerConexion());
            MySqlDataReader _reader = _comando.ExecuteReader();
while (_reader.Read())
            {
                Cliente cliente = new Cliente();
                cliente.idCliente = _reader.GetInt32(0);
                cliente.Nombres = _reader.GetString(1);
                cliente.DNI = _reader.GetString(2);
                cliente.Email = _reader.GetString(3);
                cliente.Teléfono = _reader.GetString(4);
                cliente.Dirección = _reader.GetString(5);
                    _lista.Add(cliente);
            }
            _comando.Connection.Close();
            cnx.cerrarConexion();
            return _lista;
        }

    }
}
