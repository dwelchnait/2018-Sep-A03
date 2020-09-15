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
    [Table("MediaTypes")]
    internal class MediaType
    {
        private string _Name;

        [Key]
        public int MediaTypeId { get; set; }

        //fully implemented property
        public string Name
        {
            get { return _Name; }
            set { _Name= string.IsNullOrEmpty(value) ? null : value; }
        }

        //A media type can be used for many tracks
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
