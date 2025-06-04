namespace WebApplication2.Model;

public class AvailableProgram
{
    public int AvailableProgramId { get; set; }
    public int WashingMachineId { get; set; }
    public WashingMachine WashingMachine { get; set; } = null!;
    public int ProgramId { get; set; }
    
    
    public Program Program { get; set; } = null!;
    public decimal Price { get; set; }
    public List<PurchaseHistory> PurchaseHistories { get; set; } = new();
}