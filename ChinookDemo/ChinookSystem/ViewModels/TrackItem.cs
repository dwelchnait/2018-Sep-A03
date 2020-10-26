using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
    public class TrackItem
    {
        public int TrackId { get; set; }

         public string Name { get; set; }

        public int? AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int? GenreId { get; set; }

        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public int? Bytes { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
