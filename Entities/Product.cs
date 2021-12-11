using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeGout.Entities
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Cost { get; set; } = 0.0;
        public long MenuId { get; set; }

        [ForeignKey("MenuId")]
        public Menu Menu { get; set; }

    }
}
