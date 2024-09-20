using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_Clients_Library.Data;
using WebApplication_Clients_Library.Models;
using WebApplication_Clients_Library.ViewModel;

namespace WebApplication_Clients_Library.Controllers;

public enum Filter
{
    Ask,
    Desk
}

public class ClientsController : Controller
{
    private readonly ClientContextDB _context;

    public ClientsController(ClientContextDB context)
    {
        _context = context;
    }

    // GET: Clients
    public async Task<IActionResult> Index(Filter? selectFilter,int? dateBithDate, string gender,string nameClient)
    { 
        var clientGender = from g in _context.Clients
                     orderby g.Gender
                     select g.Gender;
        //var client = await _context.Clients.FirstOrDefaultAsync(x => x.Name == nameClient);
        var clients = from cl in _context.Clients
                     select cl;

        var clientBithDate = from b in _context.Clients
                           orderby b.BithDate
                           select b.BithDate;

        if (!string.IsNullOrEmpty(nameClient))
        {
            clients = clients.Where(cl => cl.Name.ToUpper().Contains(nameClient.ToUpper()) /*|| cl.SurName.ToUpper().Contains(nameClient.ToUpper())*/);

        }
         
        if(selectFilter is not null)
        {
            if(selectFilter == Filter.Ask)
            {
                clients = clients.OrderBy(cl => cl.SurName);
            }
            else
            {
                clients = clients.OrderByDescending(cl => cl.SurName);
            }
        }

        if(dateBithDate is not null)
        {
            if (dateBithDate == 1)
            {
                clients = clients.OrderBy(cl => cl.BithDate);
            }
            else
            {
                clients = clients.OrderByDescending(cl => cl.BithDate);
            }
        }

        if(!string.IsNullOrEmpty(gender))
        {
            clients = clients.Where(cl => cl.Gender == gender);
        }

        var genderVM = new ClientFilterVM
        {
            DateFilter = new SelectList(await clientBithDate.Distinct().ToListAsync()),
            Filter = new SelectList(new List<Filter> { Filter.Ask, Filter.Desk }),
            Genders = new SelectList(await clientGender.Distinct().ToListAsync()),
            Clients = await clients.ToListAsync()
        };

        return View(genderVM);
    }

    // GET: Clients/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var client = await _context.Clients
            .FirstOrDefaultAsync(m => m.Id == id);
        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    // GET: Clients/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Clients/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,SurName,BithDate,Gender,Email,TelNumber")] Client client)
    {
        if (ModelState.IsValid)
        {
            _context.Add(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(client);
    }

    // GET: Clients/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }
        return View(client);
    }

    // POST: Clients/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SurName,BithDate,Gender,Email,TelNumber")] Client client)
    {
        if (id != client.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(client);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(client.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(client);
    }

    // GET: Clients/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var client = await _context.Clients
            .FirstOrDefaultAsync(m => m.Id == id);
        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    // POST: Clients/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClientExists(int id)
    {
        return _context.Clients.Any(e => e.Id == id);
    }
}
