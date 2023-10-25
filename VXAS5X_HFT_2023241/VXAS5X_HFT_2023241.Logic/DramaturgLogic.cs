using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;
using VXAS5X_HFT_2023241.Repository;

namespace VXAS5X_HFT_2023241.Logic
{
    public class DramaturgLogic : IDramaaturgLogic
    {

        IDramaturgRepo dramaturgRepo;

        public DramaturgLogic(IDramaturgRepo dramaturgRepo)
        {
            this.dramaturgRepo = dramaturgRepo;

        }

        public void Create(Dramaturg dramaturg)
        {
            if (dramaturg.Name != null)
                dramaturgRepo.Create(dramaturg);
            else
                throw new ArgumentException("ERROR: You royally fcked up!");

        }

        public Dramaturg Read(int id)
        {
            if (dramaturgRepo.ReadAll().Any(x => x.Id.Equals(id)))
                return dramaturgRepo.Read(id);

            throw new IndexOutOfRangeException("ERROR: Can not read -> wrong ID.");

        }

        public void Delete(int id)
        {
            if (dramaturgRepo.ReadAll().Any(x => x.Id.Equals(id)))
                dramaturgRepo.Delete(id);
            else
                throw new IndexOutOfRangeException("ERROR: Can not delete -> wrong ID.");

        }

        public void Update(Dramaturg dramaturg)
        {
            if (dramaturgRepo.ReadAll().Any(x => x.Id.Equals(dramaturg.Id)))
                dramaturgRepo.Update(dramaturg);
            else
                throw new IndexOutOfRangeException("ERROR: Can not update -> wrong ID.");

        }

        public IEnumerable<Dramaturg> ReadAll()
        {
            return dramaturgRepo.ReadAll();

        }

    }
}
