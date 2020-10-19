using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
    public class PlayListItem
    {
        public string Name { get; set; }
        public int TrackCount { get; set; }
        public string UserName { get; set; }
        public IEnumerable<PlayListSong> Songs { get; set; }
    }
}
