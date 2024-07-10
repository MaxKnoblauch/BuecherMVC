using Microsoft.EntityFrameworkCore;  // Namespace für Entity Framework Core, um mit der Datenbank zu interagieren
using System.Collections.Generic;  // Namespace für generische Typen wie List<T>
using System.Linq;  // Namespace für LINQ-Abfragen
using System.Threading.Tasks;  // Namespace für asynchrone Task-Unterstützung

namespace BuecherDatenbank
{
    public class BuecherRepository
    {
        private readonly BuecherDBContext _context;

        public BuecherRepository(BuecherDBContext context)
        {
            _context = context;
        }

        // Lädt alle aktuellen Bücher aus der Datenbank
        public async Task<List<AktuellesBuch>> HoleAlleAktuellenBuecherAsync()
        {
            return await _context.AktuelleBuecher.ToListAsync();
        }

        // Lädt alle archivierten Bücher aus der Datenbank
        public async Task<List<ArchiviertesBuch>> HoleAlleArchiviertenBuecherAsync()
        {
            return await _context.ArchivierteBuecher.ToListAsync();
        }

        // Verschiebt ein Buch von AktuelleBuecher zu ArchivierteBuecher
        public async Task VerschiebeZuArchiviertenBuechern(int id)
        {
            // Suche das Buch in der Tabelle AktuelleBuecher anhand der ID
            var aktuellesBuch = await _context.AktuelleBuecher.FindAsync(id);
            if (aktuellesBuch != null)
            {
                // Entferne das Buch aus AktuelleBuecher
                _context.AktuelleBuecher.Remove(aktuellesBuch);

                // Erstelle ein neues archiviertes Buch mit der nächsten verfügbaren ID
                var archiviertesBuch = new ArchiviertesBuch
                {
                    Id = await GetNextId(_context.ArchivierteBuecher),
                    Titel = aktuellesBuch.Titel,
                    Autor = aktuellesBuch.Autor
                };

                // Füge das archivierte Buch zu ArchivierteBuecher hinzu und speichere die Änderungen
                _context.ArchivierteBuecher.Add(archiviertesBuch);
                await _context.SaveChangesAsync();
            }
        }

        // Verschiebt ein Buch von ArchivierteBuecher zu AktuelleBuecher
        public async Task VerschiebeZuAktuellenBuechern(int id)
        {
            // Suche das Buch in der Tabelle ArchivierteBuecher anhand der ID
            var archiviertesBuch = await _context.ArchivierteBuecher.FindAsync(id);
            if (archiviertesBuch != null)
            {
                // Entferne das Buch aus ArchivierteBuecher
                _context.ArchivierteBuecher.Remove(archiviertesBuch);

                // Erstelle ein neues aktuelles Buch mit der nächsten verfügbaren ID
                var aktuellesBuch = new AktuellesBuch
                {
                    Id = await GetNextId(_context.AktuelleBuecher),
                    Titel = archiviertesBuch.Titel,
                    Autor = archiviertesBuch.Autor
                };

                // Füge das aktuelle Buch zu AktuelleBuecher hinzu und speichere die Änderungen
                _context.AktuelleBuecher.Add(aktuellesBuch);
                await _context.SaveChangesAsync();
            }
        }

        // Ermittelt die nächste verfügbare ID für ein DbSet<T>
        private async Task<int> GetNextId<T>(DbSet<T> dbSet) where T : class
        {
            // Ermittle die maximale ID in der Tabelle
            var maxId = await dbSet.AnyAsync() ? await dbSet.MaxAsync(b => EF.Property<int>(b, "Id")) : 0;

            // Ermittle die nächste freie ID, beginnend bei 1 bis zur maximalen ID + 1, die nicht vergeben ist
            var nextId = Enumerable.Range(1, maxId + 1)
                                   .Except(await dbSet.Select(b => EF.Property<int>(b, "Id")).ToListAsync())
                                   .FirstOrDefault();

            return nextId;
        }
    }
}
