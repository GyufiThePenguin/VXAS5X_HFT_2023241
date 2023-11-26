using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Repository
{
    public class ActorRepo : IActorRepo
    {

        StagePlayDbContext sdb; //as StagePlayDatabase

        public ActorRepo(StagePlayDbContext sdbContext) => sdb = sdbContext;

        public void Create(Actor actor)
        {
            sdb.Actors.Add(actor);
            sdb.SaveChanges();

        }
        public void Update(Actor actor)
        {
            var updateActor = Read(actor.Id);
            updateActor.Id = actor.Id;
            updateActor.Name = actor.Name;
            updateActor.Age = actor.Age;
            updateActor.Gender = actor.Gender;
            updateActor.Role = actor.Role;
            updateActor.Plays = actor.Plays;
            sdb.SaveChanges();

        }

        public void Delete(int id)
        {
            var deleteActor = Read(id);
            sdb.Actors.Remove(deleteActor);
            sdb.SaveChanges();

        }

        public Actor Read(int id)
        {
            return sdb.Actors.FirstOrDefault(t => t.Id == id);

        }

        public IQueryable<Actor> ReadAll()
        {
            return sdb.Actors;

        }

    }
}
