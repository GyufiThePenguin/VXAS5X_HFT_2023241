using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
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


        //[HttpPost]
        //public void Post([FromBody] StagePlay value)
        //{
        //    stagePlayLogic.Create(value);
        //}


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
            this.hub.Clients.All.SendAsync("StagePlayDeleted", stagePlayDelete);
        }

        [HttpPost]
        public IActionResult Post([FromBody] StagePlay stagePlay)
        {
            try
            {
                stagePlayLogic.Create(stagePlay);
                this.hub.Clients.All.SendAsync("StagePlayCreated", stagePlay);
                return CreatedAtAction(nameof(Get), new { id = stagePlay.Id }, stagePlay);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return a normal error if I messed up
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StagePlay stagePlay)
        {
            if (id != stagePlay.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                stagePlayLogic.Update(stagePlay);
                this.hub.Clients.All.SendAsync("StagePlayUpdated", stagePlay);
                return NoContent(); // confirms if the updaate is successful
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return a normal error, not the empty sh*t it used to
            }
        }

    }
}
