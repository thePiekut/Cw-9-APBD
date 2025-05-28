using System.ComponentModel.DataAnnotations;

namespace CW_9_s29782.DTOs;

public class PrescriptionCreateDto
{
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
    public PatientCreateDto Patient { get; set; }
    public List<MedicamentCreateDto> Medicaments { get; set; }
}