using FluentValidation.Results;
using MVP.Project.Domain.Enums;
using MVP.Project.Domain.Events;
using MVP.Project.Domain.Extentions;
using MVP.Project.Domain.Interfaces;
using MVP.Project.Domain.Models;
using MVP.Project.Domain.ValuesObjects;
using NetDevPack.Messaging;
using NetDevPack.SimpleMediator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MVP.Project.Domain.Commands
{
    public class CustomerCommandHandler(ICustomerRepository customerRepository) : CommandHandler,
                                        IRequestHandler<RegisterNewCustomerCommand, ValidationResult>,
                                        IRequestHandler<UpdateCustomerCommand, ValidationResult>,
                                        IRequestHandler<RemoveCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        //public async Task<ValidationResult> Handle(RegisterNewCustomerCommand message, CancellationToken cancellationToken)
        //{
        //    if (!message.IsValid()) return message.ValidationResult;

        //    var customer = new Customer(Guid.NewGuid(), message.Name, message.Email, message.DocumentNumber,
        //        message.BirthDate, message.Phone, message.StateInscription, message.StreetAddress, message.BuildingNumber,
        //        message.SecondaryAddress, message.Neighborhood, message.ZipCode, message.City, message.State,
        //        message.Active);

        //    if (await _customerRepository.GetByDocumentNumber(customer.DocumentNumber) != null)
        //    {
        //        AddError("Este cliente já foi cadastrado.");
        //        return ValidationResult;
        //    }

        //    customer.AddDomainEvent(new CustomerRegisteredEvent(customer.Id, customer.Name, customer.Email, customer.DocumentNumber,
        //        customer.BirthDate, customer.Phone, customer.StateInscription, customer.StreetAddress, customer.BuildingNumber,
        //        customer.SecondaryAddress, customer.Neighborhood, customer.ZipCode, customer.City, customer.State, customer.Active));

        //    _customerRepository.Add(customer);

        //    return await Commit(_customerRepository.UnitOfWork);
        //}

        public async Task<ValidationResult> Handle(RegisterNewCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            // Criar o Customer com Value Object Document
            var customer = new Customer(
                Guid.NewGuid(),
                message.Name,
                message.Email,
                message.DocumentNumber,
                message.BirthDate,
                message.Phone,
                message.StateInscription,
                message.StreetAddress,
                message.BuildingNumber,
                message.SecondaryAddress,
                message.Neighborhood,
                message.ZipCode,
                message.City,
                message.State,
                message.Active);

            // Verificar se documento é válido
            if (!Document.Create(message.DocumentNumber).IsValid)
            {
                AddError("O documento informado é inválido.");
                return ValidationResult;
            }

            // Verificar se já existe cliente com este documento
            if (await _customerRepository.GetByDocumentNumber(customer.DocumentNumber) != null)
            {
                AddError("Este cliente já foi cadastrado.");
                return ValidationResult;
            }

            // Validar regras específicas do tipo de documento
            if (!customer.ValidateDocumentRules())
            {
                if (customer.DocumentType == EDocumentType.Cpf)
                {
                    AddError("Para CPF, a idade mínima é de 18 anos.");
                }
                else if (customer.DocumentType == EDocumentType.Cnpj)
                {
                    AddError("Para CNPJ, a Inscrição Estadual é obrigatória.");
                }
                else
                {
                    AddError("Documento inválido.");
                }
                return ValidationResult;
            }

            // Adicionar evento de domínio
            customer.AddDomainEvent(new CustomerRegisteredEvent(
                customer.Id,
                customer.Name,
                customer.Email,
                customer.DocumentNumber,
                customer.BirthDate,
                customer.Phone,
                customer.StateInscription,
                customer.StreetAddress,
                customer.BuildingNumber,
                customer.SecondaryAddress,
                customer.Neighborhood,
                customer.ZipCode,
                customer.City,
                customer.State,
                customer.Active));

            _customerRepository.Add(customer);

            return await Commit(_customerRepository.UnitOfWork);
        }
 

public async Task<ValidationResult> Handle(UpdateCustomerCommand message, CancellationToken cancellationToken)
        {
            // Busca o cliente existente pelo ID
            var existingCustomer = await _customerRepository.GetById(message.Id);
            if (existingCustomer == null)
            {
                AddError("Cliente não encontrado.");
                return ValidationResult;
            }

            // Atualiza o cliente existente usando o método de extensão
            var updatedCustomer = existingCustomer.UpdateWithPatch(message);

            // Adiciona o evento de atualização
            updatedCustomer.AddDomainEvent(new CustomerUpdatedEvent(
                updatedCustomer.Id,
                updatedCustomer.Name,
                updatedCustomer.Email,
                updatedCustomer.DocumentNumber,
                updatedCustomer.BirthDate,
                updatedCustomer.Phone,
                updatedCustomer.StateInscription,
                updatedCustomer.StreetAddress,
                updatedCustomer.BuildingNumber,
                updatedCustomer.SecondaryAddress,
                updatedCustomer.Neighborhood,
                updatedCustomer.ZipCode,
                updatedCustomer.City,
                updatedCustomer.State,
                updatedCustomer.Active));

            // Atualiza o cliente no repositório
            _customerRepository.Update(updatedCustomer.Id, updatedCustomer);

            // Persiste as alterações
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