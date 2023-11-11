namespace DiscussionForum.Application.Exceptions;

public class NotFoundException<TEntity> : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException() : base(typeof(TEntity).Name + " not found")
    {
    }
}