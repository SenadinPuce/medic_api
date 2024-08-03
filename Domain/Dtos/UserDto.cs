namespace Domain.Dtos
{
    public class UserDto
    { 
        public string Id { get; set; } = null!;
        public string Username { get; set; }  = null!;
        public string? Name { get; set; } = null!;
        public int? Orders { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsBlocked { get; set; } 
        public DateTime DateOfBirth { get; set; }
    }
}