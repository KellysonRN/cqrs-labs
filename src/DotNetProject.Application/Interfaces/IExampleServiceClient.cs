using DotNetProject.Domain.Models;

namespace DotNetProject.Application.Interfaces;

public interface IExampleServiceClient
{
    Task<Example> GetExampleById(int id);
}
