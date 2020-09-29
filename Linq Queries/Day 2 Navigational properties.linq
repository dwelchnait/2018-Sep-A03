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

//Show all albums for U2, order by year, by title

//a demonstration of using the navigational properties
//   to access data on another table

//query syntax
from x in Albums
orderby x.ReleaseYear, x.Title
where x.Artist.Name.Equals("U2")
select x

//method syntax
Albums
   .OrderBy (x => x.ReleaseYear)
   .ThenBy (x => x.Title)
   .Where (x => x.Artist.Name.Equals ("U2"))
   
//List all jazz (genre) tracks by Name
from x in Tracks
where x.Genre.Name.Equals("Jazz")
orderby x.Name
select x
   
Tracks
   .Where (x => x.Genre.Name.Equals ("Jazz"))
   .OrderBy (x => x.Name)
   
   
//list all tracks for the artist AC/DC
Tracks
   .Where (x => x.Album.Artist.Name.Equals ("AC/DC"))
   .OrderBy (x => x.Name)
   
   
   
   
   
   