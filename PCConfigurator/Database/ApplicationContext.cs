using Microsoft.EntityFrameworkCore;
using PCConfigurator.Model.Components;
using System.Diagnostics;

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

    public DbSet<ConfigurationGpu> ConfigurationGpu { get; set; }
    public DbSet<ConfigurationHdd> ConfigurationHdd { get; set; }
    public DbSet<ConfigurationM2Ssd> ConfigurationM2Ssd { get; set; }
    public DbSet<ConfigurationRam> ConfigurationRam { get; set; }
    public DbSet<ConfigurationSsd> ConfigurationSsd { get; set; }


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
            .UsingEntity<ConfigurationRam>();

        modelBuilder.Entity<Configuration>()
            .HasMany(e => e.Gpus)
            .WithMany(e => e.Configurations)
            .UsingEntity<ConfigurationGpu>();

        modelBuilder.Entity<Configuration>()
            .HasMany(e => e.Ssds)
            .WithMany(e => e.Configurations)
            .UsingEntity<ConfigurationSsd>();

        modelBuilder.Entity<Configuration>()
            .HasMany(e => e.Hdds)
            .WithMany(e => e.Configurations)
            .UsingEntity<ConfigurationHdd>();

        modelBuilder.Entity<ConfigurationM2Ssd>()
            .HasKey(e => new { e.M2SlotId, e.ConfigurationId });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies().UseSqlite($"Filename={_dbPath}Database\\components.db").EnableSensitiveDataLogging(true).LogTo(message => Debug.WriteLine(message));
    }
}