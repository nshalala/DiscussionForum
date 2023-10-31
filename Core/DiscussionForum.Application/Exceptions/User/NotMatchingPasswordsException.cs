namespace DiscussionForum.Application.Exceptions;

public class NotMatchingPasswordsException : Exception
{
    public NotMatchingPasswordsException() : base("Passwords do not match")
    {
    }

    public NotMatchingPasswordsException(string message) : base(message)
    {
    }
}