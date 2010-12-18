using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TVDBLib
{
    [XmlRoot("Mirrors")]
    public class TvdbMirrorSearchResult
    {
        [XmlElement("Mirror")]
        public TvdbMirror[] Mirrors;
    }

    public class TvdbMirror
    {
        [XmlElement("mirrorpath")]
        public string MirrorPath;

        [XmlElement("typemask")]
        public int Typemask;
    }
}
