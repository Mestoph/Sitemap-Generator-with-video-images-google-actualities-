using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SiteMapGenerator.Xml
{
    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public partial class SiteMapUrlSet
    {
        [XmlElement("url")]
        public List<SiteMapUrl> Urls { get; }

        public SiteMapUrlSet()
        {
            Urls = new List<SiteMapUrl>();
        }

        public bool Contains(string _url)
        {
            return Urls.Exists(o => o.Loc == _url);
        }

        public SiteMapUrl Get(string _url)
        {
            return Urls.Find(o => o.Loc == _url);
        }

        public void Add(SiteMapUrl _url)
        {
            Urls.Add(_url);
        }

        public void Clear()
        {

            foreach (SiteMapUrl _url in Urls)
            {
                _url.ClearImage();
            }
            Urls.Clear();
        }

        public void SortUrl()
        {
            foreach (SiteMapUrl _url in Urls)
            {
                _url.SortImage();
                _url.SortVideo();
            }

            Urls.Sort(delegate (SiteMapUrl one, SiteMapUrl two)
            {
                return one.Loc.CompareTo(two.Loc);
            });
        }

        private XmlSerializerNamespaces GetXmlNameSpacesSerializer()
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("image", "http://www.google.com/schemas/sitemap-image/1.1");
            ns.Add("video", "http://www.google.com/schemas/sitemap-video/1.1");
            ns.Add("news", "http://www.google.com/schemas/sitemap-news/0.9");

            return ns;
        }

        private XmlSerializer GetXmlSerializer()
        {
            SortUrl();

            return new XmlSerializer(typeof(SiteMapUrlSet));
        }

        public void WriteToFile(string path)
        {

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    GetXmlSerializer().Serialize(fs, this, GetXmlNameSpacesSerializer());
                }
            }
        }

        public string WriteToString()
        {
            using (StringWriter sw = new StringWriter())
            {
                GetXmlSerializer().Serialize(sw, this, GetXmlNameSpacesSerializer());

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
