using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeGout.Entities
{
    public class Owner
    {
        [Key]
        public long Id { get; set; }

        public long UserId { get; set; }
        public long PlaceId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = new User();

        [ForeignKey("PlaceId")]
        public Place? Place { get; set; }

    }
}
