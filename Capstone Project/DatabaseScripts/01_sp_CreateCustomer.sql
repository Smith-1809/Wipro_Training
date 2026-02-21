CREATE OR ALTER PROCEDURE sp_CreateCustomer
    @FullName NVARCHAR(150),
    @Email NVARCHAR(150),
    @Phone NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    -- Checks for duplicate email
    IF EXISTS (SELECT 1 FROM Customers WHERE Email = @Email)
    BEGIN
        RAISERROR('Customer with this email already exists.', 16, 1);
        RETURN;
    END

    DECLARE @NewCustomerId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Customers
    (
        CustomerId,
        FullName,
        Email,
        Phone,
        CreatedAt
    )
    VALUES
    (
        @NewCustomerId,
        @FullName,
        @Email,
        @Phone,
        GETUTCDATE()
    );

    SELECT @NewCustomerId AS CustomerId;
END
GO
