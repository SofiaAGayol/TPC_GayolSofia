USE master;
GO

-- Eliminar la base de datos si ya existe
IF DB_ID('BIBLIO_DB') IS NOT NULL
BEGIN
    DROP DATABASE BIBLIO_DB;
END;

-- Crear la nueva base de datos
CREATE DATABASE BIBLIO_DB;
GO

-- Usar la nueva base de datos
USE BIBLIO_DB;
GO

-- Tabla Nacionalidad
CREATE TABLE Nacionalidad (
    IdNacionalidad INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(100)
);
GO

-- Tabla Roles
CREATE TABLE Rol (
    IDRol INT IDENTITY(1,1) PRIMARY KEY,
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
    Estado BIT DEFAULT 1,
    CONSTRAINT FK_Usuarios_Rol FOREIGN KEY (IDRol) REFERENCES Rol(IDRol)
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
CREATE TABLE MetodoDePago (
    IDMetodoPago INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT,
    TipoTarjeta NVARCHAR(50),
    NroTarjeta NVARCHAR(20),
    Vencimiento DATE,
    Cod NVARCHAR(10),
    Predeterminado BIT DEFAULT 0,
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
    Estado TINYINT,
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
    Estado BIT DEFAULT 1,
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
    IDMetodoEnvio INT,
    IDMetodoRetiro INT,
    CostoEnvio DECIMAL(10, 2),
    Estado NVARCHAR(50) DEFAULT 'Pendiente',
    FOREIGN KEY (IDLibro) REFERENCES Libro(IDLibro),
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    FOREIGN KEY (IDMetodoEnvio) REFERENCES MetodosDeEnvio(IDMetodoEnvio),
    FOREIGN KEY (IDMetodoRetiro) REFERENCES MetodosDeRetiro(IDMetodoRetiro)
);
GO

-- Tabla Ingresos
CREATE TABLE Ingresos (
    IDIngreso INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT,
    FechaIngreso DATETIME,
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario)
);
GO

CREATE TABLE Direccion (
    IDDireccion INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT,
    Calle NVARCHAR(100),
    Altura INT,
    CodigoPostal NVARCHAR(10),
    Aclaracion NVARCHAR(200) NULL,
    Predeterminada BIT DEFAULT 0
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario)
);

CREATE TABLE Envios (
    IDEnvio INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT,
    IDLibro INT,
    IDDireccion INT,
    FechaPedido DATE,
    FechaEnvio DATE,
    Estado INT,
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    FOREIGN KEY (IDLibro) REFERENCES Libro(IDLibro),
    FOREIGN KEY (IDDireccion) REFERENCES Direccion(IDDireccion)
);

CREATE TABLE Deudas (
    IDDeuda INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT,
    Monto DECIMAL(10, 2),
    FechaCreacion DATE DEFAULT GETDATE(),
    FechaPago DATE,
    Estado INT,
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario)
);

CREATE TABLE Carrito (
    IDCarrito INT IDENTITY(1,1) PRIMARY KEY,
    IDUsuario INT NOT NULL,
    IDLibro INT NOT NULL,
    FechaAgregado DATE NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    FOREIGN KEY (IDLibro) REFERENCES Libros(IDLibro)
);

CREATE TABLE MetodosDeEnvio (
    IDMetodoEnvio INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(255),
    CostoAMBA DECIMAL(10, 2),
    CostoExterior DECIMAL(10, 2)
);
GO

-- Tabla MetodosDeRetiro
CREATE TABLE MetodosDeRetiro (
    IDMetodoRetiro INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(255),
    CostoAMBA DECIMAL(10, 2),
    CostoExterior DECIMAL(10, 2)
);
GO


--INSERTS BASICOS NECESARIOS--

-- -- Insertar Roles
INSERT INTO Rol (Descripcion) VALUES
('Gerente'),
('Asistente'),
('Vendedor'),
('Cliente');

-- -- Insertar Tipos de Membresía
INSERT INTO TipoMembresia (Descripcion, Costo, LibrosALaVez, LibrosXMes, DuracionMeses, Estado) VALUES
('Básica', 10000.00, 1, 3, 1, 1),
('Premium', 20000.00, 2, 5, 2, 1);

-- Insertar Usuarios
INSERT INTO Usuarios (Usuario, Clave, Nombre, Apellido, DNI, Email, Telefono, IDRol) VALUES
('admin', 'admin', 'Guido', 'Serco', '12345678', 'guido.serco@example.com', '123456789', 1);

-- -- Insertar Categorías
INSERT INTO Categoria (Descripcion) VALUES
('Novela'),
('Ciencia Ficción'),
('Fantasía'),
('Historia'),
('Biografía'),
('Poesía'),
('Aventura'),
('Romance'),
('Misterio'),
('Thriller'),
('Terror'),
('Literatura Clásica'),
('Ficción Contemporánea'),
('Ensayo'),
('Drama'),
('Crimen'),
('Autoayuda'),
('Filosofía'),
('Psicología'),
('Educación'),
('Desarrollo Personal'),
('Ciencias Sociales'),
('Política'),
('Religión y Espiritualidad'),
('Salud y Bienestar'),
('Cocina y Gastronomía'),
('Viajes'),
('Arte y Fotografía'),
('Deportes'),
('Negocios y Economía'),
('Computación e Informática'),
('Ingeniería y Tecnología'),
('Matemáticas'),
('Ciencias Naturales'),
('Ciencia Popular'),
('Medicina'),
('Astrología'),
('Ecología y Medio Ambiente'),
('Idiomas'),
('Infantil'),
('Juvenil'),
('Cómics y Novelas Gráficas'),
('Humor'),
('Mitología y Leyendas'),
('Música'),
('Cine y Televisión');

--nacionalidades
INSERT INTO Nacionalidad (Descripcion) VALUES ('Argentina');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Brasil');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Colombia');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Reino Unido');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Francia');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Alemania');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Italia');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Rusia');
INSERT INTO Nacionalidad (Descripcion) VALUES ('España');
INSERT INTO Nacionalidad (Descripcion) VALUES ('México');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Chile');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Perú');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Venezuela');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Ecuador');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Bolivia');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Paraguay');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Uruguay');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Canadá');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Estados Unidos');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Australia');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Nueva Zelanda');
INSERT INTO Nacionalidad (Descripcion) VALUES ('China');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Japón');
INSERT INTO Nacionalidad (Descripcion) VALUES ('India');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Sudáfrica');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Egipto');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Arabia Saudita');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Turquía');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Grecia');
INSERT INTO Nacionalidad (Descripcion) VALUES ('Portugal');

-- -- Insertar Autores
INSERT INTO Autores (Nombre, Apellido, IdNacionalidad, BestSeller) VALUES
('Gabriel', 'García Márquez', 1, 'Cien años de soledad'),
('George', 'Orwell', 3, '1984'),
('Julio', 'Cortázar', 1, 'Rayuela'),
('Victor', 'Hugo', 4, 'Los Miserables'),
('Fiódor', 'Dostoyevski', 5, 'Crimen y Castigo'),
('Carlos', 'Ruiz Zafón', 1, 'La sombra del viento'),
('Paulo', 'Coelho', 2, 'El alquimista'),
('Antonio', 'Serrano', 3, 'Los ojos del perro siberiano'),
('J.K.', 'Rowling', 4, 'Harry Potter y la piedra filosofal'),
('Fernando', 'Gaitán', 5, 'Crónicas del engaño'),
('Horacio', 'Quiroga', 1, 'Cuentos de la selva'),
('Jorge', 'Bucay', 1, 'El camino de la autodependencia'),
('Aldous', 'Huxley', 2, 'Un mundo feliz'),
('Mark', 'Twain', 1, 'Las aventuras de Tom Sawyer');

-- -- Insertar Libros
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

