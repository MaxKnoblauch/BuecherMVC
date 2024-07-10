using Microsoft.EntityFrameworkCore;  // Einbindung des Entity Framework Core
namespace BuecherDatenbank
{
    // Definition des BuecherDBContext, der die Datenbankverbindung und -konfiguration verwaltet
    public class BuecherDBContext : DbContext
    {
        // Definition der DbSet für aktuelle Bücher
        public DbSet<AktuellesBuch> AktuelleBuecher { get; set; }

        // Definition der DbSet für archivierte Bücher
        public DbSet<ArchiviertesBuch> ArchivierteBuecher { get; set; }

        // Konstruktor, der die Optionen an die Basisklasse weitergibt
        public BuecherDBContext(DbContextOptions<BuecherDBContext> options)
            : base(options)
        {
        }

        // Konfiguration des Modellaufbaus
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguration für die Entität AktuellesBuch
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

    // Klasse für aktuelle Bücher
    public class AktuellesBuch
    {
        public int Id { get; set; }        // Id des Buches
        public string? Titel { get; set; } // Titel des Buches
        public string? Autor { get; set; } // Autor des Buches
    }

    // Klasse für archivierte Bücher
    public class ArchiviertesBuch
    {
        public int Id { get; set; }        // Id des Buches
        public string? Titel { get; set; } // Titel des Buches
        public string? Autor { get; set; } // Autor des Buches
    }
}
