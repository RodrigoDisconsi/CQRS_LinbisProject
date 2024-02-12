﻿using AutoMapper;
using CQRSLinbis.Application.Common.Exceptions;
using CQRSLinbis.Application.Common.Interfaces.Repository;
using CQRSLinbis.Application.Common.Interfaces.Services;
using CQRSLinbis.Application.Developers.Commands.CreateDeveloper;
using CQRSLinbis.Domain.Entities;
using CQRSLinbis.Domain.Queries;
using Microsoft.Extensions.Logging;

namespace CQRSLinbis.Infrastructure.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly ILogger<DeveloperService> _logger;
        private readonly IRepository<Developer> _developerRepository;
        private readonly MapperConfiguration _mapperConfigurator;
        private readonly IMapper _mapper;

        public DeveloperService(IRepository<Developer> projectRepository, ILogger<DeveloperService> logger) 
        {
            _developerRepository = projectRepository;
            _logger = logger;
            _mapperConfigurator = InitMapperConfigurator();
            _mapper = _mapperConfigurator.CreateMapper();
        }

        public async Task<Developer> GetDeveloperByIdAsync(int developerId)
        {
            var developer = await _developerRepository.GetByIdAsync(developerId);
            if (developer == null) throw new NotFoundException($"Developer with id {developerId} not found.");
            return developer;
        }

        public async Task<IQueryable<DeveloperView>> GetDevelopers()
        {
            var developers = await _developerRepository.GetAllAsync();

            return _mapper.ProjectTo<DeveloperView>(developers.AsQueryable());
        }

        public async Task CreateDeveloper(CreateDeveloperCommand developer)
        {
            _logger.LogInformation("Started process create developer");
            var developerDb = _mapper.Map<Developer>(developer);

            await _developerRepository.AddAsync(developerDb);

            _logger.LogInformation("Developer created correctly");
        }

        public async Task DeleteDeveloperAsync(int developerId)
        {
            try
            {
                _logger.LogError("Started delete developer process...");
                var develoepr = await GetDeveloperByIdAsync(developerId);

                await _developerRepository.DeleteAsync(develoepr);
                _logger.LogError("Develoepr deleted correctly...");
            }
            catch (NotFoundException)
            {
                _logger.LogError("DeveloperId was not found in database...");
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

                    cfg.CreateMap<CreateDeveloperCommand, Developer>()
                    .ForMember(dst => dst.AddedDate, opt => opt.MapFrom(x => DateTimeOffset.FromUnixTimeMilliseconds(x.AddedDate)));
                });
        }
    }
}
