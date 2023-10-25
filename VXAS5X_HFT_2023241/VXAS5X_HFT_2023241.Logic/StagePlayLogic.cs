using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;
using VXAS5X_HFT_2023241.Repository;

namespace VXAS5X_HFT_2023241.Logic
{
    public class StagePlayLogic : IStagePlayLogic
    {

        IStagePlayRepo stagePlayRepo;

        public StagePlayLogic(IStagePlayRepo stagePlayRepo)
        {
            this.stagePlayRepo = stagePlayRepo;

        }

        public void Create(StagePlay stagePlay)
        {
            if (stagePlay.Title != null)
                stagePlayRepo.Create(stagePlay);
            else
                throw new ArgumentException();

        }

        public void Update(StagePlay stagePlay)
        {
            if (stagePlayRepo.ReadAll().Any(x => x.Id.Equals(stagePlay.Id)))
                stagePlayRepo.Update(stagePlay);
            else
                throw new IndexOutOfRangeException("ERROR: Can not update -> missing id.");

        }
        public void Delete(int id)
        {
            if (stagePlayRepo.ReadAll().Any(x => x.Id.Equals(id)))
                stagePlayRepo.Delete(id);
            else
                throw new IndexOutOfRangeException("ERROR: Can not delete -> missing id.");

        }

        public double AvgProfit()
        {
            var result = stagePlayRepo.ReadAll().Select(x => x.Profit).Average();
            return result;

        }
        public int GoodPlays()
        {
            var result = stagePlayRepo.ReadAll().Where(x => x.Rating == "good").Count();
            return result;

        }

        //

        public StagePlay Read(int id)
        {
            if (stagePlayRepo.ReadAll().Any(x => x.Id.Equals(id)))
                return stagePlayRepo.Read(id);

            throw new IndexOutOfRangeException("ERROR: Can not read -> wrong id.");

        }

        //

        public IEnumerable<StagePlay> HorribleSuccessStagePlays()
        {
            var result = stagePlayRepo.ReadAll().Where(x => x.Profit > 1500000 && x.Rating == "horrible");
            return result;

        }

        public IEnumerable<StagePlay> ExceptionalStagePlays()
        {
            var result = stagePlayRepo.ReadAll().Where(x => x.Rating == "exceptional");
            return result;

        }


        public IEnumerable<StagePlay> NotHorribleTuktukStagePlays()
        {
            var result = stagePlayRepo.ReadAll().Where(x => x.Title.Contains("Tükör a tóban") && x.Rating != "horrible");
            return result;

        }
        public IEnumerable<StagePlay> RegisseurBelaBalla()
        {
            var result = stagePlayRepo.ReadAll().Where(x => x.Dramaturg.Name == "Balla Béla");
            return result;

        }
        public IEnumerable<StagePlay> PremieredAfter2010()
        {
            var result = stagePlayRepo.ReadAll().Where(x => x.Premier > 2010);
            return result;

        }

        public IEnumerable<StagePlay> ReadAll()
        {
            return stagePlayRepo.ReadAll();

        }

    }
}
