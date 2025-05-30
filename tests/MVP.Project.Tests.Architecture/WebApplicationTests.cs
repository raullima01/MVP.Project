using MVP.Project.Services.Api.Controllers;
using NetArchTest.Rules;

namespace MVP.Project.Tests.Architecture;

public class WebApplicationTests
{    
    [Fact(DisplayName = "Web Application Should Not Have Data Dependencies")]
    [Trait("", "Web Application")]
    public void Application_ShouldNotHave_DataAccessDependencies()
    {
        // Arrange
        var appWeb = Types.InAssembly(typeof(HomeController).Assembly)
                          .That().DoNotResideInNamespace("MVP.Project.UI.Web.Configurations");

        // Act
        var result = appWeb
                        .ShouldNot()
                        .HaveDependencyOnAny
                            ("MVP.Project.Infra.Data")
                        .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Failed in {result.FailingTypes?.Count} dependencie(s)");
    }
}
