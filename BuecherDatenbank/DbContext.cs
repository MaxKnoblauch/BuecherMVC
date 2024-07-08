using Microsoft.EntityFrameworkCore;  
namespace BuecherDatenbank
{
    
    public class BuecherDbContext : DbContext
    {
        
        public DbSet<AktuellesBuch> AktuelleBuecher { get; set; }

        
        public DbSet<ArchiviertesBuch> ArchivierteBuecher { get; set; }

       
        public BuecherDbContext(DbContextOptions<BuecherDbContext> options)
            : base(options)
        {
        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<AktuellesBuch>(entity =>
            {
                // Mapping zur Tabelle "aktuelle_buecher"
                entity.ToTable("aktuelle_buecher");

                // Primärschlüssel auf die Eigenschaft Id setzen
                entity.HasKey(e => e.Id);

                // Die Id wird nicht automatisch generiert
                entity.Property(e => e.Id).ValueGeneratedNever();

                // Das Titel-Feld ist erforderlich und hat maximal 255 Zeichen
                entity.Property(e => e.Titel).IsRequired().HasMaxLength(255);

                // Das Autor-Feld ist erforderlich und hat maximal 255 Zeichen
                entity.Property(e => e.Autor).IsRequired().HasMaxLength(255);
            });

            // Konfiguration für die Entität ArchiviertesBuch
            modelBuilder.Entity<ArchiviertesBuch>(entity =>
            {
                // Mapping zur Tabelle "archivierte_buecher"
                entity.ToTable("archivierte_buecher");

                // Primärschlüssel auf die Eigenschaft Id setzen
                entity.HasKey(e => e.Id);

                // Die Id wird nicht automatisch generiert
                entity.Property(e => e.Id).ValueGeneratedNever();

                // Das Titel-Feld ist erforderlich und hat maximal 255 Zeichen
                entity.Property(e => e.Titel).IsRequired().HasMaxLength(255);

                // Das Autor-Feld ist erforderlich und hat maximal 255 Zeichen
                entity.Property(e => e.Autor).IsRequired().HasMaxLength(255);
            });
        }
    }

    
    public class AktuellesBuch
    {
        public int Id { get; set; }        // Id des Buches
        public string? Titel { get; set; } // Titel des Buches
        public string? Autor { get; set; } // Autor des Buches
    }

    
    public class ArchiviertesBuch
    {
        public int Id { get; set; }        
        public string? Titel { get; set; } 
        public string? Autor { get; set; } 
    }
}
