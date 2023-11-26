using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Repository
{
    public class DramaturgRepo : IDramaturgRepo
    {

        StagePlayDbContext sdb;

        public DramaturgRepo(StagePlayDbContext sdb)
        {
            this.sdb = sdb;

        }

        public void Create(Dramaturg dramaturg)
        {
            sdb.Dramaturgs.Add(dramaturg);
            sdb.SaveChanges();

        }

        public Dramaturg Read(int id)
        {
            return sdb.Dramaturgs.FirstOrDefault(t => t.Id == id);

        }
        public void Update(Dramaturg dramaturg)
        {
            var updateDramaturg = Read(dramaturg.Id);
            updateDramaturg.Id = dramaturg.Id;
            updateDramaturg.Age = dramaturg.Age;
            updateDramaturg.Gender = dramaturg.Gender;
            updateDramaturg.Plays = dramaturg.Plays;
            updateDramaturg.Name = dramaturg.Name;
            sdb.SaveChanges();

        }

        public void Delete(int id)
        {
            var deleteDramaturg = Read(id);
            sdb.Dramaturgs.Remove(deleteDramaturg);
            sdb.SaveChanges();

        }

        public IQueryable<Dramaturg> ReadAll()
        {
            return sdb.Dramaturgs;

        }

    }
}
