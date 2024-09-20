using Microsoft.EntityFrameworkCore;
using WebApplication_Clients_Library.Models;

namespace WebApplication_Clients_Library.Data;

public class ClientContextDB(DbContextOptions<ClientContextDB> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
}
