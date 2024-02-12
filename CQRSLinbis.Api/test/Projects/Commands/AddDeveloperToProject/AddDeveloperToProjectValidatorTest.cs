using CQRSLinbis.Application.Projects.Commands.AddDeveloperToProject;
using FluentValidation.TestHelper;

namespace Test.Projects.Commands.AddDeveloperToProject
{
    public class AddDeveloperToProjectValidatorTests
    {
        private AddDeveloperToProjectValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new AddDeveloperToProjectValidator();
        }

        [Test]
        public async Task Validator_ReceiveRequestWithoutName_ReturnsValidationFailure()
        {
            // Arrange
            var request = new AddDeveloperToProjectCommand
            {
                ProjectId = 1,
                Name = null,
                CostByDay = 30,
                AddedDate = 15392130
            };

            // Act
            var result = await _validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                      .WithErrorMessage("Name is required");
        }

        [Test]
        public async Task Validator_ReceiveRequestWithoutCostByDay_ReturnsValidationFailure()
        {
            // Arrange
            var request = new AddDeveloperToProjectCommand
            {
                ProjectId = 1,
                Name = "Test",
                CostByDay = 0, 
                AddedDate = 15392130
            };

            // Act
            var result = await _validator.TestValidateAsync(request);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.CostByDay)
                      .WithErrorMessage("CostByDay is required.");
        }

        [Test]
        public async Task Validator_ReceiveRequestWithoutAddedDate_ReturnsValidationFailure()
        {
            // Arrange
            var request = new AddDeveloperToProjectCommand
            {
                ProjectId = 1,
                Name = "Test",
                CostByDay = 30,
                AddedDate = 0
            };

            // Act
            var result = await _validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.AddedDate)
                      .WithErrorMessage("AddedDate is required.");
        }

        [Test]
        public async Task Validator_ReceiveRequestWithNameCostByDayAndAddedDate_ReturnsSuccessfulValidation()
        {
            // Arrange
            var request = new AddDeveloperToProjectCommand
            {
                ProjectId = 1,
                Name = "Test",
                CostByDay = 30,
                AddedDate = 15392130
            };

            // Act
            var result = await _validator.TestValidateAsync(request);

            // Act
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.CostByDay);
            result.ShouldNotHaveValidationErrorFor(x => x.AddedDate);
        }
    }
}