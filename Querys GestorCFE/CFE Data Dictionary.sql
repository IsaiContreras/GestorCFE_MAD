-- /////////////// ADMINISTRADOR ///////////////////////////////////////////
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID principal del administrador.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Administrador',
@level2type = N'Column', @level2name = 'ID_Admin';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Nombre completo del administrador.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Administrador',
@level2type = N'Column', @level2name = 'Nombre_usuario';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Correo electr�nico del administrador.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Administrador',
@level2type = N'Column', @level2name = 'Correo_electr�nico';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Clave de ingreso del administrador.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Administrador',
@level2type = N'Column', @level2name = 'Contrase�a';

-- /////////////// CLIENTE /////////////////////////////////////////////////
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Clave �nica de Registro de Poblaci�n. C�digo alfanum�rico de 18 caracteres usado para identificar residentes y ciudadanos mexicanos.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Clientes',
@level2type = N'Column', @level2name = 'CURP';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Clave de ingreso del cliente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Clientes',
@level2type = N'Column', @level2name = 'Contrase�a';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Nombre o nombres del cliente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Clientes',
@level2type = N'Column', @level2name = 'Nombres';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Apellidos del cliente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Clientes',
@level2type = N'Column', @level2name = 'Apellidos';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Domicilio del cliente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Clientes',
@level2type = N'Column', @level2name = 'Domicilio';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Correo electr�nico usado para ingresar a la sesi�n del cliente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Clientes',
@level2type = N'Column', @level2name = 'Correo_electr�nico';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del empleado que di� de alta al cliente..',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Clientes',
@level2type = N'Column', @level2name = 'Num_Empleado';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Habilitador del cliente. 0 si fue eliminado 1 si sigue activo.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Clientes',
@level2type = N'Column', @level2name = 'Activo';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID principal del cliente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Clientes',
@level2type = N'Column', @level2name = 'ID_Cliente';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Edad del cliente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Clientes',
@level2type = N'Column', @level2name = 'Edad';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Fecha en que se di� de alta el cliente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Clientes',
@level2type = N'Column', @level2name = 'Fecha_alta';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Fecha de nacimiento del cliente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Clientes',
@level2type = N'Column', @level2name = 'Fecha_nac';

-- /////////////// CONSUMOS /////////////////////////////////////////////////
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes del periodo de facturaci�n.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Consumos',
@level2type = N'Column', @level2name = 'Mes';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Kilowatts correspondientes al consumo excedente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Consumos',
@level2type = N'Column', @level2name = 'KW_excedente';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Kilowatts totales consumidos.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Consumos',
@level2type = N'Column', @level2name = 'KW_total';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del usuario que registr� el consumo.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Consumos',
@level2type = N'Column', @level2name = 'Num_Empleado';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID de la tarifa en que se basar� el c�lculo del precio.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Consumos',
@level2type = N'Column', @level2name = 'ID_Tarifa';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID de los niveles de KW en que se basar�n los consumos.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Consumos',
@level2type = N'Column', @level2name = 'ID_KW';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'A�o del periodo de facturaci�n.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Consumos',
@level2type = N'Column', @level2name = 'A�o';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Kilowatts correspondientes al consumo b�sico.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Consumos',
@level2type = N'Column', @level2name = 'KW_basica';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Kilowatts correspondientes al consumo intermedio.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Consumos',
@level2type = N'Column', @level2name = 'KW_intermedia';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del servicio correspondiente al consumo.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Consumos',
@level2type = N'Column', @level2name = 'ID_Servicio';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del consumo.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Consumos',
@level2type = N'Column', @level2name = 'ID_Consumo';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'N�mero del medidor instalado al servicio.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Consumos',
@level2type = N'Column', @level2name = 'Medidor';

-- /////////////// EMPLEADOS /////////////////////////////////////////////////
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Nombre o nombres del empleado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Empleados',
@level2type = N'Column', @level2name = 'Nombres';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Apellidos del empleado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Empleados',
@level2type = N'Column', @level2name = 'Apellidos';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Correo electr�nico usado para ingresar a la sesi�n del empleado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Empleados',
@level2type = N'Column', @level2name = 'Correo_electr�nico';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del empleado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Empleados',
@level2type = N'Column', @level2name = 'Num_Empleado';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Habilitador del empleado. 0 si fue dado de baja, 1 si est� activo.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Empleados',
@level2type = N'Column', @level2name = 'Activo';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Edad del empleado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Empleados',
@level2type = N'Column', @level2name = 'Edad';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del administrador que di� de alta al empleado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Empleados',
@level2type = N'Column', @level2name = 'ID_Admin';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Fecha en que se registr� el empleado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Empleados',
@level2type = N'Column', @level2name = 'Fecha_alta';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Fecha de nacimiento del empleado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Empleados',
@level2type = N'Column', @level2name = 'Fecha_nac';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Registro Federal de Contribuyentes. Es una clave que requiere toda persona f�sica o moral en M�xico para realizar cualquier actividad econ�mica l�cita por la que est� obligada a pagar impuestos.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Empleados',
@level2type = N'Column', @level2name = 'RFC';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Clave �nica de Registro de Poblaci�n. C�digo alfanum�rico de 18 caracteres usado para identificar residentes y ciudadanos mexicanos.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Empleados',
@level2type = N'Column', @level2name = 'CURP';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Clave de ingreso del empleado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Empleados',
@level2type = N'Column', @level2name = 'Contrase�a';

-- /////////////// RECIBOS /////////////////////////////////////////////////
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del los niveles de kilowatts.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Kilowatts',
@level2type = N'Column', @level2name = 'ID_KW';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Kilowatts de consumo nivel b�sico.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Kilowatts',
@level2type = N'Column', @level2name = 'KW_basico';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Kilowatts de consumo nivel intermedio.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Kilowatts',
@level2type = N'Column', @level2name = 'KW_intermedio';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Kilowatts de consumo nivel excedente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Kilowatts',
@level2type = N'Column', @level2name = 'KW_excedente';

-- /////////////// RECIBOS /////////////////////////////////////////////////
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Fecha de vencimiento del pago del recibo.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Fecha_venc';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes del periodo de facturaci�n.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Mes';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'A�o del periodo de facturaci�n.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'A�o';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del empleado que gener� el recibo.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Num_Empleado';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Precio del consumo b�sico.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Precio_basico';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Precio del consumo intermedio.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Precio_intermedio';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Precio del consumo excedente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Precio_excedente';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Cargo del Impuesto de Valor Agregado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Cargo_IVA';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Valor del pago total del recibo.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Pago_total';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Importe del pago del cliente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Importe_pago';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Pago pendiente del cliente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Pago_pendiente';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Tipo de servicio. 0 si es Dom�stico, 1 si es Industrial.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Tipo_servicio';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Define si el recibo fu� pagado. 0 si no ha sido pagado, 1 si fue efectuado el pago.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Pagado';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del recibo.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'ID_Recibo';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'N�mero del medidor instalado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Medidor';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del consumo al que corresponde el recibo.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'ID_Consumo';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del servicio al que corresponde el recibo.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'ID_Servicio';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Domicilio del servicio instalado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Recibos',
@level2type = N'Column', @level2name = 'Domicilio';

-- /////////////// REGISTRO_ACTIVIDAD /////////////////////////////////////////////////
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = '�Movimiento o actividad realizada por el empleado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Registro_actividad',
@level2type = N'Column', @level2name = 'Acci�n';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Descripci�n de la acci�n efectuada.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Registro_actividad',
@level2type = N'Column', @level2name = 'Descripci�n';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del registro de actividad.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Registro_actividad',
@level2type = N'Column', @level2name = 'CLAVE';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del empleado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Registro_actividad',
@level2type = N'Column', @level2name = 'Num_Empleado';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Fecha y hora de la acci�n efectuada.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Registro_actividad',
@level2type = N'Column', @level2name = 'Fecha_registro';

-- /////////////// SERVICIOS /////////////////////////////////////////////////
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Fecha de alta del servicio.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Servicios',
@level2type = N'Column', @level2name = 'Fecha_alta';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del servicio.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Servicios',
@level2type = N'Column', @level2name = 'ID_Servicio';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'N�mero del medidor instalado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Servicios',
@level2type = N'Column', @level2name = 'Medidor';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del cliente que contrata el servicio.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Servicios',
@level2type = N'Column', @level2name = 'ID_Cliente';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del empleado que registra el servicio.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Servicios',
@level2type = N'Column', @level2name = 'Num_Empleado';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Tipo del servicio. 0 si es dom�stico, 1 si es industrial.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Servicios',
@level2type = N'Column', @level2name = 'Tipo_servicio';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Habilitador del servicio. 0 si fue dado de baja, 1 si sigue activo.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Servicios',
@level2type = N'Column', @level2name = 'Activo';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Domicilio donde se instala el servicio.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Servicios',
@level2type = N'Column', @level2name = 'Domicilio';

-- /////////////// TARIFAS /////////////////////////////////////////////////
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Tipo de servicio. 0 si es dom�stico, 1 si es industrial.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Tarifas',
@level2type = N'Column', @level2name = 'Tipo_servicio';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID de la tarifa.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Tarifas',
@level2type = N'Column', @level2name = 'ID_Tarifa';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'A�o del periodo de facturaci�n.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Tarifas',
@level2type = N'Column', @level2name = 'A�o';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del empleado que registra la tarifa.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Tarifas',
@level2type = N'Column', @level2name = 'Num_Empleado';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes del periodo de facturaci�n.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Tarifas',
@level2type = N'Column', @level2name = 'Mes';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Valor de la tarifa de consumo b�sico.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Tarifas',
@level2type = N'Column', @level2name = 'Tar_basica';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Valor de la tarifa de consumo intermedio.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Tarifas',
@level2type = N'Column', @level2name = 'Tar_intermedia';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Valor de la tarifa de consumo excedente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Tarifas',
@level2type = N'Column', @level2name = 'Tar_excedente';

-- /////////////// TELEFONOS CLIENTES /////////////////////////////////////////////////
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del cliente due�o del tel�fono.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Telefonos_client',
@level2type = N'Column', @level2name = 'ID_Cliente';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Tel�fono del cliente.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Telefonos_client',
@level2type = N'Column', @level2name = 'Tel�fono';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del tel�fono.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Telefonos_client',
@level2type = N'Column', @level2name = 'ID_Telcl';

-- /////////////// TELEFONOS EMPLEADOS /////////////////////////////////////////////////
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del tel�fono.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Telefonos_emp',
@level2type = N'Column', @level2name = 'ID_Telemp';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'ID del empleado due�o del tel�fono.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Telefonos_emp',
@level2type = N'Column', @level2name = 'Num_Empleado';
GO
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Tel�fono del empleado.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'Telefonos_emp',
@level2type = N'Column', @level2name = 'Tel�fono';