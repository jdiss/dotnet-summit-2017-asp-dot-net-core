using Microsoft.EntityFrameworkCore;

namespace SampleApplication.Services
{
    public class DatabaseInitializer
    {
        private readonly SampleApplicationDbContext _dbContext;

        public DatabaseInitializer(SampleApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Migrate()
        {
            _dbContext.Database.Migrate();
        }
    }
}