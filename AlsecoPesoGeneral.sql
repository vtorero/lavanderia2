/*PAGO TOTAL EFECTIVO Y VISA EN UNA SOLA CUOTA AL SECO*/
SELECT al.idOrden,UPPER(al.nombreCliente) nombreCliente,al.sucursal,al.fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombrePrenda,al.marca,al.colorPrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Agua') tipoServicio,al.pago1,al.pago2,al.fechaActualizado,al.total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM 
 (SELECT l.tipoServicio,o.idOrden,c.`nombreCliente`,o.fechaCreado,o.`fechaEntrega`,pr.`nombrePrenda`,l.precio,l.`marca`,l.`colorPrenda`,l.cantidad,total,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,p.pago1,p.pago2,p.`fechaActualizado`,o.totalOrden, o.`tipoPago`,o.`aplicaDscto`, u.`sucursal` FROM Orden o 
 INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden 
INNER JOIN  Prenda pr ON l.`idPrenda`=pr.`idPrenda`
INNER JOIN `Cliente` c ON o.`idCliente`=c.`idCliente` 
INNER JOIN `usuario` u ON u.`id`=o.`idUsuario` 
 INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago=1 AND tipoPago1 IN(0,1)) al WHERE 
al.fechaCreado BETWEEN '2017-05-26 00:00:00' AND '2017-05-26 23:59:59' AND  al.tipoServicio=1 
 UNION ALL
 /*PAGO TOTAL EFECTIVO Y VISA EN UNA SOLA CUOTA AL AGUA PESO*/
 SELECT al.idOrden,UPPER(al.nombreCliente) nombreCliente,al.sucursal,al.fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.marca,al.colorPrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Agua') tipoServicio,al.pago1,al.pago2,al.fechaActualizado,al.total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM 
 (SELECT l.tipoServicio,o.idOrden,c.`nombreCliente`,o.fechaCreado,o.fechaEntrega,pr.`nombreServicio`,l.precio,l.`marca`,l.`colorPrenda`,l.cantidad,total,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,p.`pago1`,p.`pago2`,p.`fechaActualizado`,o.`totalOrden`,o.`tipoPago`,o.`aplicaDscto`,u.`sucursal` FROM Orden o 
 INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden 
INNER JOIN  Servicio pr ON l.`idPrenda`=pr.`idServicio`
INNER JOIN `Cliente` c ON o.`idCliente`=c.`idCliente` 
INNER JOIN `usuario` u ON u.`id`=o.`idUsuario` 
 INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago IN(1) AND tipoPago1 IN(0,1) AND tipoPago2 IN (0,1)) al WHERE 
al.fechaCreado BETWEEN '2017-05-26 00:00:00' AND '2017-05-26 23:59:59' AND  al.tipoServicio=2 
 UNION ALL
 /*PAGO PARCIAL EFECTIVO Y VISA EN DOS CUOTAS AL PESO AGUA*/
SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.marca,al.colorPrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipoServicio,al.pago1,al.pago2,al.fechaActualizado,(al.pago1) total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM 
 (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.fechaCreado,o.`fechaEntrega`,pr.nombreServicio,l.`marca`,l.`colorPrenda`,l.precio,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,p.`fechaActualizado`,o.`totalOrden`,o.`tipoPago`,o.aplicaDscto,u.`sucursal` FROM Orden o 
 INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden 
INNER JOIN  servicio pr ON l.`idPrenda`=pr.`idServicio`
INNER JOIN `cliente` c ON o.`idCliente`=c.`idCliente` 
INNER JOIN `usuario` u ON U.`id`=O.`idUsuario` 
 INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago IN(2) AND tipoPago1 IN(0,1) AND tipoPago2 IN (0,1) AND p.`Estado`=0) al WHERE 
al.fechaCreado BETWEEN '2017-05-26 00:00:00' AND '2017-05-26 23:59:59' AND  al.tipoServicio IN(2)
 UNION ALL
/*PAGO PARCIAL EFECTIVO Y VISA EN DOS CUOTAS AL SECO*/
 SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaActualizado fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombrePrenda,al.marca,al.colorPrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipoServicio,al.pago1,al.pago2,al.fechaActualizado,(al.pago1+al.pago2) total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM 
 (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.`fechaEntrega`,p.fechaActualizado,pr.`nombrePrenda`,l.precio,l.`marca`,l.`colorPrenda`,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,o.`totalOrden`,o.aplicaDscto,o.`tipoPago`,u.`sucursal` FROM Orden o 
 INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden 
INNER JOIN  Prenda pr ON l.`idPrenda`=pr.`idPrenda`
INNER JOIN `cliente` c ON o.`idCliente`=c.`idCliente` 
INNER JOIN `usuario` u ON U.`id`=O.`idUsuario` 
 INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago IN(2) AND tipoPago1 IN(0,1) AND tipoPago2 IN (0,1) AND p.`Estado` IN(0,1)) al WHERE 
al.fechaActualizado BETWEEN '2017-05-26 00:00:00' AND '2017-05-26 23:59:59' AND  al.tipoServicio IN(1) 
UNION ALL
/*PAGO PARCIAL EFECTIVO Y VISA EN DOS CUOTAS AL PESO AGUA DEVOLUCION*/
 SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaActualizado fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.marca,al.colorPrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipoServicio,al.pago1,al.pago2,al.fechaActualizado,(al.precio*al.cantidad) total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM 
 (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.`fechaEntrega`,p.fechaActualizado,pr.nombreServicio,l.`marca`,l.`colorPrenda`,l.precio,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago1=0,'Efectivo','Tarjeta') modoPago,o.`totalOrden`,o.aplicaDscto,o.`tipoPago`,u.`sucursal` FROM Orden o 
 INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden 
INNER JOIN  servicio pr ON l.`idPrenda`=pr.`idServicio`
INNER JOIN `cliente` c ON o.`idCliente`=c.`idCliente` 
INNER JOIN `usuario` u ON U.`id`=O.`idUsuario` 
 INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago=2 AND tipoPago1=0 AND tipoPago2=0  AND p.`Estado`=1) al WHERE 
al.fechaActualizado BETWEEN '2017-05-26 00:00:00' AND '2017-05-26 23:59:59' AND  al.tipoServicio IN(2)
UNION ALL
/*PAGO PARCIAL SEGUNDA CUOTA EFECTIVO Y VISA  AL PESO AGUA DEVOLUCION*/
 SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaActualizado fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.marca,al.colorPrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipoServicio,al.pago1,al.pago2,al.fechaActualizado,(al.pago2) total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM 
 (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.`fechaEntrega`,p.fechaActualizado,pr.nombreServicio,l.`marca`,l.`colorPrenda`,l.precio,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago2=0,'Efectivo','Tarjeta') modoPago,o.`totalOrden`,o.aplicaDscto,o.`tipoPago`,u.`sucursal` FROM Orden o 
 INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden 
INNER JOIN  servicio pr ON l.`idPrenda`=pr.`idServicio`
INNER JOIN `cliente` c ON o.`idCliente`=c.`idCliente` 
INNER JOIN `usuario` u ON U.`id`=O.`idUsuario` 
 INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.pago1>0 AND p.tipoPago=2 AND tipoPago1=0 AND tipoPago2 IN(0,1)  AND p.`Estado`=1) al WHERE 
al.fechaActualizado BETWEEN '2017-05-26 00:00:00' AND '2017-05-26 23:59:59' AND  al.tipoServicio IN(2) 
UNION ALL
 SELECT al.idOrden,al.nombreCliente,al.sucursal,al.fechaActualizado fechaCreado,SUBSTRING(al.fechaEntrega,1,10) fechaEntrega,al.nombreServicio nombrePrenda,al.marca,al.colorPrenda,al.precio,al.cantidad,IF(al.tipoServicio=1, 'Al seco','Al Peso') tipoServicio,al.pago1,al.pago2,al.fechaActualizado,(al.pago1) total,al.modoPago,al.totalOrden,al.tipoPago coutas,al.aplicaDscto FROM 
 (SELECT l.`item`,l.tipoServicio,o.idOrden,c.`nombreCliente`,o.`fechaEntrega`,p.fechaActualizado,pr.nombreServicio,l.`marca`,l.`colorPrenda`,l.precio,l.cantidad,total,p.pago1,p.pago2,IF(tipoPago2=0,'Efectivo','Tarjeta') modoPago,o.`totalOrden`,o.aplicaDscto,o.`tipoPago`,u.`sucursal` FROM Orden o 
 INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden 
INNER JOIN  servicio pr ON l.`idPrenda`=pr.`idServicio`
INNER JOIN `cliente` c ON o.`idCliente`=c.`idCliente` 
INNER JOIN `usuario` u ON U.`id`=O.`idUsuario` 
 INNER JOIN Pago p ON o.idOrden=p.idOrden WHERE p.tipoPago=2 AND tipoPago1=0 AND tipoPago2 IN(0,1)  AND p.`Estado`=1) al WHERE 
al.fechaActualizado BETWEEN '2017-05-26 00:00:00' AND '2017-05-26 23:59:59' AND  al.tipoServicio IN(2) ORDER BY idOrden;
 