
using VXAS5X_HFT_2023241.Logic;
using VXAS5X_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using VXAS5X_HFT_2023241.Endpoint.Services;
using Newtonsoft.Json.Linq;

namespace VXAS5X_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {

        IActorLogic actorLogic;
        IHubContext<SignalRHub> hub;

        public ActorController(IActorLogic actorLogic, IHubContext<SignalRHub> hub)
        {
            this.actorLogic = actorLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Actor> Get()
        {
            return actorLogic.ReadAll();

        }

        [HttpGet("{id}")]
        public Actor Get(int id)
        {
            return actorLogic.Read(id);

        }

        //[HttpPost]
        //public void Post([FromBody] Actor value)
        //{
        //    actorLogic.Create(value);
        //    this.hub.Clients.All.SendAsync("ActorCreated", value);

        //}

        [HttpPut]
        public void Put([FromBody] Actor value)
        {
            actorLogic.Update(value);
            this.hub.Clients.All.SendAsync("ActorUpdated", value);

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            var deleteActor = actorLogic.Read(id);
            actorLogic.Delete(id);
            this.hub.Clients.All.SendAsync("ActorDeleted", deleteActor);


        }

        [HttpPost]
        public IActionResult Post([FromBody] Actor actor)
        {
            try
            {
                actorLogic.Create(actor);
                this.hub.Clients.All.SendAsync("ActorCreated", actor);
                return CreatedAtAction(nameof(Get), new { id = actor.Id }, actor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return a normal error if I messed up
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                actorLogic.Update(actor);
                this.hub.Clients.All.SendAsync("ActorUpdated", actor);
                return NoContent(); // confirms if the updaate is successful
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return a normal error, not the empty sh*t it used to
            }
        }

    }
}
