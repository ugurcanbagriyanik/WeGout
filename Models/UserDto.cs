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
        public List<PlaceShortDef>? Places { get; set; } = null;

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

    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(UserDto user, string token)
        {
            Id = user.Id;
            FirstName = user.Name;
            LastName = user.Surname;
            Email = user.Email;
            Token = token;
        }
    }
}
