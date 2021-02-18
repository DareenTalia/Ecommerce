using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Bl;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyEcommerce.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemApiController : ControllerBase
    {

        private readonly IItemsServices itemsServices;

        public ItemApiController(IItemsServices itemsServices)
        {
            this.itemsServices = itemsServices;
        }

        // GET: api/<ItemApiController>
        [HttpGet]
        public IEnumerable<VwItemCategory> Get()
        {

            return itemsServices.GetAllItems() ;
        }

        // GET api/<ItemApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ItemApiController>
        [HttpPost]
        public void Post([FromBody] string[] value)
        {

        }

        // PUT api/<ItemApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ItemApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
