using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Unleash;
using unleash_dotnet_sample.Controllers;
using Xunit;

namespace unleash_dotnet_sample_tests.Unit.Controllers
{
    public class UnleashControllerTests
    {
        private readonly IUnleash _unleash;
        private readonly UnleashController _unleashController;

        public UnleashControllerTests()
        {
            _unleash = Substitute.For<IUnleash>();
            
            _unleashController = new UnleashController(_unleash);
        }
        
        [Fact]
        public void Should_get_feature_toggle()
        {
            // Arrange
            var featureToggle = "Sample";

            // Act
            var response = _unleashController.Get(featureToggle);

            // Assert
            response.Should().BeOfType<OkObjectResult>();

            _unleash.Received(1).IsEnabled(featureToggle);
        }
    }
}