using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Models.BloodBag;
using BloodBank.Data;
using BloodBank.Contracts;
namespace BloodBank.Service
{
    public class BloodBagService : IBloodBagService
    {
        public bool CreateBloodBag(BloodBagCreate model)
        {
            var entity = new BloodBag
            {
                BloodType = model.BloodType,
                Volume = model.Volume,
                ExtractionDate = DateTime.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.BloodBags.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BloodBagListItem> GetAllBloodBags()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .BloodBags;
                var arr = query.ToArray();


                return arr.Select(
                        e => new BloodBagListItem
                        {
                            BloodID = e.ID,
                            BloodType = e.BloodType,
                            DaysStored = e.DaysStored.ToString(),
                            Volume = e.Volume
                        });
            }
        }

        public BloodBagDetail GetBloodBagByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.BloodBags.Find(id);
                if (entity is null) return null;

                return
                    new BloodBagDetail
                    {
                        BloodID = entity.ID,
                        BloodType = entity.BloodType,
                        Volume = entity.Volume,
                        ExtractionDate = entity.ExtractionDate,
                        DaysStored = entity.DaysStored
                    };
            }
        }

        public bool UpdateBloodBag(BloodBagEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.BloodBags.Find(model.BloodID);
                if (entity is null) return false;

                if (
                entity.BloodType == model.BloodType &&
                entity.Volume == model.Volume)
                    return true;

                entity.BloodType = model.BloodType;
                entity.Volume = model.Volume;

                ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBloodBag(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.BloodBags.Find(id);
                if (entity is null) return false;
                ctx.BloodBags.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
