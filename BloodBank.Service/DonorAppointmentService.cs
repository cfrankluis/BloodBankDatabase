using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data;
using BloodBank.Models.DonorAppointment;

namespace BloodBank.Service
{
    public class DonorAppointmentService
    {
        public bool AppointmentCreate(AppointmentCreate model)
        {
            var entity = new DonorAppointment
            {
                DonorID = model.DonorId,
                AppointmentTime = model.AppointmentDate,
                Status = StatusValues.UPCOMING
                
            };

            using(var ctx = new ApplicationDbContext())
            {
                ctx.Appointments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AppoinmentListItem> GetAllAppointments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Appointments
                    .Select(
                        e => new AppoinmentListItem 
                        {
                            AppoinmentTime = e.AppointmentTime,
                            DonorName = e.Donor.FullName,
                            Status = e.Status
                        });

                return query.ToArray();
            }
        }

        public AppointmentDetail GetAppointmentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Appointments.Find(id);

                if (entity is null)
                    return null;

                return
                    new AppointmentDetail
                    {
                        DonorId = entity.DonorID,
                        AppoinmentTime = entity.AppointmentTime,
                        Status = entity.Status
                    };
            }
        }

        public bool DeleteAppointment(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Appointments.Find(id);

                if (entity is null)
                    return false;

                ctx.Appointments.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateAppointment(AppointmentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Appointments.Find(model.AppointmentID);

                if (entity is null)
                    return false;

                entity.AppointmentTime = model.AppointmentTime;
                entity.DonorID = model.DonorId;

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
