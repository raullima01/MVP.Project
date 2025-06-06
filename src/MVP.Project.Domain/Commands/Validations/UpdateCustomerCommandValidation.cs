﻿namespace MVP.Project.Domain.Commands.Validations
{
    public class UpdateCustomerCommandValidation : CustomerValidation<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidatePersonBirthDate();
            ValidateEmail();
        }
    }
}