using ${{values.component_id}}.Domain.Models;

namespace ${{values.component_id}}.Application.Interfaces;

public interface IExampleServiceClient
{
    Task<Example> GetExampleById(int id);
}
