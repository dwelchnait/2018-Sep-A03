<Query Kind="Program">
  <Connection>
    <ID>ecafa140-05d4-414f-af79-0d35352e5dfd</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	
	//if you are use the C# program environment you will place
	//		your query results into a local variable
	//you will then display your contents of the local variable
	// 		using the .Dump() Linqpad extension method
	//one does NOT need to highlight the query to execute if
	//  	there are multiple queries within the phyiscal file
	//queries will execute from top to bottom in the file
	
	//in the program environment you CAN define classes and methods
	
	//execute a method
	var results = BLL_Query("AC/DC");
	results.Dump();
}

// Define other methods, classes and namespaces here
public class SongItem
{
	public string Song{get;set;}
	public string AlbumTitle{get;set;}
	public int Year{get;set;}
	public int Length{get;set;}
	public decimal Price{get;set;}
	public string Genre{get;set;}
}

//create a method to simulate the BLL method
public List<SongItem> BLL_Query(string artistname)
{
	//change the Anonymous datatype to a strongly-typed datatype
	//define a class and use it with the new
	var results = from x in Tracks
					where x.Album.Artist.Name.Equals(artistname)
					orderby x.Name
					select new SongItem
					{
						Song = x.Name,
						AlbumTitle = x.Album.Title,
						Year = x.Album.ReleaseYear,
						Length = x.Milliseconds,
						Price = x.UnitPrice,
						Genre = x.Genre.Name
					};
	return results.ToList();
}







