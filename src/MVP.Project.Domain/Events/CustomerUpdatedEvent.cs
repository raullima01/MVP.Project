using System;
using NetDevPack.Messaging;

namespace MVP.Project.Domain.Events
{
    public class CustomerUpdatedEvent : Event
    {
        public CustomerUpdatedEvent(Guid id, string name, string email, string documentNumber, DateTime birthDate, string phone, string stateInscription, 
            string streetAddress, string buildingNumber, string secondaryAddress, string neighborhood, string zipCode, string city, string state, bool active)
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
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string DocumentNumber { get; private set; }
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
    }
}