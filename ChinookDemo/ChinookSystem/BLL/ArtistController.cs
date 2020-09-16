using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
using System.ComponentModel; //need for wizard implementation of ObjectDataSource
#endregion

namespace ChinookSystem.BLL
{
    //expose the library class for the wizard
    [DataObject]
    public class ArtistController
    {
        //expose the class method for the wizard
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<SelectionList> Artist_List()
        {
            using(var context = new ChinookSystemContext())
            {
                //due to the fact that he entities will be internal
                //  you will NOT be able to use the entity defintions (classes)
                //  as the return datatypes

                //instead, we will create ViewModel classes that will contain
                //  the data definition for your return data types

                //to fill these view model classes, we will use Linq queries
                //Linq queries return their data as IEnumerable or IQueryable datasets
                //you can use var when declaring your query receiving variables
                //this Linq query uses the syntax method for coding
                var results = from x in context.Artists
                              select new SelectionList
                              {
                                  ValueId = x.ArtistId,
                                  DisplayText = x.Name
                              };
                return results.OrderBy(x => x.DisplayText).ToList();

                //return context.Artists.ToList()  // in 1517
            }
        }
    }
}
