use [GestorCFE];

go
if exists (select 1 from sysobjects where name = 'v_ListaEmpleados' and type = 'v')
drop view v_ListaEmpleados;
go
create view v_ListaEmpleados as
select
	Num_Empleado [N�mero de empleado],
	Nombres + ' ' + Apellidos [Nombre completo],
	RFC,
	Correo_electr�nico [Correo electr�nico]
from Empleados
where Activo = 1;

go
if exists (select 1 from sysobjects where name = 'v_DetallesEmpleados' and type = 'v')
	drop view v_DetallesEmpleados;
go
create view v_DetallesEmpleados as
select
	Num_Empleado [N�mero de emplado],
	Nombres,
	Apellidos,
	Fecha_nac [Fecha de nacimiento],
	datediff(year, Fecha_nac, getdate()) [Edad],
	CURP,
	RFC,
	Fecha_alta [Fecha de alta],
	Bloqueo,
	Correo_electr�nico [Correo electr�nico],
	Contrase�a
from Empleados
where Activo = 1;

go
if exists (select 1 from sysobjects where name = 'v_ListaRegistroActividad' and type = 'v')
	drop view v_ListaRegistroActividad;
go
create view v_ListaRegistroActividad as
select
	CLAVE [Clave],
	Num_Empleado,
	Fecha_registro [Fecha y hora],
	Acci�n,
	Descripci�n
from Registro_actividad;

go
if exists (select 1 from sysobjects where name = 'v_DetallesRegistroActividad' and type = 'v')
	drop view v_DetallesRegistroActividad;
go
create view v_DetallesRegistroActividad as
select
	CLAVE [Clave],
	Num_Empleado [N�mero de empleado],
	Fecha_registro [Fecha y hora],
	Acci�n,
	Descripci�n
from Registro_actividad;

go
if exists (select 1 from sysobjects where name = 'v_ListaClientes' and type = 'v')
	drop view v_ListaClientes;
go
create view v_ListaClientes as
select
	ID_Cliente [ID],
	Nombres + ' ' + Apellidos [Nombre completo],
	CURP,
	Correo_electr�nico [Correo electr�nico]
from Clientes
where Activo = 1;

go
if exists (select 1 from sysobjects where name = 'v_DetallesClientes' and type = 'v')
	drop view v_DetallesClientes;
go
create view v_DetallesClientes as
select
	ID_Cliente [ID],
	Nombres, 
	Apellidos,
	Fecha_nac [Fecha de nacimiento],
	datediff(year, Fecha_nac, getdate()) [Edad],
	Domicilio,
	CURP,
	Fecha_alta [Fecha de alta],
	Bloqueo,
	Correo_electr�nico [Correo electr�nico],
	Contrase�a
from Clientes
where Activo = 1;

go
if exists (select 1 from sysobjects where name = 'v_ListaServicios' and type = 'v')
	drop view v_ListaServicios;
go
create view v_ListaServicios as
select
	ID_Servicio [ID de Servicio], 
	ID_Cliente [ID],
	Medidor,
	case Tipo_servicio when 0 then 'Dom�stico' when 1 then 'Industrial' end [Tipo de servicio],
	Domicilio
from Servicios
where Activo = 1;

go
if exists (select 1 from sysobjects where name = 'v_DetallesServicios' and type = 'v')
	drop view v_DetallesServicios;
go
create view v_DetallesServicios as
select
	ID_Servicio [ID],
	Medidor,
	Domicilio,
	case Tipo_servicio when 0 then 'Dom�stico' when 1 then 'Industrial' end [Tipo de servicio],
	Fecha_alta [Fecha de alta]
from Servicios
where Activo = 1;

go
if exists (select 1 from sysobjects where name = 'v_ListaRecibos' and type = 'v')
	drop view v_ListaRecibos;
go
create view v_ListaRecibos as
select
	ID_Recibo [ID del Recibo],
	Medidor,
	ID_Servicio [ID de Servicio],
	case Tipo_servicio when 0 then 'Dom�stico' when 1 then 'Industrial' end [Tipo de servicio],
	Fecha_gen [Fecha de generaci�n],
	Fecha_venc [Fecha de vencimiento],
	case Pagado when 0 then 'No pagado' when 1 then 'Pagado' end [Pagado]
from Recibos;

go
if exists (select 1 from sysobjects where name = 'v_Recibos' and type = 'v')
	drop view v_Recibos;
go
create view v_Recibos as
select
	R.ID_Recibo [ID del Recibo],
	Cl.Nombres + ' ' + Cl.Apellidos [Nombre completo],
	Cl.ID_Cliente [ID],
	R.Medidor,
	case R.Tipo_servicio when 0 then 'Dom�stico' when 1 then 'Industrial' end [Tipo de servicio],
	R.Domicilio,
	R.A�o,
	R.Mes,
	Co.KW_basica [Consumo b�sico],
	Co.KW_intermedia [Consumo intermedio],
	Co.KW_excedente [Consumo excedente],
	Co.KW_total [Consumo total],
	T.Tar_basica [Tarifa b�sica],
	T.Tar_intermedia [Tarifa intermedia],
	T.Tar_excedente [Tarifa excedente],
	R.Precio_basico [Precio b�sico],
	R.Precio_intermedio [Precio intermedio],
	R.Precio_excedente [Precio excedente],
	R.Precio_total [Precio total],
	R.Cargo_IVA [IVA],
	R.Prev_pendiente [Pago pendiente previo],
	R.Pago_total [Pago total]
from Recibos R left outer join Consumos Co on Co.ID_Consumo = R.ID_Consumo
	 left outer join Tarifas T on T.ID_Tarifa = Co.ID_Tarifa
	 left outer join Servicios S on S.ID_Servicio = R.ID_Servicio
	 left outer join Clientes Cl on Cl.ID_Cliente = S.ID_Cliente