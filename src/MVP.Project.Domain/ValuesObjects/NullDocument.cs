using MVP.Project.Domain.Enums;

namespace MVP.Project.Domain.ValuesObjects
{
    public class NullDocument : Document
    {
        public override EDocumentType Type => EDocumentType.None;
        public NullDocument() : base("") { }
    }
}
