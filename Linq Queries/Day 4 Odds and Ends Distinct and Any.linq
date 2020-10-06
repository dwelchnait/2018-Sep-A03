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
anyResults.Dump();









