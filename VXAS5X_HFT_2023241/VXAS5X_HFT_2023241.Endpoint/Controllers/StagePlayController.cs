using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using VXAS5X_HFT_2023241.Logic;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StagePlayController : ControllerBase
    {

        IStagePlayLogic stagePlayLogic;

        public StagePlayController(IStagePlayLogic stagePlayLogic)
        {
            this.stagePlayLogic = stagePlayLogic;
        }

        [HttpGet]
        public IEnumerable<StagePlay> Get()
        {
            return stagePlayLogic.ReadAll();
        }


        [HttpGet("{id}")]
        public StagePlay Get(int id)
        {
            return stagePlayLogic.Read(id);
        }


        [HttpPost]
        public void Post([FromBody] StagePlay value)
        {
            stagePlayLogic.Create(value);
        }


        [HttpPut]
        public void Put([FromBody] StagePlay value)
        {
            stagePlayLogic.Update(value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var stagePlayDelete = stagePlayLogic.Read(id);
            stagePlayLogic.Delete(id);
        }

    }
}
