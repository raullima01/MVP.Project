using MVP.Project.Domain.Enums;

namespace MVP.Project.Domain.ValuesObjects
{
    public class InvalidDocument : Document
    {
        public override EDocumentType Type => EDocumentType.None;

        public InvalidDocument(string number) : base(number) { }
    }
}
