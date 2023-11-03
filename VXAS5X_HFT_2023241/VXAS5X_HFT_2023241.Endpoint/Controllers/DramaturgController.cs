using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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

        [HttpPost]
        public void Post([FromBody] Dramaturg value)
        {
            dramaturgLogic.Create(value);
            hub.Clients.All.SendAsync("DramaturgCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Dramaturg value)
        {
            dramaturgLogic.Update(value);
            hub.Clients.All.SendAsync("DramaturgUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var dramaturgDelete = dramaturgLogic.Read(id);
            dramaturgLogic.Delete(id);
            hub.Clients.All.SendAsync("DramaturgDeleted", dramaturgDelete);
        }

    }
}
