using CQRSLinbis.Application.Developers.Commands.CreateDeveloper;
using CQRSLinbis.Domain.Entities;
using CQRSLinbis.Domain.Queries;

namespace CQRSLinbis.Application.Common.Interfaces.Services
{
    public interface IDeveloperService
    {
        Task<Developer> GetDeveloperByIdAsync(int developerId);
        Task<IQueryable<DeveloperView>> GetDevelopers();
        Task CreateDeveloper(CreateDeveloperCommand developer);
        Task DeleteDeveloperAsync(int developerId);
    }
}
