use [GestorCFE];

-- //////////////////////// CREACION DE TABLAS //////////////////////// --

if exists(select 1 from sysobjects where name = 'Administrador' and type = 'u')
	drop table Administrador;
create table Administrador(
	ID_Admin					smallint identity(1000, 1) primary key,
	Nombre_usuario				varchar(30) not null,
	Correo_electrónico			varchar(60) not null,
	Contraseña					char(8) not null
);

if exists(select 1 from sysobjects where name = 'Empleados' and type = 'u')
	drop table Empleados;
create table Empleados(
	Num_Empleado		int identity(1000, 1) primary key,
	Fecha_alta			date not null,
	Nombres				varchar(60) not null,
	Apellidos			varchar(60) not null,
	Fecha_nac			date not null,
	RFC					char(13),
	CURP				char(18) not null,
	Correo_electrónico	varchar(60) not null,
	Contraseña			char(8) not null,
	Activo				bit not null,
	Bloqueo				bit not null,

	ID_Admin			smallint not null
);

if exists(select 1 from sysobjects where name = 'Clientes' and type = 'u')
	drop table Clientes;
create table Clientes(
	ID_Cliente			bigint identity(1000, 1) primary key,
	Fecha_alta			date not null,
	Nombres				varchar(60) not null,
	Apellidos			varchar(60) not null,
	Fecha_nac			date not null,
	CURP				char(18) not null,
	Domicilio			varchar(128) not null,
	Correo_electrónico	varchar(60) not null,
	Contraseña			char(8) not null,
	Activo				bit not null,
	Bloqueo				bit not null,

	Num_Empleado		int not null
);

if exists (select 1 from sysobjects where name = 'Kilowatts' and type = 'u')
	drop table Kilowatts;
create table Kilowatts(
	ID_KW				int identity(10, 1) primary key,
	KW_basico			int not null,
	KW_intermedio		int not null
);

if exists(select 1 from sysobjects where name = 'Servicios' and type = 'u')
	drop table Servicios;
create table Servicios(
	ID_Servicio			bigint identity(10000, 1) primary key,
	Medidor				bigint not null,
	Fecha_alta			date not null,
	Tipo_servicio		bit not null,
	Domicilio			varchar(128) not null,
	Activo				bit not null,

	Num_Empleado		int not null,
	ID_Cliente			bigint not null
);

if exists(select 1 from sysobjects where name = 'Tarifas' and type = 'u')
	drop table Tarifas;
create table Tarifas(
	ID_Tarifa			int identity(100, 1) primary key,
	Tipo_servicio		bit not null,
	Año					int not null,
	Mes					tinyint not null,
	Tar_basica			decimal(7, 2) not null,
	Tar_intermedia		decimal(7, 2) not null,
	Tar_excedente		decimal(7, 2) not null,

	Num_Empleado		int not null
);

if exists(select 1 from sysobjects where name = 'Consumos' and type = 'u')
	drop table Consumos;
create table Consumos(
	ID_Consumo			bigint identity(10000, 1) primary key,
	Medidor				bigint not null,
	Año					int not null,
	Mes					tinyint not null,
	KW_basica			int not null,
	KW_intermedia		int not null,
	KW_excedente		int not null,
	KW_total			int not null,

	Num_Empleado		int not null,
	ID_Tarifa			int not null,
	ID_Servicio			bigint not null
);

if exists(select 1 from sysobjects where name = 'Recibos' and type = 'u')
	drop table Recibos;
create table Recibos(
	ID_Recibo			bigint identity(10000, 1) primary key,
	Año					int not null,
	Mes					tinyint not null,
	Tipo_servicio		bit not null,
	Medidor				bigint not null,
	Domicilio			varchar(128) not null,
	Fecha_gen			date not null,
	Fecha_venc			date not null,
	Consumo_basico		int not null,
	Consumo_intermedio	int not null,
	Consumo_excedente	int not null,
	Consumo_total		int not null,
	Tarifa_basica		money not null,
	Tarifa_intermedia	money not null,
	Tarifa_excedente	money not null,
	Precio_basico		money not null,
	Precio_intermedio	money not null,
	Precio_excedente	money not null,
	Precio_total		money not null,
	Cargo_IVA			money not null,
	Pago_total			money not null,
	Importe_pago		money,
	Pago_pendiente		money,
	Prev_pendiente		money,
	Pagado				bit not null,

	ID_Consumo			bigint not null,
	Num_Empleado		int not null,
	ID_Servicio			bigint not null
);

if exists(select 1 from sysobjects where name = 'Registro_actividad' and type = 'u')
	drop table Registro_actividad;
create table Registro_actividad(
	CLAVE				bigint identity(100, 1) primary key,
	Fecha_registro		datetime not null,
	Acción				varchar(30) not null,
	Descripción			varchar(512) not null,

	Num_Empleado		int not null
);

if exists (select 1 from sysobjects where name = 'Telefonos_emp' and type = 'u')
	drop table Telefonos_emp;
create table Telefonos_emp (
	ID_Telemp			int identity(100, 1) primary key,
	Teléfono			varchar(12) not null,

	Num_Empleado		int not null
);

if exists (select 1 from sysobjects where name = 'Telefonos_client' and type = 'u')
	drop table Telefonos_client;
create table Telefonos_client (
	ID_Telcl			int identity(100, 1) primary key,
	Teléfono			varchar(12) not null,

	ID_Cliente			bigint not null
);

if exists (select 1 from sysobjects where name = 'Recordar_contra' and type = 'u')
	drop table Recordar_contra;
create table Recordar_contra(
	ID_RC				smallint identity(100, 1) primary key,
	Tipo_user			char not null,
	Correo_electrónico	varchar(60) not null,
	Contraseña			char(8) not null
);

-- //////////////////////// FOREIGN KEYS //////////////////////// --

alter table Empleados add
	constraint FK_EMP_ADM
	foreign key (ID_Admin)
	references Administrador (ID_Admin);

alter table Registro_actividad add
	constraint FK_REG_EMP
	foreign key (Num_Empleado)
	references Empleados (Num_Empleado);

alter table Clientes add
	constraint FK_CLI_EMP
	foreign key (Num_Empleado)
	references Empleados (Num_Empleado);

alter table Servicios add
	constraint FK_SERV_EMP
	foreign key (Num_Empleado)
	references Empleados (Num_Empleado),

	constraint FK_SERV_CLI
	foreign key (ID_Cliente)
	references Clientes (ID_Cliente);

alter table Tarifas add
	constraint FK_TAR_EMP
	foreign key (Num_Empleado)
	references Empleados (Num_Empleado);

alter table Consumos add
	constraint FK_CON_EMP
	foreign key (Num_Empleado)
	references Empleados (Num_Empleado),
	
	constraint FK_CON_TAR
	foreign key (ID_Tarifa)
	references Tarifas (ID_Tarifa),
	
	constraint FK_CON_SER
	foreign key (ID_Servicio)
	references Servicios (ID_Servicio);

alter table Recibos add
	constraint FK_REC_CON
	foreign key (ID_Consumo)
	references Consumos (ID_Consumo),

	constraint FK_REC_EMP
	foreign key (Num_Empleado)
	references Empleados (Num_Empleado),

	constraint FK_REC_SER
	foreign key (ID_Servicio)
	references Servicios (ID_Servicio);

alter table Telefonos_emp add
	constraint FK_TEL_EMP
	foreign key (Num_Empleado)
	references Empleados (Num_Empleado);

alter table Telefonos_client add
	constraint FK_TEL_CLI
	foreign key (ID_Cliente)
	references Clientes (ID_Cliente);

-- //////////////////////// DROP FOREIGN KEYS //////////////////////// --

alter table Empleados drop constraint FK_EMP_ADM;
alter table Registro_actividad drop constraint FK_REG_EMP;
alter table Clientes drop constraint FK_CLI_EMP;
alter table Servicios drop constraint FK_SERV_EMP, FK_SERV_CLI;
alter table Tarifas drop constraint FK_TAR_EMP;
alter table Consumos drop constraint FK_CON_EMP, FK_CON_TAR, FK_CON_SER;
alter table Recibos drop constraint FK_REC_CON, FK_REC_EMP, FK_REC_SER;
alter table Telefonos_emp drop constraint FK_TEL_EMP;
alter table Telefonos_client drop constraint FK_TEL_CLI;