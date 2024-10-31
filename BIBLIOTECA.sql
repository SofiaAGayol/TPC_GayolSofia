USE master;

ALTER DATABASE BIBLIO_DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
IF DB_ID('BIBLIO_DB') IS NOT NULL
BEGIN
    DROP DATABASE BIBLIO_DB;
END;

CREATE DATABASE BIBLIO_DB;
GO
USE BIBLIO_DB;
GO

-- Tabla Nacionalidad
CREATE TABLE Nacionalidad (
    IdNacionalidad INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(100)
);
GO

-- Tabla Usuarios
CREATE TABLE Usuarios (
    IDUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Usuario NVARCHAR(50) NOT NULL,
    Clave NVARCHAR(50) NOT NULL,
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    DNI CHAR(8) NOT NULL,
    Email NVARCHAR(100),
    Telefono NVARCHAR(15),
    IDRol INT NOT NULL,
    CONSTRAINT FK_Usuarios_Rol FOREIGN KEY (IDRol) REFERENCES Rol(IDRol)
);
GO

-- Tabla Roles
CREATE TABLE Rol (
    IDRol INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(100)
);
GO

-- Tabla TipoMembresia
CREATE TABLE TipoMembresia (
    IDTipoMembresia INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(255),
    Costo DECIMAL(10, 2),
    LibrosALaVez INT,
    LibrosXMes INT,
    DuracionMeses INT,
    Estado BIT
);
GO

-- Tabla MetodoDePago
CREATE TABLE MetodoDePago(
    IDMetodoPago INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT,
    TipoTarjeta NVARCHAR(50),
    NroTarjeta NVARCHAR(20),
    Vencimiento DATE,
    Cod NVARCHAR(10),
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario)
);
GO

-- Tabla Pago
CREATE TABLE Pago (
    IDPago INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT,
    IDTipoMembresia INT,
    IDMetodoPago INT,
    FechaPago DATE,
    Monto DECIMAL(10, 2),
    Estado BIT,
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    FOREIGN KEY (IDTipoMembresia) REFERENCES TipoMembresia(IDTipoMembresia),
    FOREIGN KEY (IDMetodoPago) REFERENCES MetodoDePago(IDMetodoPago)
);
GO

-- Tabla Membresias
CREATE TABLE Membresias (
    IDMembresia INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT,
    IDTipoMembresia INT,
    FechaInicio DATE,
    FechaFin DATE,
    IDPago INT,
    Estado BIT,
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    FOREIGN KEY (IDTipoMembresia) REFERENCES TipoMembresia(IDTipoMembresia),
    FOREIGN KEY (IDPago) REFERENCES Pago(IDPago)
);
GO

-- Tabla Categoria
CREATE TABLE Categoria (
    IDCategoria INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(100)
);
GO

-- Tabla Autores
CREATE TABLE Autores (
    IDAutor INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100),
    Apellido NVARCHAR(100),
    IdNacionalidad INT,
    BestSeller NVARCHAR(100),
    FOREIGN KEY (IdNacionalidad) REFERENCES Nacionalidad(IdNacionalidad)
);
GO
--ACTUALIZACION DE DB
-- ALTER TABLE Usuarios
--     ADD Estado BIT DEFAULT 1;
-- GO
-- UPDATE Usuarios
-- SET Estado = 1;  
-- GO

-- Tabla Libro
CREATE TABLE Libro (
    IDLibro INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(255),
    IDAutor INT,
    IDCategoria INT,
    FechaPublicacion DATE,
    Ejemplares INT,
    Disponibles INT,
    Estado BIT,
    ImagenURL NVARCHAR(255),
    FOREIGN KEY (IDAutor) REFERENCES Autores(IDAutor),
    FOREIGN KEY (IDCategoria) REFERENCES Categoria(IDCategoria)
);
GO

-- Tabla Prestamo
CREATE TABLE Prestamo (
    IDPrestamo INT IDENTITY(1,1) PRIMARY KEY,
    IDLibro INT,
    IDUsuario INT,
    FechaInicio DATE,
    FechaFin DATE,
    Devuelto BIT,
    FOREIGN KEY (IDLibro) REFERENCES Libro(IDLibro),
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario)
);
GO

-- Registro de ingresos
CREATE TABLE Ingresos (
    IDIngreso INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT,
    FechaIngreso DATETIME,
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario)
);
GO


----------------------------------------------------------INSERTS---------------------


INSERT INTO Nacionalidad (Descripcion) VALUES
('Argentina'),
('Colombia'),
('Reino Unido'),
('Francia'),
('Rusia');

INSERT INTO Rol (Descripcion) VALUES
('Gerente'),
('Asistente'),
('Vendedor');

INSERT INTO TipoMembresia (Descripcion, Costo, LibrosALaVez, LibrosXMes, DuracionMeses, Estado) VALUES
('Básica', 100.00, 2, 5, 12, 1),
('Premium', 200.00, 5, 15, 12, 1);

INSERT INTO Usuarios (Usuario, Clave, Nombre, Apellido, DNI, Email, Telefono, IDRol) VALUES
('admin', 'admin', 'Guido', 'Serco', '12345678', 'guido.serco@example.com', '123456789', 1);

INSERT INTO MetodoDePago (IDUsuario, TipoTarjeta, NroTarjeta, Vencimiento, Cod) VALUES
(1, 'Visa', '4111111111111111', '2025-12-31', '123');

INSERT INTO Pago (IDUsuario, IDTipoMembresia, IDMetodoPago, FechaPago, Monto, Estado) VALUES
(1, 1, 1, GETDATE(), 100.00, 1);

INSERT INTO Membresias (IDUsuario, IDTipoMembresia, FechaInicio, FechaFin, IDPago, Estado) VALUES
(1, 1, GETDATE(), DATEADD(MONTH, 12, GETDATE()), 1, 1);

INSERT INTO Categoria (Descripcion) VALUES
('Novela'),
('Ciencia Ficción'),
('Fantasía'),
('Historia'),
('Biografía');

INSERT INTO Autores (Nombre, Apellido, IdNacionalidad, BestSeller) VALUES
('Gabriel', 'García Márquez', 2, 'Cien años de soledad'),
('George', 'Orwell', 3, '1984'),
('Julio', 'Cortázar', 1, 'Rayuela'),
('Victor', 'Hugo', 4, 'Los Miserables'),
('Fiódor', 'Dostoyevski', 5, 'Crimen y Castigo'),
('Carlos', 'Ruiz Zafón', 1, 'La sombra del viento'),
('Paulo', 'Coelho', 2, 'El alquimista'),
('Antonio', 'Serrano', 3, 'Los ojos del perro siberiano'),
('J.K.', 'Rowling', 4, 'Harry Potter y la piedra filosofal'),
('Fernando', 'Gaitán', 5, 'Crónicas del engaño'),
('Horacio', 'Quiroga', 6, 'Cuentos de la selva'),
('Jorge', 'Bucay', 7, 'El camino de la autodependencia'),
('Aldous', 'Huxley', 8, 'Un mundo feliz'),
('Mark', 'Twain', 9, 'Las aventuras de Tom Sawyer');

INSERT INTO Libro (Titulo, IDAutor, IDCategoria, FechaPublicacion, Ejemplares, Disponibles, Estado, ImagenURL) VALUES
('Cien años de soledad', 1, 1, '1967-05-30', 10, 10, 1, 'https://images.cdn3.buscalibre.com/fit-in/360x360/61/8d/618d227e8967274cd9589a549adff52d.jpg'),
('1984', 2, 2, '1949-06-08', 8, 8, 1, 'https://images.cdn2.buscalibre.com/fit-in/360x360/33/f9/33f911d9a7ba713874725a96c341733f.jpg'),
('Rayuela', 3, 1, '1963-06-28', 5, 5, 1, 'https://acdn.mitiendanube.com/stores/746/904/products/6614591-76c5862056c5fbaa9716191355361511-640-0.jpeg'),
('Los Miserables', 4, 4, '1862-01-01', 4, 4, 1, 'https://http2.mlstatic.com/D_NQ_NP_637594-MLU54957962817_042023-O.webp'),
('Crimen y Castigo', 5, 1, '1866-01-01', 6, 6, 1, 'https://images.cdn1.buscalibre.com/fit-in/360x360/ea/1f/ea1fc691874fa49ce341d876a981e2c1.jpg'),
('El amor en los tiempos del cólera', 1, 1, '1985-11-06', 10, 10, 1, 'https://images.cdn1.buscalibre.com/fit-in/360x360/b0/3e/b03e98118b9e2cf5b94bb0548bfa59c5.jpg'),
('La sombra del viento', 6, 1, '2001-04-17', 7, 7, 1, 'https://www.planetadelibros.com.ar/usuaris/libros/fotos/334/m_libros/portada_la-sombra-del-viento_carlos-ruiz-zafon_202105042109.jpg'),
('El alquimista', 7, 2, '1988-05-01', 5, 5, 1, 'https://images.cdn2.buscalibre.com/fit-in/360x360/a2/4b/a24bec7258f27d6764fe89e0556757b7.jpg'),
('Los ojos del perro siberiano', 8, 1, '1992-01-01', 6, 6, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRRCycncw_cG7dU-seuz5D0_lSPCm3TFtM9bw&s'),
('Harry Potter y la piedra filosofal', 9, 2, '1997-06-26', 8, 8, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS5mnOw7KHOYPmUKRbAthNAK-TEgt0Xpu9tqQ&s'),
('Don Quijote de la Mancha', 6, 4, '1605-01-16', 10, 10, 1, 'https://images.cdn2.buscalibre.com/fit-in/360x360/51/55/5155e6fc0413fb43cd1b410217df7fb2.jpg'),
('Cuentos de la selva', 11, 3, '1918-01-01', 5, 5, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRuemVnVB-mJHzHJjhlzD7kdI2R8C0w55Kzwg&s'),
('El camino de la autodependencia', 12, 1, '1990-01-01', 3, 3, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRe3uN4i3zMOeC9hPIUDCfXu07vDzjfnp8NRA&s'),
('Un mundo feliz', 13, 2, '1932-01-01', 6, 6, 1, 'https://losresumenes.com/wp-content/uploads/2023/11/Aldous-Huxley-Un-mundo-feliz.jpg'),
('Las aventuras de Tom Sawyer', 14, 4, '1876-01-01', 7, 7, 1, 'https://images.cdn2.buscalibre.com/fit-in/360x360/91/14/9114855ea78255e0e1ff72f287973cb0.jpg');

--para que puedas actualizar las imagenes si ya habias corrido el sql

/*
UPDATE Libro SET ImagenURL = 
'https://images.cdn3.buscalibre.com/fit-in/360x360/61/8d/618d227e8967274cd9589a549adff52d.jpg'
WHERE Titulo = 'Cien años de soledad';  

UPDATE Libro SET ImagenURL = 
'https://images.cdn2.buscalibre.com/fit-in/360x360/33/f9/33f911d9a7ba713874725a96c341733f.jpg'
WHERE Titulo = '1984';  
UPDATE Libro SET ImagenURL = 
'https://acdn.mitiendanube.com/stores/746/904/products/6614591-76c5862056c5fbaa9716191355361511-640-0.jpeg'
WHERE Titulo = 'Rayuela';  
UPDATE Libro SET ImagenURL = 
'https://http2.mlstatic.com/D_NQ_NP_637594-MLU54957962817_042023-O.webp'
WHERE Titulo = 'Los Miserables';  
UPDATE Libro SET ImagenURL = 
'https://images.cdn1.buscalibre.com/fit-in/360x360/ea/1f/ea1fc691874fa49ce341d876a981e2c1.jpg'
WHERE Titulo = 'Crimen y Castigo';  
UPDATE Libro SET ImagenURL = 
'https://images.cdn1.buscalibre.com/fit-in/360x360/b0/3e/b03e98118b9e2cf5b94bb0548bfa59c5.jpg'
WHERE Titulo = 'El amor en los tiempos del cólera';  
UPDATE Libro SET ImagenURL = 
'https://www.planetadelibros.com.ar/usuaris/libros/fotos/334/m_libros/portada_la-sombra-del-viento_carlos-ruiz-zafon_202105042109.jpg'
WHERE Titulo = 'La sombra del viento';  
UPDATE Libro SET ImagenURL = 
'https://images.cdn2.buscalibre.com/fit-in/360x360/a2/4b/a24bec7258f27d6764fe89e0556757b7.jpg'
WHERE Titulo = 'El alquimista';  
UPDATE Libro SET ImagenURL = 
'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRRCycncw_cG7dU-seuz5D0_lSPCm3TFtM9bw&s'
WHERE Titulo = 'Los ojos del perro siberiano';  
UPDATE Libro SET ImagenURL = 
'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS5mnOw7KHOYPmUKRbAthNAK-TEgt0Xpu9tqQ&s'
WHERE Titulo = 'Harry Potter y la piedra filosofal';  
UPDATE Libro SET ImagenURL = 
'https://images.cdn2.buscalibre.com/fit-in/360x360/51/55/5155e6fc0413fb43cd1b410217df7fb2.jpg'
WHERE Titulo = 'Don Quijote de la Mancha';  
UPDATE Libro SET ImagenURL = 
'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRuemVnVB-mJHzHJjhlzD7kdI2R8C0w55Kzwg&s'
WHERE Titulo = 'Cuentos de la selva';  
UPDATE Libro SET ImagenURL = 
'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRe3uN4i3zMOeC9hPIUDCfXu07vDzjfnp8NRA&s'
WHERE Titulo = 'El camino de la autodependencia';  
UPDATE Libro SET ImagenURL = 
'https://losresumenes.com/wp-content/uploads/2023/11/Aldous-Huxley-Un-mundo-feliz.jpg'
WHERE Titulo = 'Un mundo feliz';  
*/

/*
INSERT INTO Rol (Descripcion) VALUES ('Cliente');

INSERT INTO Usuarios (Usuario, Clave, Nombre, Apellido, DNI, Email, Telefono, IDRol) VALUES 
 ('cliente1', 'cliente123', 'Sofia', 'Lopez', '23456789', 'sofia.lopez@example.com', '987654321', 2),
 ('bibliotecario1', 'biblio123', 'Carlos', 'Perez', '34567890', 'carlos.perez@example.com', '456123789', 3),
 ('cliente2', 'cliente456', 'Maria', 'Gomez', '45678901', 'maria.gomez@example.com', '321654987', 2),
 ('invitado1', 'invitado123', 'Luis', 'Martinez', '56789012', 'luis.martinez@example.com', '789321654', 4);
 */