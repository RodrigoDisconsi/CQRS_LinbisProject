using AutoMapper;
using CQRSLinbis.Application.Common.Interfaces.Repository;
using CQRSLinbis.Application.Common.Interfaces.Services;
using CQRSLinbis.Application.Common.Models;
using CQRSLinbis.Application.Projects.Queries.GetProjects;
using CQRSLinbis.Domain.Entities;
using CQRSLinbis.Domain.Queries;
using System.Linq.Expressions;

namespace CQRSLinbis.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly MapperConfiguration _mapperConfigurator;
        private readonly IMapper _mapper;

        public ProjectService(IRepository<Project> projectRepository) 
        {
            _projectRepository = projectRepository;
            _mapperConfigurator = InitMapperConfigurator();
            _mapper = _mapperConfigurator.CreateMapper();
        }


        public async Task<PaginatedList<ProjectView>> GetProjects(GetProjectsQuery query)
        {
            Expression<Func<Project, bool>> filter = null;

            if (!String.IsNullOrEmpty(query.TextoBusqueda))
            {
                filter = p => p.Name.Contains(query.TextoBusqueda);
            }

            return await _projectRepository.GetPaginatedListAsync(
                          filter: filter,
                          selector: p => new ProjectView
                          {
                              ProjectId = p.Id,
                              Name = p.Name,
                              IsActive = p.IsActive,
                              EffortRequiredInDays = p.EffortRequiredInDays,
                              Developers = p.Developers.Select(d => _mapper.Map<DeveloperView>(d)).ToList()
                          },
                          include: p => p.Developers,
                          pager: query);
        }
        private MapperConfiguration InitMapperConfigurator()
        {
            return
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DeveloperView, Developer>();
                });

        }
    }
}
