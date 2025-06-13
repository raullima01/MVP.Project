using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVP.Project.Application.Extensions;
using MVP.Project.Domain.Commands;
using MVP.Project.Domain.Interfaces;
using MVP.Project.Infra.Data.Repository.EventSourcing;
using FluentValidation.Results;
using MVP.Project.Application.EventSourcedNormalizers;
using MVP.Project.Application.Interfaces;
using MVP.Project.Application.ViewModels;
using NetDevPack.Mediator;

namespace MVP.Project.Application.Services
{
    public class CustomerAppService(ICustomerRepository customerRepository,
                                    IMediatorHandler mediator,
                                    IEventStoreRepository eventStoreRepository) : ICustomerAppService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IEventStoreRepository _eventStoreRepository = eventStoreRepository;
        private readonly IMediatorHandler _mediator = mediator;

        public async Task<IEnumerable<CustomerViewModel>> GetAll()
        {
            return (await _customerRepository.GetAll()).ToViewModel();
        }

        public async Task<CustomerViewModel> GetById(Guid id)
        {
            return (await _customerRepository.GetById(id)).ToViewModel();
        }

        public async Task<ValidationResult> Register(CustomerViewModel customerViewModel)
        {
            var registerCommand = customerViewModel.ToRegisterCommand();
            return await _mediator.SendCommand(registerCommand);
        }

        public async Task<ValidationResult> Update(CustomerViewModel customerViewModel)
        {
            var updateCommand = customerViewModel.ToUpdateCommand();
            return await _mediator.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveCustomerCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<IList<CustomerHistoryData>> GetAllHistory(Guid id)
        {
            return CustomerHistory.ToJavaScriptCustomerHistory(await _eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
