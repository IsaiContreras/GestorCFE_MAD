use [GestorCFE];

go
if exists (select 1 from sysobjects where name = 'sp_Logins' and type = 'p')
	drop procedure sp_Logins;
go
create procedure sp_Logins(
	@type				char = null,
	@correo_e			varchar(60) = null,
	@contra				char(8) = null
) as begin
	declare @logged char;
	if @type = 'A' begin
		set @logged = dbo.f_loginUser('A', @correo_e, @contra);
		if @logged = 'l'
			select
				ID_Admin,
				Nombre_usuario
			from Administrador
			where Correo_electrónico = @correo_e and Contraseña = @contra;
		else if @logged = 'w' select -1 [result];
		else select -3 [result];
	end
	if @type = 'E' begin
		set @logged = dbo.f_loginUser('E', @correo_e, @contra);
		if @logged = 'l'
			select
				[Número de emplado],
				Nombres + ' ' + Apellidos [Nombre]
			from v_DetallesEmpleados
			where [Correo electrónico] = @correo_e and Contraseña = @contra;
		else if @logged = 'w' select -1 [result];
		else if @logged = 'b' select -2 [result];
		else select -3 [result];
	end
	if @type = 'C' begin
		set @logged = dbo.f_loginUser('C', @correo_e, @contra);
		if @logged = 'l'
			select
				ID,
				Nombres + ' ' + Apellidos [Nombre]
			from v_DetallesClientes
			where [Correo electrónico] = @correo_e and Contraseña = @contra;
		else if @logged = 'w' select -1 [result];
		else if @logged = 'b' select -2 [result];
		else select -3 [result];
	end
end

go
if exists (select 1 from sysobjects where name = 'sp_Bloq' and type = 'p')
	drop procedure sp_Bloq;
go
create procedure sp_Bloq(
	@type				char = null,
	@correo_e			varchar(60) = null,
	@lock				bit = null
)as begin
	if @type = 'E' begin
		if @lock = 1 
			update Empleados set Bloqueo = 1 where Correo_electrónico = @correo_e;
		else update Empleados set Bloqueo = 0 where Correo_electrónico = @correo_e;
	end
	if @type = 'C' begin
		if @lock = 1
			update Clientes set Bloqueo = 1 where Correo_electrónico = @correo_e;
		else update Clientes set Bloqueo = 0 where Correo_electrónico = @correo_e;
	end
end

go
if exists (select 1 from sysobjects where name = 'sp_Admin' and type = 'p')
	drop procedure sp_Admin;
go
create procedure sp_Admin (
	@proc				varchar(16) = null,
	@id_adm				smallint = null,
	@correo_e			varchar(60) = null,
	@contra				char(8) = null,
	@nom_us				varchar(30) = null
) as begin
	if @proc = 'insert' begin
		if not exists (select 1 from Administrador where Correo_electrónico = @correo_e)
			insert into Administrador (Nombre_usuario, Correo_electrónico, Contraseña)
			values (@nom_us, @correo_e, @contra);
		else select 'user_exists' [result];
	end
	if @proc = 'update' begin
		if not exists (select 1 from Administrador where Correo_electrónico = @correo_e)
			update Administrador set
				Nombre_usuario = isnull(@nom_us, Nombre_usuario),
				Correo_electrónico = isnull(@correo_e, Correo_electrónico),
				Contraseña = isnull(@contra, Contraseña)
			where ID_Admin = @id_adm;
		else select 'user_exists' [result];
	end
	if @proc = 'searchbyid' begin
		select ID_Admin [ID], Nombre_usuario [Nombre de usuario], Correo_electrónico [Correo electrónico]
		from Administrador where ID_Admin = @id_adm;
	end
	if @proc = 'showall' begin
		select ID_Admin [ID], Nombre_usuario [Nombre de usuario], Correo_electrónico [Correo electrónico]
		from Administrador;
	end
	if @proc = 'delete' begin
		delete from Administrador where ID_Admin = @id_adm;
	end
end

go
if exists (select 1 from sysobjects where name = 'sp_Empleados' and type = 'p')
	drop procedure sp_Empleados;
go
create procedure sp_Empleados (
	@proc				varchar(16) = null,
	@num_emp			int = null,
	@id_adm				smallint = null,
	@correo_e			varchar(60) = null,
	@contra				char(8) = null,
	@id_tel				int = null,
	@tel				varchar(12) = null,
	@nom				varchar(60) = null,
	@ape				varchar(60) = null,
	@fec_nac			date = null,
	@curp				char(18) = null,
	@rfc				char(13) = null
) as begin
	if @proc = 'insert' begin
		if not exists (select 1 from Empleados where Correo_electrónico = @correo_e)
			insert into Empleados (Fecha_alta, Nombres, Apellidos, Fecha_nac, CURP, RFC, Activo, Bloqueo, Correo_electrónico, Contraseña, ID_Admin)
			values (getdate(), @nom, @ape, @fec_nac, @curp, @rfc, 1, 0, @correo_e, @contra, @id_adm);
	end
	if @proc = 'addtel' begin
		if not exists (select 1 from Telefonos_emp where Teléfono = @tel and Num_Empleado = @num_emp)
			insert into Telefonos_emp (Teléfono, Num_Empleado)
			values (@tel, @num_emp);
	end
	if @proc = 'updtel' begin
		if not exists (select 1 from Telefonos_emp where Teléfono = @tel and Num_Empleado = @num_emp)
			update Telefonos_emp set
				Teléfono = isnull(@tel, Teléfono)
			where ID_Telemp = @id_tel;
	end
	if @proc = 'showtel' begin
		select
			ID_Telemp [ID],
			Teléfono
		from Telefonos_emp
		where Num_Empleado = @num_emp
	end
	if @proc = 'deltel' begin
		delete from Telefonos_emp where ID_Telemp = @id_tel;
	end
	if @proc = 'update' begin
		if not exists (select 1 from Empleados where Correo_electrónico = @correo_e)
			update Empleados set
				Nombres = isnull(@nom, Nombres),
				Apellidos = isnull(@ape, Apellidos),
				Fecha_nac = isnull(@fec_nac, Fecha_nac),
				RFC = isnull(@rfc, RFC),
				CURP = isnull(@curp, CURP),
				Correo_electrónico = isnull(@correo_e, Correo_electrónico),
				Contraseña = isnull(@contra, Contraseña)
			where Num_Empleado = @num_emp;
	end
	if @proc = 'searchtomod' begin
		select
			Nombres,
			Apellidos,
			[Fecha de nacimiento],
			CURP,
			RFC,
			[Correo electrónico],
			Contraseña
		from v_DetallesEmpleados
		where [Número de emplado] = @num_emp;
	end
	if @proc = 'searchbyid' begin
	select
			[Número de emplado],
			Nombres + ' ' + Apellidos [Nombre completo],
			[Fecha de nacimiento],
			Edad,
			CURP,
			RFC,
			[Correo electrónico],
			[Fecha de alta]
		from v_DetallesEmpleados
		where [Número de emplado] = @num_emp;
	end
	if @proc = 'showall' begin
		select
			[Número de empleado],
			[Nombre completo],
			RFC,
			[Correo electrónico]
		from v_ListaEmpleados;
	end
	if @proc = 'delete' begin
		delete from Empleados where Num_Empleado = @num_emp;
	end
end

go
if exists (select 1 from sysobjects where name = 'sp_Clientes' and type = 'p')
	drop procedure sp_Clientes;
go
create procedure sp_Clientes (
	@proc				varchar(16) = null,
	@id_cl				bigint = null,
	@num_emp			int = null,
	@correo_e			varchar(60) = null,
	@contra				char(8) = null,
	@id_tel				int = null,
	@tel				varchar(12) = null,
	@nom				varchar(60) = null,
	@ape				varchar(60) = null,
	@fec_nac			date = null,
	@curp				char(18) = null,
	@dom				varchar(128) = null
) as begin
	if @proc = 'insert' begin
		if not exists (select 1 from Clientes where Correo_electrónico = @correo_e)
			insert into Clientes (Fecha_alta, Nombres, Apellidos, Fecha_nac, CURP, Domicilio, Correo_electrónico, Contraseña, Activo, Bloqueo, Num_Empleado)
			values (getdate(), @nom, @ape, @fec_nac, @curp, @dom, @correo_e, @contra, 1, 0, @num_emp);
	end
	if @proc = 'addtel' begin
		if not exists (select 1 from Telefonos_client where Teléfono = @tel and ID_Cliente = @id_cl)
			insert into Telefonos_client (Teléfono, ID_Cliente)
			values (@tel, @id_cl);
	end
	if @proc = 'updtel' begin
		if not exists (select 1 from Telefonos_client where Teléfono = @tel and ID_Cliente = @id_cl)
			update Telefonos_client set
				Teléfono = isnull(@tel, Teléfono)
			where ID_Telcl = @id_tel;
	end
	if @proc = 'showtel' begin
		select
			ID_Telcl,
			Teléfono
		from Telefonos_client
		where ID_Cliente = @id_cl
	end
	if @proc = 'deltel' begin
		delete from Telefonos_client where ID_Telcl = @id_tel;
	end
	if @proc = 'update' begin
		if not exists (select 1 from Clientes where Correo_electrónico = @correo_e)
			update Clientes set
				Nombres = isnull(@nom, Nombres),
				Apellidos = isnull(@ape, Apellidos),
				Fecha_nac = isnull(@fec_nac, Fecha_nac),
				CURP = isnull(@curp, CURP),
				Domicilio = isnull(@dom, Domicilio),
				Correo_electrónico = isnull(@correo_e, Correo_electrónico),
				Contraseña = isnull(@contra, Contraseña)
			where ID_Cliente = @id_cl;
	end
	if @proc = 'searchtomod' begin
		select
			Nombres,
			Apellidos,
			[Fecha de nacimiento],
			Domicilio,
			CURP,
			[Correo electrónico],
			Contraseña
		from v_DetallesClientes
		where [ID] = @id_cl;
	end
	if @proc = 'searchbyid' begin
		select
			[ID],
			Nombres + ' ' + Apellidos [Nombre completo],
			[Fecha de nacimiento],
			Edad,
			Domicilio,
			CURP,
			[Correo electrónico],
			[Fecha de alta] 
		from v_DetallesClientes
		where [ID] = @id_cl;
	end
	if @proc = 'showall' begin
		select
			[ID],
			[Nombre completo],
			CURP,
			[Correo electrónico]
		from v_ListaClientes;
	end
	if @proc = 'delete' begin
		delete from Clientes where ID_Cliente = @id_cl;
	end
end

go
if exists (select 1 from sysobjects where name = 'sp_Servicios' and type = 'p')
	drop procedure sp_Servicios;
go
create procedure sp_Servicios (
	@proc				varchar(16) = null,
	@id_ser				bigint = null,
	@id_cl				bigint = null,
	@num_emp			int = null,
	@med				bigint = null,
	@tip_ser			bit = null,
	@dom				varchar(128) = null
)as begin
	if @proc = 'insert' begin
		if not exists (select 1 from Servicios where Medidor = @med)
			insert into Servicios (Fecha_alta, Medidor, Tipo_servicio, Domicilio, Activo, Num_Empleado, ID_Cliente)
			values (getdate(), @med, @tip_ser, @dom, 1, @num_emp, @id_cl);
	end
	if @proc = 'update' begin
		if not exists (select 1 from Servicios where Medidor = @med)
			update Servicios set
				Medidor = isnull(@med, Medidor),
				Tipo_servicio = isnull(@tip_ser, Tipo_servicio),
				Domicilio = isnull(@dom, Domicilio)
			where ID_Servicio = @id_ser;
	end
	if @proc = 'searchtomod' begin
		select
			Medidor,
			Domicilio,
			[Tipo de servicio]
		from v_DetallesServicios
		where ID = @id_ser;
	end
	if @proc = 'searchbyid' begin
		select
			Medidor,
			Domicilio,
			[Tipo de servicio],
			[Fecha de alta]
		from v_DetallesServicios
		where ID = @id_ser;
	end
	if @proc = 'searchbycl' begin
		select
			Medidor,
			[Tipo de servicio],
			Domicilio,
			[ID de Servicio]
		from v_ListaServicios
		where [ID] = @id_cl;
	end
	if @proc = 'showall' begin
		select
			ID,
			[Fecha de alta],
			Medidor,
			[Tipo de servicio],
			Domicilio
		from v_DetallesServicios;
	end
	if @proc = 'delete' begin
		delete from Servicios where ID_Servicio = @id_ser;
	end
end

go
if exists (select 1 from sysobjects where name = 'sp_Tarifas' and type = 'p')
	drop procedure sp_Tarifas;
go
create procedure sp_Tarifas (
	@proc				varchar(16) = null,
	@id_tar				int = null,
	@num_emp			int = null,
	@año				int = null,
	@mes				tinyint = null,
	@tip_ser			bit = null,
	@tar_b				decimal(7, 2) = null,
	@tar_i				decimal(7, 2) = null,
	@tar_e				decimal(7, 2) = null
) as begin
	if @proc = 'insert'	begin
		if not exists (select 1 from Tarifas where Año = @año and Mes = @mes and Tipo_servicio = @tip_ser)
			insert into Tarifas (Año, Mes, Tipo_servicio, Tar_basica, Tar_intermedia, Tar_excedente, Num_Empleado)
			values (@año, @mes, @tip_ser, @tar_b, @tar_i, @tar_e, @num_emp);
		else begin
			update Tarifas set
			Tar_basica = isnull(@tar_b, Tar_basica),
			Tar_intermedia = isnull(@tar_i, Tar_intermedia),
			Tar_excedente = isnull(@tar_e, Tar_excedente)
		where Año = @año and Mes = @mes and Tipo_servicio = @tip_ser;
		end
	end
	if @proc = 'searchbyyear' begin
		select
			ID_Tarifa [ID],
			Año,
			Mes,
			case Tipo_servicio when 0 then 'Doméstico' when 1 then 'Industrial' end [Tipo servicio],
			Tar_basica [Tarifa básica],
			Tar_intermedia [Tarifa intermedia],
			Tar_excedente [Tarifa excedente]
		from Tarifas where Año = @año;
	end
	if @proc = 'showall' begin
		select
			ID_Tarifa,
			Año,
			Mes,
			case Tipo_servicio when 0 then 'Doméstico' when 1 then 'Industrial' end [Tipo de servicio],
			Tar_basica [Tarifa básica],
			Tar_intermedia [Tarifa intermedia],
			Tar_excedente [Tarifa excedente]
		from Tarifas;
	end
end

go
if exists (select 1 from sysobjects where name = 'sp_Consumos' and type = 'p')
	drop procedure sp_Consumos;
go
create procedure sp_Consumos (
	@proc				varchar(16) = null,
	@id_con				bigint = null,
	@num_emp			int = null,
	@id_kw				int = null,
	@año				int = null,
	@mes				tinyint = null,
	@med				bigint = null,
	@kw_tot				int = null
) as begin
	if @id_kw is not null begin
		declare @lkw_b int; set @lkw_b = (select KW_basico from Kilowatts where ID_KW = @id_kw);
		declare @lkw_i int; set @lkw_i = (select KW_intermedio from Kilowatts where ID_KW = @id_kw);
		declare @kw_t int; set @kw_t = @kw_tot;
		declare @kw_b int; set @kw_b = 0;
		declare @kw_i int; set @kw_i = 0;
		declare @kw_e int; set @kw_e = 0;
		if @kw_tot > @lkw_b begin
			set @kw_t = @kw_t - @lkw_b;
			set @kw_b = @lkw_b;
			if @kw_tot > (@lkw_i + @lkw_b) begin
				set @kw_t = @kw_t - @lkw_i;
				set @kw_i = @lkw_i;
				set @kw_e = @kw_t;
			end
			else begin
				set @kw_i = @kw_t;
			end
		end
		else begin
			set @kw_b = @kw_t;
		end
	end
	if @proc = 'insert' begin
		if not exists (select 1 from Consumos where Año = @año and Mes = @mes and Medidor = @med) begin
			declare @tip_ser bit;
			declare @tar int;
			declare @ser bigint;
			select @tip_ser = Tipo_servicio from Servicios where Medidor = @med;
			select @tar = ID_Tarifa from Tarifas where Año = @año and Mes = @mes and Tipo_servicio = @tip_ser;
			select @ser = ID_Servicio from Servicios where Medidor = @med;
			insert into Consumos (Año, Mes, Medidor, KW_total, KW_basica, KW_intermedia, KW_excedente, Num_Empleado, ID_Tarifa, ID_Servicio)
			values (@año, @mes, @med, @kw_tot, @kw_b, @kw_i, @kw_e, @num_emp, @tar, @ser);
		end
		else begin
			update Consumos set
				KW_total = isnull(@kw_tot, KW_total),
				KW_basica = isnull(@kw_b, KW_basica),
				KW_intermedia = isnull(@kw_i, KW_intermedia),
				KW_excedente = isnull(@kw_e, KW_excedente)
			where Año = @año and Mes = @mes and Medidor = @med;
		end
	end
	if @proc = 'searchbyyear' begin
		select
			ID_Consumo [ID],
			Año,
			Mes,
			Medidor,
			KW_basica [KW básica],
			KW_intermedia [KW intermedia],
			KW_excedente [KW excedente]
		from Consumos where Año = @año;
	end
	if @proc = 'showall' begin
		select
			ID_Consumo,
			Medidor,
			Año,
			Mes,
			KW_basica,
			KW_intermedia,
			KW_excedente,
			KW_total
		from Consumos;
	end
	if @proc = 'historic' begin
		select
			C.Año, C.Mes,
			C.KW_total [Consumo total],
			R.Importe_pago [Importe de pago],
			R.Pago_pendiente [Pendiente de pago]
		from Consumos C left outer join Recibos R on R.ID_Consumo = C.ID_Consumo
		where C.Año = @año and C.Medidor = @med and R.Pagado = 1
		order by C.Mes, C.Año;
	end
end

go
if exists (select 1 from sysobjects where name = 'sp_Recibos' and type = 'p')
	drop procedure sp_Recibos;
go
create procedure sp_Recibos (
	@proc				varchar(16) = null,
	@id_rec				bigint = null,
	@id_ser				bigint = null,
	@num_emp			int = null,
	@año				int = null,
	@mes				tinyint = null,
	@tip_ser			bit = null,
	@pago				money = null
)as begin
	if @proc = 'generate' begin
		declare @prevaño int;
		declare @prevmes tinyint;
		if @mes = 1 begin
			set @prevaño = @año - 1;
			set @prevmes = 12;
		end
		else begin
			set @prevaño = @año;
			set @prevmes = @mes - 1;
		end
		declare @consum table (
			med			bigint,
			ckw_b		int,
			ckw_i		int,
			ckw_e		int,
			ckw_t		int
		);
		insert into @consum
			select C.Medidor, C.KW_basica, C.KW_intermedia, C.KW_excedente, C.KW_total
			from Consumos C left outer join Servicios S on S.ID_Servicio = C.ID_Servicio
			where Año = @año and Mes = @mes and S.Tipo_servicio = @tip_ser;
		declare @recibo_f table(
			med				bigint,
			domic			varchar(128),
			fecha_gn		date,
			fecha_vn		date,
			tarifa_b		money,
			tarifa_i		money,
			tarifa_e		money,
			precio_b		money,
			precio_i		money,
			precio_e		money,
			precio_tot		money,
			cargo_iva		money,
			prev_pend		money,
			pago_pend		money,
			pago_tot		money,
			id_co			bigint,
			num_em			int,
			id_sv			bigint
		);
		insert into @recibo_f (med, domic, fecha_gn, fecha_vn, tarifa_b, tarifa_i, tarifa_e, precio_b, precio_i, precio_e, prev_pend, id_co, num_em, id_sv)
			select Ct.med,
				S.Domicilio,
				getdate(),
				dateadd(DAY, 10, getdate()),
				T.Tar_basica,
				T.Tar_intermedia,
				T.Tar_excedente,
				Ct.ckw_b * T.Tar_basica,
				Ct.ckw_i * T.Tar_intermedia,
				Ct.ckw_e * T.Tar_excedente,
				isnull(R.Pago_pendiente, 0),
				C.ID_Consumo,
				@num_emp,
				S.ID_Servicio
			from @consum Ct right outer join Consumos C on C.Medidor = Ct.med
							left outer join Servicios S on S.ID_Servicio = C.ID_Servicio
							left outer join Tarifas T on T.Año = @año and T.Mes = @mes
							left outer join Recibos R on R.Año = @prevaño and R.Mes = @mes and R.Medidor = Ct.med
			where S.Tipo_servicio = @tip_ser and T.Tipo_servicio = @tip_ser and C.Año = @año and C.Mes = @mes;
		update @recibo_f set
			precio_tot = precio_b + precio_i + precio_e;
		update @recibo_f set
			cargo_iva = precio_tot * 0.16;
		update @recibo_f set
			pago_tot = precio_tot + cargo_iva;
		update @recibo_f set
			pago_tot = pago_tot + prev_pend;
		
		if not exists (select * from Recibos R left outer join Servicios S on S.ID_Servicio = R.ID_Servicio
					   left outer join @recibo_f rf on rf.id_sv = S.ID_Servicio
					   where R.Año = @año and R.Mes = @mes and S.Medidor = rf.med) begin
			insert into Recibos(Año, Mes, Tipo_servicio, Medidor, Domicilio, Fecha_gen, Fecha_venc, Consumo_basico, Consumo_intermedio, Consumo_excedente, Consumo_total, Tarifa_basica, Tarifa_intermedia, Tarifa_excedente, Precio_basico, Precio_intermedio, Precio_excedente, Precio_total, Cargo_IVA, Prev_pendiente, Pago_total, Importe_pago, Pago_pendiente, Pagado, ID_Consumo, Num_Empleado, ID_Servicio)
				select @año, @mes, @tip_ser, R.med, R.domic, R.fecha_gn, R.fecha_vn, C.ckw_b, C.ckw_i, C.ckw_e, C.ckw_t, R.tarifa_b, R.tarifa_i, R.tarifa_e, R.precio_b, R.precio_i, R.precio_e, R.precio_tot, R.cargo_iva, R.prev_pend, R.pago_tot, 0.0, 0.0, 0, R.id_co, R.num_em, R.id_sv
				from @recibo_f R left outer join @consum C on C.med = R.med;
		end
		else begin
			update Recibos set
				Fecha_gen = rc.fecha_gn,
				Fecha_venc = rc.fecha_vn,
				Consumo_basico = C.ckw_b,
				Consumo_intermedio = C.ckw_i,
				Consumo_excedente = C.ckw_e,
				Consumo_total = C.ckw_t,
				Tarifa_basica = rc.tarifa_b,
				Tarifa_intermedia = rc.tarifa_i,
				Tarifa_excedente = rc.tarifa_e,
				Precio_basico = rc.precio_b,
				Precio_intermedio = rc.precio_i,
				Precio_excedente = rc.precio_e,
				Precio_total = rc.precio_tot,
				Prev_pendiente = rc.prev_pend,
				Cargo_IVA = rc.cargo_iva,
				Pago_total = rc.pago_tot
			from @recibo_f rc
			left outer join @consum C on C.med = rc.med
			where Recibos.Medidor = rc.med
			and Recibos.Año = @año
			and Recibos.Mes = @mes;
		end
	end
	if @proc = 'payment' begin
		if (select Pagado from Recibos where ID_Recibo = @id_rec) = 0 begin
			update Recibos set
			Importe_pago = @pago,
			Pago_pendiente = Pago_total - @pago,
			Pagado = 1
			where ID_Recibo = @id_rec;
		end
	end
	if @proc = 'searchbyserv' begin
		select
			[ID del Recibo],
			Medidor,
			[Tipo de servicio],
			[Fecha de generación],
			[Fecha de vencimiento],
			Pagado
		from v_ListaRecibos where [ID de Servicio] = @id_ser
		order by [ID del Recibo] desc;
	end
	if @proc = 'searchbyid' begin
		select
			[Nombre completo],
			ID,
			Medidor,
			[Tipo de servicio],
			Domicilio,
			Año,
			Mes,
			[Consumo básico],
			[Consumo intermedio],
			[Consumo excedente],
			[Consumo total],
			[Tarifa básica],
			[Tarifa intermedia],
			[Tarifa excedente],
			[Precio básico],
			[Precio intermedio],
			[Precio excedente],
			[Precio total],
			[Pago pendiente previo],
			[IVA],
			[Pago total]
		from v_Recibos where [ID del Recibo] = @id_rec;
	end
	if @proc = 'generalreport' begin
		select
			Año, Mes, case Tipo_servicio when 0 then 'Doméstico' when 1 then 'Industrial' end [Tipo de servicio],
			Importe_pago [Importe pagado], Pago_pendiente [Pendiente de pago]
		from Recibos
		where Año = @año and Mes = @mes and Tipo_servicio = @tip_ser and Pagado = 1
		order by Año, Mes desc;
	end
end

go
if exists (select 1 from sysobjects where name = 'sp_Kilowatts' and type = 'p')
	drop procedure sp_Kilowatts;
go
create procedure sp_Kilowatts (
	@proc				varchar(16) = null,
	@id_kw				int = null,
	@kw_b				int = null,
	@kw_i				int = null
) as begin
	if @proc = 'insert' begin
		insert into Kilowatts (KW_basico, KW_intermedio)
		values (@kw_b, @kw_i);
	end
	if @proc = 'update' begin
		update Kilowatts set
			KW_basico = isnull(@kw_b, KW_basico),
			KW_intermedio = isnull(@kw_i, KW_intermedio)
		where ID_KW = @id_kw;
	end
	if @proc = 'searchbyid' begin
		select ID_KW [ID], KW_basico [kW básico], KW_intermedio [kW intermedio]
		from Kilowatts where ID_KW = @id_kw;
	end
	if @proc = 'showall' begin
		select ID_KW [ID], KW_basico [kW básico], KW_intermedio [kW intermedio]
		from Kilowatts;
	end
	if @proc = 'delete' begin
		delete from Kilowatts where ID_KW = @id_kw;
	end
end

go
if exists (select 1 from sysobjects where name = 'sp_Recordar_contra' and type = 'p')
	drop procedure sp_Recordar_contra;
go
create procedure sp_Recordar_contra (
	@proc				varchar(16) = null,
	@id_rc				smallint = null,
	@tip_us				char = null,
	@correo_e			varchar(60) = null,
	@contra				char(8) = null
) as begin
	if @proc = 'insert' begin
		insert into Recordar_contra (Tipo_user, Correo_electrónico, Contraseña)
		values (@tip_us, @correo_e, @contra);
	end
	if @proc = 'searchbyid' begin
		select ID_RC [ID], Correo_electrónico [Correo electrónico], Contraseña
		from Recordar_contra where ID_RC = @id_rc;
	end
	if @proc = 'showbytype' begin
		select ID_RC [ID], Correo_electrónico [Correo electrónico], Contraseña, Tipo_user
		from Recordar_contra where Tipo_user = @tip_us;
	end
	if @proc = 'delete' begin
		delete from Recordar_contra where ID_RC = @id_rc;
	end
end

go
if exists (select 1 from sysobjects where name = 'sp_RegistroActividad' and type = 'p')
	drop procedure sp_RegistroActividad;
go
create procedure sp_RegistroActividad(
	@proc				varchar(16) = null,
	@clave				bigint = null,
	@num_emp			int = null,
	@accion				varchar(30) = null,
	@descripcion		varchar(512) = null
) as begin
	if @proc = 'insert' begin
		insert into Registro_actividad (Fecha_registro, Acción, Descripción, Num_Empleado)
		values (getdate(), @accion, @descripcion, @num_emp);
	end
	if @proc = 'showbyemp' begin
		select
			Clave,
			[Fecha y hora],
			Acción
		from v_ListaRegistroActividad
		where Num_Empleado = @num_emp
		order by [Fecha y hora] desc;
	end
	if @proc = 'searchbyid'	begin
		select
			[Clave],
			[Número de empleado],
			[Fecha y hora],
			Acción,
			Descripción
		from v_DetallesRegistroActividad
		where CLAVE = @clave
	end
	if @proc = 'showall' begin
		select
			[Clave],
			[Número de empleado],
			[Fecha y hora],
			Acción,
			Descripción
		from v_DetallesRegistroActividad;
	end
end