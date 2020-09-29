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
    }
}
