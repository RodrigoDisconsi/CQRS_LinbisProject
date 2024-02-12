using AutoMapper;
using CQRSLinbis.Application.Common.Exceptions;
using CQRSLinbis.Application.Common.Interfaces.Repository;
using CQRSLinbis.Application.Common.Interfaces.Services;
using CQRSLinbis.Application.Common.Models;
using CQRSLinbis.Application.Projects.Commands.AddDeveloperToProject;
using CQRSLinbis.Application.Projects.Queries.GetProjectById;
using CQRSLinbis.Application.Projects.Queries.GetProjects;
using CQRSLinbis.Domain.Entities;
using CQRSLinbis.Domain.Queries;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CQRSLinbis.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ILogger<ProjectService> _logger;
        private readonly IRepository<Project> _projectRepository;
        private readonly IDeveloperService _developerService;
        private readonly MapperConfiguration _mapperConfigurator;
        private readonly IMapper _mapper;

        public ProjectService(IRepository<Project> projectRepository, IDeveloperService developerService, ILogger<ProjectService> logger) 
        {
            _projectRepository = projectRepository;
            _developerService = developerService;
            _logger = logger;
            _mapperConfigurator = InitMapperConfigurator();
            _mapper = _mapperConfigurator.CreateMapper();
        }

        public async Task<ProjectView> GetProjectById(GetProjectByIdQuery request)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId, include: p => p.Developers);
            if (project == null) throw new NotFoundException($"Project with id {request.ProjectId} not found.");

            return _mapper.Map<ProjectView>(project);
        }

        public async Task<Project> GetProjectById(int projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null) throw new NotFoundException($"Project with id {projectId} not found.");

            return project;
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
                          selector: p => _mapper.Map<ProjectView>(p),
                          include: p => p.Developers,
                          pager: query);
        }

        public async Task AddDeveloperToProjectAsync(AddDeveloperToProjectCommand request)
        {
            try
            {
                _logger.LogInformation("Started add developer to project process...");

                var project = await _projectRepository.GetByIdAsync(request.ProjectId, include: p => p.Developers);
                if (project == null) throw new NotFoundException($"Project with id {request.ProjectId} not found.");

                var developer = request.DeveloperId == 0 ? _mapper.Map<Developer>(request)
                    : await _developerService.GetDeveloperByIdAsync(request.DeveloperId);

                if (developer.Id != 0 && project.Developers.Any(p => p.Id == developer.Id))
                {
                    _logger.LogWarning("Developer already exist in project...");
                    return;
                }
                project.Developers.Add(developer);

                await _projectRepository.UpdateAsync(project);

                _logger.LogInformation("Developer created and added in project correctly...");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: {ex.Message} - {ex.StackTrace}");
                throw;
            }
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            try
            {
                _logger.LogError("Started delete project process...");
                var project = await _projectRepository.GetByIdAsync(projectId, include: p => p.Developers);
                if (project == null) throw new NotFoundException($"Project with id {projectId} not found.");

                await _projectRepository.DeleteAsync(project);
                _logger.LogError("Project deleted correctly...");
            }
            catch (NotFoundException)
            {
                _logger.LogError("ProjectId was not found in database...");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: {ex.Message} - {ex.StackTrace}");
                throw;
            }
        }

        private MapperConfiguration InitMapperConfigurator()
        {
            return
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Developer, DeveloperView>()
                    .ForMember(dst => dst.DeveloperId, opt => opt.MapFrom(x => x.Id));

                    cfg.CreateMap<Project, ProjectView>()
                       .ForMember(dst => dst.ProjectId, opt => opt.MapFrom(x => x.Id))
                       .ForMember(dst => dst.Developers, opt => opt.MapFrom(x => x.Developers));

                    cfg.CreateMap<AddDeveloperToProjectCommand, Developer>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(x => x.DeveloperId))
                    .ForMember(dst => dst.AddedDate, opt => opt.MapFrom(x => DateTimeOffset.FromUnixTimeMilliseconds(x.AddedDate)));
                });
        }
    }
}
