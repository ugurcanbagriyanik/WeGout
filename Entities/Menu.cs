using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeGout.Entities
{
    public class Menu
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public long PlaceId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        [ForeignKey("PlaceId")]
        public Place Place { get; set; }

    }
}
