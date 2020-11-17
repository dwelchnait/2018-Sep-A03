using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.ViewModels;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class GenreController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SelectionList> List_GenreNames()
        {
            using (var context = new ChinookSystemContext())
            {

                var results = from x in context.Genres
                              orderby x.Name
                              select new SelectionList
                              {
                                  ValueId = x.GenreId,
                                  DisplayText = x.Name
                              };
                return results.ToList();
            }
        }
    }
}
