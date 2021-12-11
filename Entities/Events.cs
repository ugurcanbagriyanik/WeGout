using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeGout.Entities
{
    public class Events
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Cost { get; set; } = 0.0;
        public long PlaceId { get; set; }
        public long PosterId { get; set; }

        [ForeignKey("PlaceId")]
        public Place Place { get; set; }
        [ForeignKey("PosterId")]
        public FileStorage FileStorage { get; set; }

    }
}
