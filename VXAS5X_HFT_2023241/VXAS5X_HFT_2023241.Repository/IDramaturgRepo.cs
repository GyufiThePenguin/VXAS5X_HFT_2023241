using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Repository
{
    public interface IDramaturgRepo
    {

        void Create(Dramaturg dramaturg);
        void Update(Dramaturg dramaturg);
        void Delete(int id);

        Dramaturg Read(int id);

        IQueryable<Dramaturg> ReadAll();


    }
}
