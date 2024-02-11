using CQRSLinbis.Domain.Attributes;

namespace CQRSLinbis.Domain.Queries
{
    public class ProjectView
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int EffortRequiredInDays { get; set; }
        public DateTimeOffset AddedDate { get; set; }
        public List<DeveloperView> Developers { get; set; }
    }
}
