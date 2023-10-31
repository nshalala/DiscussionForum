namespace DiscussionForum.Application.Exceptions;

public class UnavailableNameException : Exception
{
    public UnavailableNameException() : base("This name is not available")
    {
    }

    public UnavailableNameException(string message) : base(message)
    {
    }
}