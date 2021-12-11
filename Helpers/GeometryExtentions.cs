using NetTopologySuite.IO;
using NetTopologySuite.Geometries;

namespace WeGout.Helpers
{    public static class GeometryExtentions
    {


        /// <summary>
        /// GeomWkt türündeki stringi IGeometry türünde döner.
        /// </summary>
        public static Geometry ToGeometry(this string wkt, int srid = 4326)
        {
            Geometry response;
            try
            {
                WKTReader reader = new WKTReader();
                reader.DefaultSRID = srid;
                response = reader.Read(wkt);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }
        /// <summary>
        /// GeomWkt türündeki stringi IGeometry türünde döner.
        /// </summary>
        public static Geometry ToGeometry(this string wkt)
        {
            Geometry response;
            try
            {
                WKTReader reader = new WKTReader();
                reader.DefaultSRID = 4326;
                response = reader.Read(wkt);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }

    }
}