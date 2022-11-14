namespace ${{values.component_id}}.Application.Interfaces;

public interface IExampleRepository
{
    Task<int> UpdateExampleNameById(int id, string name);
}
