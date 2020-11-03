using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lavanderia.Models;
using MySql.Data.MySqlClient;
using System.Data;
using Lavanderia.util;

namespace Lavanderia.Persistencia
{
    class OrdenDao
    {

        public static int Agregar(Orden orden)
        {
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand cmd = new MySqlCommand("addOrden", cnx.ObtenerConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("PidCliente", orden.idCliente));
            cmd.Parameters.Add(new MySqlParameter("PfechaEntrega", orden.fechaEntrega));
            cmd.Parameters.Add(new MySqlParameter("PtotalOrden", orden.totalOrden));
            cmd.Parameters.Add(new MySqlParameter("PidUsuario", orden.idUsuario));
            cmd.Parameters.Add(new MySqlParameter("Pobservacion", orden.observacion));
            cmd.Parameters.Add(new MySqlParameter("Pestado", orden.estado));
            cmd.Parameters.Add(new MySqlParameter("PtipoPago", orden.tipoPago));
            cmd.Parameters.Add(new MySqlParameter("Pdscto", orden.Descuento));
            cmd.Parameters.Add(new MySqlParameter("pDescuento", orden.pDescuento));
            cmd.Parameters.Add(new MySqlParameter("pGarantia", orden.pGarantia));
            cmd.Parameters.Add(new MySqlParameter("pExpress", orden.pExpress));
            cmd.Parameters.Add(new MySqlParameter("pDelivery", orden.pDelivery));
            cmd.Parameters.Add(new MySqlParameter("ultimoId", MySqlDbType.Int64));
            cmd.Parameters["ultimoId"].Direction = ParameterDirection.Output;
           
            try
        {

        cmd.ExecuteNonQuery(); // insert second row for update
            return  Convert.ToInt32(cmd.Parameters["ultimoId"].Value);
                }
                catch (MySqlException)
                {

                    return 0;
                    
                }
                finally
                {
                     cmd.Connection.Close();
                     cnx.cerrarConexion();
                }

           
          
          
        }


        public static int Agregarsql(string insert)
        {

            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(insert, cnx.ObtenerConexion());
            comando.CommandType = CommandType.Text;
            retorno = comando.ExecuteNonQuery();
            cnx.cerrarConexion();
            return retorno;



        }



        public static void AgregarLinea(OrdenLinea ordenlinea)
        {
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand cmd = new MySqlCommand("addLineaOrden", cnx.ObtenerConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("PidOrden", ordenlinea.idOrden));
            cmd.Parameters.Add(new MySqlParameter("Pitem", ordenlinea.Item));
            cmd.Parameters.Add(new MySqlParameter("PidPrenda", ordenlinea.idPrenda));
            cmd.Parameters.Add(new MySqlParameter("Pdescripcion", ordenlinea.Descripcion));
            cmd.Parameters.Add(new MySqlParameter("Pcantidad", ordenlinea.Cantidad));
            cmd.Parameters.Add(new MySqlParameter("Pprecio", ordenlinea.Precio));
            cmd.Parameters.Add(new MySqlParameter("Pdefecto", ordenlinea.Defecto));
            cmd.Parameters.Add(new MySqlParameter("Pcolor", ordenlinea.Colores));
            cmd.Parameters.Add(new MySqlParameter("Pmarca", ordenlinea.Marca));
            cmd.Parameters.Add(new MySqlParameter("Ptotal", ordenlinea.Total));
            cmd.Parameters.Add(new MySqlParameter("Ptipo", ordenlinea.TipoServicio));
            cmd.Parameters.Add(new MySqlParameter("Pestado", ordenlinea.Estado));
            
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            cnx.cerrarConexion();
            //return ultimo_id();
        }

        public static int entregaOrden(int id,int pago2,string obs,int delivery) {
            int retorno=1;
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand cmd = new MySqlCommand("entregaOrden", cnx.ObtenerConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("id", id));
            cmd.Parameters.Add(new MySqlParameter("tipopago2", pago2));
            cmd.Parameters.Add(new MySqlParameter("obs", obs));
            cmd.Parameters.Add(new MySqlParameter("pDelivery", delivery));
            cmd.ExecuteReader();
            cnx.cerrarConexion();
            return retorno;
        }

        public static List<OrdenClientes> buscarOrden(string nombre,string dni, string fechainicio,string fechafin,int estado)
        {
            List<OrdenClientes> _lista = new List<OrdenClientes>();
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand cmd = new MySqlCommand("buscarOrdenes", cnx.ObtenerConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("usuario", varGlobales.sessionUsuario));
            cmd.Parameters.Add(new MySqlParameter("nombreCliente", nombre));
            cmd.Parameters.Add(new MySqlParameter("dniCliente", dni));
            cmd.Parameters.Add(new MySqlParameter("fechaInicio", fechainicio));
            cmd.Parameters.Add(new MySqlParameter("fechaFin", fechafin));
            cmd.Parameters.Add(new MySqlParameter("estado", estado));
           
            MySqlDataReader dr = cmd.ExecuteReader();
           
            while (dr.Read())
            {
            OrdenClientes ordencliente= new OrdenClientes();
                ordencliente.idOrden= Convert.ToInt32(dr["idOrden"]);
                ordencliente.idCliente= Convert.ToInt32(dr["idCliente"]);
                ordencliente.nombreCliente= Convert.ToString(dr["nombreCliente"]);
                ordencliente.sucursal = Convert.ToString(dr["sucursal"]);
                ordencliente.dniCliente = Convert.ToString(dr["dniCliente"]);
                ordencliente.fechaCreado = Convert.ToString(dr["fechaCreado"]);
                ordencliente.fechaEntrega = Convert.ToString(dr["fechaEntrega"]);
                ordencliente.pago1 = Convert.ToDecimal(dr["pago1"]);
                ordencliente.pago2 = Convert.ToDecimal(dr["pago2"]);
                ordencliente.Monto= Convert.ToDecimal(dr["totalOrden"]);
                ordencliente.MontoPendiente= Convert.ToDecimal(dr["pago2"]);
                ordencliente.TipoPago = Convert.ToInt32(dr["tipoPago"]);
               _lista.Add(ordencliente);
            }
            cmd.Connection.Close();
            cnx.cerrarConexion();
           
            return _lista;
        }

        public static List<OrdenClientes> buscarOrdenId(int id)
        {
            List<OrdenClientes> _lista = new List<OrdenClientes>();
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand cmd = new MySqlCommand("buscarOrdenesId", cnx.ObtenerConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("id", id));
            cmd.Parameters.Add(new MySqlParameter("usuario", varGlobales.sessionUsuario));
            
            
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                OrdenClientes ordencliente = new OrdenClientes();
                ordencliente.idOrden = Convert.ToInt32(dr["idOrden"]);
                ordencliente.idCliente = Convert.ToInt32(dr["idCliente"]);
                ordencliente.nombreCliente = Convert.ToString(dr["nombreCliente"]);
                ordencliente.sucursal = Convert.ToString(dr["sucursal"]);
                ordencliente.dniCliente = Convert.ToString(dr["dniCliente"]);
                ordencliente.fechaCreado = Convert.ToString(dr["fechaCreado"]);
                ordencliente.fechaEntrega = Convert.ToString(dr["fechaEntrega"]);
                ordencliente.pago1 = Convert.ToDecimal(dr["pago1"]);
                ordencliente.pago2 = Convert.ToDecimal(dr["pago2"]);
                ordencliente.Monto = Convert.ToDecimal(dr["totalOrden"]);
                ordencliente.MontoPendiente = Convert.ToDecimal(dr["pago2"]);
                ordencliente.TipoPago = Convert.ToInt32(dr["tipoPago"]);
                _lista.Add(ordencliente);
            }
            cnx.cerrarConexion();

            return _lista;
        }

        public static List<OrdenClientes> buscarOrdenIdFin(int id)
        {
            List<OrdenClientes> _lista = new List<OrdenClientes>();
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand cmd = new MySqlCommand("buscarOrdenesIdFin", cnx.ObtenerConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("id", id));
            cmd.Parameters.Add(new MySqlParameter("usuario", varGlobales.sessionUsuario));


            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                OrdenClientes ordencliente = new OrdenClientes();
                ordencliente.idOrden = Convert.ToInt32(dr["idOrden"]);
                ordencliente.idCliente = Convert.ToInt32(dr["idCliente"]);
                ordencliente.nombreCliente = Convert.ToString(dr["nombreCliente"]);
                ordencliente.sucursal = Convert.ToString(dr["sucursal"]);
                ordencliente.dniCliente = Convert.ToString(dr["dniCliente"]);
                ordencliente.fechaCreado = Convert.ToString(dr["fechaCreado"]);
                ordencliente.fechaEntrega = Convert.ToString(dr["fechaActualizado"]);
                ordencliente.pago1 = Convert.ToDecimal(dr["pago1"]);
                ordencliente.pago2 = Convert.ToDecimal(dr["pago2"]);
                ordencliente.Monto = Convert.ToDecimal(dr["totalOrden"]);
                ordencliente.MontoPendiente = Convert.ToDecimal(dr["pago2"]);
                ordencliente.TipoPago = Convert.ToInt32(dr["tipoPago"]);
                _lista.Add(ordencliente);
            }
            cnx.cerrarConexion();

            return _lista;
        }
        public static List<OrdenLinea> consultarOrden(int id)
        {
            List<OrdenLinea> _lista = new List<OrdenLinea>();
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand cmd = new MySqlCommand("consultaOrden", cnx.ObtenerConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("id", id));
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                OrdenLinea   ordenlinea = new OrdenLinea();
                ordenlinea.idOrden = Convert.ToInt32(dr["idOrden"]);
                ordenlinea.Item = Convert.ToInt32(dr["item"]);
                ordenlinea.Descripcion = Convert.ToString(dr["descripcion"]);
                ordenlinea.Cantidad = Convert.ToDecimal(dr["cantidad"]);
                ordenlinea.Precio = Convert.ToDecimal(dr["precio"]);
                ordenlinea.Colores = Convert.ToString(dr["colorPrenda"]);
                ordenlinea.Marca = Convert.ToString(dr["marca"]);
                ordenlinea.Defecto = Convert.ToString(dr["defecto"]);
                ordenlinea.Total = Convert.ToDecimal(dr["total"]);
                _lista.Add(ordenlinea);
            }

            cnx.cerrarConexion();

            return _lista;
        }


        public static string consultaOferta(int dia) {
            string nombre="";
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand _comando = new MySqlCommand("ofertasDelDia", cnx.ObtenerConexion());
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.Add(new MySqlParameter("dia", dia));
            MySqlDataReader _reader = _comando.ExecuteReader(CommandBehavior.CloseConnection);
            while (_reader.Read())
            {
                nombre = Convert.ToString(_reader["nombre"]);
            }
            _comando.Connection.Close();
            cnx.cerrarConexion();
            return nombre;
        }


        public static int consultaPendientes(int usuario)
        {
            int id = 0;
            ConexBD cn = new ConexBD();
            cn.Conectar();
            MySqlCommand _comando = new MySqlCommand("pendienteEntregas", cn.ObtenerConexion());
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.Add(new MySqlParameter("USUARIO", usuario));
            MySqlDataReader _reader = _comando.ExecuteReader(CommandBehavior.CloseConnection);
            while (_reader.Read())
            {
                id = Convert.ToInt32(_reader["total"]);
            }
            _comando.Connection.Close();
            cn.cerrarConexion();
            
            return id;
            
          
        }


        public static int ultimo_id()
        {

            int id=0;
            ConexBD cnx = new ConexBD();
            cnx.Conectar();
            MySqlCommand cmd= new MySqlCommand("ultimoIdOrden", cnx.ObtenerConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("usuario",varGlobales.sessionUsuario));
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
              while (dr.Read())
            {
                id = Convert.ToInt32(dr["ultimoid"]);
            }
          
              cnx.cerrarConexion();
            return id;
        }

    }
}
