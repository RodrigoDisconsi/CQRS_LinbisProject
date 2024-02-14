using CQRSLinbis.Domain.Attributes;
using CQRSLinbis.Domain.Common;

namespace CQRSLinbis.Domain.Entities
{
    public class Developer : HasDomainEvent, IEntity
    {
        public int Id { get; set; }
        [Buscador]
        public string Name { get; set; }
        public int? ProjectId { get; set; }
        public int CostByDay { get; set; }
        public DateTimeOffset AddedDate { get; set; }
        public virtual Project? Project { get; set; }
    }
}
