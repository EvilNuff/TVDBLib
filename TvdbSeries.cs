using System.Xml.Serialization;

namespace TVDBLib
{
    [XmlRoot("Data")]
    public class TvdbSeriesSearchResult
    {
        [XmlElement("Series")]
        public TvdbSeries[] Series;
    }

    public class TvdbSeries
    {
        [XmlElement("seriesid")]
        public int Id;

        [XmlElement("SeriesName")]
        public string Name;

        [XmlElement("Overview")]
        public string Description;

        [XmlElement("IMDB_ID")]
        public string ImdbId;

        public override string ToString()
        {
            return Name + " (" + ImdbId + ", " + Id + ")\n" + Description;
        }
    }
}
