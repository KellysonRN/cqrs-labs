using ${{values.component_id}}.Application.Models;
using MediatR;

namespace ${{values.component_id}}.Application.Commands.Readiness;

public class PerformReadinessCheckCommand : IRequest<CommandResult<string>>
{
}
