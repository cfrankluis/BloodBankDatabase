using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Models.Patient;
using BloodBank.Data;

namespace BloodBank.Service
{
    public class PatientService
    {
        public bool CreatePatient(PatientCreate model)
        {
            var entity =
                new Patient()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    BloodType = model.BloodType,
                    CheckInDate = DateTime.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Patients.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PatientListItem> GetAllPatients()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.
                    Patients.
                    Select(
                        e => new PatientListItem()
                        {
                            PatientID = e.ID,
                            Name = e.FullName,
                            Age = e.Age,
                            BloodType = e.BloodType,
                            CheckInDate = e.CheckInDate
                        }
                     );

                return query.ToArray();
            }
        }

        public PatientDetail GetPatientByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Patients
                    .SingleOrDefault(e => e.ID == id);

                if (entity is null)
                    return null;

                return
                    new PatientDetail
                    {
                        PatientID = entity.ID,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Age = entity.Age,
                        BloodType = entity.BloodType,
                        BirthDate = entity.BirthDate,
                        CheckInDate = entity.CheckInDate
                    };
            }
        }



        public bool UpdatePatient(PatientEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = 
                    ctx
                    .Patients
                    .SingleOrDefault(e => e.ID == model.PatientID);

                if (entity is null)
                    return false;

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.BloodType = model.BloodType;
                entity.BirthDate = model.BirthDate;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePatient(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Patients
                    .SingleOrDefault(e => e.ID == id);

                if (entity is null)
                    return false;


                ctx.Patients.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
