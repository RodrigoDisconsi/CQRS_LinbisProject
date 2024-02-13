using CQRSLinbis.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CQRSLinbis.Application.Projects.Events
{
    internal class DeleteProjectEventHandler : INotificationHandler<DeleteProjectEvent>
    {
        private readonly ILogger<DeleteProjectEventHandler> _logger;

        public DeleteProjectEventHandler(ILogger<DeleteProjectEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(DeleteProjectEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Domain Event: {notification.GetType().Name}, Ex: {ex.Message}");
            }
        }
    }
}
