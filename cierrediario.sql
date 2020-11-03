(SELECT pg.idOrden,c.nombreCliente,o.fechaCreado,o.idUsuario, u.sucursal,pg.pago1 AS pago,IF(tipoPago1=0,'Efectivo','Visa') modoPago,IF(o.Estado=0,'Entrega','Recojo') Movimiento FROM (SELECT * FROM Pago WHERE fechaPago BETWEEN '2017-04-01 00:00:00' AND '2017-04-05 23:00:00'
AND pago1>0) pg
INNER JOIN Orden o ON o.idOrden=pg.idOrden AND o.tipoPago IN(1) AND o.`estado`=0 INNER JOIN usuario u ON o.idUsuario=u.id
INNER JOIN Cliente c ON o.idCliente=c.idCliente AND u.sucursal IN ('LAURELES','FLORATRISTAN')  ORDER BY modoPago)
UNION 
(SELECT pg.idOrden,c.nombreCliente,pg.fechaActualizado AS fechaCreado,o.idUsuario, u.sucursal,pg.pago2 AS pago,IF(tipoPago2=0,'Efectivo','Visa') modoPago,IF(o.Estado=0,'Entrega','Recojo') Movimiento FROM (SELECT * FROM Pago WHERE fechaActualizado BETWEEN '2017-04-10 00:00:00' AND '2017-04-10 23:00:00'
) pg
INNER JOIN Orden o ON o.idOrden=pg.idOrden AND o.tipoPago IN(1,2) AND o.`estado`=1 INNER JOIN usuario u ON o.idUsuario=u.id
INNER JOIN Cliente c ON o.idCliente=c.idCliente AND u.sucursal IN ('LAURELES','FLORATRISTAN') ORDER BY modoPago) ORDER BY sucursal, modopago,idOrden;


SELECT * FROM pago WHERE fechaActualizado BETWEEN '2017-04-10 00:00:00' AND '2017-04-10 23:00:00';

SELECT id, sucursal FROM usuario ORDER BY sucursal;

