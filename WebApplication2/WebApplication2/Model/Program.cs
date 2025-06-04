namespace WebApplication2.Model;

public class Program
{
    public int ProgramId { get; set; }
    public string Name { get; set; } = null!;
    public int DurationMinutes { get; set; }
    public int TemperatureCelsius { get; set; }

    public List<AvailableProgram> AvailablePrograms { get; set; } = new();
}