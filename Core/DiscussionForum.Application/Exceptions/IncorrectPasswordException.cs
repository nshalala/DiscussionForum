namespace DiscussionForum.Application.Exceptions;

public class IncorrectPasswordException : Exception
{
    public IncorrectPasswordException():base("Incorrect Password")
    {
        
    }
    public IncorrectPasswordException(string message) : base(message)
    {
        
    }
}