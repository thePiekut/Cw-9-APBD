namespace CW_9_s29782.DTOs;

public class PrescriptionGetDto
{
    public int IdPrescription { get; set; }
    public DoctorGetDto Doctor { get; set; } = null!;
    public ICollection<MedicamentGetDto> Medicaments { get; set; } = null!;
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}