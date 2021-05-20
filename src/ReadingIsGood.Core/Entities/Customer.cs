using ReadingIsGood.MongoDB.Abstractions;

namespace ReadingIsGood.Core.Entities
{
    public class Customer : IEntity<string>
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
