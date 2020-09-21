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
    public class AlbumController
    {
        #region Queries
        public List<AlbumArtist> Album_FindByArtist(int artistid)
        {
            using (var context = new ChinookSystemContext())
            {
                var results = from x in context.Albums
                              where x.ArtistId == artistid
                              select new AlbumArtist
                              {
                                  AlbumId = x.AlbumId,
                                  Title = x.Title,
                                  ArtistId = x.ArtistId,
                                  ReleaseYear = x.ReleaseYear,
                                  ReleaseLabel = x.ReleaseLabel
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<AlbumList> Album_List()
        {
            //return all album records
            using (var context = new ChinookSystemContext())
            {
                var results = from x in context.Albums
                              select new AlbumList
                              {
                                  AlbumId = x.AlbumId,
                                  Title = x.Title,
                                  ArtistId = x.ArtistId,
                                  ReleaseYear = x.ReleaseYear,
                                  ReleaseLabel = x.ReleaseLabel
                              };
                return results.ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AlbumList Album_FindByID(int albumid)
        {
            //return album record matching supply pkey (or null is not found)
            using (var context = new ChinookSystemContext())
            {
                var results = (from x in context.Albums
                              where x.AlbumId == albumid
                              select new AlbumList
                              {
                                  AlbumId = x.AlbumId,
                                  Title = x.Title,
                                  ArtistId = x.ArtistId,
                                  ReleaseYear = x.ReleaseYear,
                                  ReleaseLabel = x.ReleaseLabel
                              }).FirstOrDefault();
                return results;
            }
        }
        #endregion
        #region CRUD methods ADD, Update, Delete
        [DataObjectMethod(DataObjectMethodType.Insert,false)]
        public void Album_Add(AlbumList item)
        {
            using(var context=new ChinookSystemContext())
            {
                //moving the data from the external ViewModel instance
                //  into an internal instance of the entity
                Album addItem = new Album
                {
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };
                context.Albums.Add(addItem);
                context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Album_Update(AlbumList item)
        {
            using (var context = new ChinookSystemContext())
            {
                //moving the data from the external ViewModel instance
                //  into an internal instance of the entity
                Album updateItem = new Album
                {
                    AlbumId = item.AlbumId,
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };
                context.Entry(updateItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Album_Delete(AlbumList item)
        {
            Album_Delete(item.AlbumId);
        }

        public void Album_Delete(int albumid)
        {
            using(var context = new ChinookSystemContext())
            {
                var exists = context.Albums.Find(albumid);
                context.Albums.Remove(exists);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
