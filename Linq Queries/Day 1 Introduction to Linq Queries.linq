<Query Kind="Expression">
  <Connection>
    <ID>4338c651-e987-4875-ab75-d1fef2d82ab0</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

//comments are entered as C# comments

//hotkeys for comments
//Ctrl + K,C make comment
//Ctrl + K,U uncomment

//there are two styles of coding linq queries
//Query Syntax (very sql-ish)
//Method syntax (very C#-ish)

//in the Expression environment you can code multiple queries
//  BUT you MUST highlight the query to execute  (F5)

//in the Statement environment you can code multiple queries
//  as C# statements and run the entire phyiscal file without
//  highlighting the query

//int the Program environment you can code multiple queries
//   AND class definitions or programs methods which are tested
//   in a Main() program

//Simple selection with sort
//Query Syntax of a query
//from clause is 1st and select clause is last
from x in Albums
orderby x.Title ascending
select x

//Method Syntax of a query
Albums.OrderBy (x => x.Title)

from x in Albums
orderby x.ReleaseYear descending, x.Title 
select x

Albums
   .OrderByDescending (x => x.ReleaseYear)
   .ThenBy (x => x.Title)

//filtering of data
//where clause
//list artists with a Q in their name
from x in Artists
where x.Name.Contains("Q")
select x

//show all Albums released in the 90's
from x in Albums
where x.ReleaseYear > 1989 && x.ReleaseYear < 2000
select x

//list all customers in alphabetic order by last name
//who live in the USA. The customer must have an yahoo email

from customer in Customers
orderby customer.LastName
where customer.Country =="USA" &&
	customer.Email.Contains("yahoo")
select customer











