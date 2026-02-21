CREATE OR ALTER PROCEDURE sp_CreateTicket
    @CustomerId UNIQUEIDENTIFIER,
    @AgentId UNIQUEIDENTIFIER,
    @CategoryId INT,
    @Title NVARCHAR(200),
    @Description NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validate Customer Exists
    IF NOT EXISTS (SELECT 1 FROM Customers WHERE CustomerId = @CustomerId)
    BEGIN
        RAISERROR('Invalid CustomerId.', 16, 1);
        RETURN;
    END

    -- Validate Agent Exists
    IF NOT EXISTS (SELECT 1 FROM Agents WHERE AgentId = @AgentId)
    BEGIN
        RAISERROR('Invalid AgentId.', 16, 1);
        RETURN;
    END

    -- Validate Category Exists
    IF NOT EXISTS (SELECT 1 FROM TicketCategories WHERE CategoryId = @CategoryId)
    BEGIN
        RAISERROR('Invalid CategoryId.', 16, 1);
        RETURN;
    END

    DECLARE @NewTicketId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Tickets
    (
        TicketId,
        CustomerId,
        AgentId,
        CategoryId,
        Title,
        Description,
        Status,
        CreatedAt
    )
    VALUES
    (
        @NewTicketId,
        @CustomerId,
        @AgentId,
        @CategoryId,
        @Title,
        @Description,
        0, -- Default Status = Open
        GETUTCDATE()
    );

    SELECT @NewTicketId AS TicketId;
END
GO


