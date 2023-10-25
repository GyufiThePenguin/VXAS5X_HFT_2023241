using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Logic
{
    public interface IDramaaturgLogic
    {
        void Create(Dramaturg dramaturg);
        void Delete(int id);
        void Update(Dramaturg dramaturg);

        Dramaturg Read(int id);

        IEnumerable<Dramaturg> ReadAll();


    }
}
