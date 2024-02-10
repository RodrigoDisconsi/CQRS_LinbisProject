using CQRSLinbis.Domain.Common;

namespace CQRSLinbis.Domain.Entities
{
    public class Project : HasDomainEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int EffortRequiredInDays { get; set; }
        public DateTimeOffset AddedDate { get; set; }
        public virtual ICollection<Developer> Developers { get; set;}
    }
}
