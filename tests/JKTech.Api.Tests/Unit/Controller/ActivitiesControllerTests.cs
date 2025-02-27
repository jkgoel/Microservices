﻿using FluentAssertions;
using JKTech.Api.Controllers;
using JKTech.Api.Repositories;
using JKTech.Common.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace JKTech.Api.Tests.Unit.Controller
{
    public class ActivitiesControllerTests
    {
        [Fact]
        public async Task Activities_controller_post_should_return_accepted()
        {
            var busClientMock = new Mock<IBusClient>();
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var controller = new ActivitiesController(busClientMock.Object,activityRepositoryMock.Object);
            var userId = Guid.NewGuid();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] {new Claim(ClaimTypes.Name,userId.ToString())},"test"))
                }
            };
            var command = new CreateActivity
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            var result = await controller.Post(command);
            var contentResult = result as AcceptedResult;
            contentResult.Should().NotBeNull();
            contentResult.Location.Should().BeEquivalentTo($"activities/{command.Id}");


        }
    }
}
