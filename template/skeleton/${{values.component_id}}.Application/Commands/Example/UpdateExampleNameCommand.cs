using ${{values.component_id}}.Application.Models;
using MediatR;

namespace ${{values.component_id}}.Application.Commands.Example;

public class UpdateExampleNameCommand : IRequest<CommandResult<bool>>
{
    public int Id { get; set; }

    public string Name { get; set; }
}
