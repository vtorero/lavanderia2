SELECT o.idOrden,sum(total) FROM Orden o inner join OrdenLinea l on o.idOrden=l.idOrden
inner join Pago p on o.idOrden=p.idOrden where p.tipoPago in(1,2) and tipoPago1=0 and tipoPago2=0 and l.tipoServicio=2;

-- EFECTIVO AL SECO----
SELECT al.idOrden,al.fechaCreado,if(al.tipoServicio=1, 'Al seco','Al Peso') tipo,al.pago1,al.pago2,(al.precio*al.cantidad) total from (SELECT l.tipoServicio,o.idOrden,o.fechaCreado,l.precio,l.cantidad,total,p.pago1,p.pago2 FROM Orden o inner join OrdenLinea l on o.idOrden=l.idOrden
inner join Pago p on o.idOrden=p.idOrden where p.tipoPago in(1,2) and tipoPago1 in(0,1) and tipoPago2 in (0,1)) al where al.tipoServicio in(1,2);

-- VISA  AL SECO--
SELECT al.idOrden,SUM(al.pago1) total from (SELECT o.idOrden,p.pago1,p.pago2,sum(total) FROM Orden o inner join OrdenLinea l on o.idOrden=l.idOrden
inner join Pago p on o.idOrden=p.idOrden where p.tipoPago in(1) and tipoPago1=1 and tipoPago2=0
group by 1) al;
