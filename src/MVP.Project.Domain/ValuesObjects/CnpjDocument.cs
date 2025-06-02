using MVP.Project.Domain.Enums;

namespace MVP.Project.Domain.ValuesObjects
{
    public class CnpjDocument : Document
    {
        public override EDocumentType Type => EDocumentType.Cnpj;

        public CnpjDocument(string number) : base(number) { }

        public bool HasValidStateInscription(string stateInscription)
        {
            return !string.IsNullOrWhiteSpace(stateInscription);
        }
    }
}
