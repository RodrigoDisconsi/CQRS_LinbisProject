using CQRSLinbis.Application.Common.Exceptions;
using CQRSLinbis.Application.Common.Interfaces.Services;
using CQRSLinbis.Application.Projects.Commands.AddDeveloperToProject;
using MediatR;
using Moq;

namespace Test.Projects.Commands.AddDeveloperToProject
{
    public class AddDeveloperToProjectTest
    {
        private Mock<IProjectService> _projectServiceMock;
        private AddDeveloperToProjectCommandHandler _requestHandler;

        public AddDeveloperToProjectTest()
        {
            _projectServiceMock = new Mock<IProjectService>();
            _requestHandler = new(_projectServiceMock.Object);
        }

        [Test]
        public async Task Hadle_AddDeveloperToProject_ReturnOk()
        {
            // Arrange
            var request = new AddDeveloperToProjectCommand
            {
                ProjectId = 1,
                Name = "Test",
                IsActive = true,
                CostByDay = 30,
                AddedDate = 1573843210

            };

            _projectServiceMock.Setup(x => x.AddDeveloperToProjectAsync(It.IsAny<AddDeveloperToProjectCommand>()))
                                 .Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _requestHandler.Handle(request, new CancellationToken());

            // Assert
            Assert.That(result == Unit.Value);
        }


        [Test]
        public void Handle_AddDeveloperToProject_ThrowNotFoundException()
        {
            var request = new AddDeveloperToProjectCommand
            {
                ProjectId = 1,
                Name = "Test",
                IsActive = true,
                CostByDay = 30,
                AddedDate = 1573843210
            };

            _projectServiceMock.Setup(x => x.AddDeveloperToProjectAsync(It.IsAny<AddDeveloperToProjectCommand>()))
                                 .ThrowsAsync(new NotFoundException($"Project with id 1 not found."));


            Assert.ThrowsAsync<NotFoundException>(async () => await _requestHandler.Handle(request, new CancellationToken()));
        }

    }

}