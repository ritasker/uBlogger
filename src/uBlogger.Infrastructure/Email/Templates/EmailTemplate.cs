namespace uBlogger.Infrastructure.Email.Templates
{
    public abstract class EmailTemplate
    {
        public string Subject { get; protected set; }
        public string Body { get; protected set; }
    }
}