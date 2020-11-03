UPDATE Orden SET estado=3 WHERE idOrden between 61159 and 61159;
UPDATE Pago SET estado=3 WHERE idOrden between 61159 and 61159;
UPDATE OrdenLinea SET estado=3  WHERE idOrden between 61159 and 61159;

select * from Orden where idOrden in(60806,54418);
select * from Pago where idOrden in(60806,54418);