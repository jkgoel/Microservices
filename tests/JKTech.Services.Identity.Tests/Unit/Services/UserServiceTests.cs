using JKTech.Common.Auth;
using JKTech.Services.Identity.Domain.Models;
using JKTech.Services.Identity.Domain.Repositories;
using JKTech.Services.Identity.Domain.Services;
using JKTech.Services.Identity.Services;
using JKTech.Services.Identity.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Threading.Tasks;
using FluentAssertions;

namespace JKTech.Services.Identity.Tests.Unit.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task User_service_login_should_return_jwt()
        {
            var email = "test@test.com";
            var password = "secret";
            var name = "test";
            var salt = "salt";
            var hash = "hash";
            var token = "token";
            var userRepositoryMock  = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            encrypterMock.Setup(x=>x.GetSalt()).Returns(salt);
            encrypterMock.Setup(x=>x.GetHash(password,salt)).Returns(hash);
            jwtHandlerMock.Setup(x=>x.CreateToken(It.IsAny<Guid>())).Returns(new JsonWebToken
            {
                Token = token
            });

            var user  = new User(email,name);
            user.SetPassword(password,encrypterMock.Object);
            userRepositoryMock.Setup(x=>x.GetAsync(email)).ReturnsAsync(user);
            
            var userService = new UserService(userRepositoryMock.Object,encrypterMock.Object,jwtHandlerMock.Object);
            var jwt = await userService.LoginAsync(email,password);
            userRepositoryMock.Verify(x=>x.GetAsync(email),Times.Once);
            jwtHandlerMock.Verify(x=>x.CreateToken(It.IsAny<Guid>()),Times.Once);
            jwt.Should().NotBeNull();
            jwt.Token.Should().BeEquivalentTo(token);

        }
    }
}
