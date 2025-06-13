using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MVP.Project.Domain.Events;
using MVP.Project.Domain.Interfaces;
using MVP.Project.Domain.Models;
using NetDevPack.Messaging;
using NetDevPack.SimpleMediator.Core.Interfaces;

namespace MVP.Project.Domain.Commands
{
    public class CustomerCommandHandler(ICustomerRepository customerRepository) : CommandHandler,
                                        IRequestHandler<RegisterNewCustomerCommand, ValidationResult>,
                                        IRequestHandler<UpdateCustomerCommand, ValidationResult>,
                                        IRequestHandler<RemoveCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<ValidationResult> Handle(RegisterNewCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Customer(Guid.NewGuid(), message.Name, message.Email, message.DocumentNumber,
                message.BirthDate, message.Phone, message.StateInscription, message.StreetAddress, message.BuildingNumber,
                message.SecondaryAddress, message.Neighborhood, message.ZipCode, message.City, message.State,
                message.Active);

            if (await _customerRepository.GetByEmail(customer.Email) != null)
            {
                AddError("The customer e-mail has already been taken.");
                return ValidationResult;
            }

            customer.AddDomainEvent(new CustomerRegisteredEvent(customer.Id, customer.Name, customer.Email, customer.DocumentNumber,
                customer.BirthDate,customer.Phone, customer.StateInscription, customer.StreetAddress, customer.BuildingNumber, 
                customer.SecondaryAddress, customer.Neighborhood, customer.ZipCode, customer.City, customer.State, customer.Active));

            _customerRepository.Add(customer);

            return await Commit(_customerRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Customer(Guid.NewGuid(), message.Name, message.Email, message.DocumentNumber,
                message.BirthDate, message.Phone, message.StateInscription, message.StreetAddress, message.BuildingNumber,
                message.SecondaryAddress, message.Neighborhood, message.ZipCode, message.City, message.State,
                message.Active);
            
            var existingCustomer = await _customerRepository.GetByEmail(customer.Email);

            if (existingCustomer != null && existingCustomer.Id != customer.Id)
            {
                if (!existingCustomer.Equals(customer))
                {
                    AddError("The customer e-mail has already been taken.");
                    return ValidationResult;
                }
            }

            customer.AddDomainEvent(new CustomerUpdatedEvent(customer.Id, customer.Name, customer.Email, customer.DocumentNumber, customer.BirthDate, 
                customer.Phone, customer.StateInscription, customer.StreetAddress, customer.BuildingNumber, customer.SecondaryAddress, 
                customer.Neighborhood, customer.ZipCode, customer.City, customer.State, customer.Active));

            _customerRepository.Update(customer);

            return await Commit(_customerRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = await _customerRepository.GetById(message.Id);

            if (customer is null)
            {
                AddError("The customer doesn't exists.");
                return ValidationResult;
            }

            customer.AddDomainEvent(new CustomerRemovedEvent(message.Id));

            _customerRepository.Remove(customer);

            return await Commit(_customerRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _customerRepository.Dispose();
        }
    }
}