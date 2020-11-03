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
    class UsuarioDao
    {
        public  static Usuario Consultar(string user,string pass)
        {
            ConexBD conec = new ConexBD();
            List<Usuario> _lista = new List<Usuario>();
            conec.Conectar();
            MySqlCommand _comando = new MySqlCommand("consultaUsuario",conec.ObtenerConexion());
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.Add(new MySqlParameter("USUARIO", user));
            _comando.Parameters.Add(new MySqlParameter("PASS", pass));
            MySqlDataReader _reader = _comando.ExecuteReader(CommandBehavior.CloseConnection);
            Usuario usuario = new Usuario();
            while (_reader.Read())
            {
                 usuario.idUsuario = _reader.GetInt32(0);
                usuario.nombreUsuario = _reader.GetString(1);
                usuario.apellidoUsuario = _reader.GetString(2);
                usuario.emailUsuario = _reader.GetString(3);
                usuario.sucursalUsuario = _reader.GetString(4);
                usuario.tipoUsuario = _reader.GetInt32(6);
                             
            }
            conec.cerrarConexion();
            return usuario;
        }

    }
}
