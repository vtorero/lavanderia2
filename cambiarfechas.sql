select * from Pago p where idOrden in(select idOrden from Orden o where idUsuario=2 and fechaCreado between '2020-10-20 00:00:00' and '2020-10-20 23:00:00');
select * from Orden o where idUsuario=2 and fechaCreado between '2020-10-20 00:00:00' and '2020-10-20 23:00:00';

update Pago set fechaPago=date_format(fechaPago,'%Y-%m-14 09:02:01'),fechaActualizado=date_format(fechaActualizado,'%Y-%m-14 09:02:01') where idOrden in(select idOrden from Orden o where idUsuario=2 and idOrden between 61499 and 61508);

update Orden set fechaCreado=date_format(fechaCreado,'%Y-%m-1 09:00:01'),fechaEntrega=date_format(fechaEntrega,'%Y-%m-21 09:00:01') where idUsuario=2 and fechaCreado between '2020-10-21 00:00:00' and '2020-10-21 23:00:00';
