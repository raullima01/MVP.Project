using MVP.Project.Domain.Enums;
using MVP.Project.Domain.Extentions;

namespace MVP.Project.Domain.ValuesObjects
{
    public abstract class Document
    {
        public string Number { get; protected set; }
        public abstract EDocumentType Type { get; }
        public bool IsValid => Number.ValidateDocument().IsValid;

        protected Document(string number)
        {
            Number = number;
        }

        public static Document Create(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return new NullDocument();

            var (_, type) = number.ValidateDocument();

            return type switch
            {
                EDocumentType.Cpf => new CpfDocument(number),
                EDocumentType.Cnpj => new CnpjDocument(number),
                _ => new InvalidDocument(number)
            };
        }

        public static Document CreateCpf(string number) => new CpfDocument(number);
        public static Document CreateCnpj(string number) => new CnpjDocument(number);
        public override string ToString() => Number;
    }
}
