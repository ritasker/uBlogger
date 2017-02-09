using MediatR;
using uBlogger.Domain.Accounts;
using uBlogger.Infrastructure.Accounts;

namespace uBlogger.Api.Features.Accounts.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Unit>
    {
        private readonly AccountRepository _accountRepository;

        public SignUpCommandHandler(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Unit Handle(SignUpCommand message)
        {
            var account = new Account(message.Name, message.Email);

            _accountRepository.Save(account);

            return Unit.Value;
        }
    }
}