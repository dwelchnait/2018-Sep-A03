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

//Nested queries
//simply put are queries with queries

//list all sales support employees showing their fullname (lastname, firstname),
//their title and the number of customers each support. Order by fullname.
//In addition show a list of the customers for each employee. Show the customer
//fullname, city and state


from x in Employees
where x.Title.Contains("Support")
orderby x.LastName, x.FirstName
select new
{
	name = x.LastName + ", " + x.FirstName,
	title = x.Title,
	//clientcount = x.SupportRepCustomers.Count(),
	clientcount = (from y in x.SupportRepCustomers
					select y).Count(),
	//clientlist = from y in x.SupportRepCustomers
	//				orderby y.LastName, y.FirstName
	//				select new
	//				{
	//					name = y.LastName + ", " + y.FirstName,
	//					city = y.City,
	//					state = y.State
	//				}
	clientlist = from y in Customers
					where y.SupportRepId == x.EmployeeId
					orderby y.LastName, y.FirstName
					select new
					{
						name = y.LastName + ", " + y.FirstName,
						city = y.City,
						state = y.State
					}
}


//Create a list of albums showing their title and artist.
//Show albums with 25 or more tracks only.
//Show the songs on the album (name and length)

//the inner query create an IEnumberable collection
from x in Albums
where x.Tracks.Count() >= 25
select new
{
	title = x.Title,
	artist = x.Artist.Name,
	songcount = x.Tracks.Count(),
	songs = from y  in x.Tracks
			select new
			{
				name = y.Name,
				length = y.Milliseconds
			}
}

//Create a Playlist report that shows the Playlist name, the number
//of songs on the playlist, the user name belonging to the playlist
//and the songs on the playlist with their Genre

from x in Playlists
where x.PlaylistTracks.Count() >= 20
select new
{
	name = x.Name,
	Trackcount = x.PlaylistTracks.Count(),
	user = x.UserName,
	songs = from y in x.PlaylistTracks
			select new
			{
				track = y.Track.Name,
				genre = y.Track.Genre.Name
			}
}





