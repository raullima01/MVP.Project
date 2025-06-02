using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MVP.Project.Application.EventSourcedNormalizers;
using MVP.Project.Application.ViewModels;

namespace MVP.Project.Application.Interfaces
{
    public interface ICustomerAppService : IDisposable
    {
        Task<ValidationResult> Register(CustomerViewModel customerViewModel);
        Task<ValidationResult> Update(Guid id, CustomerViewModel customerViewModel);
        Task<ValidationResult> Remove(Guid id);
        Task<IEnumerable<CustomerViewModel>> GetAll();
        Task<IList<CustomerHistoryData>> GetAllHistory(Guid id);
        Task<CustomerViewModel> GetById(Guid id);
    }
}
