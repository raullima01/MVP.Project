using MVP.Project.Application.Services;
using MVP.Project.Domain.Core.Events;
using MVP.Project.Domain.Models;
using MVP.Project.Infra.CrossCutting.Bus;
using MVP.Project.Infra.CrossCutting.Identity.User;
using MVP.Project.Infra.CrossCutting.IoC;
using MVP.Project.Infra.Data.Context;
using MVP.Project.Services.Api.Controllers;
using System.Reflection;

namespace MVP.Project.Tests.Architecture.Support;

public class TestsSupport
{
    public static IEnumerable<Assembly> GetAllProjectAssemblies()
    {
        IEnumerable<Assembly> assemblies = [typeof(AccountController).Assembly,
                                            typeof(CustomerAppService).Assembly,
                                            typeof(Customer).Assembly,
                                            typeof(StoredEvent).Assembly,
                                            typeof(CustomerContext).Assembly,
                                            typeof(InMemoryBus).Assembly,
                                            typeof(AspNetUser).Assembly,
                                            typeof(NativeInjectorBootStrapper).Assembly];

        return assemblies;
    }
}
