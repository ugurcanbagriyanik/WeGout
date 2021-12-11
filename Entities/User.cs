using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeGout.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public long ProfilePhotoId { get; set; }
        public long GenderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime LastSignIn { get; set; }= DateTime.UtcNow;
        public DateTime SignUpDate { get; set; }= DateTime.UtcNow;
        public long Coin { get; set; }=0;

        [ForeignKey("GenderId")]
        public CL_Gender Gender { get; set; }

        [ForeignKey("ProfilePhotoId")]
        public FileStorage? ProfilePhoto { get; set; }

    }
}
