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

namespace CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataPrizeController : ControllerBase
    {
        AppDbContext db;
        public DataPrizeController(AppDbContext db)
        {
            this.db = db;
        }

        // GET: api/<DataPrizeController>
        [HttpGet]
        public IEnumerable<Prize> GetPrize()
        {
            return db.Prizes.Include(c => c.Laureates).ToList();
           

        }
        [HttpGet]
        [Route("laureates")]
        public IEnumerable<Laureate> GetLaureate()
        {
            return db.Laureates.ToList();

        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Prize> GetAll()
        {
            return db.Prizes.Include(c => c.Laureates).OrderBy(x => x.Year).ToList();

        }

        [HttpPut]
        [Route("update/{id}")]
        public string Update(int id, Prize prize1)
        {
            var data = db.Prizes.Include(c => c.Laureates).FirstOrDefault(g => g.PrizeId == id);
            var data1 = db.Prizes.Update(prize1).Entity.Laureates;

           
            

            db.SaveChanges();
            return "Prize Updated";
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Prize>> GetDetailsbyID(int id)
        {
            var record = await db.Prizes.Include(c => c.Laureates).Where(c => c.PrizeId == id).FirstOrDefaultAsync();
            
            if (record==null)
            {
                return NotFound();
            }
                       
            return record;
        }

        //Add New Record
        [HttpPost]
        [Route("add")]
        public string Post(Prize prize1)
        {
            try
            {
               var data= db.Prizes.Add(prize1).Entity.Laureates;
                db.SaveChanges();
                return "Prize Added";
            }
            catch (Exception)
            {
                return "Enter Correct credentials!";
            }
        }
        //Update Record
        [HttpPut]
        [Route("update")]
        public Prize UpdatePrizeById(int id, Prize prize1)
        {
            Laureate lr = new Laureate();
           
            var data = db.Prizes.Include(l => l.Laureates).FirstOrDefault(p => p.PrizeId == id);
            if (data != null)
            {
                data.Year = prize1.Year;
                data.Category = prize1.Category;
                data.OverallMotivation = prize1.OverallMotivation;
                foreach (var item in data.Laureates)
                {

                    item.Firstname = lr.Firstname;
                    item.Surname = lr.Surname;                     
                    item.Motivation = lr.Motivation;
                    item.Share = lr.Share;
                   
                }

                db.SaveChanges();
            }

            return data;
        }

        //Delete the Record
        [HttpGet]
        [Route("deleterecord")]    
        public string Delete(int id)
        {
            try
            {
                Prize prize1 = db.Prizes.Find(id);
                db.Prizes.Remove(prize1);
                db.SaveChanges();
                return "Record Deleted";
            }

            catch (Exception)
            {
                return "Something went wrong. Please Check!";
            }
        }



        [HttpGet]
        [Route("NobelPrize")]
        public IEnumerable<NobelPrize> GetNobelPrizes()
        {
            var result = (from p in db.Prizes
                          join l in db.Laureates on p.PrizeId equals l.PrizeId
                          select new NobelPrize()
                          {
                              PrizeId = p.PrizeId,
                              Year = p.Year,
                              Category = p.Category,
                              LaureateId = l.LaureateId,
                              Firstname = l.Firstname,
                              Surname = l.Surname,
                              Motivation = l.Motivation,
                              Share = l.Share,
                              OverallMotivation = p.OverallMotivation

                          }).ToList();
            return result;
        }

        private async Task<string> WriteFile(IFormFile file)
        {
            string fileName = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extension;
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");
                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
            }
            return fileName;
        }

        [HttpPost]
        [Route("UploadFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult>UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            var result = await WriteFile(file);
            return Ok(result +" File Uploaded");
        }

         
    }



    

}

