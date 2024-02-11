using CQRSLinbis.Application.Common.Mappings;
using CQRSLinbis.Domain.Attributes;
using CQRSLinbis.Domain.Queries;

namespace CQRSLinbis.Application.Projects.Queries.Response
{
    public class ProjectDto : IMapFrom<ProjectView>
    {
        public int ProjectId { get; set; }
        [Buscador]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int EffortRequiredInDays { get; set; }
        public IEnumerable<DeveloperDto> Developers { get; set; }
    }

    public class DeveloperDto : IMapFrom<DeveloperView>
    {
        public int DeveloperId { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int CostByDay { get; set; }
        public DateTimeOffset AddedDate { get; set; }
    }

}