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

//Union
//will combine 2 or more queries into one result
//each query needs to have the same number of columns
//each query should have the same associated data within the column
//each query column needs to be the same datatype between queries

//syntax
// (query1).union(query2).union(queryn).OrderBy(first sort).ThenBy(nth sort)
//sorting is done using the column name from the union

//Generate a report covering all albums showing their title
//  their track count, the album price, and average track length.
//  Order by the number of tracks on the album, then by album title

//remember datatypes of columns must match
//Sum added up a decimal field, Average returns a double
//Albums.Count().Dump();
var unionresults = (from x in Albums
					where x.Tracks.Count() > 0
					select new
					{
						title = x.Title,
						trackcount = x.Tracks.Count(),
						albumprice = x.Tracks.Sum(y => y.UnitPrice),
						averagelength = x.Tracks.Average(y => y.Milliseconds/1000.0)
					}).Union(from x in Albums
							where x.Tracks.Count() == 0
							select new
							{
								title = x.Title,
								trackcount = x.Tracks.Count(),
								albumprice = 0.00m,
								averagelength = 0.0
							}).OrderBy(y => y.trackcount).ThenBy(y => y.title);
//unionresults.Dump();

//Joins

// www.dotnetlearners.com/linq

//AVOID joins if there is an acceptable navigational property available
//joins can be used where navigational property DO NOT EXIST
//joins cna be used between associated entities
//		scenario fkey <==> pkey

//left side of the join, i use the support data
//right side of the join, I use the processing record collection

//unfortunatley, Chinook entities are all navigational property setup
//****assume there is NO navigational property between artist and album*****

// syntax
// leftside entity join rightside entity on leftside.pkey == rightside.fkey
//  supportside join processside on supportkey == processfkey

//in our question support => artist and the process => album

var joinResults = from supportside in Artists
					join processside in Albums
				    on supportside.ArtistId equals processside.ArtistId
					select new
					{
					title = processside.Title,
					year = processside.ReleaseYear,
					label = processside.ReleaseLabel == null ? "Unknown" 
								: processside.ReleaseLabel,
					artist = supportside.Name,
					trackcount = processside.Tracks.Count()
					};
joinResults.Dump();
					








