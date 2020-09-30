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

////conditional statement using if
//if (condition)
//{
//	true path complex logic
//}
//else
//{
//	false path complex logic
//}

////conditions
////arg1 operator arg2


////ternary operator
//condition(s) ? true value : false value

////nested ternary operator
//condition(s) ? 
//	(condition(s) ? true value : false value)
//  : (condition(s) ? true value :
//  				   (condition(s) ? true value : false value))
				   
//List all albums by release label. Any album with no label should
// be indicated as unknown. List title and label.

var ternaryResults = from x in Albums
					 orderby x.ReleaseLabel
					 select new
					 {
					 	title = x.Title,
						label = x.ReleaseLabel != null ? x.ReleaseLabel : "Unknown"
					 };

ternaryResults.Dump();

//List all Albums showing their Title, ArtistName, and 
//decade (oldies, 70's, 80's, 90's, modern). Order by Artist.

var nestedResults = from x in Albums
					orderby x.Artist.Name
					select new
					{
						title = x.Title,
						Artist = x.Artist.Name,
						year = x.ReleaseYear,
						decade = x.ReleaseYear < 1970 ? "Oldies" :
								 x.ReleaseYear < 1980 ? "70's" :
								 x.ReleaseYear < 1990 ? "80's" :
								 x.ReleaseYear < 2000 ? "90's" : "Modern"
										
					};
nestedResults.Dump();

//List all tracks indicating whether they are longer, shorter or equal to the 
//average of all track lengths. Show track name and length.


//example of using multiple queries to answer a question

//query 1, Find the average
//pre processing
var resultavg = Tracks.Average(x => x.Milliseconds);
//using results of query 1 in query 2
var ternaryAverage = from y in Tracks
					 select new
					 {
					 	song = y.Name,
						length = y.Milliseconds < resultavg ? "shorter" :
								 y.Milliseconds == resultavg ? "average" : "longer" ,
						actuallength = y.Milliseconds
					 };
resultavg.Dump();
ternaryAverage.Dump();



