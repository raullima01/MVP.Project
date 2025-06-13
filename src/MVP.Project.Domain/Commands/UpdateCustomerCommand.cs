using System;
using MVP.Project.Domain.Commands.Validations;

namespace MVP.Project.Domain.Commands
{
    public class UpdateCustomerCommand : CustomerCommand
    {
        public UpdateCustomerCommand(Guid id, string name, string email, string documentNumber, DateTime birthDate, string phone, string stateInscription, string streetAddress, string buildingNumber, string secondaryAddress, string neighborhood, string zipCode, string city, string state, bool active)
        {
            Id = id;
            Name = name;
            Email = email;
            DocumentNumber = documentNumber;
            BirthDate = birthDate;
            Phone = phone;
            StateInscription = stateInscription;
            StreetAddress = streetAddress;
            BuildingNumber = buildingNumber;
            SecondaryAddress = secondaryAddress;
            Neighborhood = neighborhood;
            ZipCode = zipCode;
            City = city;
            State = state;
            Active = active;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}