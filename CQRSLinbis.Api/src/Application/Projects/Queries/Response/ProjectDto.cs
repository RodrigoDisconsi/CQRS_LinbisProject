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
        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<PermissionView, PermissionDto>()
        //           .ForMember(dst => dst.TipoPermiso, opt => opt.MapFrom(x => new PermissionTypeDto { TipoPermisoId = x.TipoPermisoId, Descripcion = x.Descripcion }));

        //    profile.CreateMap<PermissionDto, Permiso>()
        //            .ForMember(dst => dst.Id, opt => opt.MapFrom(x => x.PermisoId))
        //            .ForMember(dst => dst.TipoPermisoId, opt => opt.MapFrom(x => x.TipoPermiso.TipoPermisoId))
        //            .ForMember(dst => dst.TipoPermiso, opt => opt.MapFrom(x => new TipoPermiso { Id = x.TipoPermiso.TipoPermisoId, Descripcion = x.TipoPermiso.Descripcion }));
        //}
    }

    //public class PermissionTypeDto
    //{
    //    public int TipoPermisoId { get; set; }
    //    public string Descripcion { get; set; }
    //}

}