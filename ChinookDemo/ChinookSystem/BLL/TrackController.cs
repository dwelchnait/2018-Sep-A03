using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
using System.ComponentModel;    //expose for ODS configuration
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<SongItem> Track_FindByArtist(string artistname)
        {
            using (var context = new ChinookSystemContext())
            {
                //when you are working in LinqPad, you are using Linq to Sql
                //it your application you are using Linq to Entity
                //the one CHANGE you will need to added to your query
                //   is a reference to your context DbSet<> source
                var results = from x in context.Tracks
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
        }

       
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TrackList> List_TracksForPlaylistSelection(string tracksby, string arg)
        {
           
            using (var context = new ChinookSystemContext())
            {
                //var results = from x in context.Tracks
                //          where (x.Album.Artist.Name.Contains(arg) && tracksby.Equals("Artist")) ||
                //                (x.Album.Title.Contains(arg) && tracksby.Equals("Album"))
                //          orderby x.Name
                //          select new TrackList
                //          {
                //              TrackID = x.TrackId,
                //              Name = x.Name,
                //              Title = x.Album.Title,
                //              ArtistName = x.Album.Artist.Name,
                //              GenreName = x.Genre.Name,
                //              Composer = x.Composer,
                //              Milliseconds = x.Milliseconds,
                //              Bytes = x.Bytes,
                //              UnitPrice = x.UnitPrice
                //          };

                IEnumerable<TrackList> results = null;
                if (tracksby.Equals("Artist"))
                {
                    results = from x in context.Tracks
                              where x.Album.Artist.Name.Contains(arg)
                                     
                              orderby x.Album.Artist.Name,x.Name
                              select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  ArtistName = x.Album.Artist.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };
                }
                else if (tracksby.Equals("Album"))
                {
                    results = from x in context.Tracks
                                  where x.Album.Title.Contains(arg)
                                      
                                  orderby x.Album.Title, x.Name
                                  select new TrackList
                                  {
                                      TrackID = x.TrackId,
                                      Name = x.Name,
                                      Title = x.Album.Title,
                                      ArtistName = x.Album.Artist.Name,
                                      GenreName = x.Genre.Name,
                                      Composer = x.Composer,
                                      Milliseconds = x.Milliseconds,
                                      Bytes = x.Bytes,
                                      UnitPrice = x.UnitPrice
                                  };
                }
                else if (tracksby.Equals("Genre"))
                {
                    int narg = int.Parse(arg);
                    results = from x in context.Tracks
                              where x.GenreId == narg

                              orderby x.Name
                              select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  ArtistName = x.Album.Artist.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };
                }
                if (results == null)
                {
                    return null;
                }
                else
                {
                    return results.ToList();
                }
                
            }
        }//eom
    }
}
