// Einbindung der notwendigen Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuecherDatenbank
{
    // Definition der BuecherDTO-Klasse, die als Data Transfer Object (DTO) dient
    // Ein DTO wird verwendet, um Daten zwischen Schichten einer Anwendung zu übertragen
    public class BuecherDTO
    {
        // Eindeutige Kennung des Buches
        public int Id { get; set; }

        // Titel des Buches, kann null sein
        public string? Titel { get; set; }

        // Autor des Buches, kann null sein
        public string? Autor { get; set; }
    }
}
