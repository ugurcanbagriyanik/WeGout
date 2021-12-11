using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeGout.Models
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public long Coin { get; set; }=0;
        public string Gender { get; set; } = string.Empty;
        public string ProfilePhoto { get; set; } = string.Empty;

    }

    public class UserRequest
    {
        public long Id { get; set; }=0;
        public long ProfilePhotoId { get; set; }=0;
        public long GenderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
