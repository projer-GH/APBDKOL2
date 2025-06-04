namespace WebApplication2.DTOs;

public class AddWashingMachineRequest
{
    public WashingMachineDto WashingMachine { get; set; }
    
    public List<AvailableProgramDto> AvailablePrograms { get; set; }
    
}

public class WashingMachineDto
{
    public decimal MaxWeight { get; set; } 
    
    public string SerialNumber { get; set; }
    
    
}

public class AvailableProgramDto
{
    public string ProgramName { get; set; }
    
    public decimal Price { get; set; }
}