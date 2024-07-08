using Microsoft.Extensions.Configuration;


namespace BuecherMVC
{
    public class KonfigurationsLeser : IKonfigurationsLeser
    {
        private readonly IConfiguration _configuration;

        public KonfigurationsLeser(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string LiesDatenbankVerbindungZurMariaDB()
        {
            var connectionString = _configuration.GetConnectionString("MariaDB");
            if (connectionString == null)
            {
                throw new InvalidOperationException("Die Verbindungszeichenfolge für 'MariaDB' ist nicht konfiguriert.");
            }
            return connectionString;
        }
    }
}

