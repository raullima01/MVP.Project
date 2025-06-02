using FluentValidation;
using MVP.Project.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace MVP.Project.Domain.Extentions
{
    public static class CustomerValidationExtentions
    {
        public static IRuleBuilderOptions<T, string> IsValidDocument<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(doc => !string.IsNullOrWhiteSpace(doc) && doc.ValidateDocument().IsValid)
                .WithMessage("O número de documento digitado é inválido.");
        }

        public static IRuleBuilderOptions<T, (string DocumentNumber, string StateInscription)> HasValidStateInscription<T>(this IRuleBuilder<T, (string DocumentNumber, string StateInscription)> ruleBuilder)
        {
            return ruleBuilder
                .Must(x =>
                {
                    var (_, documentType) = x.DocumentNumber.ValidateDocument();

                    if (documentType == EDocumentType.Cnpj)
                        return !string.IsNullOrWhiteSpace(x.StateInscription);

                    return true;
                })
                .WithMessage("A inscrição estadual é obrigatória para CNPJ.");
        }

        public static IRuleBuilderOptions<T, string> MustBeUniqueDocument<T>(this IRuleBuilder<T, string> ruleBuilder,Func<string, bool> documentExists)
        {
            return ruleBuilder
                .MustAsync(async (document, cancellation) => !await Task.FromResult(documentExists(document)))
                .WithMessage("Este cliente já existe na base de dados.");
        }
    }
}
