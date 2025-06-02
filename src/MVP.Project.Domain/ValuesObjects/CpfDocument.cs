using MVP.Project.Domain.Enums;
using System;

namespace MVP.Project.Domain.ValuesObjects
{
    public class CpfDocument : Document
    {
        public override EDocumentType Type => EDocumentType.Cpf;

        public CpfDocument(string number) : base(number) { }

        public bool HasMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
    }
}
