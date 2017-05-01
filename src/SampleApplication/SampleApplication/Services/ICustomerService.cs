using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using SampleApplication.Models;

namespace SampleApplication.Services
{
    public interface ICustomerService
    {
        Customer Create(Customer entity);
        bool Delete(int id);
        ICollection<Customer> List();
        Customer Update(Customer entity);
        Customer Get(int id);
    }  
}