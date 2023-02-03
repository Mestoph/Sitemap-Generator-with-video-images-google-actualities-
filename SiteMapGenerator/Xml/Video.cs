using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SiteMapGenerator.Xml
{
    [XmlType(Namespace = "http://www.google.com/schemas/sitemap-video/1.1")]
    public partial class SiteMapVideo
    {
        [XmlElement("thumbnail_loc")]
        public string ThumbnailLoc { get; set; }

        public bool ShouldSerializeThumbnailLoc()
        {
            return !string.IsNullOrEmpty(ThumbnailLoc);
        }

        [XmlElement("title")]
        public string Title { get; set; }

        public bool ShouldSerializeTitle()
        {
            return !string.IsNullOrEmpty(Title);
        }

        [XmlElement("description")]
        public string Description { get; set; }

        public bool ShouldSerializeDescription()
        {
            return !string.IsNullOrEmpty(Description);
        }

        [XmlElement("content_loc")]
        public string Loc { get; set; }

        public bool ShouldSerializeLoc()
        {
            return !string.IsNullOrEmpty(Loc);
        }

        [XmlElement("player_loc")]
        public string PlayerLoc { get; set; }

        public bool ShouldSerializePlayerLoc()
        {
            return !string.IsNullOrEmpty(PlayerLoc);
        }

        [XmlElement("duration")]
        public UInt32 Duration { get; set; }

        public bool ShouldSerializeDuration()
        {
            return Duration > 0;
        }

        [XmlElement("expiration_date")]
        public DateTime? ExpireDate { get; set; }

        public bool ShouldSerializeExpireDate()
        {
            return ExpireDate.HasValue;
        }

        [XmlElement("rating")]
        public double Rating { get; set; }

        public bool ShouldSerializeRating()
        {
            return Rating > 0d;
        }

        [XmlElement("view_count")]
        public UInt32 ViewCount { get; set; }

        public bool ShouldSerializeViewCount()
        {
            return ViewCount > 0;
        }

        [XmlElement("publication_date")]
        public DateTime? PublicationDate { get; set; }

        public bool ShouldSerializePublicationDate()
        {
            return PublicationDate.HasValue;
        }

        [XmlElement("family_friendly")]
        public YesNo? FamilyFriendly { get; set; }

        public bool ShouldSerializeFamilyFriendly()
        {
            return FamilyFriendly.HasValue;
        }

        //<video:restriction relationship = "allow" > IE GB US CA</video:restriction>
        //<video:price currency = "EUR" > 1.99 </ video:price>

        [XmlElement("requires_subscription")]
        public YesNo? RequiresSubscription { get; set; }

        public bool ShouldSerializeRequiresSubscription()
        {
            return RequiresSubscription.HasValue;
        }

        //<video:uploader info = "https://www.example.com/users/grillymcgrillerson" > GrillyMcGrillerson </ video:uploader>

        [XmlElement("live")]
        public YesNo? Live { get; set; }

        public bool ShouldSerializeLive()
        {
            return Live.HasValue;
        }
    }
}