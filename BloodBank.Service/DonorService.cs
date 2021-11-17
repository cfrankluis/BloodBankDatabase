using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data;
using BloodBank.Models.Donor;
using BloodBank.Contracts;

namespace BloodBank.Service
{
    public class DonorService : IDonorService
    {
        public bool CreateDonor(DonorCreate model)
        {
            var entity =
                new Donor
                {
                    ID = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    BloodType = model.BloodType
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Donors.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<DonorListItem> GetAllDonors()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Donors
                    .Select(
                        e => new DonorListItem
                        {
                            DonorID = e.ID,
                            Name = e.FullName,
                            Age = e.Age,
                            BloodType = e.BloodType
                        }
                     );

                return query.ToArray();
            }
        }

        public DonorDetail GetDonorByID(Guid id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Donors
                    .SingleOrDefault(e => e.ID == id);

                if (entity is null)
                    return null;

                return
                    new DonorDetail
                    {
                        DonorID = entity.ID,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Age = entity.Age,
                        BloodType = entity.BloodType,
                        BirthDate = entity.BirthDate
                    };
            }
        }

        public bool UpdateDonor(DonorEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Donors
                    .SingleOrDefault(e => e.ID == model.DonorID);

                if (entity is null)
                    return false;

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.BloodType = model.BloodType;
                entity.BirthDate = model.BirthDate;

                ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteDonor(Guid id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Donors
                    .SingleOrDefault(e => e.ID == id);

                if (entity is null)
                    return false;

                ctx.Donors.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
