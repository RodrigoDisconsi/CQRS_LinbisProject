using CQRSLinbis.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CQRSLinbis.Application.Projects.Events
{
    public class AddDeveloperToProjectEventHandler : INotificationHandler<AddDeveloperToProjectEvent>
    {
        private readonly ILogger<AddDeveloperToProjectEventHandler> _logger;

        public AddDeveloperToProjectEventHandler(ILogger<AddDeveloperToProjectEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(AddDeveloperToProjectEvent notification, CancellationToken cancellationToken)
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
