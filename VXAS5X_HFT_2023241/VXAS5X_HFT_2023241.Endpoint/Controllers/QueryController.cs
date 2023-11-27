using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VXAS5X_HFT_2023241.Logic;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Endpoint.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class QueryController : ControllerBase
    {

        IStagePlayLogic stagePlayLogic;
        public QueryController(IStagePlayLogic stagePlayLogic)
        {
            this.stagePlayLogic = stagePlayLogic;
        }

        [HttpGet]
        public IEnumerable<StagePlay> One()
        {
            return stagePlayLogic.RegisseurBelaBalla();
        }

        [HttpGet]
        public IEnumerable<StagePlay> Two()
        {
            return stagePlayLogic.ExceptionalStagePlays();
        }

        [HttpGet]
        public IEnumerable<StagePlay> Three()
        {
            return stagePlayLogic.PremieredAfter2010();
        }

        [HttpGet]
        public IEnumerable<StagePlay> Four()
        {
            return stagePlayLogic.HorribleSuccessStagePlays();
        }

        [HttpGet]
        public IEnumerable<StagePlay> Five()
        {
            return stagePlayLogic.NotHorribleTuktukStagePlays();
        }

        [HttpGet]
        public int Six()
        {
            return stagePlayLogic.GoodPlays();
        }

        [HttpGet]
        public double Seven()
        {
            return stagePlayLogic.AvgProfit();
        }

    }
}
