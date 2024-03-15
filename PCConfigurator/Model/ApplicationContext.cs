using Microsoft.EntityFrameworkCore;
using PCConfigurator.Model.Components;

namespace PCConfigurator.Model;

internal class ApplicationContext : DbContext
{
    private readonly string _dbPath;

    public DbSet<Socket> Socket { get; set; }
    public DbSet<Chipset> Chipset { get; set; }
    public DbSet<Ram> Ram { get; set; }
    public DbSet<M2Slot> M2Slot { get; set; }
    public DbSet<Motherboard> Motherboard { get; set; }
    public DbSet<Configuration> Configuration { get; set; }
    public DbSet<Cpu> Cpu { get; set; }
    public DbSet<PowerSupply> PowerSupply { get; set; }
    public DbSet<Cooler> Cooler { get; set; }
    public DbSet<Gpu> Gpu { get; set; }
    public DbSet<Hdd> Hdd { get; set; }
    public DbSet<M2Ssd> M2Ssd { get; set; }
    public DbSet<Ssd> Ssd { get; set; }


    public ApplicationContext()
    {
        _dbPath = AppDomain.CurrentDomain.BaseDirectory;

        if (_dbPath.Contains("bin"))
        {
            int index = _dbPath.IndexOf("bin");
            _dbPath = _dbPath[..index];
        }
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
        optionsBuilder.UseLazyLoadingProxies().UseSqlite($"Filename={_dbPath}Model\\Database\\components.db");
    }
}