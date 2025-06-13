using System;
using NetDevPack.Messaging;

namespace MVP.Project.Domain.Events
{
    public class CustomerRegisteredEvent : Event
    {
        public CustomerRegisteredEvent(Guid id, string name, string email, string documentNumber, DateTime birthDate, string phone, string stateInscription, string streetAddress, string buildingNumber, string secondaryAddress, string neighborhood, string zipCode, string city, string state, bool active)
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
            AggregateId = id;
        }
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string DocumentNumber { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public string Phone { get; protected set; }
        public string StateInscription { get; protected set; }
        public string StreetAddress { get; protected set; }
        public string BuildingNumber { get; protected set; }
        public string SecondaryAddress { get; protected set; }
        public string Neighborhood { get; protected set; }
        public string ZipCode { get; protected set; }
        public string City { get; protected set; }
        public string State { get; protected set; }
        public bool Active { get; protected set; }
    }
}