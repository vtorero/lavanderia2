DROP PROCEDURE IF EXISTS addLineaOrden; 
DROP PROCEDURE IF EXISTS addPago; 
DROP PROCEDURE IF EXISTS addEgreso; 
DROP PROCEDURE IF EXISTS addOrden; 
DROP PROCEDURE IF EXISTS ultimoIdOrden; 
DROP PROCEDURE IF EXISTS clientesAll;	
DROP PROCEDURE IF EXISTS buscarOrdenes;	
DROP PROCEDURE IF EXISTS entregaOrden;	
DROP PROCEDURE IF EXISTS coloresAll;
DROP PROCEDURE IF EXISTS serviciosAll;
DROP PROCEDURE IF EXISTS prendasSearch;
DROP PROCEDURE IF EXISTS prendasAll;
DROP PROCEDURE IF EXISTS prendasTipo;
DROP PROCEDURE IF EXISTS marcasAll;
DROP PROCEDURE IF EXISTS insertaMarca;
DROP PROCEDURE IF EXISTS consultaOrden;
DROP PROCEDURE IF EXISTS consultaPago;
DROP PROCEDURE IF EXISTS modificaPago;
DROP PROCEDURE IF EXISTS modificaEstado;
DROP PROCEDURE IF EXISTS buscarOrdenesId;
DROP PROCEDURE IF EXISTS buscarOrdenesIdFin;
DROP PROCEDURE IF EXISTS pendienteEntregas;
DROP PROCEDURE IF EXISTS ofertasDelDia;
DROP PROCEDURE IF EXISTS consultaUsuario;
DELIMITER $$
CREATE PROCEDURE addOrden(
IN PidCliente INT,
IN PfechaEntrega VARCHAR(60),
IN PtotalOrden DECIMAL(10,2),
IN PidUsuario INT,
IN Pobservacion VARCHAR(200),
IN Pestado INT,
IN PtipoPago INT,
IN Pdscto INT,
IN pDescuento DECIMAL(10,2),
IN pGarantia INT,
IN pExpress INT,
IN pDelivery INT,
OUT ultimoId INT)
BEGIN
START TRANSACTION;
INSERT INTO Orden (idCliente,fechaEntrega,totalOrden,idUsuario, Observacion, estado, tipoPago,aplicaDscto,descuento,garantia,express,delivery) VALUES 
(PidCliente,PfechaEntrega,PtotalOrden,PidUsuario,Pobservacion,Pestado,PtipoPago,Pdscto,pDescuento,pGarantia,pExpress,pDelivery);
SELECT LAST_INSERT_ID() INTO ultimoId;
COMMIT;
END $$

DELIMITER $$
CREATE PROCEDURE pendienteEntregas(
IN USUARIO INT
)
BEGIN
SELECT COUNT(*) total FROM Orden o WHERE SUBSTRING(o.fechaEntrega,1,10)=SUBSTRING(NOW(),1,10) AND o.estado=0 AND o.`idUsuario`=USUARIO;
END $$

DELIMITER $$
CREATE PROCEDURE ofertasDelDia(
IN dia VARCHAR(1)
)
BEGIN
SELECT * FROM oferta  WHERE diasOferta LIKE CONCAT('%', dia , '%') AND estado=1;
END $$

DELIMITER $$
CREATE PROCEDURE consultaUsuario(
IN USUARIO VARCHAR(30),
IN PASS VARCHAR(40)
)
BEGIN
SELECT * FROM usuario WHERE nombre=USUARIO AND PASSWORD=PASS;
END $$


DELIMITER $$
CREATE PROCEDURE addLineaOrden(
IN PidOrden INT ,
IN Pitem INT, 
IN PidPrenda INT, 
IN Pdescripcion VARCHAR(200),
IN Pcantidad DECIMAL(10,2),
IN Pprecio DECIMAL(10,2),
 IN Pdefecto VARCHAR(200),
 IN Pcolor VARCHAR(200),
IN Pmarca VARCHAR(100),
 IN Ptotal DECIMAL(10,2),
 IN Ptipo INT,
 IN Pestado INT)
BEGIN
START TRANSACTION;
INSERT INTO OrdenLinea(idOrden,item,idPrenda,Descripcion,cantidad,precio,defecto,colorPrenda,marca,total,tipoServicio,estado)
VALUES(PidOrden,Pitem,PidPrenda,Pdescripcion,Pcantidad,Pprecio,Pdefecto,Pcolor,Pmarca,Ptotal,Ptipo,Pestado);
COMMIT;
END $$

DELIMITER $$
CREATE PROCEDURE addPago(
IN PidOrden INT ,
IN pPago1 DECIMAL(10,2),
IN pPago2 DECIMAL(10,2),
IN pPagoTotal DECIMAL(10,2), 
IN pTipoPago INT,
IN pTipoPago1 INT,
IN pTipoPago2 INT,
IN ptipoDoc INT,
IN pIgv DECIMAL(10,2),
IN pEstado DECIMAL(10,2)
)
BEGIN
START TRANSACTION;
INSERT INTO Pago (idOrden, pago1,pago2,pagoTotal,tipoPago,tipoPago1,tipoDocumento,igv,Estado,fechaPago,fechaActualizado) 
VALUES (PidOrden,pPago1,pPago2,pPagoTotal,pTipoPago,pTipoPago1,pTipoPago2,ptipoDoc,pIgv,pEstado);
COMMIT;
END $$

DELIMITER $$
CREATE PROCEDURE addEgreso(
IN pIdUsuario DECIMAL(10,2),
IN pMonto DECIMAL(10,2),
IN pFecha VARCHAR(20), 
IN pMotivo VARCHAR(500), 
IN pEstado INT
)
BEGIN
START TRANSACTION;
INSERT INTO egresos (idUsuario,monto,fechaEgreso,motivo,estado) 
VALUES (pIdUsuario,pMonto,pFecha,pMotivo,pEstado);
COMMIT;
END $$

DELIMITER $$
CREATE PROCEDURE ultimoIdOrden(IN usuario INT)
BEGIN
SELECT MAX(idOrden) AS ultimoid FROM Orden WHERE idUsuario=usuario;
END $$
DELIMITER $$
CREATE PROCEDURE clientesAll(IN  idUsuario INT)
BEGIN
SELECT * FROM Cliente WHERE usuarioCreador=idUsuario ORDER BY idCliente ASC;
END $$
DELIMITER $$
CREATE PROCEDURE coloresAll()
BEGIN
SELECT idColor,UPPER(nombreColor) nombreColor,UPPER(valorColor) valorColor FROM Color ORDER BY nombreColor ASC;
END $$
DELIMITER $$
CREATE PROCEDURE serviciosAll()
BEGIN
SELECT * FROM Servicio ORDER BY nombreServicio ASC;
END $$
DELIMITER $$
CREATE PROCEDURE prendasAll()
BEGIN
SELECT * FROM Prenda ORDER BY nombrePrenda ASC;
END $$
DELIMITER $$
CREATE PROCEDURE prendasTipo()
BEGIN
SELECT * FROM tipo_prenda ORDER BY descripcion ASC;
END $$
DELIMITER $$
CREATE PROCEDURE marcasAll()
BEGIN
SELECT idMarca,`nombreMarca` FROM Marca ORDER BY nombreMarca ASC;
END $$
DELIMITER $$
CREATE PROCEDURE buscarOrdenes(
IN usuario INT,
IN nombreCliente VARCHAR(200),
IN dniCliente VARCHAR(8),
fechaInicio VARCHAR(20),
fechaFin VARCHAR(20),
IN estado INT
)
BEGIN
IF(usuario<>1) THEN
SELECT * FROM Orden o INNER JOIN Cliente c ON o.idCliente=c.idCliente INNER JOIN Pago p ON o.idOrden=p.idOrden INNER JOIN usuario u ON u.id=o.idUsuario
WHERE (fechaEntrega BETWEEN fechaInicio AND fechaFin AND o.estado in(0,1,2) AND o.idUsuario=usuario) AND (c.nombreCliente LIKE nombreCliente AND o.idUsuario=usuario);
END IF;
IF(usuario=1) THEN
SELECT * FROM Orden o INNER JOIN Cliente c ON o.idCliente=c.idCliente INNER JOIN Pago p ON o.idOrden=p.idOrden INNER JOIN usuario u ON u.id=o.idUsuario
WHERE (fechaCreado BETWEEN fechaInicio AND fechaFin AND o.estado IN(0,1,2)) AND (c.nombreCliente LIKE nombreCliente);
END IF;
END $$

DELIMITER $$
CREATE PROCEDURE buscarOrdenesId(
IN id INT,
IN usuario INT
)
BEGIN
IF(usuario<>1) THEN
SELECT * FROM Orden o INNER JOIN Cliente c ON o.idCliente=c.idCliente INNER JOIN Pago p ON o.idOrden=p.idOrden INNER JOIN usuario u ON u.id=o.idUsuario  WHERE o.idUsuario=usuario AND o.estado=0 AND o.idOrden=id;
END IF;
IF(usuario=1) THEN
SELECT * FROM Orden o INNER JOIN Cliente c ON o.idCliente=c.idCliente INNER JOIN Pago p ON o.idOrden=p.idOrden INNER JOIN usuario u ON u.id=o.idUsuario  WHERE o.idOrden=id;
END IF;
END $$

DELIMITER $$
CREATE PROCEDURE buscarOrdenesIdFin(
IN id INT,
IN usuario INT
)
BEGIN
IF(usuario<>1) THEN
SELECT * FROM Orden o INNER JOIN Cliente c ON o.idCliente=c.idCliente INNER JOIN Pago p ON o.idOrden=p.idOrden INNER JOIN usuario u ON u.id=o.idUsuario  WHERE o.idUsuario=usuario AND o.estado in(0,1,2) AND o.idOrden=id;
END IF;
IF(usuario=1) THEN
SELECT * FROM Orden o INNER JOIN Cliente c ON o.idCliente=c.idCliente INNER JOIN Pago p ON o.idOrden=p.idOrden INNER JOIN usuario u ON u.id=o.idUsuario  WHERE o.idOrden=id;
END IF;
END $$

DELIMITER $$
CREATE PROCEDURE consultaOrden(
IN id INT
)
BEGIN
SELECT o.idOrden,l.item,c.nombreCliente,o.fechaCreado,o.fechaEntrega, o.totalOrden,l.cantidad,l.precio,l.descripcion,l.total,l.colorPrenda,l.marca,l.defecto,p.pago1,p.pago2,p.tipoPago,p.tipoPago1,p.tipoPago2,p.fechaActualizado FROM Orden o INNER JOIN Cliente c ON o.idCliente=c.idCliente INNER JOIN Pago p ON o.idOrden=p.idOrden INNER JOIN OrdenLinea l ON o.idOrden=l.idOrden WHERE o.idOrden=id;

END $$
DELIMITER $$
CREATE PROCEDURE consultaPago(
IN id INT
)
BEGIN
SELECT * FROM Pago p WHERE p.idOrden=id;

END $$


DELIMITER $$
CREATE PROCEDURE prendasSearch(
IN criterio VARCHAR(100)
)
BEGIN
SELECT idPrenda,UPPER(nombrePrenda) AS nombrePrenda,precioServicio FROM Prenda WHERE nombrePrenda=criterio;
END $$
DELIMITER $$
CREATE PROCEDURE entregaOrden(
IN id INT,
IN tipopago2 INT,
IN obs VARCHAR(200),
IN pDelivery INT
)
BEGIN
START TRANSACTION;
UPDATE Pago SET Estado=1,tipoPago2=tipopago2,Observacion=obs,fechaActualizado=NOW() WHERE idOrden=id;
UPDATE Orden SET estado=1 ,delivery=Pdelivery WHERE idOrden=id;
UPDATE OrdenLinea SET estado=1 WHERE idOrden=id;
COMMIT;
END $$

DELIMITER $$
CREATE PROCEDURE modificaPago(
IN id INT,
IN pago1 INT,
IN pago2 INT

)
BEGIN
START TRANSACTION;
UPDATE Pago SET tipoPago1=pago1,tipoPago2=pago2,Observacion="cambio tipo pago" WHERE idPago=id;
COMMIT;
END $$

DELIMITER $$
CREATE PROCEDURE modificaEstado(
IN id INT,
IN ESTADO INT

)
BEGIN
START TRANSACTION;
UPDATE Orden SET estado=ESTADO WHERE idOrden=id;
COMMIT;
END $$

DELIMITER $$
CREATE PROCEDURE insertaMarca(
IN nombre VARCHAR(100)
)
BEGIN
DECLARE cantidad INT;
SELECT COUNT(*) INTO cantidad FROM Marca WHERE nombreMarca=nombre;
IF (cantidad<1) THEN
INSERT INTO Marca (nombreMarca) VALUES(nombre);
END IF;
END $$


/*---reportes---

SELECT YEAR(o.`fechaCreado`), MONTHNAME(o.`fechaCreado`) ,DAYOFMONTH(o.`fechaCreado`) ,SUM(o.`totalOrden`) FROM Orden o GROUP BY 1,2,3;*/