using System;
using NetDevPack.Domain;
using MVP.Project.Domain.Enums;
using MVP.Project.Domain.Extentions;
using MVP.Project.Domain.ValuesObjects;

namespace MVP.Project.Domain.Models
{
    public class Customer : Entity, IAggregateRoot
    {
        private Document _document;
        private string _documentNumber;

        public Customer(Guid id, string name, string email, string documentNumber, DateTime birthDate,
            string phone, string stateInscription, string streetAddress, string buildingNumber, string secondaryAddress,
            string neighborhood, string zipCode, string city, string state, bool active)
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

        protected Customer() { }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string DocumentNumber
        {
            get => _documentNumber; private set
            {
                _documentNumber = value;
                _document = Document.Create(value);
            }
        }
        public EDocumentType DocumentType => _document?.Type ?? DocumentNumber.ValidateDocument().DocumentType;
        public DateTime BirthDate { get; private set; }
        public string Phone { get; private set; }
        public string StateInscription { get; private set; }
        public string StreetAddress { get; private set; }
        public string BuildingNumber { get; private set; }
        public string SecondaryAddress { get; private set; }
        public string Neighborhood { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public bool Active { get; private set; }

        public bool ValidateDocumentRules()
        {
            if (_document == null)
                _document = Document.Create(_documentNumber);

            if (_document.Type == EDocumentType.Cpf)
            {
                return BirthDate <= DateTime.Now.AddYears(-18);
            }

            if (_document.Type == EDocumentType.Cnpj)
            {
                return !string.IsNullOrWhiteSpace(StateInscription);
            }

            return false;
        }
    }
};