using JKTech.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace JKTech.Api.Tests.Unit.Controller
{
    public class HomeControllerTests
    {
        [Fact]
        public void Home_controller_get_should_return_string_content()
        {
            var controller = new HomeController();

            var result = controller.Get();

            var contentResult = result as ContentResult;
            contentResult.Should().NotBeNull();
            contentResult.Content.Should().BeEquivalentTo("Hello from JKTech API");
        }
    }
}