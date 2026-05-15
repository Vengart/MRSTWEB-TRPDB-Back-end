using Microsoft.EntityFrameworkCore;
using Orichalcum.Domains;
using Orichalcum.Domains.Entities.Application;
using Orichalcum.Domains.Entities.GameCard;
using Orichalcum.Domains.Entities.GameCard;
using Orichalcum.Domains.Entities.GameNote;
using Orichalcum.Domains.Entities.GameSession;
using Orichalcum.Domains.Entities.User;


namespace Orichalcum.DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UserData> Users { get; set; }
        public DbSet<GameSessionData> GameSessions { get; set; }
        public DbSet<ApplicationData> Applications { get; set; }
        public DbSet<GameCardData> GameCards { get; set; }
        public DbSet<GameNoteData> GameNotes { get; set; }

        public DbSet<Availability> Availabilities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSession.ConnectionStrings);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>().ToTable("Users");
            modelBuilder.Entity<GameSessionData>().ToTable("GameSessions");
            modelBuilder.Entity<ApplicationData>().ToTable("Applications");
            modelBuilder.Entity<GameCardData>().ToTable("GameCards");
            modelBuilder.Entity<GameNoteData>().ToTable("GameNotes");

            modelBuilder.Entity<GameSessionData>()
                .HasOne(s => s.GameMaster)
                .WithMany(u => u.GameSessions)
                .HasForeignKey(s => s.GameMasterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ApplicationData>()
                .HasOne(a => a.GameSession)
                .WithMany(s => s.Applications)
                .HasForeignKey(a => a.GameSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationData>()
                .HasOne(a => a.Player)
                .WithMany(u => u.Applications)
                .HasForeignKey(a => a.PlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GameCardData>()
                .HasOne(c => c.Owner)
                .WithMany(u => u.GameCards)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GameNoteData>()
                .HasOne(n => n.GameCard)
                .WithMany(c => c.Notes)
                .HasForeignKey(n => n.GameCardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameNoteData>()
                .HasOne(n => n.Author)
                .WithMany()
                .HasForeignKey(n => n.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}