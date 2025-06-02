using FluentValidation;
using MVP.Project.Domain.Enums;
using MVP.Project.Domain.Extentions;
using System;

namespace MVP.Project.Domain.Commands.Validations
{
    public abstract class CustomerValidation<T> : AbstractValidator<T> where T : CustomerCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
        }

        protected void ValidatePersonBirthDate()
        {
            RuleFor(c => c.BirthDate)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .When(c => {
                    var (_, documentType) = c.DocumentNumber.ValidateDocument();
                    return documentType == EDocumentType.Cpf;
                })
                .WithMessage("The customer must have 18 years or more");
        }

        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }

        protected void ValidateDocument()
        {
            // Valida que o documento tem formato válido
            RuleFor(c => c.DocumentNumber)
                .NotEmpty().WithMessage("Por favor informe o número do documento")
                .IsValidDocument();

            // Valida a inscrição estadual apenas para CNPJ
            RuleFor(c => c.StateInscription)
                .NotEmpty()
                .When(c => {
                    var (_, documentType) = c.DocumentNumber.ValidateDocument();
                    return documentType == EDocumentType.Cnpj;
                })
                .WithMessage("A inscrição estadual é obrigatória para CNPJ.");

            // Valida a idade mínima para CPF
            RuleFor(c => c.BirthDate)
                .Must(HaveMinimumAge)
                .When(c => {
                    var (isValid, documentType) = c.DocumentNumber.ValidateDocument();
                    return isValid && documentType == EDocumentType.Cpf;
                })
                .WithMessage("Para CPF, a idade mínima é de 18 anos");
        }

        protected static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
    }
}