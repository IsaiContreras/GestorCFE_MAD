use [GestorCFE]

go
if exists (select 1 from sysobjects where name = 'delete_Empleado' and type = 'tr')
drop trigger delete_Empleado;
go
create trigger delete_Empleado on Empleados instead of delete as begin
	declare @num_emp int;
	select @num_emp = Num_Empleado from deleted;
	update Empleados set
		Activo = 0
	where Num_Empleado = @num_emp;
end
go
if exists (select 1 from sysobjects where name = 'insert_Cliente' and type = 'tr')
drop trigger insert_Cliente;
go
create trigger insert_Cliente on Clientes after insert as begin
	declare @id_cl bigint;
	declare @num_emp int;
	declare @desc varchar(512);
	select @id_cl = ID_Cliente from inserted;
	select @num_emp = Num_Empleado from inserted;
	select @desc = 'Empleado con número ' + convert(varchar, E.Num_Empleado) + ', ' + E.Nombres + ' ' + E.Apellidos + ', registró a cliente con ID ' + convert(varchar, C.ID_Cliente)  + ', ' + C.Nombres + ' ' + C.Apellidos + '.'
	from inserted C left outer join Empleados E on E.Num_Empleado = C.Num_Empleado
	where C.ID_Cliente = @id_cl;
	exec sp_RegistroActividad 'insert', null, @num_emp, 'Registro Cliente', @desc;
end

go
if exists (select 1 from sysobjects where name = 'delete_Cliente' and type = 'tr')
drop trigger delete_Cliente;
go
create trigger delete_Cliente on Clientes instead of delete as begin
	declare @id_cl bigint;
	select @id_cl = ID_Cliente from deleted;
	update Clientes set
		Activo = 0
	where ID_Cliente = @id_cl;
	update Servicios set
		Activo = 0
	where ID_Cliente = @id_cl;
end

go
if exists (select 1 from sysobjects where name = 'insert_Servicio' and type = 'tr')
drop trigger insert_Servicio;
go
create trigger insert_Servicio on Servicios after insert as begin
	declare @id_serv bigint;
	declare @num_emp int;
	declare @desc varchar(512);
	select @id_serv = ID_Servicio from inserted;
	select @num_emp = Num_Empleado from inserted;
	select @desc = 'Empleado con número ' + convert(varchar, E.Num_Empleado) + ', ' + E.Nombres + ' ' + E.Apellidos + ', registró un servicio ' + case S.Tipo_servicio when 0 then 'doméstrico' when 1 then 'industrial' end + ' con ID ' + convert(varchar, S.ID_Servicio) + ' para cliente con ID ' + convert(varchar, C.ID_Cliente)  + ', ' +  C.Nombres + ' ' + C.Apellidos + '.'
	from inserted S left outer join Empleados E on E.Num_Empleado = S.Num_Empleado left outer join Clientes C on C.ID_Cliente = S.ID_Cliente
	where S.ID_Servicio = @id_serv;
	exec sp_RegistroActividad 'insert', null, @num_emp, 'Registro Servicio', @desc;
end

go
if exists (select 1 from sysobjects where name = 'delete_Servicio' and type = 'tr')
drop trigger delete_Servicio;
go
create trigger delete_Servicio on Servicios instead of delete as begin
	declare @id_serv bigint;
	select @id_serv = ID_Servicio from deleted;
	update Servicios set
		Activo = 0
	where ID_Servicio = @id_serv;
end
go
if exists (select 1 from sysobjects where name = 'insert_Tarifa' and type = 'tr')
drop trigger insert_Tarifa;
go
create trigger insert_Tarifa on Tarifas after insert as begin
	declare @id_tar int;
	declare @num_emp int;
	declare @desc varchar(512);
	select @id_tar = ID_Tarifa from inserted;
	select @num_emp = Num_Empleado from inserted;
	select @desc = 'Empleado con número ' + convert(varchar, E.Num_Empleado) + ', ' + E.Nombres + ' ' + E.Apellidos + ', registró tarifa con ID ' + convert(varchar, T.ID_Tarifa) + '.'
	from inserted T left outer join Empleados E on E.Num_Empleado = T.Num_Empleado
	where T.ID_Tarifa = @id_tar;
	exec sp_RegistroActividad 'insert', null, @num_emp, 'Registro Tarifa', @desc;
end

go
if exists (select 1 from sysobjects where name = 'insert_Consumo' and type = 'tr')
drop trigger insert_Consumo;
go
create trigger insert_Consumo on Consumos after insert as begin
	declare @id_cons bigint;
	declare @num_emp int;
	declare @desc varchar(512);
	select @id_cons = ID_Consumo from inserted;
	select @num_emp = Num_Empleado from inserted;
	select @desc = 'Empleado con número ' + convert(varchar(12), E.Num_Empleado) + ', ' + E.Nombres + ' ' + E.Apellidos + ' registró Consumo con ID ' + convert(varchar(12), C.ID_Consumo) + '.'
	from inserted C left outer join Empleados E on E.Num_Empleado = C.Num_Empleado
	where C.ID_Consumo = @id_cons;
	exec sp_RegistroActividad 'insert', null, @num_emp, 'Registro Conusmo', @desc;
end

go
if exists (select 1 from sysobjects where name = 'insert_Recibo' and type = 'tr')
drop trigger insert_Recibo;
go
create trigger insert_Recibo on Recibos after insert as begin
	declare @id_rec bigint;
	declare @num_emp int;
	declare @desc varchar(256);
	select @id_rec = ID_Recibo from inserted;
	select @num_emp = Num_Empleado from inserted;
	select @desc = 'Empleado con número ' + convert(varchar(12), E.Num_Empleado) + ', ' + E.Nombres + ' ' + E.Apellidos + ' registró Recibo con ID ' + convert(varchar(12), R.ID_Recibo) + '.'
	from inserted R left outer join Empleados E on E.Num_Empleado = R.Num_Empleado
	where R.ID_Recibo = @id_rec;
	exec sp_RegistroActividad 'insert', null, @num_emp, 'Registro Recibo', @desc;
end