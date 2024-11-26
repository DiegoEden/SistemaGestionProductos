-- Crear la base de datos 'gestionProductos'
create database gestionProductos;

-- Usar la base de datos 'gestionProductos'
use gestionProductos;

-- Crear tabla de proveedores
create table proveedores(
    Id_Proveedor int Identity(1,1) not null primary key,
    Empresa Varchar(50) not null,
    Email Varchar(50) not null,
    Direccion nvarchar(max) not null,
    Numero_Telefono varchar(50) not null
);

-- Crear tabla de categorías de productos
create table categorias(
    Id_Categoria int Identity(1,1) not null primary key,
    Nombre_Categoria varchar(50) not null,
    Descripcion nvarchar(max) not null
);

-- Crear tabla de productos con claves foráneas a proveedores y categorías
create table productos(
    Id_Producto int Identity(1,1) not null primary key,
    Nombre_Producto varchar(50) not null,
    Precio decimal(10, 2) not null,
    Id_Proveedor int not null, 
    Id_Categoria int not null,
    Descripcion nvarchar(max) not null,
    stock int,
    -- Claves foráneas que referencian a proveedores y categorías
    foreign key (Id_Proveedor) references proveedores(Id_Proveedor),
    foreign key (Id_Categoria) references categorias(Id_Categoria)
);

-- Crear tabla de usuarios
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

-- Procedimiento almacenado para agregar una categoría
CREATE PROCEDURE AgregarCategoria
    @NombreCategoria VARCHAR(255),
    @Descripcion VARCHAR(MAX)
AS
BEGIN
    INSERT INTO categorias (Nombre_Categoria, Descripcion)
    VALUES (@NombreCategoria, @Descripcion);
END;

-- Procedimiento almacenado para actualizar una categoría
CREATE PROCEDURE ActualizarCategoria
    @IdCategoria int,
    @NombreCategoria VARCHAR(255),
    @Descripcion VARCHAR(MAX)
AS    
BEGIN
    UPDATE categorias 
    SET Nombre_Categoria = @NombreCategoria, Descripcion = @Descripcion
    WHERE Id_Categoria = @IdCategoria
END;

-- Procedimiento almacenado para eliminar una categoría
CREATE PROCEDURE EliminarCategoria
    @IdCategoria int
AS    
BEGIN
    DELETE FROM categorias WHERE Id_Categoria = @IdCategoria
END;

-- Procedimiento almacenado para agregar un proveedor
CREATE PROCEDURE AgregarProveedor
    @Empresa varchar(50),
    @Email varchar(50),
    @Direccion nvarchar(max), 
    @NumeroTelefono varchar(50)
AS
BEGIN
    INSERT INTO proveedores(Empresa, Email, Direccion, Numero_Telefono) 
    VALUES (@Empresa, @Email, @Direccion, @NumeroTelefono)
END;

-- Procedimiento almacenado para actualizar un proveedor
CREATE PROCEDURE ActualizarProveedor
    @IdProveedor int,
    @Empresa varchar(50),
    @Email varchar(50),
    @Direccion nvarchar(max), 
    @NumeroTelefono varchar(50)
AS
BEGIN
    UPDATE proveedores 
    SET Empresa = @Empresa, Email = @Email, Direccion = @Direccion, Numero_Telefono = @NumeroTelefono
    WHERE Id_Proveedor = @IdProveedor
END;

-- Procedimiento almacenado para eliminar un proveedor
CREATE PROCEDURE EliminarProveedor
    @IdProveedor int
AS    
BEGIN
    DELETE FROM proveedores WHERE Id_Proveedor = @IdProveedor
END;

-- Procedimiento almacenado para obtener todos los proveedores
CREATE PROCEDURE GetProveedores
AS
BEGIN 
    SELECT Id_Proveedor, Empresa FROM proveedores
END;

-- Procedimiento almacenado para obtener todas las categorías
CREATE PROCEDURE GetCategorias
AS
BEGIN
    SELECT Id_Categoria, Nombre_Categoria FROM categorias
END;

-- Agregar columna Id_Usuario a la tabla Productos
alter table Productos add Id_Usuario int;

-- Establecer la clave foránea entre productos y usuarios
alter table Productos 
add foreign key(Id_Usuario) references usuarios(Id_Usuario);

-- Procedimiento almacenado para agregar un producto
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
END;

-- Procedimiento almacenado para obtener la lista de productos
CREATE PROCEDURE GetProductosList
AS 
BEGIN
    SELECT Id_Producto, Nombre_Producto as Producto, Precio, p.Empresa, 
           c.Nombre_Categoria as Categoria, productos.Id_categoria, 
           productos.Id_Proveedor, productos.Descripcion, stock, u.Usuario, Imagen 
    FROM productos 
    INNER JOIN proveedores p on productos.Id_Proveedor = p.Id_Proveedor 
    INNER JOIN categorias c on productos.Id_categoria = c.Id_categoria 
    INNER JOIN usuarios u on productos.Id_Usuario = u.Id_Usuario
END;

-- Procedimiento almacenado para actualizar un producto
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
    UPDATE productos 
    SET Nombre_Producto = @NombreProducto, Precio = @Precio, 
        Id_Proveedor = @IdProveedor, Id_Categoria = @IdCategoria,
        Descripcion = @Descripcion, stock = @Stock,  
        Imagen = ISNULL(@Imagen, Imagen) 
    WHERE Id_Producto = @IdProducto
END;

-- Procedimiento almacenado para eliminar un producto
CREATE PROCEDURE EliminarProducto
    @IdProducto int
AS
BEGIN
    DELETE FROM productos WHERE Id_Producto = @IdProducto
END;

-- Procedimiento almacenado para actualizar datos de un usuario
CREATE PROCEDURE ActualizarUsuario
    @IdUsuario int,
    @Nombre varchar(50),
    @Apellido varchar(50),
    @Direccion nvarchar(max),
    @Usuario varchar(50),
    @Correo varchar(50),
    @FechaNac date
AS 
BEGIN
    UPDATE usuarios 
    SET Nombre = @Nombre, Apellido = @Apellido, Direccion = @Direccion,
        Usuario = @Usuario, Correo = @Correo, Fecha_Nacimiento = @FechaNac
    WHERE Id_Usuario = @IdUsuario
END;

-- Procedimiento almacenado para agregar un usuario
CREATE PROCEDURE AgregarUsuario
    @Nombre varchar(50),
    @Apellido varchar(50),
    @Direccion nvarchar(max),
    @Contra varchar(50),
    @Usuario varchar(50),
    @Correo varchar(50),
    @FechaNac date
AS 
BEGIN
    INSERT INTO usuarios VALUES(@Nombre, @Apellido, @Direccion, @Usuario, @Contra, @Correo, @FechaNac)
END;

-- Procedimiento almacenado para obtener todos los usuarios excepto el usuario con el id especificado
CREATE PROCEDURE GetUsuarios
    @IdUsuario int
AS 
BEGIN
    SELECT Id_Usuario, Nombre, Apellido, Direccion, Usuario, Correo, Fecha_Nacimiento 
    FROM usuarios
    WHERE Id_Usuario != @IdUsuario
END;

-- Procedimiento almacenado para eliminar un usuario
CREATE PROCEDURE EliminarUsuario
    @IdUsuario int
AS 
BEGIN
    DELETE from usuarios WHERE Id_Usuario = @IdUsuario
END;

-- Procedimiento almacenado para verificar si el nombre de usuario ya existe
CREATE PROCEDURE CheckUsername
    @Usuario varchar(50),
    @IdUsuario int
AS
BEGIN
    SELECT * FROM usuarios 
    WHERE Usuario = @Usuario COLLATE SQL_Latin1_General_CP1_CS_AS 
    AND Id_Usuario != @IdUsuario
END;
