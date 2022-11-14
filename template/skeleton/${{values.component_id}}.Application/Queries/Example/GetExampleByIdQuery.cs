using ${{values.component_id}}.Application.Models;
using MediatR;

namespace ${{values.component_id}}.Application.Queries.Example;

public class GetExampleByIdQuery : IRequest<QueryResult<Domain.Models.Example>>
{
    public int Id { get; set; }
}
