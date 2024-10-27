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
    IDRol INT NOT NULL
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

INSERT INTO Cargo (Descripcion) VALUES
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
('Fiódor', 'Dostoyevski', 5, 'Crimen y Castigo');