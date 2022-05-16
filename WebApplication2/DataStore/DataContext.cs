using Microsoft.EntityFrameworkCore;

namespace WebApplication2.DataStore
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions op) : base(op)
        {

        }
    }
}
