using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleApplication.Models;
using SampleApplication.Models.Entities;

namespace SampleApplication.Services
{
    public class DatabaseCustomerService : ICustomerService
    {
        private readonly SampleApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public DatabaseCustomerService(SampleApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Customer Create(Customer entity)
        {
            var dbEntity = _mapper.Map<CustomerEntity>(entity);

            _dbContext.Customers.Add(dbEntity);
            _dbContext.SaveChanges();

            return _mapper.Map<Customer>(dbEntity);
        }

        public bool Delete(int id)
        {
            var dbEntity = _dbContext.Customers
                .AsNoTracking()
                .SingleOrDefault(e => e.Id == id);

            if (dbEntity == null)
            {
                return false;
            }

            _dbContext.Customers.Remove(dbEntity);
            _dbContext.SaveChanges();

            return true;
        }

        public ICollection<Customer> List()
        {
            return _dbContext.Customers
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(c => _mapper.Map<Customer>(c))
                .ToList();
        }

        public Customer Update(Customer entity)
        {
            var dbEntity = _dbContext.Customers.SingleOrDefault(e => e.Id == entity.Id);

            if (dbEntity == null)
            {
                return null;
            }

            dbEntity.FirstName = entity.FirstName;
            dbEntity.LastName = entity.LastName;
            dbEntity.Age = entity.Age;

            _dbContext.SaveChanges();

            return _mapper.Map<Customer>(dbEntity);
        }

        public Customer Get(int id)
        {
            return _mapper.Map<Customer>(_dbContext.Customers.SingleOrDefault(e => e.Id == id));
        }
    }
}