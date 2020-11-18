using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
using System.ComponentModel;    //expose for ODS configuration
#endregion

namespace ChinookSystem.BLL
{
    public class CustomerController
    {
        public CustomerItem Customer_FindByID(int customerid)
        {
            using (var context = new ChinookSystemContext())
            {
                var results = context.Customers
                                .Where(x => x.CustomerId == customerid)
                                .Select(x => x)
                                .FirstOrDefault();
                CustomerItem item = new CustomerItem
                {
                    CustomerId = results.CustomerId,
                    LastName = results.LastName,
                    FirstName = results.FirstName,
                    Company = results.Company,
                    Address = results.Address,
                    City = results.City,
                    State = results.State,
                    Country = results.Country,
                    PostalCode = results.PostalCode,
                    Phone = results.Phone,
                    Fax = results.Fax,
                    Email = results.Email,
                    SupportRepId = results.SupportRepId

                };
                return item;
            }
        }

    }
}
