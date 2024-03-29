﻿using Microsoft.EntityFrameworkCore;
using CQRSLinbis.Application.Common.Interfaces;
using System.Reflection;
using MediatR;
using CQRSLinbis.Domain.Entities;

namespace CQRSLinbis.Infrastructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;

    public ApplicationDbContext(
        DbContextOptions options,
        IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Developer> Developers { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        await _mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<Project>().HasData(
            new Project() { Id = 1, Name = "Project 1", IsActive = true, EffortRequiredInDays = 30, AddedDate = DateTimeOffset.UtcNow },
            new Project() { Id = 2, Name = "Project 2", IsActive = true, EffortRequiredInDays = 60, AddedDate = DateTimeOffset.UtcNow }
            );
        builder.Entity<Developer>().HasData(
            new Developer() { Id = 1, Name = "Developer 1", CostByDay = 60, ProjectId = 1, AddedDate = DateTimeOffset.UtcNow },
            new Developer() { Id = 2, Name = "Developer 2", CostByDay = 30, ProjectId = 1, AddedDate = DateTimeOffset.UtcNow }
            );

        base.OnModelCreating(builder);
    }
}
