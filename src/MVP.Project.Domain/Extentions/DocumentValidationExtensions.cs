using MVP.Project.Domain.Enums;
using System.Linq;
using System.Text.RegularExpressions;

namespace MVP.Project.Domain.Extentions
{
    public static class DocumentValidationExtensions
    {
        public static (bool IsValid, EDocumentType DocumentType) ValidateDocument(this string documentNumber)
        {
            if (string.IsNullOrWhiteSpace(documentNumber))
                return (false, EDocumentType.None);

            var document = Regex.Replace(documentNumber, @"[^\d]", "");

            if (document.Length == 11)
                return (IsCpfValid(document), EDocumentType.Cpf);
            else if (document.Length == 14)
                return (IsCnpjValid(document), EDocumentType.Cnpj);
            else
                return (false, EDocumentType.None);
        }
        private static bool IsCpfValid(string cpf)
        {
            if (cpf.All(c => c == cpf[0]))
                return false;

            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

            int remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            string digit = remainder.ToString();
            tempCpf += digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

            remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            digit += remainder.ToString();

            return cpf.EndsWith(digit);
        }
        private static bool IsCnpjValid(string cnpj)
        {
            if (cnpj.All(c => c == cnpj[0]))
                return false;

            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int sum = 0;

            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

            int remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            string digit = remainder.ToString();
            tempCnpj += digit;
            sum = 0;

            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

            remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            digit += remainder.ToString();

            return cnpj.EndsWith(digit);
        }
        public static bool ValidateStateInscription(this string stateInscription, EDocumentType documentType)
        {
            if (documentType == EDocumentType.Cnpj)
            {
                return !string.IsNullOrWhiteSpace(stateInscription);
            }

            return true;
        }
    }
}