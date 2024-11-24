



create database gestionProductos;

use gestionProductos


create table proveedores(
Id_Proveedor int Identity(1,1) not null primary key,
     Empresa Varchar(50) not null,
	 Email Varchar(50) not null,
	 Direccion nvarchar(max) not null,
	 Numero_Telefono varchar(50) not null
);


create table categorias(
Id_Categoria int Identity(1,1) not null primary key, 
	Nombre_Categoria varchar(50) not null, 
	Descripcion nvarchar(max) not null
);


create table productos(
Id_Producto int Identity(1,1) not null primary key, 
	Nombre_Producto varchar(50) not null,
	Precio decimal(10, 2) not null, 
	Id_Proveedor int not null, 
	Id_Categoria int not null,
	Descripcion nvarchar(max) not null, 
	stock int,
	foreign key (Id_Proveedor) references proveedores(Id_Proveedor),
	foreign key (Id_Categoria) references categorias(Id_Categoria)
);


create table usuarios(
Id_Usuario int Identity(1,1) not null primary key, 
	Nombre varchar(50) not null,
	Apellido varchar(50) not null,
	Direccion varchar(255) not null, 
	Usuario varchar(50) not null, 
	Contra varchar(50), 
	Correo varchar(50),
	Fecha_Nacimiento date
);

/*procedimientos almacenados*/


/*agregar categoria*/
CREATE PROCEDURE AgregarCategoria
    @NombreCategoria VARCHAR(255),
    @Descripcion VARCHAR(MAX)
AS
BEGIN
    INSERT INTO categorias (Nombre_Categoria, Descripcion)
    VALUES (@NombreCategoria, @Descripcion);
END;

/*actualizar categoria*/
CREATE PROCEDURE ActualizarCategoria
	@IdCategoria int,
	@NombreCategoria VARCHAR(255),
    @Descripcion VARCHAR(MAX)
	AS	
	BEGIN
	UPDATE categorias SET Nombre_Categoria =@NombreCategoria, Descripcion =@Descripcion
	where Id_Categoria = @IdCategoria
END;


CREATE PROCEDURE EliminarCategoria
	@IdCategoria int
	AS	
	BEGIN
	DELETE FROM categorias where Id_Categoria = @IdCategoria
END;


CREATE PROCEDURE AgregarProveedor
	@Empresa varchar(50),
	@Email varchar(50),
	@Direccion nvarchar(max) , 
	@NumeroTelefono varchar(50)
	AS
	BEGIN
	INSERT INTO proveedores(Empresa, Email, Direccion, Numero_Telefono) VALUES
	(@Empresa, @Email, @Direccion, @NumeroTelefono)
	END;

CREATE PROCEDURE ActualizarProveedor
	@IdProveedor int,
	@Empresa varchar(50),
	@Email varchar(50),
	@Direccion nvarchar(max) , 
	@NumeroTelefono varchar(50)
	AS
	BEGIN
	UPDATE proveedores set Empresa = @Empresa, Email = @Email, Direccion = @Direccion, Numero_Telefono = @NumeroTelefono
	WHERE Id_Proveedor = @IdProveedor

END;

CREATE PROCEDURE EliminarProveedor
	@IdProveedor int
	AS	
	BEGIN
	DELETE FROM proveedores where Id_Proveedor = @IdProveedor
END;


CREATE PROCEDURE GetProveedores
	AS
	BEGIN 
	SELECT Id_Proveedor, Empresa From proveedores
END

CREATE PROCEDURE GetCategorias
	AS
	BEGIN
	SELECT Id_Categoria, Nombre_Categoria from categorias
END

alter table Productos add Id_Usuario int

alter table Productos 
add foreign key(Id_Usuario) references usuarios(Id_Usuario)

CREATE PROCEDURE AgregarProducto
	@NombreProducto varchar(50),
	@Precio decimal(10,2),
	@IdProveedor int,
	@IdCategoria int,
	@Descripcion nvarchar(max),
	@Stock int,
	@IdUsuario int,
	@Imagen image
	AS
	BEGIN
	INSERT INTO productos(Nombre_Producto, Precio, Id_Proveedor, Id_Categoria, Descripcion, stock, Id_Usuario, Imagen)
	VALUES (@NombreProducto, @Precio, @IdProveedor, @IdCategoria, @Descripcion, @Stock, @IdUsuario, @Imagen)
END


select * from proveedores

insert into usuarios values('Diego', 'Ramirez', 'Calle de la amargura', 'ede69', 'cRDtpNCeBiql5KOQsKVyrA0sAiA=', 'diego@gmail.com', '2002-12-16')



CREATE PROCEDURE GetProductosList
	AS 
	BEGIN

	SELECT Id_Producto,Nombre_Producto as Producto, Precio, p.Empresa, c.Nombre_Categoria as Categoria,
	productos.Id_categoria,productos.Id_Proveedor, productos.Descripcion, stock, u.Usuario, Imagen FROM productos 
	INNER JOIN proveedores p on productos.Id_Proveedor = p.Id_Proveedor 
	INNER JOIN categorias c on productos.Id_categoria = c.Id_categoria 
	INNER JOIN usuarios u on productos.Id_Usuario = u.Id_Usuario
END

CREATE PROCEDURE ActualizarProductos
	@IdProducto int,
	@NombreProducto varchar(50),
	@Precio decimal(10,2),
	@IdProveedor int,
	@IdCategoria int,
	@Descripcion nvarchar(max),
	@Stock int,
	@Imagen image
	AS
	BEGIN
	UPDATE productos SET Nombre_Producto = @NombreProducto, Precio = @Precio, Id_Proveedor = @IdProveedor, Id_Categoria = @IdCategoria,
	Descripcion = @Descripcion, stock = @Stock,  Imagen = ISNULL(@Imagen, Imagen) WHERE Id_Producto = @IdProducto
END


CREATE PROCEDURE EliminarProducto
	@IdProducto int
	AS
	BEGIN
	DELETE FROM productos WHERE Id_Producto = @IdProducto
END


CREATE PROCEDURE ActualizarUsuario
	@IdUsuario int,
	@Nombre varchar(50),
	@Apellido varchar (50),
	@Direccion nvarchar(max),
	@Usuario varchar(50),
	@Correo varchar(50),
	@FechaNac date
	AS 
	BEGIN
	UPDATE usuarios SET Nombre = @Nombre, Apellido = @Apellido, Direccion = @Direccion,
	Usuario = @Usuario, Correo = @Correo, Fecha_Nacimiento = @FechaNac
	WHERE Id_Usuario = @IdUsuario
END



CREATE PROCEDURE AgregarUsuario
	@Nombre varchar(50),
	@Apellido varchar (50),
	@Direccion nvarchar(max),
	@Contra varchar(50),
	@Usuario varchar(50),
	@Correo varchar(50),
	@FechaNac date
	AS 
	BEGIN
	INSERT INTO usuarios VALUES(@Nombre, @Apellido, @Direccion, @Usuario, @Contra, @Correo, @FechaNac)
END

drop procedure AgregarUsuario

SELECT * FROM usuarios