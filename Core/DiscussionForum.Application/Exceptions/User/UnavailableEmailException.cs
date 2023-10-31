namespace DiscussionForum.Application.Exceptions;

public class UnavailableEmailException : Exception
{
    public UnavailableEmailException() : base("This email is not available")
    {
    }

    public UnavailableEmailException(string message) : base(message)
    {
    }
}