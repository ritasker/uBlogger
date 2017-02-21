using MediatR;
using uBlogger.Domain.Entities;
using uBlogger.Infrastructure.Accounts;
using uBlogger.Infrastructure.Email;
using uBlogger.Infrastructure.Email.Templates;
using uBlogger.Infrastructure.Security;

namespace uBlogger.Api.Features.Accounts.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Unit>
    {
        private readonly AccountRepository _accountRepository;
        private readonly EmailService _emailService;
        private readonly HashingService _hashingService;

        public SignUpCommandHandler(AccountRepository accountRepository, EmailService emailService, HashingService hashingService)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
            _hashingService = hashingService;
        }

        public Unit Handle(SignUpCommand message)
        {
            var hash = _hashingService.HashPassword(message.Password);
            var account = new Account(message.UserName, message.Email, hash);

            _accountRepository.Save(account);

           // _emailService.SendEmail(new CompleteRegistration(account.UserName, $"{account.Email}"), account.Email);

            return Unit.Value;
        }
    }
}