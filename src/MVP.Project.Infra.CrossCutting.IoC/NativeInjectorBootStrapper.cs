using MVP.Project.Application.Interfaces;
using MVP.Project.Application.Services;
using MVP.Project.Domain.Commands;
using MVP.Project.Domain.Core.Events;
using MVP.Project.Domain.Events;
using MVP.Project.Domain.Interfaces;
using MVP.Project.Infra.Data.Context;
using MVP.Project.Infra.Data.EventSourcing;
using MVP.Project.Infra.Data.Repository;
using MVP.Project.Infra.Data.Repository.EventSourcing;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MVP.Project.Infra.CrossCutting.Bus;
using NetDevPack.Mediator;
using NetDevPack.SimpleMediator.Core.Implementation;
using NetDevPack.SimpleMediator.Core.Interfaces;

namespace MVP.Project.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(WebApplicationBuilder builder)
        {
            // Domain Bus (Mediator)
            builder.Services.AddScoped<IMediator, Mediator>();
            builder.Services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();

            // Domain - Events
            builder.Services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            builder.Services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            builder.Services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();

            // Domain - Commands
            builder.Services.AddScoped<IRequestHandler<RegisterNewCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<UpdateCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<RemoveCustomerCommand, ValidationResult>, CustomerCommandHandler>();

            // Infra - Data
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<CustomerContext>();

            // Infra - Data EventSourcing
            builder.Services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            builder.Services.AddScoped<IEventStore, SqlEventStore>();
            builder.Services.AddScoped<EventStoreSqlContext>();
        }
    }
}