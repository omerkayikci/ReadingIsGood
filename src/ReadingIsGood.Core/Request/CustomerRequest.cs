using ReadingIsGood.Core.CQRS;

namespace ReadingIsGood.Core.Request
{
    public class CustomerRequest : ICommand<string>
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
