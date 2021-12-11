using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace WeGout.Entities
{
    public class Place
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        [Column(TypeName = "geometry")]
        public Geometry Location { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public long? BannerPhotoId { get; set; }

        [ForeignKey("BannerPhotoId")]
        public FileStorage? BannerPhoto { get; set; }

    }
}
