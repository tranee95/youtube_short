using Common.Models.Bloger;
using Common.Models.Favorite;
using Common.Models.Logging;
using Common.Models.Themes;
using Common.Models.User;
using Common.Models.Video;
using Common.SqlVIewModel;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace DataContext
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly bool _isTest;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, bool isTest = false)
            : base(options)
        {
            _isTest = isTest;

            if (isTest) return;

            var npgsqlOptions = options.FindExtension<NpgsqlOptionsExtension>();

            if (npgsqlOptions != null)
            {
                _connectionString = npgsqlOptions.ConnectionString;
            }
        }

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (_isTest)
            {
                modelBuilder
                    .Entity<FilterVideoModel>()
                    .Ignore(s => s.ThemesId);

                modelBuilder
                    .Entity<FilterVideoModel>()
                    .Ignore(s => s.BlogersId);

                modelBuilder
                    .Entity<Video>()
                    .Ignore(s => s.ThemesId);

                modelBuilder
                    .Entity<Favorite>()
                    .Ignore(s => s.VideosId);
            }

            if (!_isTest)
            {
                modelBuilder
                    .Entity<Video>()
                    .HasIndex(s => new {s.ThemesId, s.BlogerId, s.Name});

                modelBuilder
                    .Entity<Themes>()
                    .HasIndex(s => s.Name);

                modelBuilder
                    .Entity<Likes>()
                    .HasIndex(s => new {s.VideoId, s.UserId});

                modelBuilder
                    .Entity<Dislikes>()
                    .HasIndex(s => new {s.VideoId, s.UserId});
            }

            modelBuilder
                .Entity<Favorite>()
                .HasIndex(f => f.Guid)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }

        public DbSet<EventLog> EventLog { get; set; }
        public DbSet<UserData> Users { get; set; }
        public DbSet<Bloger> Blogers { get; set; }
        public DbSet<Themes> Themes { get; set; }
        public DbSet<UserThemes> UserThemes { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<UserBloger> UserBloger { get; set; }
        public DbSet<FilterVideoModel> UserFilterVideos { get; set; }
        public DbSet<DefaultVideoView> DefaultVideoView { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Dislikes> Dislikes { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
    }
}