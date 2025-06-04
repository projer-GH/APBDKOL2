namespace WebApplication2.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _context;

    public CustomersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{customerId}/purchases")]
    public async Task<IActionResult> GetCustomerPurchases(int customerId)
    {
        var customer = await _context.Customers
            .Include(c => c.Purchases)
            .ThenInclude(p => p.AvailableProgram)
            .ThenInclude(ap => ap.WashingMachine)
            .Include(c => c.Purchases)
            .ThenInclude(p => p.AvailableProgram)
            .ThenInclude(ap => ap.Program)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);

        if (customer == null)
        {
            return NotFound();
        }

        var result = new
        {
            firstName = customer.FirstName,
            lastName = customer.LastName,
            phoneNumber = customer.PhoneNumber,
            purchases = customer.Purchases.Select(ph => new
            {
                date = ph.PurchaseDate,
                rating = ph.Rating,
                price = ph.AvailableProgram.Price,
                washingMachine = new
                {
                    serial = ph.AvailableProgram.WashingMachine.SerialNumber,
                    maxWeight = ph.AvailableProgram.WashingMachine.MaxWeight
                },
                program = new
                {
                    name = ph.AvailableProgram.Program.Name,
                    duration = ph.AvailableProgram.Program.DurationMinutes
                }
            }).ToList()
        };

        return Ok(result);
    }
}