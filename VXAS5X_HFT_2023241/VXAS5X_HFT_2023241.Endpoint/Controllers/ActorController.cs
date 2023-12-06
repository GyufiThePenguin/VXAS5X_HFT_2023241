
using VXAS5X_HFT_2023241.Logic;
using VXAS5X_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;


namespace VXAS5X_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {

        IActorLogic actorLogic;

        public ActorController(IActorLogic actorLogic)
        {
            this.actorLogic = actorLogic;

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

        }

        [HttpPut]
        public void Put([FromBody] Actor value)
        {
            actorLogic.Update(value);


        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deleteActor = actorLogic.Read(id);
            actorLogic.Delete(id);


        }

    }
}
