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

namespace CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataPrizeController : ControllerBase
    {
        private readonly PrizeService service;
        private readonly IRepository<Prize> _Prize;

        public DataPrizeController(PrizeService service, IRepository<Prize> _Prize)
        {
            this.service = service;
            this._Prize = _Prize;
        }


        //Get All Prizes
        [HttpGet("GetAll")]
        public Object GetAllPrizes()
        {
            var data = service.GetAllPrizes();
            return data;
        }

        //Add Prize  
        [HttpPost("AddPrize")]
        public Object AddPrize([FromBody] Prize prize1)
        {
            try
            {
                service.Add(prize1);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        //Update Prize  
        [HttpPut("UpdatePrize")]
        public Object UpdatePrize(int id, Prize prize1)
        {
            try
            {
                service.UpdateByID(id, prize1);
                return true;
            }
            catch (Exception)
            {
                return false;
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
        public Object GetByCategoryYear(string cat, string year)
        {
            var data = service.GetListByYearCategory(cat, year);
            return data;
        }

        //Get Prize By Firstname
        [HttpGet("GetPrizeByFirstname")]
        public Object GetPrizeByFirstname(string name)
        {
            var data = service.GetLaureateByFirstName(name);
            return data;
        }

        //Get Prize By Year
        [HttpGet("GetByYear")]
        public Object GetByYear()
        {
            var data = service.GetYearPrize();
            return data;

        }

    }

}

