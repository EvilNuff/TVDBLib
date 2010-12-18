using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace TVDBLib
{
    public class TvdbLib
    {
        private string _xmlMirror;
        private string _bannerMirror;
        private string _zipMirror;

        private const int XML_MASK = 1;
        private const int BANNER_MASK = 2;
        private const int ZIP_MASK = 4;

        private readonly string _apiKey;

        private const string API_MIRRORS = "http://www.thetvdb.com/api/{0}/mirrors.xml";

        public TvdbLib(string api)
        {
            _apiKey = api;

            var search = Parse<TvdbMirrorSearchResult>(string.Format(API_MIRRORS, _apiKey));
            var mirrors = search.Mirrors;

            // Save a random mirror, per the API specifications
            var rnd = new Random();
            _xmlMirror = mirrors.Where(x => (x.Typemask & XML_MASK) != 0).OrderBy(x => rnd.Next()).First().MirrorPath;
            _bannerMirror = mirrors.Where(x => (x.Typemask & BANNER_MASK) != 0).OrderBy(x => rnd.Next()).First().MirrorPath;
            _zipMirror = mirrors.Where(x => (x.Typemask & ZIP_MASK) != 0).OrderBy(x => rnd.Next()).First().MirrorPath;
        }

        private static T Parse<T>(string uri)
        {
            var wc = new WebClient();
            var s = new XmlSerializer(typeof(T));
            var data = wc.DownloadData(uri);
            var xml = System.Text.Encoding.UTF8.GetString(data).Trim();

            if (!xml.StartsWith("<?xml"))
            {
                var first = xml.IndexOf("<p>");
                var last = xml.IndexOf("</p>");
                var e = xml.Substring(first, last - first);

                throw new Exception(e);
            }

            TextReader r = new StringReader(xml);

            T t;
            try
            {
                t = (T) s.Deserialize(r);
            } catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                throw;
            }
            return t;
        }
    }
}
