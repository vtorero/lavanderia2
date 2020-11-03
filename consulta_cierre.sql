(SELECT pg.idOrden,c.nombreCliente,o.fechaCreado,o.idUsuario, UPPER(u.sucursal) sucursal,pg.pago1 AS pago,
IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,IF(o.Estado=0,'Entrega','Recojo') Movimiento FROM 
(SELECT * FROM Pago WHERE fechaPago BETWEEN '2017-05-01 00:00:00' AND '2017-05-08 23:59:59' 
          AND pago1>0) pg INNER JOIN Orden o ON o.idOrden=pg.idOrden AND o.tipoPago IN(1) AND o.`estado`=0 INNER JOIN usuario u ON o.idUsuario=u.id 
          INNER JOIN Cliente c ON o.idCliente=c.idCliente AND u.id IN(1,2) ORDER BY modoPago) UNION ALL
(SELECT pg.idOrden,c.nombreCliente,pg.fechaActualizado AS fechaCreado,o.idUsuario, UPPER(u.sucursal) sucursal,pg.pago2 AS pago,IF(pg.tipoPago2=0,'Efectivo','Tarjeta') modoPago,
IF(o.Estado=0,'Entrega','Recojo') Movimiento FROM (SELECT * FROM Pago WHERE fechaActualizado BETWEEN '2017-01-08 00:00:00' AND '2017-05-08 23:59:59' 
 ) pg INNER JOIN Orden o ON o.idOrden=pg.idOrden AND o.tipoPago IN(2) AND o.`estado` IN(0,1) INNER JOIN usuario u ON o.idUsuario=u.id 
  INNER JOIN Cliente c ON o.idCliente=c.idCliente AND u.id IN(1,2) ORDER BY modoPago) ORDER BY modopago,idOrden;