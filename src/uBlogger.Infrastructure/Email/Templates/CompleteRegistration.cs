namespace uBlogger.Infrastructure.Email.Templates
{
    public class CompleteRegistration : EmailTemplate
    {
        public CompleteRegistration(string name, string link)
        {
            Subject = "Complete Registration";
            Body = $@"<p>{name}</p>
                      <p>
                          Thanks for registering. But your not quite done please set a password by clicking, or copying, the link below.
                          <a href={link}>{link}</a>
                      </p>
                      <p>The uBlogger Team</p>";
        }
    }
}