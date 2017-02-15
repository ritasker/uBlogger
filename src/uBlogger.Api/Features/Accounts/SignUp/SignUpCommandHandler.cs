using MediatR;
using uBlogger.Domain.Accounts;
using uBlogger.Infrastructure.Accounts;
using uBlogger.Infrastructure.Email;
using uBlogger.Infrastructure.Email.Templates;

namespace uBlogger.Api.Features.Accounts.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Unit>
    {
        private readonly AccountRepository _accountRepository;
        private readonly EmailService _emailService;

        public SignUpCommandHandler(AccountRepository accountRepository, EmailService emailService)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
        }

        public Unit Handle(SignUpCommand message)
        {
            var account = new Account(message.Name, message.Email);

            _accountRepository.Save(account);

            _emailService.SendEmail(new CompleteRegistration(account.Name, $"{account.Email}"), account.Email);

            return Unit.Value;
        }
    }
}