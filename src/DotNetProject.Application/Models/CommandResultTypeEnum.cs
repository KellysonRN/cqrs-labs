namespace DotNetProject.Application.Models;

public enum CommandResultTypeEnum
{
    Success,
    InvalidInput,
    UnprocessableEntity,
    Conflict,
    NotFound
}