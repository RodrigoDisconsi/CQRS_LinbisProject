using AutoMapper;
using CQRSLinbis.Application.Common.Mappings;
using CQRSLinbis.Domain.Queries;

namespace CQRSLinbis.Application.Projects.Queries.Models
{
    public class ProjectDto : IMapFrom<ProjectView>
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int EffortRequiredInDays { get; set; }
        public int DevelopmentCost { get; set; }
        public long AddedDate { get; set; }
        public IEnumerable<DeveloperDto> Developers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProjectView, ProjectDto>()
                   .ForMember(dst => dst.DevelopmentCost, 
                              opt => opt.MapFrom(x => x.Developers.Sum(d => d.CostByDay) * x.EffortRequiredInDays))
                   .ForMember(dst => dst.AddedDate,
                              opt => opt.MapFrom(x => x.AddedDate.ToUnixTimeMilliseconds()));
        }
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