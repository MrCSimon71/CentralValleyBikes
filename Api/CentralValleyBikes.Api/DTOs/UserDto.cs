namespace CentralValleyBikes.Api.DTOs
{
    public class UserDto
    {
        public Guid? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public DateTime? DateJoined { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }
    }
}
