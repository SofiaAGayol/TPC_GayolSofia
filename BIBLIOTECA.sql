-- CREATE DATABASE BIBLIO_DB
-- GO
-- USE BIBLIO_DB;
-- GO

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

-- Tabla Clientes
CREATE TABLE Clientes (
    IDCliente INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT,
    DNI INT,
    Nombre NVARCHAR(100),
    Apellido NVARCHAR(100),
    Email NVARCHAR(100),
    Telefono NVARCHAR(50),
    IDMembresia INT,
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    FOREIGN KEY (IDMembresia) REFERENCES Membresias(IDMembresia)
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


INSERT INTO Clientes (IDUsuario, DNI, Nombre, Apellido, Email, Telefono, IDMembresia) VALUES
(1, 30123456, 'Juan', 'Pérez', 'juan.perez@example.com', '123456789', NULL);


INSERT INTO Bibliotecarios (IDUsuario, DNI, Nombre, Apellido, Email, Telefono, IDCargo) VALUES
(2, 27123456, 'María', 'Gómez', 'maria.gomez@example.com', '987654321', 1);

INSERT INTO MetodoDePago (IDCliente, TipoTarjeta, NroTarjeta, Vencimiento, Cod) VALUES
(1, 'Visa', '4111111111111111', '2025-12-31', '123');

INSERT INTO Pago (IDCliente, IDTipoMembresia, IDMetodoPago, FechaPago, Monto, Estado) VALUES
(1, 1, 1, GETDATE(), 100.00, 1);

INSERT INTO Membresias (IDCliente, IDTipoMembresia, FechaInicio, FechaFin, IDPago, Estado) VALUES
(1, 1, GETDATE(), DATEADD(MONTH, 12, GETDATE()), 1, 1);

UPDATE Clientes SET IDMembresia = 1 WHERE IDCliente = 1;

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

INSERT INTO Libro (Titulo, IDAutor, IDCategoria, FechaPublicacion, Ejemplares, Disponibles, Estado, ImagenURL) VALUES
('Cien años de soledad', 1, 1, '1967-05-30', 10, 10, 1, 'http://example.com/cien_anos.jpg'),
('1984', 2, 2, '1949-06-08', 8, 8, 1, 'http://example.com/1984.jpg'),
('Rayuela', 3, 1, '1963-06-28', 5, 5, 1, 'http://example.com/rayuela.jpg'),
('Los Miserables', 4, 4, '1862-01-01', 4, 4, 1, 'http://example.com/los_miserables.jpg'),
('Crimen y Castigo', 5, 1, '1866-01-01', 6, 6, 1, 'http://example.com/crimen_y_castigo.jpg');

INSERT INTO Ingresos (IDUsuario, FechaIngreso) VALUES
(1, GETDATE());


