using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Logic
{
    public interface IActorLogic
    {
        void Create(Actor actor);
        void Delete(int id);
        void Update(Actor actor);

        Actor Read(int id);

        IEnumerable<Actor> ReadAll();



    }
}
