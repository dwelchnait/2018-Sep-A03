using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Addition Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace ChinookSystem.Entities
{
    [Table("Tracks")]
    internal class Track
    {
        [Key]
        public int TrackId { get; set; }

        [Required(ErrorMessage = "Track Name is required")]
        [StringLength(200, ErrorMessage = "Track Name is limited to 200 characters.")]
        public string Name { get; set; }
        public int? AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int? GenreId { get; set; }

      
        [StringLength(220, ErrorMessage = "Composer is limited to 220 characters.")]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Album Album { get; set; }
    }
}
