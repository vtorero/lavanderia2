(SELECT pg.idOrden,UPPER(c.nombreCliente) nombreCliente,SUBSTRING(o.fechaCreado,1,10) AS fechaCreado,pg.fechaPago,o.idUsuario, u.sucursal,pg.pagoTotal,pg.pago1 AS pago,
 IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,IF(o.Estado=0,'Entrega','Recojo') Movimiento FROM 
 (SELECT * FROM Pago WHERE fechaPago BETWEEN '2017-08-05 00:00:00' AND '2017-08-05 23:59:59' 
 AND pago1>0) pg INNER JOIN Orden o ON o.idOrden=pg.idOrden AND o.tipoPago IN(1) AND o.`estado` IN(0,1) INNER JOIN usuario u ON o.idUsuario=u.id 
 INNER JOIN Cliente c ON o.idCliente=c.idCliente AND u.id=4 ORDER BY modoPago) 
 UNION ALL 
 (SELECT pg.idOrden,UPPER(c.nombreCliente) nombreCliente,SUBSTRING(o.fechaCreado,1,10) AS fechaCreado,pg.fechaActualizado AS fechaPago,o.idUsuario, u.sucursal,pg.pagoTotal,pg.pago1 AS pago,IF(pg.tipoPago1=0,'Efectivo','Tarjeta') modoPago,
 IF(o.Estado=0,'Entrega','Recojo') Movimiento FROM (SELECT * FROM Pago WHERE fechaPago BETWEEN '2017-08-05 00:00:00' AND '2017-08-05 23:59:59' 
 ) pg INNER JOIN Orden o ON o.idOrden=pg.idOrden AND o.tipoPago IN(2) AND o.`estado` IN(0,1) INNER JOIN usuario u ON o.idUsuario=u.id 
 INNER JOIN Cliente c ON o.idCliente=c.idCliente AND u.id=4 ORDER BY modoPago) 
   UNION ALL 
 (SELECT pg.idOrden,UPPER(c.nombreCliente) nombreCliente,SUBSTRING(o.fechaCreado,1,10) AS fechaCreado,pg.fechaActualizado AS fechaPago,o.idUsuario, u.sucursal,pg.pagoTotal, pg.pago2 AS pago ,IF(pg.tipoPago2=0,'Efectivo','Tarjeta') modoPago, 
 IF(o.Estado=0,'Entrega','Recojo') Movimiento FROM (SELECT * FROM Pago WHERE fechaActualizado BETWEEN '2017-08-05 00:00:00' AND '2017-08-05 23:59:59' 
  ) pg INNER JOIN Orden o ON o.idOrden=pg.idOrden AND o.tipoPago IN(2) AND o.`estado` IN(1) INNER JOIN usuario u ON o.idUsuario=u.id 
   INNER JOIN Cliente c ON o.idCliente=c.idCliente AND u.id=4 ORDER BY modoPago) ORDER BY modopago,idOrden