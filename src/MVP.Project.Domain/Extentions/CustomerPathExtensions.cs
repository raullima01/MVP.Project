using System;
using MVP.Project.Domain.Models;
using MVP.Project.Domain.Commands;

namespace MVP.Project.Domain.Extentions
{
    public static class CustomerPathExtensions
    {
        public static Customer UpdateWithPatch(this Customer customer, UpdateCustomerCommand command)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            if (command == null) throw new ArgumentNullException(nameof(command));
            return new Customer(
                customer.Id,
                command.Name ?? customer.Name,
                command.Email ?? customer.Email,
                command.DocumentNumber ?? customer.DocumentNumber,
                command.BirthDate != default ? command.BirthDate : customer.BirthDate,
                command.Phone ?? customer.Phone,
                command.StateInscription ?? customer.StateInscription,
                command.StreetAddress ?? customer.StreetAddress,
                command.BuildingNumber ?? customer.BuildingNumber,
                command.SecondaryAddress ?? customer.SecondaryAddress,
                command.Neighborhood ?? customer.Neighborhood,
                command.ZipCode ?? customer.ZipCode,
                command.City ?? customer.City,
                command.State ?? customer.State,
                command.Active
            );
        }
    }
}
