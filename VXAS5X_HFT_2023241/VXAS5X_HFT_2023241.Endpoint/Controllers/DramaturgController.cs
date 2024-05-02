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
    public class DramaturgController : ControllerBase
    {

        IDramaaturgLogic dramaturgLogic;
        IHubContext<SignalRHub> hub;

        public DramaturgController(IDramaaturgLogic dramaturgLogic, IHubContext<SignalRHub> hub)
        {
            this.dramaturgLogic = dramaturgLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Dramaturg> Get()
        {
            return dramaturgLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Dramaturg Get(int id)
        {
            return dramaturgLogic.Read(id);
        }

        //[HttpPost]
        //public void Post([FromBody] Dramaturg value)
        //{
        //    dramaturgLogic.Create(value);
        //    this.hub.Clients.All.SendAsync("DramaturgCreated", value);
        //}

        [HttpPut]
        public void Put([FromBody] Dramaturg value)
        {
            dramaturgLogic.Update(value);
            this.hub.Clients.All.SendAsync("DramaturgUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var dramaturgDelete = dramaturgLogic.Read(id);
            dramaturgLogic.Delete(id);
            this.hub.Clients.All.SendAsync("DramaturgDeleted", dramaturgDelete);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Dramaturg dramaturg)
        {
            try
            {
                dramaturgLogic.Create(dramaturg);
                this.hub.Clients.All.SendAsync("DramaturgCreated", dramaturg);
                return CreatedAtAction(nameof(Get), new { id = dramaturg.Id }, dramaturg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return a normal error if I messed up
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Dramaturg dramaturg)
        {
            if (id != dramaturg.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                dramaturgLogic.Update(dramaturg);
                this.hub.Clients.All.SendAsync("ActorUpdated", dramaturg);
                return NoContent(); // confirms if the updaate is successful
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return a normal error, not the empty sh*t it used to
            }
        }

    }
}
