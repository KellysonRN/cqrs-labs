using AutoMapper;
using ${{values.component_id}}.Application.Interfaces;
using ${{values.component_id}}.Application.Models;
using ${{values.component_id}}.Infrastructure.EF;
using ${{values.component_id}}.Infrastructure.ExampleService;
using Microsoft.Extensions.Options;

namespace ${{values.component_id}}.Infrastructure.Ef;

public class ExampleRepository : IExampleRepository
{
    private readonly IOptions<EnvironmentConfiguration> _configuration;

    private readonly IMapper _mapper;

    private readonly ExampleDbContext _context;

    public ExampleRepository(ExampleDbContext context, IMapper mapper, IOptions<EnvironmentConfiguration> configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<int> UpdateExampleNameById(int id, string name)
    {
        var example = new ExampleEntity()
        {
            Id = id,
            Name = name
        };

        _context.Example.Update(example);
        await _context.SaveChangesAsync();
        return example.Id;
    }
}
