USE DBNEONET


--CREACION DE VISTA PARA CONSUTLAR HISTORIAL DE VENTAS POR CLIENTE 
USE DBNEONET;

ALTER VIEW VW_HISTORIAL_VENTAS AS
SELECT 
    V.ID AS VentaId,
    V.CLIENTE_ID AS ClienteId,
    V.FECHA AS Fecha,
    C.NOMBRE AS Cliente,
    C.EMAIL AS Email,
    D.PRODUCTO_ID AS ProductoId,
    P.NOMBRE AS Producto,
    D.CANTIDAD AS Cantidad,
    D.PRECIO_UNITARIO AS PrecioUnitario,
    (D.CANTIDAD * D.PRECIO_UNITARIO) AS TotalLinea
FROM
	VENTAS V
	INNER JOIN CLIENTES C ON C.ID = V.CLIENTE_ID
	INNER JOIN DETALLEVENTA D ON D.VENTA_ID = V.ID
	INNER JOIN PRODUCTOS P ON P.ID = D.PRODUCTO_ID;


SELECT * FROM VW_HISTORIAL_VENTAS WHERE ClienteId = 1



--TRIGGER PARA VALIDAR EL QUE NO SE REGISTRE EN EL DETALLE MAS PRODUCTOS DE LOS QUE EXISTE EN EL STOCK
CREATE OR ALTER TRIGGER TRG_VALIDAR_STOCK
ON DETALLEVENTA
AFTER INSERT
AS
BEGIN
    DECLARE @ProductoId INT, @Cantidad INT;

    SELECT @ProductoId = PRODUCTO_ID,
           @Cantidad = CANTIDAD
    FROM inserted;

    IF (SELECT STOCK FROM PRODUCTOS WHERE ID = @ProductoId) < 0
    BEGIN
        ROLLBACK TRANSACTION;
        RAISERROR('No se puede registrar la venta. Stock resultante negativo.', 16, 1);
        RETURN;
    END
END;

UPDATE PRODUCTOS SET STOCK =  10 WHERE ID = 1


--SP PARA REGISTRAR LA VENTA, UTILZA TVP PARA PODER HACER UNA CARGA DE VARIOS REGISTROS A LA TABLA 
--CREAR ESTE OBJETO
CREATE TYPE TVP_DetalleVenta AS TABLE
(
    ProductoId INT,
    Cantidad INT
);



CREATE OR ALTER PROCEDURE SP_REGISTRAR_VENTA
(
    @ClienteEmail VARCHAR(150),
    @Detalles TVP_DetalleVenta READONLY
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ClienteId INT;

    -- VALIDAR EMAIL EXISTE
    SELECT @ClienteId = ID 
    FROM CLIENTES 
    WHERE EMAIL = @ClienteEmail;

    IF @ClienteId IS NULL
    BEGIN
        RAISERROR('El email del cliente no existe.', 16, 1);
        RETURN;
    END

    -- VALIDAR STOCK DE CADA PRODUCTO
    IF EXISTS (
        SELECT 1 
        FROM @Detalles d
        JOIN PRODUCTOS p ON p.ID = d.ProductoId
        WHERE p.STOCK < d.Cantidad
    )
    BEGIN
        RAISERROR('Uno o más productos no tienen stock suficiente.', 16, 1);
        RETURN;
    END

    -- INSERTAR VENTA
    INSERT INTO VENTAS (CLIENTE_ID)
    VALUES (@ClienteId);

    DECLARE @VentaId INT = SCOPE_IDENTITY();

    -- INSERTAR DETALLES
    INSERT INTO DETALLEVENTA (VENTA_ID, PRODUCTO_ID, CANTIDAD, PRECIO_UNITARIO)
    SELECT 
        @VentaId,
        d.ProductoId,
        d.Cantidad,
        p.PRECIO
    FROM @Detalles d
    JOIN PRODUCTOS p ON p.ID = d.ProductoId;

    -- DESCONTAR STOCK
    UPDATE p
    SET p.STOCK = p.STOCK - d.Cantidad
    FROM PRODUCTOS p
    JOIN @Detalles d ON d.ProductoId = p.ID;

    SELECT 'Venta registrada correctamente' AS Mensaje, @VentaId AS VentaId;
END



