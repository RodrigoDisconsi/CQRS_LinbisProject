using CQRSLinbis.Application.Common.Interfaces.Services;
using MediatR;

namespace CQRSLinbis.Application.Developers.Commands.CreateDeveloper
{
    public class CreateDeveloperCommand : IRequest
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int CostByDay { get; set; }
        public long AddedDate { get; set; }
    }

    public class CreateDeveloperCommandHandler : IRequestHandler<CreateDeveloperCommand>
    {
        private readonly IDeveloperService _developerService;

        public CreateDeveloperCommandHandler(IDeveloperService developerService)
        {
            _developerService = developerService;
        }
        public async Task<Unit> Handle(CreateDeveloperCommand request, CancellationToken cancellationToken)
        {
            await _developerService.CreateDeveloper(request);

            return Unit.Value;
        }
    }

}