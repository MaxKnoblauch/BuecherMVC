using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BuecherDatenbank
{
    public class BuecherRepository
    {
        private readonly BuecherDbContext _context;

        public BuecherRepository(BuecherDbContext context)
        {
            _context = context;
        }

        public async Task<List<AktuellesBuch>> HoleAlleAktuellenBuecherAsync()
        {
            return await _context.AktuelleBuecher.ToListAsync();
        }

        public async Task<List<ArchiviertesBuch>> HoleAlleArchiviertenBuecherAsync()
        {
            return await _context.ArchivierteBuecher.ToListAsync();
        }

        public async Task VerschiebeZuArchiviertenBuechern(int id)
        {
            var aktuellesBuch = await _context.AktuelleBuecher.FindAsync(id);
            if (aktuellesBuch != null)
            {
                _context.AktuelleBuecher.Remove(aktuellesBuch);

                var archiviertesBuch = new ArchiviertesBuch
                {
                    Id = await GetNextId(_context.ArchivierteBuecher),
                    Titel = aktuellesBuch.Titel,
                    Autor = aktuellesBuch.Autor
                };

                _context.ArchivierteBuecher.Add(archiviertesBuch);
                await _context.SaveChangesAsync();
            }
        }

        public async Task VerschiebeZuAktuellenBuechern(int id)
        {
            var archiviertesBuch = await _context.ArchivierteBuecher.FindAsync(id);
            if (archiviertesBuch != null)
            {
                _context.ArchivierteBuecher.Remove(archiviertesBuch);

                var aktuellesBuch = new AktuellesBuch
                {
                    Id = await GetNextId(_context.AktuelleBuecher),
                    Titel = archiviertesBuch.Titel,
                    Autor = archiviertesBuch.Autor
                };

                _context.AktuelleBuecher.Add(aktuellesBuch);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<int> GetNextId<T>(DbSet<T> dbSet) where T : class
        {
            var maxId = await dbSet.AnyAsync() ? await dbSet.MaxAsync(b => EF.Property<int>(b, "Id")) : 0;
            var nextId = Enumerable.Range(1, maxId + 1).Except(await dbSet.Select(b => EF.Property<int>(b, "Id")).ToListAsync()).FirstOrDefault();
            return nextId;
        }

    }
}