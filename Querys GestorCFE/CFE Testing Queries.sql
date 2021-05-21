use [GestorCFE];

-- /////////////////////////
-- ///////// ADMINISTRADORES
exec sp_Admin 'insert', null, 'TestAdm@user.com', '12345678', 'Administrator'
exec sp_Admin 'insert', null, 'drfer@user.com', '51235121', 'Fernando'

exec sp_Admin 'update', 1001, 'sample@user.com', null, null

exec sp_Admin 'searchbyid', 1001

exec sp_Admin 'showall'

exec sp_Admin 'delete', 1001

select * from Administrador;

-- ///////////////////
-- ///////// EMPLEADOS
exec sp_Empleados 'insert', null, 1000, 'karin@user.com', 'UTd1023d', null, null, 'Karin', 'Garza', '20000818', 'CURPSAMPLE20081800', 'RFCSAMPLE0008'
exec sp_Empleados 'insert', null, 1001, 'juanarm@user.com', 'PQd302t3', null, null, 'Juan', 'Casas', '19950301', 'CURPSAMPLE95030100', 'RFCSAMPLE9503'
exec sp_Empleados 'insert', null, 1000, 'abi@user.com', 'RTy530td', null, null, 'Abril', 'Cárdenas', '19921005', 'CURPSAMPLE92100500', 'RFCSAMPLE1234'
exec sp_Empleados 'insert', null, 1001, 'alvabot@user.com', 'BNd0tr22', null, null, 'Álvaro', 'Rodríguez', '19930810', 'CURPSAMPLE93081000', 'RFCSAMPLE9308'

exec sp_Empleados 'update', 1001, null, null, null, null, null, null, null, null, 'CURPSAMPLE99009900', 'RFCSAMPLE9999'

exec sp_Empleados 'delete', 1002

exec sp_Empleados 'searchbyid', 1001

exec sp_Logins 'E', 'alexis_5476@hotmail.com', '06050403'

exec sp_Empleados 'showall'

select * from Empleados;

exec sp_Empleados 'addtel', 1000, null, null, null, null, '8120118122'
exec sp_Empleados 'addtel', 1000, null, null, null, null, '8186519859'
exec sp_Empleados 'addtel', 1001, null, null, null, null, '8120118122'

exec sp_Empleados 'updtel', 1000, null, null, null, 102, '8186912531'

exec sp_Empleados 'showtel', 1000

exec sp_Empleados 'deltel', null, null, null, null, 100

-- //////////////////
-- ///////// CLIENTES
exec sp_Clientes 'insert', null, 1001, 'carmelo@gmail.com', '22302210', null, null, 'Carmelo Gerardo', 'Fernandez Uribe', '19951020', 'CURPSAMPLE22222222', 'Calle X Numero Y'
exec sp_Clientes 'insert', null, 1003, 'daniela@olimp.com', '30313233', null, null, 'Isai', 'Juarez Casas', '19990223', 'CURPSAMPLE23232323', 'Calle X Numero Y DS'
exec sp_Clientes 'insert', null, 1001, 'derkrox@user.com', '20005000', null,null, 'Derek', 'Rosales', '19900328', 'CURPSAMPLE33333333', 'Dmx'
exec sp_Clientes 'insert', null, 1002, 'dilasbri@user.com', '01827375', null, null, 'Diana', 'Castro', '20000311', 'CURPSAMPLE00031100', 'CAD'

exec sp_Clientes 'update', 1000, null, null, null, null, null, null, null, null, null, 'Jar,223,,Puentes,Juarez,Nuevo León,66424'
exec sp_Clientes 'update', 1001, null, null, null, null, null, null, null, null, null, 'Oliv,521,322,Olivos,Guadalupe,Nuevo León,66242'
exec sp_Clientes 'update', 1002, null, null, null, null, null, null, null, null, null, 'Carranza,223,,Fomerrey 23,Monterrey,Nuevo León,61224'
exec sp_Clientes 'update', 1003, null, null, null, null, null, null, null, null, null, 'O6,482,,Metroplex,Apodaca,Nuevo León,65212'

exec sp_Clientes 'delete', 1003

exec sp_Clientes 'login', null, null, 'carmelo@gmail.com', '12121212'

exec sp_Clientes 'unlock', null, null, 'carmelo@gmail.com'

exec sp_Clientes 'showall'

exec sp_Clientes 'searchbyid', 1003

exec sp_Clientes 'searchtomod', 1001

exec sp_Clientes 'addtel', 1000, null, null, null, null, '8186912531'

exec sp_Clientes 'updtel', 1000, null, null, null, 101, '8186912531'

exec sp_Clientes 'deltel', null, null, null, null, 101

exec sp_Clientes 'showtel', 1000

select * from Clientes;

-- ///////////////////
-- ///////// SERVICIOS
exec sp_Servicios 'insert', null, 1000, 1002, 10922, 0, 'Jar,223,,Puentes,Juarez,Nuevo León,66424'
exec sp_Servicios 'insert', null, 1001, 1003, 12300, 1, 'Ave. Concordia,552,,Juarez,San Nicolas,Nuevo León,66721'
exec sp_Servicios 'insert', null, 1001, 1003, 31021, 0, 'Oliv,521,322,Olivos,Guadalupe,Nuevo León,66242'
exec sp_Servicios 'insert', null, 1002, 1001, 23223, 0, 'Carranza,223,,Fomerrey 23,Monterrey,Nuevo León,61224'
exec sp_Servicios 'insert', null, 1003, 1000, 01241, 0, 'O6,482,,Metroplex,Apodaca,Nuevo León,65212'
exec sp_Servicios 'insert', null, 1003, 1002, 52673, 1, 'Flores,821,,Salcedo,Marin,Nuevo León,2231'

exec sp_Servicios 'update', 10000, null, null, null, null, 'Jar,223,,Puentes,Juarez,Nuevo León,66424'
exec sp_Servicios 'update', 10001, null, null, null, null, 'Ave. Concordia,552,,Juarez,San Nicolas,Nuevo León,66721'
exec sp_Servicios 'update', 10002, null, null, null, null, 'Oliv,521,322,Olivos,Guadalupe,Nuevo León,66242'
exec sp_Servicios 'update', 10003, null, null, null, null, 'Carranza,223,,Fomerrey 23,Monterrey,Nuevo León,61224'
exec sp_Servicios 'update', 10004, null, null, null, null, 'O6,482,,Metroplex,Apodaca,Nuevo León,65212'
exec sp_Servicios 'update', 10005, null, null, null, null, 'Flores,821,,Salcedo,Marin,Nuevo León,2231'

exec sp_Servicios 'delete', 10002

exec sp_Servicios 'showall'

exec sp_Servicios 'searchtomod', 10000

exec sp_Servicios 'searchbyid', 10000

exec sp_Servicios 'searchlistid', 10006

exec sp_Servicios 'searchbycl', null, 1001

-- ////////////////
-- //////// TARIFAS
exec sp_Tarifas 'insert', null, 1000, 2020, 3, 0, 0.89, 1.20, 2.00
exec sp_Tarifas 'insert', null, 1000, 2020, 3, 1, 1.0, 2.50, 3.5

exec sp_Tarifas 'showall'

-- /////////////////
-- //////// CONSUMOS
exec sp_Consumos 'insert', null, 1001, 10, 2020, 4, 10922, 220
exec sp_Consumos 'insert', null, 1001, 10, 2020, 4, 12300, 110
exec sp_Consumos 'insert', null, 1001, 10, 2020, 4, 31021, 452
exec sp_Consumos 'insert', null, 1001, 10, 2020, 4, 23223, 520
exec sp_Consumos 'insert', null, 1001, 10, 2020, 4, 01241, 110
exec sp_Consumos 'insert', null, 1001, 10, 2020, 4, 52673, 721

exec sp_Consumos 'showall'

-- ////////////////
-- //////// RECIBOS
exec sp_Recibos 'generate', null, null, 1002, 2020, 04, 0

exec sp_Recibos 'searchbyserv', null, 10004
exec sp_Recibos 'searchbyid', 10003

select * from Recibos;

select * from Recibos where ID_Recibo = 10003;

exec sp_Recibos 'payment', 10003, null, null, null, null, null, 500.0

-- //////////////////
-- //////// KILOWATTS
exec sp_Kilowatts 'insert', null, 120, 300

exec sp_Kilowatts 'showall'

-- ///////////////////////////
-- //////// REGISTRO ACTIVIDAD
exec sp_RegistroActividad 'searchbyid', 101

exec sp_RegistroActividad 'showall'
