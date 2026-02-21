namespace CustomerEngagement.Domain.Exceptions;

public class DuplicateTicketException : Exception
{
    public DuplicateTicketException(string message)
        : base(message)
    {
    }
}