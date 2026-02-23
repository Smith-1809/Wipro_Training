CREATE OR ALTER PROCEDURE sp_GetTicketsPaged
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TicketId,
           Title,
           Description,
           Status,
           CreatedAt,
           UpdatedAt
    FROM dbo.Tickets
    ORDER BY CreatedAt DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END