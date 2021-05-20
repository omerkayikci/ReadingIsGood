using ReadingIsGood.Core.CQRS;

namespace ReadingIsGood.Core.Request
{
    public class AuthRequest : ICommand<string>
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
