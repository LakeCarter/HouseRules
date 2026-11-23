using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HouseRules.Models;
using Microsoft.AspNetCore.Identity;

namespace HouseRules.Data;


public class HouseRulesDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Chore> Chores { get; set; }
    public DbSet<ChoreAssignment> ChoreAssignments { get; set; }
    public DbSet<ChoreCompletion> ChoreCompletions { get; set; }
    public HouseRulesDbContext(DbContextOptions<HouseRulesDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // The seeded admin IdentityUser's PasswordHash is computed from the
        // student's own AdminPassword secret using PasswordHasher, which salts
        // randomly on every call. That makes the computed model "change" on
        // every design-time build even though nothing about it actually needs
        // fixing - tell EF Core this particular drift is expected. (The seeded
        // role's and user's ConcurrencyStamp/SecurityStamp would also change on
        // every build, since their constructors generate new Guids, so those are
        // given fixed values in OnModelCreating below. The PasswordHash is the
        // only seeded value that still changes, and it will show up as an
        // UpdateData in every new migration.)
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            Name = "Admin",
            NormalizedName = "admin",
            ConcurrencyStamp = "a6f8b1d2-6c3e-4f7a-9b2d-1e5c8a4f7b90"
        });

        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
        {
            Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
            UserName = "Administrator",
            Email = "admina@strator.comx",
            ConcurrencyStamp = "3d9e7c41-8b2a-4f6d-a1e5-7c0b9f2d4e68",
            SecurityStamp = "e2b4f6a8-1c3d-4e5f-8a9b-0c1d2e3f4a5b",
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
        });
        modelBuilder.Entity<UserProfile>().HasData(new UserProfile
        {
            Id = 1,
            IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
            FirstName = "Admina",
            LastName = "Strator",
            Address = "101 Main Street",
        });


        modelBuilder.Entity<Chore>().HasData(new Chore[]
        {
            new Chore {Id = 1, Name = "Wash the Dishes", ChoreFrequencyDays = 1, Difficulty = 2},
            new Chore {Id = 2, Name = "Vacuum the Floors", ChoreFrequencyDays = 7, Difficulty = 3},
            new Chore {Id = 3, Name = "Clean the Bathroom", ChoreFrequencyDays = 7, Difficulty = 4},
            new Chore {Id = 4, Name = "Take Out the Trash", ChoreFrequencyDays = 3, Difficulty = 1},
            new Chore {Id = 5, Name = "Mow the Lawn", ChoreFrequencyDays = 14, Difficulty = 4},
        });

        modelBuilder.Entity<ChoreAssignment>().HasData(new ChoreAssignment[]
        {
           new ChoreAssignment {Id =1, UserProfileId = 1, ChoreId=2},
           new ChoreAssignment {Id =2, UserProfileId=1, ChoreId =5}
        });

        modelBuilder.Entity<ChoreCompletion>().HasData(new ChoreCompletion[]
        {
           new ChoreCompletion {Id = 1, UserProfileId = 1, ChoreId=1,CompletedOn=new DateTime (2026,2,3)}
        });
    }
}