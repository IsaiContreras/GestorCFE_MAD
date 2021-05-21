use [GestorCFE];

-- // n - not_exists | w - wrong_pass | b - blocked_user | l - logged
go
if exists (select 1 from sysobjects where name = 'f_loginUser' and type = 'fn')
	drop function f_loginUser;
go
create function f_loginUser(
	@type				char = null,
	@correo_e			varchar(60) = null,
	@contra				char(8) = null
) returns char
as begin
	declare @state char = 'n';
	if @type = 'A' begin
		if exists (select 1 from Administrador where Correo_electrónico = @correo_e) begin
			if (select Contraseña from Administrador where Correo_electrónico = @correo_e) = @contra
				set @state = 'l';
			else set @state = 'w'
		end
	end
	if @type = 'E' begin
		if exists (select 1 from Empleados where Correo_electrónico = @correo_e) begin
			if (select Bloqueo from Empleados where Correo_electrónico = @correo_e) = 1
				set @state = 'b';
			else if (select Contraseña from Empleados where Correo_electrónico = @correo_e) = @contra
				set @state = 'l';
			else set @state = 'w';
		end
	end
	if @type = 'C' begin
		if exists (select 1 from Clientes where Correo_electrónico = @correo_e) begin
			if (select Bloqueo from Clientes where Correo_electrónico = @correo_e) = 1
				set @state = 'b';
			else if (select Contraseña from Clientes where Correo_electrónico = @correo_e) = @contra
				set @state = 'l';
			else set @state = 'w';
		end
	end
	return @state;
end