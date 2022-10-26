using DotNetProject.Application.Commands.Example;
using DotNetProject.Application.Interfaces;
using DotNetProject.Application.Models;
using Moq;
using Serilog;

namespace DotNetProject.Application.Tests.Commands.Example;

public class UpdateExampleNameCommandHandlerTests
{
    [Fact]
    public async void Request_With_No_Id_Should_Return_Invalid_Input()
    {
        // ARRANGE
        var validator = new UpdateExampleNameCommandValidator();
        var exampleRepositoryMock = new Mock<IExampleRepository>();
        var mockLogger = new Mock<ILogger>();

        var handler = new UpdateExampleNameCommandHandler(
            mockLogger.Object,
            exampleRepositoryMock.Object,
            validator
        );

        // ACT
        var response = await handler.Handle(new UpdateExampleNameCommand(), new CancellationToken());

        // ASSERT
        Assert.Equal(CommandResultTypeEnum.InvalidInput, response.Type);
    }

    [Fact]
    public async void Should_Call_GetExampleById_In_Repository()
    {
        // ARRANGE
        var validator = new UpdateExampleNameCommandValidator();
        var exampleRepositoryMock = new Mock<IExampleRepository>();
        var mockLogger = new Mock<ILogger>();

        exampleRepositoryMock.Setup(x => x.UpdateExampleNameById(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(1);

        var handler = new UpdateExampleNameCommandHandler(
            mockLogger.Object,
            exampleRepositoryMock.Object,
            validator
        );

        // ACT
        var response = await handler.Handle(new UpdateExampleNameCommand() { Id = 1, Name = "newName" }, new CancellationToken());

        // ASSERT
        Assert.Equal(CommandResultTypeEnum.Success, response.Type);
        exampleRepositoryMock.Verify(x => x.UpdateExampleNameById(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async void Should_Return_Not_Found_If_No_Example()
    {
        // ARRANGE
        var validator = new UpdateExampleNameCommandValidator();
        var exampleRepositoryMock = new Mock<IExampleRepository>();
        var mockLogger = new Mock<ILogger>();

        var handler = new UpdateExampleNameCommandHandler(
            mockLogger.Object,
            exampleRepositoryMock.Object,
            validator
        );

        // ACT
        var response = await handler.Handle(new UpdateExampleNameCommand() { Id = 1, Name = "newName" }, new CancellationToken());

        // ASSERT
        Assert.Equal(CommandResultTypeEnum.NotFound, response.Type);
        exampleRepositoryMock.Verify(x => x.UpdateExampleNameById(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
    }
}