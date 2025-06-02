using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVP.Project.Domain.Models;
using NetDevPack.Data;

namespace MVP.Project.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void Add(Customer customer);
        void Update(Guid id,Customer customer);
        void Remove(Customer customer);
        Task<Customer> GetById(Guid id);
        Task<Customer> GetByEmail(string email);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByDocumentNumber(string documentNumber);
    }
}