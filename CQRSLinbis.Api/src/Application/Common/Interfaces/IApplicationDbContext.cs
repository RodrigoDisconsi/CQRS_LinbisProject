using CQRSLinbis.Application.Common.Models;
using CQRSLinbis.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRSLinbis.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Project> Projects { get; set; }
    DbSet<Developer> Developers { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
