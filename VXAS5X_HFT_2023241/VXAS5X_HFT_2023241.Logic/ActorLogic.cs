using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;
using VXAS5X_HFT_2023241.Repository;

namespace VXAS5X_HFT_2023241.Logic
{
    public class ActorLogic : IActorLogic
    {

        IActorRepo actorRepo;

        public ActorLogic(IActorRepo actorRepo)
        {
            this.actorRepo = actorRepo;

        }

        public void Create(Actor actor)
        {
            if (actor.Name != null)
                actorRepo.Create(actor);
            else
                throw new ArgumentException();

        }

        public void Update(Actor actor)
        {
            if (actorRepo.ReadAll().Any(x => x.Id.Equals(actor.Id)))
                actorRepo.Update(actor);
            else
                throw new IndexOutOfRangeException("ERROR: Can not update -> non existing ID.");

        }

        public void Delete(int id)
        {
            if (actorRepo.ReadAll().Any(x => x.Id.Equals(id)))
                actorRepo.Delete(id);
            else
                throw new IndexOutOfRangeException("ERROR: Can not delete -> non existing ID.");

        }

        public Actor Read(int id)
        {
            if (actorRepo.ReadAll().Any(x => x.Id.Equals(id)))
                return actorRepo.Read(id);

            throw new IndexOutOfRangeException("ERROR: Can not read -> non existing ID.");

        }

        public IEnumerable<Actor> ReadAll()
        {
            return actorRepo.ReadAll();

        }

    }
}
