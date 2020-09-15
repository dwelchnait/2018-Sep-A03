using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Entities;
using System.Data.Entity;
#endregion

namespace ChinookSystem.DAL
{
    internal class ChinookSystemContext:DbContext
    {
        public ChinookSystemContext():base("name=ChinookDB")
        {

        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }

        public DbSet<MediaType> MediaTypes { get; set; }

    }
}
