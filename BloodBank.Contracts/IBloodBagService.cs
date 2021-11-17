using BloodBank.Models.BloodBag;
using System.Collections.Generic;

namespace BloodBank.Contracts
{
    public interface IBloodBagService
    {
        bool CreateBloodBag(BloodBagCreate model);
        bool DeleteBloodBag(int id);
        IEnumerable<BloodBagListItem> GetAllBloodBags();
        BloodBagDetail GetBloodBagByID(int id);
        bool UpdateBloodBag(BloodBagEdit model);
    }
}