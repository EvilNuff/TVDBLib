using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TVDBLib
{
    [XmlRoot("Banners")]
    public class TvdvbImageSearchResults
    {
        [XmlElement("Banner")]
        public TvdbImage[] Images;
    }

    public enum ImageType { fanart, poster, season, series }

    public class Dimensions
    {
        public int Width;
        public int Height;

        public override string ToString()
        {
            return Width + "x" + Height;
        }
    }

    public class SeriesRating
    {
        public decimal Average;
        public int NumberOfVotes;

        public override string ToString()
        {
            return Average + " (" + NumberOfVotes + ")";
        }
    }

    public class TvdbImage
    {
        [XmlElement("id")]
        public int Id;

        [XmlElement("BannerType")]
        public ImageType Type;

        [XmlElement("BannerType2")]
        public string _bannerType2;

        private Dimensions _imageDimensions = null;
        public Dimensions ImageDimensions
        {
            get
            {
                if (_imageDimensions == null)
                {
                    string[] ds = null;

                    if (_bannerType2 != null)
                    {
                        ds = _bannerType2.Split('x');
                    }

                    if (this.Type == ImageType.fanart && ds != null && ds.Length == 2)
                    {
                        this._imageDimensions = new Dimensions() { Width = Convert.ToInt32(ds[0]), Height = Convert.ToInt32(ds[1])};
                    }
                    else
                    {
                        this._imageDimensions = new Dimensions() { Width = 0, Height = 0 };
                    }
                }

                return _imageDimensions;
            }
            set { this._imageDimensions = value; }
        }

        [XmlElement("Rating")]
        public string _rating;

        [XmlElement("RatingCount")]
        public int _ratingCount;

        private SeriesRating _seriesRating = null;
        public SeriesRating SeriesRating
        {
            get
            {
                if (_seriesRating == null)
                {
                    if (_ratingCount > 0 && _rating != "")
                    {
                        var rating = decimal.Parse(_rating.Replace(".", ","), NumberStyles.Any);
                        _seriesRating = new SeriesRating() {Average = rating, NumberOfVotes = _ratingCount};
                    }
                    else
                    {
                        _seriesRating = new SeriesRating() { Average = 0, NumberOfVotes = 0 };
                    }
                }
                return _seriesRating;
            }
            set { this._seriesRating = value; }
        }

        [XmlElement("SeriesName")]
        public bool ContainsName;

        [XmlElement("BannerPath")]
        public string Path;

        [XmlElement("ThumbnailPath")]
        public string ThumbnailPath;

        [XmlElement("VignettePath")]
        public string VignettePath;

        public override string ToString()
        {
            return Id + ": [" + Type + "] " + SeriesRating + " <" + ImageDimensions + "> (" + Path + ")";
        }
    }
}
