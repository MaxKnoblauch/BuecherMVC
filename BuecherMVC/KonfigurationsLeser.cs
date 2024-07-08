using Microsoft.Extensions.Configuration; // Ermöglicht den Zugriff auf Konfigurationsdaten in .NET-Anwendungen

namespace BuecherMVC
{
    // Implementiert die Schnittstelle IKonfigurationsLeser
    public class KonfigurationsLeser : IKonfigurationsLeser
    {
        private readonly IConfiguration _configuration;

        // Konstruktor, der IConfiguration als Parameter erwartet
        public KonfigurationsLeser(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Methode zum Lesen der Datenbankverbindung zur MariaDB aus der Konfiguration
        public string LiesDatenbankVerbindungZurMariaDB()
        {
            // Holt die Verbindungszeichenfolge "MariaDB" aus der IConfiguration
            var connectionString = _configuration.GetConnectionString("MariaDB");

            // Überprüft, ob die Verbindungszeichenfolge gefunden wurde
            if (connectionString == null)
            {
                // Falls nicht, wird eine InvalidOperationException ausgelöst
                throw new InvalidOperationException("Die Verbindungszeichenfolge für 'MariaDB' ist nicht konfiguriert.");
            }

            // Gibt die gelesene Verbindungszeichenfolge zurück
            return connectionString;
        }
    }
}
