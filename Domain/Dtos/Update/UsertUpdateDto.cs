using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Update
{
    public class UserUpdateDto
    {
        public string? Username { get; set; }
        public string? Name { get; set; } 

        [Range(0, 10)]
        public int Orders { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}