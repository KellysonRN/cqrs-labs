namespace DotNetProject.Application.Interfaces;

public interface IExampleRepository
{
    Task<int> UpdateExampleNameById(int id, string name);
}
