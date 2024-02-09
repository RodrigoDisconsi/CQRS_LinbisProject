using CRUDCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDCleanArchitecture.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Project> Projects { get; set; }
    DbSet<Developer> Developers { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
