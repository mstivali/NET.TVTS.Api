using System;
using System.Linq;
using CEN4020.TVTS.Api.Models;
using CEN4020.TVTS.Infrastructure;

namespace CEN4020.TVTS.Services
{
    public class CustomerService
    {
        public void AddNewCustomer(CustomerModel customer)
        {
            using (var context = new TvtsDataEntities())
            {
                var customerEntity = new customer()
                {
                    Id = Guid.NewGuid(),
                    Firstname = customer.Firstname,
                    Lastname = customer.Lastname,
                    Phone = customer.Phone,
                    Email = customer.Email
                };

                context.customers.Add(customerEntity);

                context.SaveChangesAsync();
            }
        }

        public dynamic GetCustomersList()
        {
            dynamic response;

            using (var context = new TvtsDataEntities())
            {
                var responseEntity = context.customers.ToList();

                response = responseEntity;
            }

            return response;
        }
    }
}
