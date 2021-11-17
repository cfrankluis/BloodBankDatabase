using BloodBank.Models.DonorAppointment;
using System.Collections.Generic;

namespace BloodBank.Contracts
{
    public interface IDonorAppointmentService
    {
        bool AppointmentCreate(AppointmentCreate model);
        bool DeleteAppointment(int id);
        IEnumerable<AppoinmentListItem> GetAllAppointments();
        AppointmentDetail GetAppointmentById(int id);
        bool UpdateAppointment(AppointmentEdit model);
    }
}