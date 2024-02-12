using AutoMapper;
using CQRSLinbis.Application.Common.Mappings;
using CQRSLinbis.Application.Projects.Queries.Models;
using CQRSLinbis.Domain.Queries;

namespace CQRSLinbis.Application.Developers.Queries.Models
{
    public class DeveloperDto : IMapFrom<DeveloperView>
    {
        public int DeveloperId { get; set; }
        public string Name { get; set; }
        public int? ProjectId { get; set; }
        public int CostByDay { get; set; }
        public long AddedDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeveloperView, DeveloperDto>()
                   .ForMember(dst => dst.AddedDate,
                              opt => opt.MapFrom(x => x.AddedDate.ToUnixTimeMilliseconds()));
        }
    }
}
