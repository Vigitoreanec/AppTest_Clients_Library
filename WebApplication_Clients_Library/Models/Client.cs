using System.ComponentModel.DataAnnotations;

namespace WebApplication_Clients_Library.Models;

public class Client
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? SurName { get; set; }
    [DataType(DataType.Date)]
    public required DateTime BithDate { get; set; }
    public string? Gender { get; set; }
    public string? Email { get; set; }
    public string? TelNumber { get; set; }
}
