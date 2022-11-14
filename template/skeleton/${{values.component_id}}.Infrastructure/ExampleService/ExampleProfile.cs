using AutoMapper;
using ${{values.component_id}}.Domain.Models;

namespace ${{values.component_id}}.Infrastructure.ExampleService;

public class ExampleProfile : Profile
{
    public ExampleProfile()
    {
        CreateMap<ExampleEntity, Example>()
            .ReverseMap();
    }
}
