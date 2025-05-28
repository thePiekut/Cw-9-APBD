using CW_9_s29782.Data;
using CW_9_s29782.DTOs;
using CW_9_s29782.Exceptions;
using CW_9_s29782.Models;
using Microsoft.EntityFrameworkCore;

namespace CW_9_s29782.Services;

public interface IDbService
{
    public Task<PatientGetDto> GetPatientDetailsAsync(int idPatient);
    public Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionCreateDto prescription);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<PatientGetDto> GetPatientDetailsAsync(int id)
    {
        var result = await data.Patients
            .Where(patient => patient.IdPatient == id)
            .Select(patient => new PatientGetDto
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Birthdate = patient.Birthdate,

                Prescriptions = patient.Prescriptions.Select(prescription => new PrescriptionGetDto
                    {
                        Date = prescription.Date,
                        DueDate = prescription.DueDate,
                        IdPrescription = prescription.IdPrescription,
                        Doctor = new DoctorGetDto
                        {
                            IdDoctor = prescription.IdDoctor,
                            FirstName = prescription.Doctor.FirstName,
                            LastName = prescription.Doctor.LastName,
                            Email = prescription.Doctor.Email
                        },
                        Medicaments = prescription.PrescriptionMedicament.Select(pm => new MedicamentGetDto
                        {
                            IdMedicament = pm.IdMedicament,
                            Name = pm.Medicament.Name,
                            Description = pm.Medicament.Description,
                            Dose = pm.Dose,
                            Details = pm.Details,
                            Type = pm.Medicament.Type
                        }).ToList()
                    }).OrderBy(pre => pre.DueDate)
                    .ToList()
            }).FirstOrDefaultAsync(p => p.IdPatient == id);

        return result ?? throw new NotFoundException($"Patient with id: {id} not found");
    }

    public async Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionCreateDto prescription)
    {
        if (prescription.Medicaments.Count > 10)
            throw new BadRequestException("Prescription cannot contain more than 10 medicaments.");

        if (prescription.DueDate < prescription.Date)
            throw new BadRequestException("DueDate must be greater than Date.");

        var doctor = await data.Doctors.FindAsync(prescription.IdDoctor);
        if (doctor == null)
            throw new NotFoundException($"Doctor with id {prescription.IdDoctor} not found.");

        var patient = await data.Patients.FindAsync(prescription.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                Birthdate = prescription.Patient.Birthdate
            };
            await data.Patients.AddAsync(patient);
            await data.SaveChangesAsync();
        }

        var medicamentIds = prescription.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicamentIds = await data.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();

        var missing = medicamentIds.Except(existingMedicamentIds).ToList();
        if (missing.Any())
            throw new NotFoundException($"Medicament(s) with ids {string.Join(", ", missing)} not found.");

        var newPrescription = new Prescription
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdDoctor = prescription.IdDoctor,
            IdPatient = patient.IdPatient,
            PrescriptionMedicament = prescription.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details
            }).ToList()
        };

        await data.Prescriptions.AddAsync(newPrescription);
        await data.SaveChangesAsync();

        return new PrescriptionGetDto
        {
            IdPrescription = newPrescription.IdPrescription,
            Date = newPrescription.Date,
            DueDate = newPrescription.DueDate,
            Doctor = new DoctorGetDto
            {
                IdDoctor = doctor.IdDoctor,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            },
            Medicaments = newPrescription.PrescriptionMedicament.Select(pm => new MedicamentGetDto
            {
                IdMedicament = pm.IdMedicament,
                Name = data.Medicaments.Find(pm.IdMedicament)?.Name ?? "",
                Description = data.Medicaments.Find(pm.IdMedicament)?.Description ?? "",
                Dose = pm.Dose,
                Details = pm.Details,
                Type = data.Medicaments.Find(pm.IdMedicament)?.Type ?? ""
            }).ToList()
        };
    }
}