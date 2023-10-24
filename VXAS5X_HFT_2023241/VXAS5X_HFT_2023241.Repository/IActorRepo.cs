using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Repository
{
    public interface IActorRepo
    {

        void Create(Actor actor);
        void Update(Actor actor);
        void Delete(int id);

        Actor Read(int id);

        IQueryable<Actor> ReadAll();


    }
}
