using Microsoft.AspNetCore.Mvc;
using ${{values.component_id}}.Application.Models;
using ${{values.component_id}}.Domain.Models;
using ${{values.component_id}}.Application.Commands.Example;
using ${{values.component_id}}.Application.Queries.Example;
using MediatR;
using ILogger = Serilog.ILogger;

namespace ${{values.component_id}}.Api.Controllers;

[Route("api/v{version:apiVersion}")]
[ApiController]
public class ExampleController : Controller
{
    private readonly ILogger _logger;

    private readonly IMediator _mediator;

    public ExampleController(
        ILogger logger,
        IMediator mediator
    )
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Get an Example by it's ID
    /// </summary>
    /// <remarks>
    /// Retrieves an Example by the ID specified
    /// </remarks>
    /// <param name="id">ID of the example</param>
    [HttpGet]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [Route("examples/{id}")]
    public async Task<ActionResult<Example>> GetExampleById([FromRoute] int id)
    {
        var getExampleByIdQuery = new GetExampleByIdQuery()
        {
            Id = id,
        };

        var result = await _mediator.Send(getExampleByIdQuery);

        if (result.Type == QueryResultTypeEnum.InvalidInput)
        {
            return new BadRequestResult();
        }

        if (result.Type == QueryResultTypeEnum.NotFound)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(result.Result);
    }

    /// <summary>
    /// Update an Example's name by it's ID
    /// </summary>
    /// <remarks>
    /// Updates the Name of any Examples with the ID specified to the name specified
    /// </remarks>
    /// <param name="id">ID of the example</param>
    /// <param name="name">The new name for the Example</param>
    [HttpPut]
    [ApiVersion("2")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [Route("{id}")]
    public async Task<ActionResult<bool>> UpdateExampleNameById([FromRoute] int id, [FromBody] string name)
    {
        var updateExampleNameCommand = new UpdateExampleNameCommand()
        {
            Id = id,
            Name = name,
        };
        var result = await _mediator.Send(updateExampleNameCommand);

        if (result.Type == CommandResultTypeEnum.InvalidInput)
        {
            return new BadRequestResult();
        }

        if (result.Type == CommandResultTypeEnum.NotFound)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(result.Result);
    }
}
