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

//Anonymous Types

//This allows creating a query that will display
//data not on the from table source AND/OR a subset
//of data from your table source

//List all tracks by AC/DC; orderedby track name.
//Display the Track Name, the Album title, the Album Release year
//the track length, price and genre.

//the default datatype for the query is either <IQueryable> or <IEnumerable>
//new creates another instance
//within the new coding block you will enter the data that you wish returned
var results = from x in Tracks
				where x.Album.Artist.Name.Equals("AC/DC")
				orderby x.Name
				select new
				{
					Song = x.Name,
					AlbumTitle = x.Album.Title,
					Year = x.Album.ReleaseYear,
					Length = x.Milliseconds,
					Price = x.UnitPrice,
					Genre = x.Genre.Name
				};
//if you are use the C# Statement environment you will place
//		your query results into a local variable
//you will then display your contents of the local variable
// 		using the .Dump() Linqpad extension method
//one does NOT need to highlight the query to execute if
//  	there are multiple queries within the phyiscal file
//queries will execute from top to bottom in the file
results.Dump();

var results2 = Tracks
			   .Where (x => x.Genre.Name.Equals ("Jazz"))
			   .OrderBy (x => x.Name);
results2.Dump();











