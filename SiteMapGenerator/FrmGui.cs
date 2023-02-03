using FastColoredTextBoxNS;
using HtmlAgilityPack;
using SiteMapGenerator.Extensions;
using SiteMapGenerator.Xml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace SiteMapGenerator
{
    public partial class FrmGui : Form
    {
        private SiteMapUrlSet m_urlSet;
        private int m_maxDepth = 1;
        private Uri m_baseUri;
        private readonly string[] m_pictureExtensions = new[] { ".apng", ".avif", ".bmp", ".cur", ".gif", ".ico", ".jfif", ".jpeg", ".jpg", ".pjp", ".pjpeg", ".png", ".svg", ".tif", ".tiff", ".webp" };
        private readonly string m_www = "www.";
        private Thread m_sitemapThread = null;
        private readonly ManualResetEvent m_sitemapResetEvent = new ManualResetEvent(true);
        private bool m_inProgress = false;

        public FrmGui()
        {
            m_urlSet = new SiteMapUrlSet();

            InitializeComponent();

            EnableCtrl(BtnGen, false);
            EnableCtrl(TxtUrl, true);
            EnableCtrl(Num, true);
            EnableCtrl(BtnCancel, false);
        }

        ~FrmGui()
        {
            if (m_sitemapThread != null)
            {
                if (m_sitemapThread.IsAlive)
                    m_sitemapThread.Abort();

                m_sitemapThread = null;
            }

            Environment.Exit(Environment.ExitCode);
        }

        private void BtnGen_Click(object sender, EventArgs e)
        {
            if (m_inProgress)
            {
                m_sitemapResetEvent.Set();

                m_sitemapThread?.Join();
            }
            else
            {
                if (!IsValidUrl(TxtUrl.Text.Trim()))
                {
                    MessageBox.Show("The entered URL is invalid.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                Thread sitemapThread = new Thread(GenerateSiteMap)
                {
                    IsBackground = true
                };
                sitemapThread.Start();
            }
        }

        private void GenerateSiteMap()
        {
            if (m_inProgress)
                return;

            m_inProgress = true;

            AnimatePb(true);

            EnableCtrl(BtnGen, false);
            EnableCtrl(TxtUrl, false);
            EnableCtrl(BtnCancel, true);
            EnableCtrl(Num, false);

            m_urlSet.Clear();

            m_baseUri = GetUri(TxtUrl.Text);

            CrawlUri(m_baseUri);

            SetTextCtrl(Fctb, m_urlSet.WriteToString());
            m_urlSet.WriteToFile("sitemap.xml");

            EnableCtrl(BtnGen, true);
            EnableCtrl(TxtUrl, true);
            EnableCtrl(BtnCancel, false);
            EnableCtrl(Num, true);

            AnimatePb(false);

            Thread.Sleep(1000);

            m_inProgress = false;
        }

        private void AnimatePb(bool _enabled)
        {
            if (Pb.InvokeRequired)
            {
                Pb.BeginInvoke((MethodInvoker)delegate
                {
                    Pb.Style = _enabled ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
                });
            }
            else
            {
                Pb.Style = _enabled ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
            }
        }

        private void EnableCtrl(Control _c, bool _enabled)
        {
            if (_c == null)
                return;

            if (_c.InvokeRequired)
            {
                _c.BeginInvoke((MethodInvoker)delegate
                {
                    _c.Enabled = _enabled;
                });
            }
            else
            {
                _c.Enabled = _enabled;
            }
        }

        private void SetTextCtrl(Control _c, string _str)
        {
            if (_c == null || string.IsNullOrEmpty(_str))
                return;

            if (_c.InvokeRequired)
            {
                _c.BeginInvoke((MethodInvoker)delegate
                {
                    _c.Text = _str;
                });
            }
            else
            {
                _c.Text = _str;
            }
        }

        private Uri GetUri(string _url)
        {
            //RemoveWWW(ref _url);

            return new UriBuilder(_url).Uri;
        }

        /*private void RemoveWWW(ref string _url)
        {
            if (_url.Contains(m_www))
                _url = _url.Replace(m_www, string.Empty);
        }*/

        private bool IsSameHost(Uri _host1, Uri _host2)
        {
            return IsSameHost(_host1.Host, _host2.Host);
        }

        private bool IsSameHost(string _host1, string _host2)
        {
            if (_host1.StartsWith(m_www, StringComparison.OrdinalIgnoreCase))
                _host1 = _host1.Replace(m_www, string.Empty);

            if (_host2.StartsWith(m_www, StringComparison.OrdinalIgnoreCase))
                _host2 = _host2.Replace(m_www, string.Empty);

            return _host1.Equals(_host2);
        }

        private void CrawlUri(Uri _uri)
        {
            m_sitemapResetEvent.WaitOne();

            if (m_urlSet.Contains(_uri.AbsoluteUri))
                return;

            if (!IsSameHost(m_baseUri.Host, _uri.Host))
                return;

            int currentDepth = GetDepth(_uri);
            if (currentDepth > m_maxDepth)
                return;

            HtmlWeb web = new HtmlWeb();
            HttpStatusCode lastStatusCode = HttpStatusCode.OK;
            DateTime? lastModified = null;

            web.PostResponse = (request, response) =>
            {
                if (response != null)
                {
                    lastStatusCode = response.StatusCode;
                    lastModified = response.LastModified;
                }
            };

            HtmlDocument doc = web.Load(_uri.AbsoluteUri);

#if DEBUG
            Console.WriteLine("Crawling: url = " + _uri.AbsoluteUri);
            Console.WriteLine("Crawling: status code = " + lastStatusCode);
            Console.WriteLine("Crawling: depth = " + currentDepth);
#endif

            if (lastStatusCode == HttpStatusCode.NotFound)
                return;

            SiteMapUrl nUrl = new SiteMapUrl
            {
                Loc = _uri.AbsoluteUri,
                Priority = CalculatePriority(doc, currentDepth),
                ChangeFrequency = CalculateChangeFrequency(doc),
                LastModified = lastModified
            };

            m_urlSet.Add(nUrl);

            HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a[@href]");
            HtmlNodeCollection images = doc.DocumentNode.SelectNodes("//img[@src]");
            HtmlNodeCollection videos = doc.DocumentNode.SelectNodes("//video | //media | //object");

            if (links != null)
            {
                foreach (HtmlNode link in links)
                {
                    Uri href = GetAbsoluteUrl(_uri, link.GetAttributeValue("href", string.Empty));
                    if (href != null && !IsImageFile(href.AbsoluteUri))
                        CrawlUri(href);
                }
            }

            if (images != null)
            {
                foreach (HtmlNode img in images)
                {
                    Uri src = GetAbsoluteUrl(_uri, img.GetAttributeValue("src", string.Empty));
                    if (src != null)
                    {
                        if (nUrl.ContainsImage(src.AbsoluteUri))
                            continue;

                        if (!IsSameHost(m_baseUri.Host, src.Host))
                            continue;

                        SiteMapImage nImg = new SiteMapImage
                        {
                            Loc = src.AbsoluteUri,
                            Title = img.GetAttributeValue("title", string.Empty),
                            Caption = img.GetAttributeValue("alt", string.Empty)
                        };


                        nUrl.AddImage(nImg);
                    }
                }
            }

            // Todo : Need to separate the parsing of <video> <media> & <object>.
            if (videos != null)
            {
                foreach (HtmlNode vid in videos)
                {
                    Uri src = GetAbsoluteUrl(_uri, vid.GetAttributeValue("src", string.Empty));
                    if (src != null)
                    {
                        if (nUrl.ContainsVideo(src.AbsoluteUri))
                            continue;

                        if (!IsSameHost(m_baseUri.Host, src.Host))
                            continue;

                        // Todo : Not fully implemented...
                        bool live;
                        bool subscription;
                        DateTime publication_date;
                        UInt32 duration;

                        bool.TryParse(vid.GetAttributeValue("live", string.Empty), out live);
                        bool.TryParse(vid.GetAttributeValue("subscription", string.Empty), out subscription);
                        DateTime.TryParse(vid.GetAttributeValue("publication_date", string.Empty), out publication_date);
                        UInt32.TryParse(vid.GetAttributeValue("duration", string.Empty), out duration);

                        SiteMapVideo nVid = new SiteMapVideo
                        {
                            Loc = src.AbsoluteUri,
                            Title = vid.GetAttributeValue("title", string.Empty),
                            Duration = duration,
                            Description = vid.GetAttributeValue("description", string.Empty),
                            ThumbnailLoc = vid.GetAttributeValue("thumbnail", string.Empty),
                            PublicationDate = publication_date,
                            RequiresSubscription = subscription ? YesNo.yes : YesNo.no,
                            Live = live ? YesNo.yes : YesNo.no,
                        };

                        nUrl.AddVideo(nVid);
                    }
                }
            }
        }

        private bool IsImageFile(string _url)
        {
            if (string.IsNullOrEmpty(_url))
                return false;

            foreach (string ext in m_pictureExtensions)
            {
                if (_url.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private int GetDepth(Uri url)
        {
            int baseSegments = m_baseUri.Segments.Length;
            int urlSegments = url.Segments.Length;
            int depth = urlSegments - baseSegments;

            return depth;
        }

        private Uri GetAbsoluteUrl(Uri _baseUri, string _url)
        {
            if (string.IsNullOrWhiteSpace(_url))
                return null;

            //RemoveWWW(ref _url);

            if (_url.StartsWith("#"))
                return null;

            if (_url.StartsWith("file://"))
                return null;

            if (_url.StartsWith("//"))
                _url = _url.Remove(0, 2);

            if (_url.StartsWith("../") || _url.Equals("/"))
                _url = _baseUri.AbsoluteUri.Substring(0, _baseUri.AbsoluteUri.LastIndexOf("/"));

            if (_url.StartsWith("/") || _url.StartsWith("?"))
                _url = string.Concat(_baseUri.AbsoluteUri.Substring(0, _baseUri.AbsoluteUri.LastIndexOf("/")), _url);

            Uri uri;

            try
            {
                if (!Uri.TryCreate(_url, UriKind.Absolute, out uri))
                {
                    if (!Uri.TryCreate(string.Concat(_baseUri.AbsoluteUri.Substring(0, _baseUri.AbsoluteUri.LastIndexOf("/") + 1), _url), UriKind.Absolute, out uri))
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return uri;
        }

        private double CalculatePriority(HtmlDocument _doc, int _depth)
        {
            if (_doc == null)
                return 0.0d;

            double priority = 0.1d;

            string page = _doc.DocumentNode.InnerText;
            if (!string.IsNullOrWhiteSpace(page))
            {
                if (page.ContainsIgnoreCase("news") || page.ContainsIgnoreCase("nouvelles"))
                {
                    priority += 0.3d;
                }
                else if (page.ContainsIgnoreCase("blog"))
                {
                    priority += 0.2d;
                }
                else if (page.ContainsIgnoreCase("about") || page.ContainsIgnoreCase("à propos"))
                {
                    priority += 0.1d;
                }
            }

            IEnumerable<HtmlNode> links = _doc.DocumentNode.Descendants("a");

            priority += Convert.ToDouble(links.Count()) / 100d;

            priority += Convert.ToDouble(_depth) / 10d;

            if (priority > 1.0d)
            {
                priority = 1.0d;
            }

            return priority;
        }

        private ChangeFrequency CalculateChangeFrequency(HtmlDocument _doc)
        {
            ChangeFrequency changefreq = ChangeFrequency.always;

            if (_doc == null)
                return changefreq;

            string revisitAfter = string.Empty;

            if (_doc.DocumentNode.SelectSingleNode("//meta[@name='revisit-after']") != null)
                revisitAfter = _doc.DocumentNode.SelectSingleNode("//meta[@name='revisit-after']").Attributes["content"].Value;

            if (!string.IsNullOrWhiteSpace(revisitAfter))
            {
                revisitAfter = revisitAfter.Trim();

                int quantity = 0;
                string unit = "";

                string[] parts = revisitAfter.Split(' ');
                if (parts.Length > 0)
                {
                    quantity = int.Parse(parts[0]);
                }
                if (parts.Length > 1)
                {
                    unit = parts[1];
                }

                int interval = 0;
                switch (unit)
                {
                    case "days":
                        interval = quantity;
                        break;
                    case "weeks":
                        interval = quantity * 7;
                        break;
                    case "months":
                        interval = quantity * 30;
                        break;
                    case "years":
                        interval = quantity * 365;
                        break;
                }

                TimeSpan timeDiff = DateTime.Now.AddDays(interval) - DateTime.Now;
                double totalDays = timeDiff.TotalDays;

                changefreq = totalDays >= 365
                    ? ChangeFrequency.yearly
                    : totalDays >= 30
                        ? ChangeFrequency.monthly
                        : totalDays >= 7 ? ChangeFrequency.weekly : totalDays >= 1 ? ChangeFrequency.daily : ChangeFrequency.hourly;
            }
            else
            {
                string page = _doc.DocumentNode.InnerText;
                if (!string.IsNullOrWhiteSpace(page))
                {
                    if (page.ContainsIgnoreCase("news") || page.ContainsIgnoreCase("nouvelles"))
                    {
                        changefreq = ChangeFrequency.daily;
                    }
                    else if (page.ContainsIgnoreCase("blog"))
                    {
                        changefreq = ChangeFrequency.weekly;
                    }
                    else if (page.ContainsIgnoreCase("about") || page.ContainsIgnoreCase("à propos"))
                    {
                        changefreq = ChangeFrequency.monthly;
                    }
                    else if (page.ContainsIgnoreCase("static"))
                    {
                        changefreq = ChangeFrequency.never;
                    }
                }
            }

            return changefreq;
        }

        private bool IsValidUrl(string _url)
        {
            if (string.IsNullOrWhiteSpace(_url))
                return false;

            Uri uriResult;
            bool tryCreateResult = Uri.TryCreate(_url, UriKind.Absolute, out uriResult);
            return tryCreateResult == true && uriResult != null;
        }

        private void TxtUrl_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtUrl.Text))
                return;

            bool valid = IsValidUrl(TxtUrl.Text.Trim());

            TxtUrl.ForeColor = valid ? Color.Green : Color.Red;

            EnableCtrl(BtnGen, valid);
        }

        private void Num_ValueChanged(object sender, EventArgs e)
        {
            int depth;
            int.TryParse(Num.Value.ToString(), out depth);
            if (depth < 1)
                depth = 1;
            else if (depth > 100)
                depth = 100;

            m_maxDepth = depth;
        }
    }
}
