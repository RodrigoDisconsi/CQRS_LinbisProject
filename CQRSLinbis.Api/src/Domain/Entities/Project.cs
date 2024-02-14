using CQRSLinbis.Domain.Attributes;
using CQRSLinbis.Domain.Common;

namespace CQRSLinbis.Domain.Entities
{
    public class Project : HasDomainEvent, IEntity
    {
        public int Id { get; set; }
        [Searchable]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int EffortRequiredInDays { get; set; }
        public DateTimeOffset AddedDate { get; set; }
        public virtual ICollection<Developer> Developers { get; set; }
    }
}
