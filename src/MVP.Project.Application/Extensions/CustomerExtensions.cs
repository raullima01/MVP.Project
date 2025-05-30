using MVP.Project.Domain.Commands;
using MVP.Project.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using MVP.Project.Application.ViewModels;

namespace MVP.Project.Application.Extensions
{
    public static class CustomerExtensions
    {
        public static CustomerViewModel ToViewModel(this Customer customer)
        {
            if (customer == null) return null;

            return new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                DocumentNumber = customer.DocumentNumber,
                BirthDate = customer.BirthDate,
                Phone = customer.Phone,
                StateInscription = customer.StateInscription,
                StreetAddress = customer.StreetAddress,
                BuildingNumber = customer.BuildingNumber,
                SecondaryAddress = customer.SecondaryAddress,
                Neighborhood = customer.Neighborhood,
                ZipCode = customer.ZipCode,
                City = customer.City,
                State = customer.State,
                Active = customer.Active
            };
        }

        public static IEnumerable<CustomerViewModel> ToViewModel(this IEnumerable<Customer> customers)
        {
            return customers?.Select(c => c.ToViewModel());
        }

        public static Customer ToEntity(this CustomerViewModel customer)
        {
            if (customer == null) return null;

            return new Customer(customer.Id, customer.Name, customer.Email, customer.DocumentNumber, customer.BirthDate, 
                customer.Phone, customer.StateInscription, customer.StreetAddress, customer.BuildingNumber, customer.SecondaryAddress, 
                customer.Neighborhood, customer.ZipCode, customer.City, customer.State, customer.Active);
        }

        public static RegisterNewCustomerCommand ToRegisterCommand(this CustomerViewModel customer)
        {
            if (customer == null) return null;

            return new RegisterNewCustomerCommand(customer.Id,customer.Name, customer.Email, customer.DocumentNumber, customer.BirthDate, 
                customer.Phone, customer.StateInscription, customer.StreetAddress, customer.BuildingNumber, customer.SecondaryAddress, 
                customer.Neighborhood, customer.ZipCode, customer.City, customer.State, customer.Active);
        }

        public static UpdateCustomerCommand ToUpdateCommand(this CustomerViewModel customer)
        {
            if (customer == null) return null;

            return new UpdateCustomerCommand(customer.Id, customer.Name, customer.Email, customer.DocumentNumber, customer.BirthDate, 
                customer.Phone, customer.StateInscription, customer.StreetAddress, customer.BuildingNumber, customer.SecondaryAddress, 
                customer.Neighborhood, customer.ZipCode, customer.City, customer.State, customer.Active);
        }
    }
}
