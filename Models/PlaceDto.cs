using NetTopologySuite.Geometries;

namespace WeGout.Models
{
    public class PlaceDto
    {
        public long Id { get; set; }=0;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LocationWkt { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string BannerPhoto { get; set; }=string.Empty;

    }
    public class PlaceRequest
    {
        public long Id { get; set; }=0;
        public long? BannerPhotoId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LocationWkt { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

    }

}
