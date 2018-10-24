using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ITemperatureRepository _temperatureRepository;

        public ValuesController(TemperatureContext context)
        {
            this._temperatureRepository = new TemperatureRepository(context);
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<Temperature> Get()
        {
            return _temperatureRepository.GetTemperatures();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Temperature Get(int id)
        {
            return _temperatureRepository.GetTemperatureById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}