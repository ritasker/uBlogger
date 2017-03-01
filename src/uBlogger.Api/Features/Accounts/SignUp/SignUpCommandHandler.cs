using MediatR;
using uBlogger.Infrastructure.Accounts;
using uBlogger.Infrastructure.Security;

namespace uBlogger.Api.Features.Accounts.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand>
    {
        private readonly AccountRepository _accountRepository;
        private readonly HashingService _hashingService;

        public SignUpCommandHandler(AccountRepository accountRepository, HashingService hashingService)
        {
            _accountRepository = accountRepository;
            _hashingService = hashingService;
        }

        public void Handle(SignUpCommand message)
        {
            var hash = _hashingService.HashPassword(message.Password);
            _accountRepository.Save(message.UserName, message.Email, hash).GetAwaiter();
        }
    }
}