using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DTOs;
using WebApplication2.Model;

namespace WebApplication2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WashingMachinesController : ControllerBase
{
    private readonly AppDbContext _context;

    public WashingMachinesController(AppDbContext context)
    {
        _context = context;
        
    }

    [HttpPost]
    public async Task<IActionResult> AddWashingMachine(AddWashingMachineRequest request)
    {
        
        
        if (request.WashingMachine.MaxWeight < 8)
        {
            return BadRequest("Max weight must be at least 8.");
        }

        
        var exists = await _context.WashingMachines
            .AnyAsync(wm => wm.SerialNumber == request.WashingMachine.SerialNumber);

        if (exists)
        {
            return BadRequest("Washing machine with this serial number already exists.");
            
        }

        
        var programNames = request.AvailablePrograms.Select(p => p.ProgramName).ToList();

        var programsInDb = await _context.Programs
            .Where(p => programNames.Contains(p.Name))
            .ToListAsync();

        
        
        if (programsInDb.Count != request.AvailablePrograms.Count)
        {
            return NotFound("One or more programs not found.");
        }

        
        if (request.AvailablePrograms.Any(p => p.Price >= 25))
        {
            return BadRequest("Program price cannot be greater than or equal to 25.");
            
        }
        
        
        
        var washingMachine = new WashingMachine
        {
            SerialNumber = request.WashingMachine.SerialNumber,
            MaxWeight = request.WashingMachine.MaxWeight
        };

        await _context.WashingMachines.AddAsync(washingMachine);
        await _context.SaveChangesAsync();

        
        foreach (var programDto in request.AvailablePrograms)
        {
            var program = programsInDb.First(p => p.Name == programDto.ProgramName);

            var availableProgram = new AvailableProgram
            {
                WashingMachineId = washingMachine.WashingMachineId,
                ProgramId = program.ProgramId,
                Price = programDto.Price
            };

            await _context.AvailablePrograms.AddAsync(availableProgram);
        }

        await _context.SaveChangesAsync();

        return StatusCode(201);
    }
}