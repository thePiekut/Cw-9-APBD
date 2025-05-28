using CW_9_s29782.Models;
using Microsoft.EntityFrameworkCore;

namespace CW_9_s29782.Data;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var patients = new List<Patient>
        {
            new()
            {
                IdPatient = 1,
                FirstName = "Jan",
                LastName = "Nowicki",
                Birthdate = DateTime.Parse("1988-02-14")
            },
            new()
            {
                IdPatient = 2,
                FirstName = "Barbara",
                LastName = "Szulc",
                Birthdate = DateTime.Parse("1992-09-30")
            },
            new()
            {
                IdPatient = 3,
                FirstName = "Tomasz",
                LastName = "Lewandowski",
                Birthdate = DateTime.Parse("1975-06-18")
            }
        };

        var doctors = new List<Doctor>
        {
            new()
            {
                IdDoctor = 1,
                FirstName = "Magdalena",
                LastName = "Kwiatkowska",
                Email = "magda.kwiatkowska@healthcare.pl"
            },
            new()
            {
                IdDoctor = 2,
                FirstName = "Robert",
                LastName = "Czarnecki",
                Email = "robert.czarnecki@clinic.org"
            }
        };

        var prescriptions = new List<Prescription>
        {
            new()
            {
                IdPrescription = 1,
                Date = DateTime.Parse("2025-04-10"),
                DueDate = DateTime.Parse("2025-05-10"),
                IdPatient = 1,
                IdDoctor = 1
            },
            new()
            {
                IdPrescription = 2,
                Date = DateTime.Parse("2025-04-12"),
                DueDate = DateTime.Parse("2025-05-12"),
                IdPatient = 2,
                IdDoctor = 2
            },
            new()
            {
                IdPrescription = 3,
                Date = DateTime.Parse("2025-04-15"),
                DueDate = DateTime.Parse("2025-05-15"),
                IdPatient = 3,
                IdDoctor = 1
            }
        };

        var medicaments = new List<Medicament>
        {
            new()
            {
                IdMedicament = 1,
                Name = "lek1",
                Type = "Antybiotyk",
                Description = "Antybiotyk na infekcje bakteryjne"
            },
            new()
            {
                IdMedicament = 2,
                Name = "Ketonal",
                Type = "Tabletki",
                Description = "Silny środek przeciwbólowy"
            },
            new()
            {
                IdMedicament = 3,
                Name = "Rutinoscorbin",
                Type = "Suplement",
                Description = "Wzmacnia naczynia krwionośne"
            }
        };

        var prescriptionMedicaments = new List<PrescriptionMedicament>
        {
            new()
            {
                IdMedicament = 1,
                IdPrescription = 1,
                Dose = 500,
                Details = "1 kapsułka co 12 godzin"
            },
            new()
            {
                IdMedicament = 2,
                IdPrescription = 1,
                Dose = 100,
                Details = "Po silnym bólu"
            },
            new()
            {
                IdMedicament = 2,
                IdPrescription = 2,
                Dose = 50,
                Details = "Rano i wieczorem"
            },
            new()
            {
                IdMedicament = 3,
                IdPrescription = 3,
                Dose = 2,
                Details = "2 razy dziennie z posiłkiem"
            }
        };
        modelBuilder.Entity<Medicament>().HasData(medicaments);
        modelBuilder.Entity<Doctor>().HasData(doctors);
        modelBuilder.Entity<Patient>().HasData(patients);
        modelBuilder.Entity<Prescription>().HasData(prescriptions);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionMedicaments);
    }
}