// Einbindung der notwendigen Namespaces
using System.Collections.Generic;
using BuecherDatenbank;

namespace BuecherMVC.Models
{
    // Definition der BuecherListeModel-Klasse, die zwei Listen von Büchern (aktuelle und archivierte) verwaltet
    public class BuecherListeModel
    {
        // Konstruktor der Klasse, der die Listen der aktuellen und archivierten Bücher initialisiert
        public BuecherListeModel(IEnumerable<AktuellesBuch> aktuelleBuecher, IEnumerable<ArchiviertesBuch> archivierteBuecher)
        {
            // Initialisierung der Liste der aktuellen Bücher mit den übergebenen Daten
            AktuelleBuecher = new List<AktuellesBuch>(aktuelleBuecher);

            // Initialisierung der Liste der archivierten Bücher mit den übergebenen Daten
            ArchivierteBuecher = new List<ArchiviertesBuch>(archivierteBuecher);
        }

        // Eigenschaft für die Liste der aktuellen Bücher
        public List<AktuellesBuch> AktuelleBuecher { get; set; }

        // Eigenschaft für die Liste der archivierten Bücher
        public List<ArchiviertesBuch> ArchivierteBuecher { get; set; }
    }
}
