using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lavanderia.Persistencia;
using MySql.Data.MySqlClient;
using CrystalDecisions.CrystalReports.Engine;
using Lavanderia.util;


namespace Lavanderia.forms
{
    public partial class frmReporteDetalle : Form
    {
        public frmReporteDetalle()
        {
            InitializeComponent();
        }

        private void fillSusursal()
        {
            chKSucursal.Items.Clear();
            MySqlDataReader _readerS = ServicioDao.fillSucursales();
            while (_readerS.Read())
            {
                string name = _readerS.GetString("sucursal");
                string id = _readerS.GetString("id");
                chKSucursal.Items.Add(name);
                chKSucursal.DisplayMember = name;
                chKSucursal.ValueMember = id;
            }

        }

        private void frmReporteDetalle_Load(object sender, EventArgs e)
        {
            fillSusursal();
        }

        private void btnSrcCliente_Click(object sender, EventArgs e)
        {

            string s = "";
            for (int x = 0; x <= chKSucursal.CheckedItems.Count - 1; x++)
            {
                s += "'" + chKSucursal.CheckedItems[x].ToString() + "',";
            }
            s = s.TrimEnd(',');

            if (s != "")
            {
                ReportDocument cryrep = new ReportDocument();
                string query = "SELECT al.idOrden,UPPER(al.nombreCliente) nombreCliente,al.sucursal,al.fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombrePrenda,al.marca,al.colorPrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Agua') tipoServicio,al.pago1,al.pago2,al.fechaActualizado,al.total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM  (SELECT l.tipoServicio,o.idOrden,c.`nombreCliente`,o.fechaCreado,o.`fechaEntrega`,pr.`nombrePrenda`,l.precio,l.`marca`,l.`colorPrenda`,l.cantidad,total,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,p.pago1,p.pago2,p.`fechaActualizado`,o.totalOrden, o.`tipoPago`,o.`aplicaDscto`, u.`sucursal` FROM Orden o  INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN  Prenda pr ON l.`idPrenda`=pr.`idPrenda`INNER JOIN `Cliente` c ON o.`idCliente`=c.`idCliente` INNER JOIN `usuario` u ON u.`id`=o.`idUsuario` AND u.sucursal IN(" + s + ") INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago=1 AND tipoPago1 IN(0,1)) al WHERE al.fechaCreado BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND  al.tipoServicio=1  UNION ALL SELECT al.idOrden,UPPER(al.nombreCliente) nombreCliente,al.sucursal,al.fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.marca,al.colorPrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Agua') tipoServicio,al.pago1,al.pago2,al.fechaActualizado,al.total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM  (SELECT l.tipoServicio,o.idOrden,c.`nombreCliente`,o.fechaCreado,o.fechaEntrega,pr.`nombreServicio`,l.precio,l.`marca`,l.`colorPrenda`,l.cantidad,total,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,p.`pago1`,p.`pago2`,p.`fechaActualizado`,o.`totalOrden`,o.`tipoPago`,o.`aplicaDscto`,u.`sucursal` FROM Orden o  INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN  Servicio pr ON l.`idPrenda`=pr.`idServicio`INNER JOIN `Cliente` c ON o.`idCliente`=c.`idCliente` INNER JOIN `usuario` u ON u.`id`=o.`idUsuario` AND u.sucursal IN(" + s + ")  INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago IN(1) AND tipoPago1 IN(0,1) AND tipoPago2 IN (0,1)) al WHERE al.fechaCreado BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND  al.tipoServicio=2  UNION ALL SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.marca,al.colorPrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipoServicio,al.pago1,al.pago2,al.fechaActualizado,(al.precio*al.cantidad) total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM  (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.fechaCreado,o.`fechaEntrega`,pr.nombreServicio,l.`marca`,l.`colorPrenda`,l.precio,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,p.`fechaActualizado`,o.`totalOrden`,o.`tipoPago`,o.aplicaDscto,u.`sucursal` FROM Orden o  INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN  Servicio pr ON l.`idPrenda`=pr.`idServicio`INNER JOIN `Cliente` c ON o.`idCliente`=c.`idCliente` INNER JOIN `usuario` u ON u.`id`=o.`idUsuario` AND u.sucursal IN(" + s + ") INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago IN(2) AND tipoPago1 IN(0,1) AND tipoPago2 IN (0,1) AND p.`Estado`=0) al WHERE al.fechaCreado BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND  al.tipoServicio IN(2) UNION ALL SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaActualizado fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombrePrenda,al.marca,al.colorPrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipoServicio,al.pago1,al.pago2,al.fechaActualizado, total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM  (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.`fechaEntrega`,p.fechaActualizado,pr.`nombrePrenda`,l.precio,l.`marca`,l.`colorPrenda`,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,o.`totalOrden`,o.aplicaDscto,o.`tipoPago`,u.`sucursal` FROM Orden o  INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN  Prenda pr ON l.`idPrenda`=pr.`idPrenda`INNER JOIN `Cliente` c ON o.`idCliente`=c.`idCliente` INNER JOIN `usuario` u ON u.`id`=o.`idUsuario` AND u.sucursal IN(" + s + ") INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago IN(2) AND tipoPago1 IN(0,1) AND tipoPago2 IN (0,1) AND p.`Estado` IN(0,1)) al WHERE al.fechaActualizado  BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND  al.tipoServicio IN(1) UNION ALL SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaActualizado fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.marca,al.colorPrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipoServicio,al.pago1,al.pago2,al.fechaActualizado,(al.precio*al.cantidad) total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM  (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.`fechaEntrega`,p.fechaActualizado,pr.nombreServicio,l.`marca`,l.`colorPrenda`,l.precio,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,o.`totalOrden`,o.aplicaDscto,o.`tipoPago`,u.`sucursal` FROM Orden o  INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN  Servicio pr ON l.`idPrenda`=pr.`idServicio`INNER JOIN `Cliente` c ON o.`idCliente`=c.`idCliente` INNER JOIN `usuario` u ON u.`id`=o.`idUsuario` AND u.sucursal IN(" + s + ") INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago=2 AND tipoPago1=0 AND tipoPago2=0  AND p.`Estado`=1) al WHERE al.fechaActualizado BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND  al.tipoServicio IN(2) UNION ALL SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaActualizado fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.marca,al.colorPrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipoServicio,al.pago1,al.pago2,al.fechaActualizado,(al.pago2) total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM  (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.`fechaEntrega`,p.fechaActualizado,pr.nombreServicio,l.`marca`,l.`colorPrenda`,l.precio,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago2=0,'Efectivo','Tarjeta') modoPago,o.`totalOrden`,o.aplicaDscto,o.`tipoPago`,u.`sucursal` FROM Orden o  INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN  Servicio pr ON l.`idPrenda`=pr.`idServicio`INNER JOIN `Cliente` c ON o.`idCliente`=c.`idCliente` INNER JOIN `usuario` u ON u.`id`=o.`idUsuario` AND u.sucursal IN(" + s + ") INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.pago1>0 AND p.tipoPago=2 AND tipoPago1=0 AND tipoPago2 IN(0,1)  AND p.`Estado`=1) al WHERE al.fechaActualizado  BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND  al.tipoServicio IN(2)  ORDER BY idOrden";
                // "SELECT al.idOrden,UPPER(al.nombreCliente) nombreCliente,al.sucursal,al.fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombrePrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Agua') tipoServicio,al.total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM  (SELECT l.tipoServicio,o.idOrden,c.`nombreCliente`,o.fechaCreado,o.`fechaEntrega`,pr.`nombrePrenda`,l.precio,l.cantidad,total,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,o.totalOrden, o.`tipoPago`,o.`aplicaDscto`, u.`sucursal` FROM Orden o  INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN  Prenda pr ON l.`idPrenda`=pr.`idPrenda`INNER JOIN `Cliente` c ON o.`idCliente`=c.`idCliente` INNER JOIN `usuario` u ON u.`id`=o.`idUsuario`  INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago=1 AND tipoPago1 IN(0,1)) al WHERE al.fechaCreado BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND  al.tipoServicio=1  UNION ALL SELECT al.idOrden,UPPER(al.nombreCliente) nombreCliente,al.sucursal,al.fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Agua') tipoServicio,al.total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM  (SELECT l.tipoServicio,o.idOrden,c.`nombreCliente`,o.fechaCreado,o.fechaEntrega,pr.`nombreServicio`,l.precio,l.cantidad,total,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,o.`totalOrden`,o.`tipoPago`,o.`aplicaDscto`,u.`sucursal` FROM Orden o  INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN  Servicio pr ON l.`idPrenda`=pr.`idServicio`INNER JOIN `Cliente` c ON o.`idCliente`=c.`idCliente` INNER JOIN `usuario` u ON u.`id`=o.`idUsuario`  INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago IN(1) AND tipoPago1 IN(0,1) AND tipoPago2 IN (0,1)) al WHERE al.fechaCreado BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND  al.tipoServicio=2  UNION ALL SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipo,(al.pago1) total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM  (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.fechaCreado,o.`fechaEntrega`,pr.nombreServicio,l.precio,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,o.`totalOrden`,o.`tipoPago`,o.aplicaDscto,u.`sucursal` FROM Orden o  INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN  servicio pr ON l.`idPrenda`=pr.`idServicio`INNER JOIN `cliente` c ON o.`idCliente`=c.`idCliente` INNER JOIN `usuario` u ON U.`id`=O.`idUsuario`  INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago IN(2) AND tipoPago1 IN(0,1) AND tipoPago2 IN (0,1) AND p.`Estado`=0) al WHERE al.fechaCreado BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND  al.tipoServicio IN(2) UNION ALL SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaActualizado fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombrePrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipo,(al.pago1+al.pago2) total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM  (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.`fechaEntrega`,p.fechaActualizado,pr.`nombrePrenda`,l.precio,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,o.`totalOrden`,o.aplicaDscto,o.`tipoPago`,u.`sucursal` FROM Orden o  INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN  Prenda pr ON l.`idPrenda`=pr.`idPrenda`INNER JOIN `cliente` c ON o.`idCliente`=c.`idCliente` INNER JOIN `usuario` u ON U.`id`=O.`idUsuario`  INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago IN(2) AND tipoPago1 IN(0,1) AND tipoPago2 IN (0,1) AND p.`Estado`=0) al WHERE al.fechaActualizado BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND  al.tipoServicio IN(1) UNION ALL SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaActualizado fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipo,(al.precio*al.cantidad) total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM  (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.`fechaEntrega`,p.fechaActualizado,pr.nombreServicio,l.precio,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,o.`totalOrden`,o.aplicaDscto,o.`tipoPago`,u.`sucursal` FROM Orden o  INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN  servicio pr ON l.`idPrenda`=pr.`idServicio`INNER JOIN `cliente` c ON o.`idCliente`=c.`idCliente` INNER JOIN `usuario` u ON U.`id`=O.`idUsuario`  INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago=2 AND tipoPago1=0 AND tipoPago2=0  AND p.`Estado`=1) al WHERE al.fechaActualizado BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND  al.tipoServicio IN(2)UNION ALL SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaActualizado fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipo,(al.pago2) total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM  (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.`fechaEntrega`,p.fechaActualizado,pr.nombreServicio,l.precio,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago2=0,'Efectivo','Tarjeta') modoPago,o.`totalOrden`,o.aplicaDscto,o.`tipoPago`,u.`sucursal` FROM Orden o  INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN  servicio pr ON l.`idPrenda`=pr.`idServicio`INNER JOIN `cliente` c ON o.`idCliente`=c.`idCliente` INNER JOIN `usuario` u ON U.`id`=O.`idUsuario`  INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.pago1>0 AND p.tipoPago=2 AND tipoPago1=0 AND tipoPago2=1  AND p.`Estado`=1) al WHERE al.fechaActualizado BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND  al.tipoServicio IN(2) UNION ALL SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaActualizado fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipo,(al.pago1) total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM  (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.`fechaEntrega`,p.fechaActualizado,pr.nombreServicio,l.precio,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago2=0,'Efectivo','Tarjeta') modoPago,o.`totalOrden`,o.aplicaDscto,o.`tipoPago`,u.`sucursal` FROM Orden o  INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden INNER JOIN  servicio pr ON l.`idPrenda`=pr.`idServicio`INNER JOIN `cliente` c ON o.`idCliente`=c.`idCliente` INNER JOIN `usuario` u ON U.`id`=O.`idUsuario`  INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago=2 AND tipoPago1=0 AND tipoPago2=1  AND p.`Estado`=1) al WHERE al.fechaActualizado BETWEEN '" + dtFechaInicial.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + dtFechaFin.Value.ToString("yyyy-MM-dd") + " 23:59:59' AND  al.tipoServicio IN(2) ORDER BY idOrden"
                MySqlDataAdapter myadap = new MySqlDataAdapter(String.Format(query), BdComun.ObtenerConexion());
                DataSet ds = new DataSet();


                myadap.Fill(ds,"dtDetalleOrdenes");


                cryrep.Load(varGlobales.rutaReportes+"\\Reportes\\crReporteDetalle.rpt");

                cryrep.SetDataSource(ds);


                frmReporte rt = new frmReporte();
                rt.Text = "Reporte de Cierre";
                rt.crystalReportViewer1.ReportSource = cryrep;
                rt.Show();﻿
            }
            else
            {
                MessageBox.Show("Debe seleccionar al menos una sucursal", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        
    }
}
