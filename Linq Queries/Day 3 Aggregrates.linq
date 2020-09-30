<Query Kind="Expression">
  <Connection>
    <ID>ecafa140-05d4-414f-af79-0d35352e5dfd</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Aggregrates
//.Count(), .Sum(), .Max(), .Min(), .Average()

//.Sum(), .Max(), .Min(), .Average() require a delegate expression

//query syntax
//  (from ....
//      .....
//    ).Max()

//method syntax
//  collection.Max(x => x.collectionfield)
//collectionfield could also be a calculation
//    .Sum(x => x.quantity * x.price)

//IMPORTANT!! aggregrates work ONLY on a collection of data 
//            NOT on a single row

//A collection CAN have 0, 1 or more rows
//the delegate of .Sum(), .Max(), .Min(), .Average() CANNOT be null
//.Count() does not need a delegate, it counts occurances

//bad example of using aggregate
//aggregrate is against a single row
from x in Tracks
select new
{
	Name = x.Name,
	AvgLength = x.Average(x => x.Milliseconds) //wrong, single row
}

//ok
//the list of all Miliseconds in Tracks is create THEN the aggregrate
//   is applied
(from x in Tracks
select x.Milliseconds
).Average()

Tracks.Average(x => x.Milliseconds)

//List all Albums showing the title, artist name and very aggregate values
//for albums containing tracks. For each each show the number of tracks,
//the longest track length, the shortest track length, the total price of the
//tracks, and the average track length.

from x in Albums
where x.Tracks.Count() > 1
select new
{
	title = x.Title,
	artist = x.Artist.Name,
	methodtrackcount = x.Tracks.Count(),
	//querytrackcount = (from y in x.Tracks  //implied join condition
	//					select x).Count(),
	querytrackcount = (from y in Tracks
						where x.AlbumId == y.AlbumId  //join condition
						select x).Count(),
	AvgLength = x.Tracks.Average(y => y.Milliseconds),
	MaxLength = x.Tracks.Max(y => y.Milliseconds),
	MinLength = x.Tracks.Min(y => y.Milliseconds),
	AlbumPrice = x.Tracks.Sum(y => y.UnitPrice),
	BadPrice = x.Tracks.Count() * 0.99 // assumes all tracks are the same price
	
}




