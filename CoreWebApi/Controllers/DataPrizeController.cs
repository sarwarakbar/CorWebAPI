using DAL;
using DAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Data;
using BAL.Service;
using DAL.Interface;
using BAL;

namespace CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataPrizeController : ControllerBase
    {
        //private readonly PrizeService service;

        //public DataPrizeController(PrizeService service)
        //{
        //    this.service = service;

        //}

        private readonly IRepositoryBAL service;

        public DataPrizeController(IRepositoryBAL service)
        {
            this.service = service;
        }



        //Get All Prizes
        [HttpGet("GetAll")]
        public IEnumerable<Prize> GetAllPrizes()
        {
            
                var data = service.GetAll();
                return data;
           
        }

        //Add Prize  
        [HttpPost("AddPrize")]       
        public IActionResult Post([FromBody] Prize prize1)
        {
            try
            {
                service.Add(prize1);
                return Ok(prize1);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Update Prize  
        [HttpPut("UpdatePrize")]
        public Prize UpdatePrize(int id, Prize prize1)
        {
            try
            {
                var data = service.UpdateByID(id, prize1);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delete Person  
        [HttpDelete("DeletePrize")]
        public bool DeletePrize(int id)
        {
            try
            {
                service.DeleteRecord(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Get Prize By Year & Category
        [HttpGet("GetPrizeByYear&Category")]
        public ActionResult<IEnumerable<Prize>> GetByCategoryYear(string cat, string year)
        {            
            
                var data = service.GetListByYearCategory(cat, year);
            if (data==null)
            {
                return NotFound("Record not Found");
            }
                return Ok(data);         
        }


        //Get Prize By Firstname
        [HttpGet("GetPrizeByFirstname")]
        public ActionResult<Laureate> GetPrizeByFirstname(string name)
        {
            
            var data = service.GetLaureateByFirstName(name);
            if (data == null)
            {
                return NotFound("Record Not Found by this name.");
            }
            else
            {
                return Ok(data);
            }
        }

        //Get Prize By Year
        [HttpGet("GetByYear")]
        public IEnumerable<Prize> GetByYear()
        {
            var data = service.GetYearPrize();
            return data;

        }

        //Get Prize By ID
        [HttpGet("GetByID")]
        public IEnumerable<Prize> GetById(int id)
        {
            var data = service.GetById(id);
            return data;
        }


       
        
    }

}

