using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Logic
{
    public interface IStagePlayLogic
    {
        void Create(StagePlay stagePlay);
        void Update(StagePlay stageplay);
        void Delete(int id);
        double AvgProfit();
        int GoodPlays();

        StagePlay Read(int id);

        IEnumerable<StagePlay> HorribleSuccessStagePlays();
        IEnumerable<StagePlay> ExceptionalStagePlays();

        IEnumerable<StagePlay> NotHorribleTuktukStagePlays();
        IEnumerable<StagePlay> RegisseurBelaBalla();
        IEnumerable<StagePlay> PremieredAfter2010();

        IEnumerable<StagePlay> ReadAll();



    }
}
