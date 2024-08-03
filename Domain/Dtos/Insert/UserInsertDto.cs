using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Insert
{
    public class UserInsertDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Range(0, 10)]
        public int Orders { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}