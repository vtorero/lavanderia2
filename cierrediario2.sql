SELECT * FROM `orden` WHERE idOrden=550 ORDER BY idOrden DESC;
SELECT * FROM OrdenLinea WHERE idOrden=595;
SELECT * FROM pago WHERE ;
SELECT * FROM usuario;


SELECT * FROM Pago WHERE fechaPago BETWEEN '2017-04-10 00:00:00' AND '2017-04-10 23:00:00';


SELECT pa.idOrden,ANO,MES,DIA,SUM(PA) TOTAL FROM ((SELECT o.idOrden ,o.`idUsuario`,YEAR(fechaPago) ANO,
DATE_FORMAT(fechaPago,'%M') MES,
DAY(fechaPago) DIA,pago1 AS pa FROM Pago p, Orden o WHERE p.`idOrden`=o.`idOrden` AND o.`tipoPago`=1 AND fechaPago BETWEEN '2016-04-01 00:00:00' AND '2017-04-10 23:00:00' ORDER BY fechaPago)
UNION ALL 
(SELECT o.idOrden ,o.`idUsuario`,YEAR(fechaPago) ANO,
DATE_FORMAT(fechaPago,'%M') MES,
DAY(fechaPago) DIA,pago2 AS pa FROM Pago p, Orden o WHERE p.`idOrden`=o.`idOrden` AND o.`tipoPago`=2 AND fechaPago BETWEEN '2016-04-01 00:00:00' AND '2017-04-10 23:00:00' ORDER BY fechaPago)) PA GROUP BY MES,DIA ORDER BY ANO,MES ASC ;  



