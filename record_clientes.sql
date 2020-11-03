SELECT o.idCliente,u.`sucursal`,c.`nombreCliente`,c.`telefonoCliente`,o.`totalOrden` FROM Orden o INNER JOIN Cliente c ON o.`idCliente`=c.`idCliente` 
INNER JOIN usuario u ON o.`idUsuario`=u.`id`
WHERE o.`fechaCreado` BETWEEN '2018-01-01 00:00:00' AND '2018-01-05 23:59:59' AND o.`estado`=1 ORDER BY totalOrden DESC