using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeGout.Entities
{
    public class UserPlace
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PlaceId { get; set; }
        public DateTime Date { get; set; }= DateTime.UtcNow;

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("PlaceId")]
        public Place Place { get; set; }

    }
}
