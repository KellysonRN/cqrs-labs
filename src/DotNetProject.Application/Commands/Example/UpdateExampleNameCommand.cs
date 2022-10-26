using DotNetProject.Application.Models;
using MediatR;

namespace DotNetProject.Application.Commands.Example;

public class UpdateExampleNameCommand : IRequest<CommandResult<bool>>
{
    public int Id { get; set; }
    
    public string Name { get; set; }
}
