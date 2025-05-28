using System.ComponentModel.DataAnnotations;

namespace CW_9_s29782.DTOs;

public class PatientCreateDto
{
    [Required] public int IdPatient { get; set; }
    [MaxLength(100)]
    [Required] public string FirstName { get; set; }
    [MaxLength(100)]
    [Required] public string LastName { get; set; }
    [Required] public DateTime Birthdate { get; set; }
}