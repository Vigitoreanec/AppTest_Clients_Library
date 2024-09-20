using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication_Clients_Library.Controllers;
using WebApplication_Clients_Library.Models;

namespace WebApplication_Clients_Library.ViewModel;

public class ClientFilterVM
{
    public Filter SelectFilter { get; set; }
    public required SelectList Filter { get; set; }
    public required SelectList DateFilter { get; set; }
    public int? DateBithDate { get; set; }
    public string? NameClient { get; set; }
    public string? Gender { get; set; }
    public required List<Client> Clients { get; set; }
    public required SelectList Genders { get; set; }
}
