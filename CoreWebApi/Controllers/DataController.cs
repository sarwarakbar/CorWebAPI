using BAL.GenService;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly GenPrizeService Service;

        public DataController(GenPrizeService Service)
        {
            this.Service = Service;
        }

        //GET All Person  
        [HttpGet("GetAllPrizes")]
        public IEnumerable<Prize> GetAllPersons()
        {
            var data = Service.GetAllPrizeDetails();
            return data;

        }

        //Add New Prize Record         
        [HttpPost("AddPrize")]
        public async Task<Prize> AddPrize([FromBody] Prize prize1)
        {
            try
            {
                await Service.AddPrize(prize1);
                return prize1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Get Prize By ID
        [HttpGet("GetByID")]
        public IEnumerable<Prize> GetById(int id)
        {
            var data = Service.GetById(id);

            //if (data == null)
            //{
            //    return NotFound();
            //}

            return data;
        }

        ////Update Prize Details  
        //public Prize UpdatePrize(int id, Prize _object)
        //{

        //    var Data = Service.Update(id, _object);
        //    return Data;

        //}

        // PUT api/<DataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
