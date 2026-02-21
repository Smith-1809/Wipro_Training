namespace CustomerEngagement.Domain.Exceptions;

public class InvalidTicketStatusTransitionException : Exception
{
    public InvalidTicketStatusTransitionException(string message)
        : base(message)
    {
    }
}
