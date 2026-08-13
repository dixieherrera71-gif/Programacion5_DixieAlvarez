USE CafeLabDb;
GO

UPDATE dbo.Usuarios
SET Rol = 'Administrador'
WHERE Correo = 'barista2@cafelab.com';

GO

SELECT Id, Correo, Rol
FROM dbo.Usuarios;