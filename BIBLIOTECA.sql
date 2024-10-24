-- CREATE DATABASE BIBLIO_DB
-- GO
-- USE BIBLIO_DB;
-- GO
/*
-- Tabla Nacionalidad
CREATE TABLE Nacionalidad (
    IdNacionalidad INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(100)
);
GO

-- Tabla Usuarios
CREATE TABLE Usuarios (
    IDUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Usuario NVARCHAR(100),
    Clave NVARCHAR(100)
);
GO

-- Tabla Cargo
CREATE TABLE Cargo (
    IDCargo INT IDENTITY(1,1) PRIMARY KEY,
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

-- Primero, crea la tabla Clientes
CREATE TABLE Clientes (
    IDCliente INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT,
    DNI INT,
    Nombre NVARCHAR(100),
    Apellido NVARCHAR(100),
    Email NVARCHAR(100),
    Telefono NVARCHAR(50),
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
);
GO

-- Tabla MetodoDePago
CREATE TABLE MetodoDePago(
    IDMetodoPago INT IDENTITY(1,1) PRIMARY KEY,
    IDCliente INT,
    TipoTarjeta NVARCHAR(50),
    NroTarjeta NVARCHAR(20),
    Vencimiento DATE,
    Cod NVARCHAR(10),
    FOREIGN KEY (IDCliente) REFERENCES Clientes(IDCliente)
);
GO

-- Tabla Pago
CREATE TABLE Pago (
    IDPago INT IDENTITY(1,1) PRIMARY KEY,
    IDCliente INT,
    IDTipoMembresia INT,
    IDMetodoPago INT,
    FechaPago DATE,
    Monto DECIMAL(10, 2),
    Estado BIT,
    FOREIGN KEY (IDCliente) REFERENCES Clientes(IDCliente),
    FOREIGN KEY (IDTipoMembresia) REFERENCES TipoMembresia(IDTipoMembresia),
    FOREIGN KEY (IDMetodoPago) REFERENCES MetodoDePago(IDMetodoPago)
);
GO

-- Tabla Membresias
CREATE TABLE Membresias (
    IDMembresia INT IDENTITY(1,1) PRIMARY KEY,
    IDCliente INT,
    IDTipoMembresia INT,
    FechaInicio DATE,
    FechaFin DATE,
    IDPago INT,
    Estado BIT,
    FOREIGN KEY (IDCliente) REFERENCES Clientes(IDCliente),
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

-- Tabla Bibliotecarios
CREATE TABLE Bibliotecarios (
    IDBibliotecario INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT,
    DNI INT,
    Nombre NVARCHAR(100),
    Apellido NVARCHAR(100),
    Email NVARCHAR(100),
    Telefono NVARCHAR(50),
    IDCargo INT,
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    FOREIGN KEY (IDCargo) REFERENCES Cargo(IDCargo)
);
GO

-- Tabla Prestamo
CREATE TABLE Prestamo (
    IDPrestamo INT IDENTITY(1,1) PRIMARY KEY,
    IDLibro INT,
    IDCliente INT,
    FechaInicio DATE,
    FechaFin DATE,
    Devuelto BIT,
    FOREIGN KEY (IDLibro) REFERENCES Libro(IDLibro),
    FOREIGN KEY (IDCliente) REFERENCES Clientes(IDCliente)
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
*/
/*
INSERT INTO Nacionalidad (Descripcion) VALUES
('Argentina'),
('Colombia'),
('Reino Unido'),
('Francia'),
('Rusia');

INSERT INTO Usuarios (Usuario, Clave) VALUES
('cliente1', 'clave123'),
('bibliotecario1', 'clave456');

INSERT INTO Cargo (Descripcion) VALUES
('Gerente'),
('Asistente'),
('Vendedor');

INSERT INTO TipoMembresia (Descripcion, Costo, LibrosALaVez, LibrosXMes, DuracionMeses, Estado) VALUES
('Básica', 100.00, 2, 5, 12, 1),
('Premium', 200.00, 5, 15, 12, 1);

/*
INSERT INTO Clientes (IDUsuario, DNI, Nombre, Apellido, Email, Telefono) VALUES
(1, 30123456, 'Juan', 'Pérez', 'juan.perez@example.com', '123456789');
*/

INSERT INTO Bibliotecarios (IDUsuario, DNI, Nombre, Apellido, Email, Telefono, IDCargo) VALUES
(2, 27123456, 'María', 'Gómez', 'maria.gomez@example.com', '987654321', 1);

INSERT INTO MetodoDePago (IDCliente, TipoTarjeta, NroTarjeta, Vencimiento, Cod) VALUES
(1, 'Visa', '4111111111111111', '2025-12-31', '123');

INSERT INTO Pago (IDCliente, IDTipoMembresia, IDMetodoPago, FechaPago, Monto, Estado) VALUES
(1, 1, 1, GETDATE(), 100.00, 1);

INSERT INTO Membresias (IDCliente, IDTipoMembresia, FechaInicio, FechaFin, IDPago, Estado) VALUES
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
('Fiódor', 'Dostoyevski', 5, 'Crimen y Castigo');

/*
INSERT INTO Libro (Titulo, IDAutor, IDCategoria, FechaPublicacion, Ejemplares, Disponibles, Estado, ImagenURL) VALUES
('Cien años de soledad', 1, 1, '1967-05-30', 10, 10, 1, 'http://example.com/cien_anos.jpg'),
('1984', 2, 2, '1949-06-08', 8, 8, 1, 'http://example.com/1984.jpg'),
('Rayuela', 3, 1, '1963-06-28', 5, 5, 1, 'http://example.com/rayuela.jpg'),
('Los Miserables', 4, 4, '1862-01-01', 4, 4, 1, 'http://example.com/los_miserables.jpg'),
('Crimen y Castigo', 5, 1, '1866-01-01', 6, 6, 1, 'http://example.com/crimen_y_castigo.jpg');
*/
INSERT INTO Ingresos (IDUsuario, FechaIngreso) VALUES
(1, GETDATE());

*/
/*
INSERT INTO Autores (Nombre, Apellido, IdNacionalidad, BestSeller) VALUES
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
('El amor en los tiempos del cólera', 1, 1, '1985-11-06', 10, 10, 1, 'http://example.com/el_amor_en_los_tiempos_del_colera.jpg'),
('La sombra del viento', 6, 1, '2001-04-17', 7, 7, 1, 'http://example.com/la_sombra_del_viento.jpg'),
('El alquimista', 7, 2, '1988-05-01', 5, 5, 1, 'http://example.com/el_alquimista.jpg'),
('Los ojos del perro siberiano', 8, 1, '1992-01-01', 6, 6, 1, 'http://example.com/los_ojos_del_perro_siberiano.jpg'),
('Harry Potter y la piedra filosofal', 9, 2, '1997-06-26', 8, 8, 1, 'http://example.com/harry_potter_y_la_piedra_filosofal.jpg'),
('Crónicas del engaño', 10, 4, '2010-03-01', 4, 4, 1, 'http://example.com/cronicas_del_engano.jpg'),
('Cuentos de la selva', 11, 3, '1918-01-01', 5, 5, 1, 'http://example.com/cuentos_de_la_selva.jpg'),
('El camino de la autodependencia', 12, 1, '1990-01-01', 3, 3, 1, 'http://example.com/el_camino_de_la_autodependencia.jpg'),
('Un mundo feliz', 13, 2, '1932-01-01', 6, 6, 1, 'http://example.com/un_mundo_feliz.jpg'),
('Las aventuras de Tom Sawyer', 14, 4, '1876-01-01', 7, 7, 1, 'http://example.com/las_aventuras_de_tom_sawyer.jpg');
*/

