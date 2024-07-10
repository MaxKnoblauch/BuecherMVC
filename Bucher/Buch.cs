namespace Bucher // Definition des Namensraums Bucher
{
    // Definition der Buch-Klasse, die ein Buch repräsentiert
    public class Buch
    {
        // Eigenschaft für die eindeutige ID des Buches
        public int Id { get; set; }

        // Eigenschaft für den Titel des Buches, kann null sein
        public string? Titel { get; set; }

        // Eigenschaft für den Autor des Buches, kann null sein
        public string? Autor { get; set; }
    }
}
