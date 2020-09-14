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
    //annotate your entity to link to the sql table
    //                      to indicate primary key and key type
    //                      to include validation on fields
    [Table("Artists")]
    internal class Artist
    {
        //private data member
        private string _Name;
        //properties
        //the annotation for a primary key is [Key , ....]
        //by default an annotation of [Key] indicates the field is an identity primary key
        //an option on this annotation call DataBaseGenerated(DataBaseGeneratedOption.xxxx)
        //  where xxxx is Identity, Computed, or None
        //Computed indicates attribute is not real data but a computed field from your database
        //None is used on a primary data that the user MUST supply

        [Key]
        public int ArtistId { get; set; }

        //[Required(ErrorMessage = "Name is required")]
        [StringLength(120, ErrorMessage ="Name is limited to 120 characters.")]
        public string Name
        {
            get { return _Name; }
            set { _Name = string.IsNullOrEmpty(value) ? null : value; }
        }

        //navigational properties
        //the relationship in Artist is parent to child (1:m)
        public virtual ICollection<Album> Albums { get; set; }

        //constructor
        //behaviour
    }
}
