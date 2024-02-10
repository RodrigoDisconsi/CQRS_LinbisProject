namespace CQRSLinbis.Domain.Queries
{
    public class DeveloperView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int CostByDay { get; set; }
        public DateTimeOffset AddedDate { get; set; }
    }
}
