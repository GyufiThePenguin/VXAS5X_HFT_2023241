using VXAS5X_HFT_2023241.Endpoint.Services;
using VXAS5X_HFT_2023241.Logic;
using VXAS5X_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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

        [HttpPost]
        public void Post([FromBody] Actor value)
        {
            actorLogic.Create(value);
            hub.Clients.All.SendAsync("ActorCreated", value);

        }

        [HttpPut]
        public void Put([FromBody] Actor value)
        {
            actorLogic.Update(value);
            hub.Clients.All.SendAsync("ActorUpdated", value);

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deleteActor = actorLogic.Read(id);
            actorLogic.Delete(id);
            hub.Clients.All.SendAsync("ActorDeleted", deleteActor);

        }

    }
}
