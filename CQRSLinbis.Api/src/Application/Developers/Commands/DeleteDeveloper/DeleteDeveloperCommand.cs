using CQRSLinbis.Application.Common.Interfaces.Services;
using MediatR;

namespace CQRSLinbis.Application.Developers.Commands.DeleteDeveloper
{
    public class DeleteDeveloperCommand : IRequest
    {
        public int DeveloperId { get; set; }
    }

    public class DeleteDeveloperCommandHandler : IRequestHandler<DeleteDeveloperCommand>
    {
        private readonly IDeveloperService _developerService;

        public DeleteDeveloperCommandHandler(IDeveloperService developerService)
        {
            _developerService = developerService;
        }
        public async Task<Unit> Handle(DeleteDeveloperCommand request, CancellationToken cancellationToken)
        {
            await _developerService.DeleteDeveloperAsync(request.DeveloperId);

            return Unit.Value;
        }
    }

}