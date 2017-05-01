using System;
using System.Collections.Generic;
using System.Linq;
using SampleApplication.Models;

namespace SampleApplication.Services
{
    public class InMemoryCustomerService : ICustomerService
    {
        private readonly IList<Customer> _storage = new List<Customer>()
        {
            new Customer()
            {
                Id = 1,
                FirstName = "Max",
                LastName = "Mustermann"
            },
            new Customer()
            {
                Id = 2,
                FirstName = "Erika",
                LastName = "Musterfrau"
            }
        };

        private int _id = 2;

        public Customer Create(Customer entity)
        {
            entity.Id = ++_id;

            _storage.Add(entity);

            return entity;
        }

        public bool Delete(int id)
        {
            var item = _storage.FirstOrDefault(i => i.Id == id);
            return _storage.Remove(item);
        }

        public ICollection<Customer> List()
        {
            return _storage.OrderBy(i => i.Id).ToList();
        }

        public Customer Update(Customer entity)
        {
            var item = _storage.FirstOrDefault(i => i.Id == entity.Id);

            if (item == null)
            {
                throw new Exception($"Customer with {entity.Id} not found.");
            }

            _storage.Remove(item);
            _storage.Add(entity);

            return entity;
        }

        public Customer Get(int id)
        {
            return _storage.FirstOrDefault(i => i.Id == id);
        }
    }
}