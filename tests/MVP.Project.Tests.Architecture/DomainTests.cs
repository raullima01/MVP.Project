using MVP.Project.Domain.Core.Events;
using MVP.Project.Domain.Models;
using NetArchTest.Rules;

namespace MVP.Project.Tests.Architecture;

public class DomainTests
{
    [Fact(DisplayName = "Domain Should Not Have Any Dependencies")]
    [Trait("", "Domain")]
    public void Domain_ShouldNotHave_ProjectDependencies()
    {
        // Arrange
        var domain = Types.InAssembly(typeof(Customer).Assembly);

        // Act
        var result = domain
                        .ShouldNot()
                        .HaveDependencyOnAny
                            ("MVP.Project.UI.Web",
                             "MVP.Project.Services.Api",
                             "MVP.Project.Application",
                             "MVP.Project.Infra.Data",
                             "MVP.Project.Domain.Core",
                             "MVP.Project.Infra.CrossCutting.Bus",
                             "MVP.Project.Infra.CrossCutting.Identity",
                             "MVP.Project.Infra.CrossCutting.IoC")
                        .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Failed in {result.FailingTypes?.Count} dependencie(s)");
    }

    [Fact(DisplayName = "Domain Namespace Should Have A Pattern")]
    [Trait("", "Domain")]
    public void DomainElements_MustReside_InSameNameSpace()
    {
        // Arrange
        var domain = Types.InAssembly(typeof(Customer).Assembly);

        // Act
        var result = domain
                        .Should()
                        .ResideInNamespaceStartingWith("MVP.Project.Domain")
                        .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Failed in {result.FailingTypes?.Count} type(s)");
    }

    [Fact(DisplayName = "Domain Core Should Not Have Any Dependencies")]
    [Trait("", "Domain")]
    public void DomainCore_ShouldNotHave_ProjectDependencies()
    {
        // Arrange
        var domainCore = Types.InAssembly(typeof(StoredEvent).Assembly);

        // Act
        var result = domainCore
                        .ShouldNot()
                        .HaveDependencyOnAny
                            ("MVP.Project.UI.Web",
                             "MVP.Project.Services.Api",
                             "MVP.Project.Application",
                             "MVP.Project.Infra.Data",
                             "MVP.Project.Domain",
                             "MVP.Project.Infra.CrossCutting.Bus",
                             "MVP.Project.Infra.CrossCutting.Identity",
                             "MVP.Project.Infra.CrossCutting.IoC")
                        .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Failed in {result.FailingTypes?.Count} dependencie(s)");
    }
}
