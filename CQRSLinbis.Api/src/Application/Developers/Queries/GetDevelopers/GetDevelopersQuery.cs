using AutoMapper;
using CQRSLinbis.Application.Common.Interfaces.Services;
using MediatR;
using CQRSLinbis.Application.Developers.Queries.Models;
using CQRSLinbis.Application.Developers.Queries.GetDevelopers.Response;
using CQRSLinbis.Application.Common.Base;

namespace CQRSLinbis.Application.Developers.Queries.GetDeveloperById
{
    public class GetDevelopersQuery : PagerBase, IRequest<GetDevelopersResponse>
    {
        public string TextoBusqueda { get; set; }
    }

    public class GetDevelopersQueryHandler : IRequestHandler<GetDevelopersQuery, GetDevelopersResponse>
    {
        private readonly IDeveloperService _developerService;
        private readonly IMapper _mapper;
        public GetDevelopersQueryHandler(IDeveloperService developerService, IMapper mapper)
        {
            _developerService = developerService;
            _mapper = mapper;
        }

        public async Task<GetDevelopersResponse> Handle(GetDevelopersQuery request, CancellationToken cancellationToken)
        {
            var response = new GetDevelopersResponse
            {
                Developers = (await _developerService.GetDevelopers(request)).Map<DeveloperDto>(_mapper.ConfigurationProvider),
            };

            return await Task.FromResult(response);
        }
    }


}