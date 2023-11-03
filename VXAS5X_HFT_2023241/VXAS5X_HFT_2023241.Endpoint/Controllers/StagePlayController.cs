using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using VXAS5X_HFT_2023241.Endpoint.Services;
using VXAS5X_HFT_2023241.Logic;
using VXAS5X_HFT_2023241.Models;
using VXAS5X_HFT_2023241.Endpoint.Services;
using VXAS5X_HFT_2023241.Logic;
using VXAS5X_HFT_2023241.Models;

namespace VXAS5X_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StagePlayController : ControllerBase
    {

        IStagePlayLogic stagePlayLogic;
        IHubContext<SignalRHub> hub;

        public StagePlayController(IStagePlayLogic stagePlayLogic, IHubContext<SignalRHub> hub)
        {
            this.stagePlayLogic = stagePlayLogic;
            this.hub = hub;
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
            hub.Clients.All.SendAsync("StagePlayCreated", value);
        }


        [HttpPut]
        public void Put([FromBody] StagePlay value)
        {
            stagePlayLogic.Update(value);
            hub.Clients.All.SendAsync("StagePlayUpdated", value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var stagePlayDelete = stagePlayLogic.Read(id);
            stagePlayLogic.Delete(id);
            hub.Clients.All.SendAsync("StagePlayDeleted", stagePlayDelete);
        }

    }
}
