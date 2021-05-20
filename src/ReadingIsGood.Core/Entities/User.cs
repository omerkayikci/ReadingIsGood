using ReadingIsGood.MongoDB.Abstractions;

namespace ReadingIsGood.Core.Entities
{
    public class User : IEntity<string>
    {
        public string Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
    }
}
