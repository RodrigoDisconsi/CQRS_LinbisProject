using CRUDCleanArchitecture.Domain.Common;

namespace CRUDCleanArchitecture.Domain.Entities
{
    public class Developer : HasDomainEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int CostByDay { get; set; }
        public virtual Project Project { get; set; }
        public DateTimeOffset AddedDate { get; set; }
    }
}
