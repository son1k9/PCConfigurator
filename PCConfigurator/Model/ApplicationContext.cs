using Microsoft.EntityFrameworkCore;
using PCConfigurator.Model.Components;

namespace PCConfigurator.Model
{
    internal class ApplicationContext : DbContext
    {
        private readonly string _dbPath;

        public DbSet<Socket> Sockets { get; set; }
        public DbSet<Chipset> Chipsets { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<M2Slot> M2Slots { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Cpu> Cpus { get; set; }
        public DbSet<PowerSupply> PowerSupplys { get; set; }
        public DbSet<Cooler> Coolers { get; set; }
        public DbSet<Gpu> Gpus { get; set; }
        public DbSet<Hdd> Hdds { get; set; }
        public DbSet<M2Ssd> M2Ssds { get; set; }
        public DbSet<Ssd> Ssds { get; set; }


        public ApplicationContext()
        {
            _dbPath = AppDomain.CurrentDomain.BaseDirectory;

            if (_dbPath.Contains("bin"))
            {
                int index = _dbPath.IndexOf("bin");
                _dbPath = _dbPath[..index];
            }

            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Socket>()
                .HasMany(e => e.Coolers)
                .WithMany(e => e.Sockets);

            modelBuilder.Entity<Configuration>()
                .HasMany(e => e.Rams)
                .WithMany(e => e.Configurations)
                .UsingEntity(j =>
                    {
                        j.IndexerProperty<int>("ConfigurationRamId");
                        j.HasKey("ConfigurationRamId");
                    });

            modelBuilder.Entity<Configuration>()
                .HasMany(e => e.Gpus)
                .WithMany(e => e.Configurations)
                .UsingEntity(j =>
                {
                    j.IndexerProperty<int>("ConfigurationGpuId");
                    j.HasKey("ConfigurationGpuId");
                });

            modelBuilder.Entity<Configuration>()
                .HasMany(e => e.Ssds)
                .WithMany(e => e.Configurations)
                .UsingEntity(j =>
                {
                    j.IndexerProperty<int>("ConfigurationSsdId");
                    j.HasKey("ConfigurationSsdId");
                });

            modelBuilder.Entity<Configuration>()
                .HasMany(e => e.Hdds)
                .WithMany(e => e.Configurations)
                .UsingEntity(j =>
                {
                    j.IndexerProperty<int>("ConfigurationHddId");
                    j.HasKey("ConfigurationHddId");
                });

            modelBuilder.Entity<Configuration>()
                .HasMany(e => e.M2Ssds)
                .WithMany(e => e.Configurations)
                .UsingEntity(j =>
                {
                    j.IndexerProperty<int>("ConfigurationM2SsdId");
                    j.HasKey("ConfigurationM2SsdId");
                    j.Property<int>("SlotNumber");
                });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}Model\\Database\\components.db");
        }
    }
}