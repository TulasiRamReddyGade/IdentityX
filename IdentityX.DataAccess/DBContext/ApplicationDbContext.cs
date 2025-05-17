using EntityFramework.Exceptions.SqlServer;
using IdentityX.DataAccess.Entities;
using IdentityX.DataAccess.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IdentityX.DataAccess.DBContext;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole, Guid>
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    public DbSet<ApplicationEntity> Applications { get; set; }
    public DbSet<ApplicationRoleEntity> ApplicationRoles { get; set; }
    public DbSet<ApplicationUserRelationsEntity> ApplicationUserRelations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
       
        // Application Entity
        modelBuilder.Entity<ApplicationEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<ApplicationEntity>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<ApplicationEntity>().HasIndex(x => x.NormalizedName).IsUnique();
        modelBuilder.Entity<ApplicationEntity>().Property(x => x.Active).HasDefaultValue(true);
        modelBuilder.Entity<ApplicationEntity>().HasMany(x => x.ApplicationRoles).WithOne(x => x.Application)
            .HasForeignKey(x => x.ApplicationId).HasPrincipalKey(x => x.Id);
        modelBuilder.Entity<ApplicationEntity>().HasMany(x => x.ApplicationUserRelations).WithOne(x => x.Application).HasForeignKey(x => x.ApplicationId).HasPrincipalKey(x => x.Id);
        
        // Application User
        modelBuilder.Entity<ApplicationUser>().HasMany(x => x.ApplicationUserRelations).WithOne(x => x.ApplicationUser)
            .HasForeignKey(x => x.ApplicationUserId).OnDelete(DeleteBehavior.Restrict).HasPrincipalKey(x => x.Id);
        
        // Application Role
        modelBuilder.Entity<ApplicationRole>().HasMany(x => x.ApplicationUserRelations).WithOne(x => x.ApplicationRole)
            .HasForeignKey(x => x.ApplicationRoleId).OnDelete(DeleteBehavior.Restrict).HasPrincipalKey(x => x.Id);
        modelBuilder.Entity<ApplicationRole>().Property(x=>x.Active).HasDefaultValue(true);
        
        
        // Application User Relations
        modelBuilder.Entity<ApplicationUserRelationsEntity>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<ApplicationUserRelationsEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<ApplicationUserRelationsEntity>()
            .HasIndex(x => new { x.ApplicationUserId, x.ApplicationRoleId })
            .IsUnique();
        modelBuilder.Entity<ApplicationUserRelationsEntity>().Property(x => x.Active).HasDefaultValue(true);
        
        // ApplicationRoleEntity
        modelBuilder.Entity<ApplicationRoleEntity>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<ApplicationRoleEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<ApplicationRoleEntity>().Property(x => x.Active).HasDefaultValue(true);
        modelBuilder.Entity<ApplicationRoleEntity>().HasIndex(x => new {x.ApplicationId,x.NormalizedRoleName}).IsUnique();
        
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }
    private void SetTimestamps()
    {
        var applicationEntries = ChangeTracker.Entries<ApplicationEntity>();
        var ApplicationRoleEntries = ChangeTracker.Entries<ApplicationRoleEntity>();

        foreach (var entry in applicationEntries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedOn = DateTime.UtcNow;
                entry.Entity.UpdatedOn = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedOn = DateTime.UtcNow;
            }
        }
        
        foreach (var entry in ApplicationRoleEntries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedOn = DateTime.UtcNow;
                entry.Entity.UpdatedOn = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedOn = DateTime.UtcNow;
            }
        }
    }
    
    

}