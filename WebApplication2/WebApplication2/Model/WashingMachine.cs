namespace WebApplication2.Model;

public class WashingMachine
{
    public int WashingMachineId { get; set; }
    public decimal MaxWeight { get; set; }
    public string SerialNumber { get; set; } = null!;

    public List<AvailableProgram> AvailablePrograms { get; set; } = new();
}