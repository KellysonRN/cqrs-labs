using AutoMapper;
using DotNetProject.Domain.Models;

namespace DotNetProject.Infrastructure.ExampleService;

public class ExampleProfile : Profile
{
    public ExampleProfile()
    {
        CreateMap<ExampleEntity, Example>()
            .ReverseMap();
    }
}
