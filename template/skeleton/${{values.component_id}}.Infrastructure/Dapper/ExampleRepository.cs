using System.Data;
using System.Data.SqlClient;
using Dapper;
using AutoMapper;
using ${{values.component_id}}.Application.Interfaces;
using ${{values.component_id}}.Application.Models;
using Microsoft.Extensions.Options;

namespace ${{values.component_id}}.Infrastructure.Dapper;

public class ExampleRepository : IExampleRepository
{
    private readonly IOptions<EnvironmentConfiguration> _configuration;

    private readonly IMapper _mapper;

    public ExampleRepository(IMapper mapper, IOptions<EnvironmentConfiguration> configuration)
    {
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<int> UpdateExampleNameById(int id, string name)
    {
        var query = Sql.UpdateExampleNameById.Value;

        using (IDbConnection db = new SqlConnection(this._configuration.Value.SQL_CONNECTION_STRING))
        {
            var rowsAffected = await db.ExecuteScalarAsync<int>(query, new { Id = id, Name = name });

            db.ExecuteScalar(query, new { Id = id, Name = name });
        }

        return 1;
    }
}
