using System;
using NetDevPack.Messaging;

namespace MVP.Project.Domain.Commands
{
    public abstract class CustomerCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
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