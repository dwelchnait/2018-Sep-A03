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
    [Table("Albums")]
    internal class Album
    {
        private string _ReleaseLabel { get; set; }

        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Albumn title is required")]
        [StringLength(160, ErrorMessage ="Album title is limited to 160 characters.")]
        public string Title { get; set; }

        //[ForeignKey] DO NOT USE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public int ArtistId { get; set; }

        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage = "Release label is limited to 50 characters.")]
        public string ReleaseLabel
        {
            get { return _ReleaseLabel; }
            set { _ReleaseLabel = string.IsNullOrEmpty(value) ? null : value; }
        }

        //navigational properties
        //use to overlay a model of the database ERD relationships
        //you need to know th ERD relationship between Table A and Table B
        //we have a relationship between Artist and Album
        //that relationship is parent (Artist) to child (Album)
        //an Album has one parent
        //an Aritst has zero, one or more children


        //the relationship in Album is child to parent (1:1)
        public virtual Artist Artist { get; set; }
    }
}
