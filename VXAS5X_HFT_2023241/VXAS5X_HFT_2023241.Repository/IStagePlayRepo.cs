using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Repository
{
    public interface IStagePlayRepo
    {
        void Create(StagePlay stagePlay);
        void Update(StagePlay stageplay);
        void Delete(int id);

        StagePlay Read(int id);

        IQueryable<StagePlay> ReadAll();


    }
}
