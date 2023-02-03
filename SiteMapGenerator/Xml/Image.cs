using System.Xml.Serialization;

namespace SiteMapGenerator.Xml
{
    [XmlType(Namespace = "http://www.google.com/schemas/sitemap-image/1.1")]
    public partial class SiteMapImage
    {
        [XmlElement("loc")]
        public string Loc { get; set; }

        [XmlElement("caption")]
        public string Caption { get; set; }

        public bool ShouldSerializeCaption()
        {
            return !string.IsNullOrEmpty(Caption);
        }

        [XmlElement("title")]
        public string Title { get; set; }
        public bool ShouldSerializeTitle()
        {
            return !string.IsNullOrEmpty(Title);
        }

        [XmlElement("geo_location")]
        public string GeoLocation { get; set; }

        public bool ShouldSerializeGeoLoacation()
        {
            return !string.IsNullOrEmpty(GeoLocation);
        }

        [XmlElement("license")]
        public string License { get; set; }

        public bool ShouldSerializeLicense()
        {
            return !string.IsNullOrEmpty(GeoLocation);
        }
    }
}