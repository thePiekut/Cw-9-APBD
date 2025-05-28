using System.ComponentModel.DataAnnotations;

namespace CW_9_s29782.DTOs;

public class MedicamentCreateDto
{
    [Required] public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    [Required] [MaxLength(100)] public string Details { get; set; } = null!;
}