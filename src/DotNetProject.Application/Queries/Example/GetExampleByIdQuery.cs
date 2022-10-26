using DotNetProject.Application.Models;
using MediatR;

namespace DotNetProject.Application.Queries.Example;

public class GetExampleByIdQuery : IRequest<QueryResult<Domain.Models.Example>>
{
    public int Id { get; set; }
}
