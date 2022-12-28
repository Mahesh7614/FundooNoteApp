
using Microsoft.Extensions.Configuration;

namespace FundooRepository.Repository
{
    public class NotesRepository
    {
        string connectionString;

        public NotesRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
        }
    }
}
