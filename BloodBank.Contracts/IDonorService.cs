using BloodBank.Models.Donor;
using System;
using System.Collections.Generic;

namespace BloodBank.Contracts
{
    public interface IDonorService
    {
        bool CreateDonor(DonorCreate model);
        bool DeleteDonor(Guid id);
        IEnumerable<DonorListItem> GetAllDonors();
        DonorDetail GetDonorByID(Guid id);
        bool UpdateDonor(DonorEdit model);
    }
}