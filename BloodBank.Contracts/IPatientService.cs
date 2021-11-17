using BloodBank.Models.PatientModels;
using System.Collections.Generic;

namespace BloodBank.Contracts
{
    public interface IPatientService
    {
        bool CreatePatient(PatientCreate model);
        bool DeletePatient(int id);
        IEnumerable<PatientListItem> GetAllPatients();
        PatientDetail GetPatientByID(int id);
        bool UpdatePatient(PatientEdit model);
    }
}