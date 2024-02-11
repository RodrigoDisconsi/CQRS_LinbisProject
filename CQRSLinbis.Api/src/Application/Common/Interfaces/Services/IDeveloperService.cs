using CQRSLinbis.Domain.Entities;

namespace CQRSLinbis.Application.Common.Interfaces.Services
{
    public interface IDeveloperService
    {
        Task<Developer> GetDeveloperByIdAsync(int developerId);
    }
}
