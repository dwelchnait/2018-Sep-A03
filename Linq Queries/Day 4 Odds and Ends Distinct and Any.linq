<Query Kind="Statements">
  <Connection>
    <ID>ecafa140-05d4-414f-af79-0d35352e5dfd</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

//.Distinct() removes duplicates
//Create a list of customer countries

var distinctResults = (from x in Customers
orderby x.Country
select x.Country).Distinct();

var distinctMethodResults =Customers
   .OrderBy (x => x.Country)
   .Select (x => x.Country)
   .Distinct();
   
//boolean filters .Any() and .All()
//.Any() method iterates through the entire collection
//	if any of the items match the specified condition, true is returned
//boolean filters return NO data, just true or false
//an instance of the collection that receives a true on the condition
//   is selected for processing

//show Genres that have tracks which are not on any playlist
var anyResults =from x in Genres
				where x.Tracks.Any(trk => trk.PlaylistTracks.Count() == 0)
				orderby x.Name
				select new
				{
					genre = x.Name,
					tracksingenre = x.Tracks.Count(),
					boringtracks = from y in x.Tracks
									where y.PlaylistTracks.Count() == 0
									select y.Name
				};
//anyResults.Dump();

//sometimes you have two lists that need to be compared
//Usually you are looking for items that are the same (in both collections) OR
//        you are looking for items that are different
//in either case: you are comparing one collection to a second collection

//we are going to compare the playlists of two individuals on the database

//obtain a distinct list of all playlist tracks for Roberto Almeida (user AlmeidaR)
var almeida = (from x in PlaylistTracks
				where x.Playlist.UserName.Contains("Almeida")
				select new
				{
					song = x.Track.Name,
					genre = x.Track.Genre.Name,
					id = x.TrackId
				}).Distinct().OrderBy(x => x.song);
//almeida.Dump();

//obtain a distinct list of all playlist tracks for Michelle Brooks (user BrooksM)
var brooks = (from x in PlaylistTracks
				where x.Playlist.UserName.Contains("Brooks")
				select new
				{
					song = x.Track.Name,
					genre = x.Track.Genre.Name,
					id = x.TrackId
				}).Distinct().OrderBy(x => x.song);
//brooks.Dump();

//start with the comparisons
//list the tracks that both Roberto and Michelle like
//i find it best to think of the collections A and B
//think of processing a for each A, check to see if it is any of B
var likes = almeida
			.Where(a => brooks.Any(b => b.id == a.id))
			.OrderBy(a => a.song)
			.Select(a => a);
//likes.Dump();
			
//differences
//list Roberto's tracks that Michelle does not have
//find records in collection A that are NOT in collection B
var ameidaDiff = almeida
			.Where(a => !brooks.Any(b => b.id == a.id))
			.OrderBy(a => a.song)
			.Select(a => a);
//ameidaDiff.Dump();

var brooksDiff = brooks
			.Where(a => !almeida.Any(b => b.id == a.id))
			.OrderBy(a => a.song)
			.Select(a => a);
//brooksDiff.Dump();

//All() method iterates through the entire collection to see 
//    all items that match the condition
//returns true or false
//an instance of the collection that receives a true on the condition
//   is selected for processing

//show Genres that have all their tracks appearing at least once on a
//playlist
var genretotal = Genres.Count();
genretotal.Dump();

var popularGenres = from x in Genres
					where x.Tracks.All(trk => trk.PlaylistTracks.Count() > 0)
					orderby x.Name
					select new
					{
						genre = x.Name,
						genretracks = x.Tracks.Count(),
						theTracks = from y in x.Tracks
									where y.PlaylistTracks.Count() > 0
									select new
									{
										song = y.Name
									}
					};
popularGenres.Dump();







