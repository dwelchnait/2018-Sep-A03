using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
	public class SongItem
	{
		public string Song { get; set; }
		public string AlbumTitle { get; set; }
		public int Year { get; set; }
		public int Length { get; set; }
		public decimal Price { get; set; }
		public string Genre { get; set; }
	}
}
