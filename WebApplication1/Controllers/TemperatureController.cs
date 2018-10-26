using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class TemperatureController : Controller
    {
        private ITemperatureRepository _temperatureRepository;

        public TemperatureController(TemperatureContext context)
        {
            _temperatureRepository = new TemperatureRepository(context);
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<Temperature> Get(int skip = 0, int limit = 100)
        {
            limit = limit <= 100 ? limit : 100;
            return _temperatureRepository.GetTemperatures().Skip(skip).Take(limit);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetValue")]
        public Temperature Get(int id)
        {
            return _temperatureRepository.GetTemperatureById(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(Temperature item)
        {
            _temperatureRepository.InsertTemperature(item);
            _temperatureRepository.Save();
            return CreatedAtRoute("GetValue", new { id = item.Id }, item);
        }

        // PUT api/values
        [HttpPut]
        public IActionResult Put(Temperature item)
        {
            if (ModelState.IsValid)
            {
                _temperatureRepository.UpdateTemperature(item);
                _temperatureRepository.Save();
                return CreatedAtRoute("GetValue", new { id = item.Id }, item);
            }

            return RedirectToAction("Get");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (null != _temperatureRepository.GetTemperatureById(id))
            {
                _temperatureRepository.DeleteTemperature(id);
                _temperatureRepository.Save();
                return Ok();
            }

            return NotFound();
        }
        
        [HttpGet("chart/{date1}")]
        [HttpGet("chart/{date1}/{date2}")]
        public IEnumerable<Temperature> Chart(int date1, int date2 = 0)
        {
            if (date2 == 0)
            {
                date2 = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            }
            
            var data = from t in _temperatureRepository.GetTemperatures()
                where t.Timestamp >= date1 && t.Timestamp <= date2
                      orderby t.Timestamp
                select t;

            var dataList = data.ToList();
            var result = new List<Temperature>();
            int count = dataList.Count();
            int period = (int) Math.Ceiling((decimal) count / 100);
            int cnt = 0;
            double tBuffer = 0;
            long dateBuffer = 0;
            
            for (int i = 0; i < count; i++)
            {
                tBuffer += dataList[i].Temp / period;
                dateBuffer += dataList[i].Timestamp / period;
                
                if (i%period == 0)
                {
                    Temperature t = new Temperature
                    {
                        Id = cnt,
                        Temp = tBuffer,
                        Timestamp = (int) dateBuffer
                    };
                    result.Add(t);
                    
                    cnt++;
                    tBuffer = 0;
                    dateBuffer = 0;
                }
            }

            return result;
        }
    }
}