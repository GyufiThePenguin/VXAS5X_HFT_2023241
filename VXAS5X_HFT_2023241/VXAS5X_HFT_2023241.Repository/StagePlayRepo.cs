using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Repository
{
    public class StagePlayRepo : IStagePlayRepo
    {

        StagePlayDbContext sdb;

        public StagePlayRepo(StagePlayDbContext sdb)
        {
            this.sdb = sdb;
        }

        public void Create(StagePlay stagePlay)
        {
            sdb.Plays.Add(stagePlay);
            sdb.SaveChanges();

        }

        public StagePlay Read(int id)
        {
            return sdb.Plays.FirstOrDefault(t => t.Id == id);

        }

        public void Update(StagePlay stagePlay)
        {
            var updateStagePlay = Read(stagePlay.Id);
            updateStagePlay.Id = stagePlay.Id;
            updateStagePlay.Profit = stagePlay.Profit;
            updateStagePlay.Premier = stagePlay.Premier;
            updateStagePlay.Rating = stagePlay.Rating;
            updateStagePlay.Title = stagePlay.Title;
            updateStagePlay.Actors = stagePlay.Actors;
            updateStagePlay.Dramaturg = stagePlay.Dramaturg;
            updateStagePlay.DramaturgId = stagePlay.DramaturgId;
            sdb.SaveChanges();

        }

        public void Delete(int id)
        {
            var deletePlay = Read(id);
            sdb.Plays.Remove(deletePlay);
            sdb.SaveChanges();

        }

        public IQueryable<StagePlay> ReadAll()
        {
            return sdb.Plays;

        }

    }
}
