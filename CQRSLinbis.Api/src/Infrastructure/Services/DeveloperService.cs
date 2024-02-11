using CQRSLinbis.Application.Common.Exceptions;
using CQRSLinbis.Application.Common.Interfaces.Repository;
using CQRSLinbis.Application.Common.Interfaces.Services;
using CQRSLinbis.Domain.Entities;

namespace CQRSLinbis.Infrastructure.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IRepository<Developer> _projectRepository;

        public DeveloperService(IRepository<Developer> projectRepository) 
        {
            _projectRepository = projectRepository;
        }

        public async Task<Developer> GetDeveloperByIdAsync(int developerId)
        {
            var developer = await _projectRepository.GetByIdAsync(developerId);
            if (developer == null) throw new NotFoundException($"Developer with id {developerId} not found.");
            return developer;
        }
    }
}
