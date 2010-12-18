using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TVDBLib
{
    [XmlRoot("Mirrors")]
    class TvdbMirrorSearchResult
    {
        [XmlArrayAttribute("Mirror")]
        public TvdbMirror[] Mirrors;
    }

    [XmlType("Mirror")]
    class TvdbMirror
    {
        [XmlAttribute("mirrorpath")]
        public string MirrorPath;

        [XmlAttribute("typepath")]
        public int Typemask;
    }
}
