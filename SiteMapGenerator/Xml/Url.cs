using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SiteMapGenerator.Xml
{
    public partial class SiteMapUrl
    {
        [XmlElement("loc")]
        public string Loc { get; set; }

        [XmlElement("changefreq")]
        public ChangeFrequency? ChangeFrequency { get; set; }

        public bool ShouldSerializeChangeFrequency()
        {
            return ChangeFrequency.HasValue;
        }

        [XmlElement("lastmod")]
        public DateTime? LastModified { get; set; }

        public bool ShouldSerializeLastModified()
        {
            return LastModified.HasValue;
        }

        [XmlElement("priority")]
        public double? Priority { get; set; }

        public bool ShouldSerializePriority()
        {
            return Priority.HasValue;
        }

        [XmlElement("image", Namespace = "http://www.google.com/schemas/sitemap-image/1.1")]
        public List<SiteMapImage> Images { get; }

        public bool ShouldSerializeImages()
        {
            return Images != null && Images.Count > 0;
        }

        [XmlElement("video", Namespace = "http://www.google.com/schemas/sitemap-news/0.9")]
        public List<SiteMapVideo> Videos { get; }

        public bool ShouldSerializeVideos()
        {
            return Videos != null && Videos.Count > 0;
        }

        public SiteMapUrl()
        {
            Images = new List<SiteMapImage>();
            Videos= new List<SiteMapVideo>();
        }

        public bool ContainsImage(string _url)
        {
            return Images.Exists(o => o.Loc == _url);
        }

        public void AddImage(SiteMapImage _img)
        {
            Images.Add(_img);
        }

        public void ClearImage()
        {
            Images.Clear();
        }

        public void SortImage()
        {
            Images.Sort(delegate (SiteMapImage one, SiteMapImage two)
            {
                return one.Loc.CompareTo(two.Loc);
            });
        }

        public bool ContainsVideo(string _url)
        {
            return Videos.Exists(o => o.Loc == _url);
        }

        public void AddVideo(SiteMapVideo _vid)
        {
            Videos.Add(_vid);
        }

        public void ClearVideo()
        {
            Videos.Clear();
        }

        public void SortVideo()
        {
            Videos.Sort(delegate (SiteMapVideo one, SiteMapVideo two)
            {
                return one.Loc.CompareTo(two.Loc);
            });
        }
    }
}