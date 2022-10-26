using DotNetProject.Application.Models;
using MediatR;

namespace DotNetProject.Application.Commands.Readiness;

public class PerformReadinessCheckCommand : IRequest<CommandResult<string>>
{
}