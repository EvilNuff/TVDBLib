# API for thetvdb.com

Usage:

*Initialization:*
    string api = ...
    var tvdb = new TvdbLib(api);

*Search for a show:*
	string showname = ...
    TvdbSeries[] series = tvdb.SeriesSearch(showname);

*Get details for a specific show:*
	int showid = ...
    TvdbSeries show = tvdb.Series(showid)

*Get images (fanart, posters, etc) for a show:*
	int showid = ...
	TvdbImage[] images = tvdb.ImageList(showid);


Feel free to fork/improve.