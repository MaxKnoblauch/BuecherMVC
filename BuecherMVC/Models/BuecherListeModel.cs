using System.Collections.Generic;
using BuecherDatenbank;

namespace BuecherMVC.Models
{
    public class BuecherListeModel
    {
        public BuecherListeModel(IEnumerable<AktuellesBuch> aktuelleBuecher, IEnumerable<ArchiviertesBuch> archivierteBuecher)
        {
            AktuelleBuecher = new List<AktuellesBuch>(aktuelleBuecher);
            ArchivierteBuecher = new List<ArchiviertesBuch>(archivierteBuecher);
        }

        public List<AktuellesBuch> AktuelleBuecher { get; set; }
        public List<ArchiviertesBuch> ArchivierteBuecher { get; set; }
    }
}
