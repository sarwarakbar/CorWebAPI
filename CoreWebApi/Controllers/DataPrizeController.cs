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


        //Get records from Laureates table
        [HttpGet]
        [Route("laureates")]
        public IEnumerable<Laureate> GetLaureate()
        {
            return db.Laureates.ToList();
        }


        //Get All Records by Laureate ID in Ascending Order
        [HttpGet]
        [Route("List-laureateID")]
        public async Task<ActionResult<Laureate>> GetLaureatesAsc()
        {
            var data = await db.Laureates.Include(c => c.Prize).OrderBy(x => x.LaureateId).ToListAsync();
            return Ok(data);
        }


        //Get All Records by Year
        [HttpGet]
        [Route("ListByYear")]
        public IEnumerable<Prize> GetAll()
        {
            return db.Prizes.Include(c => c.Laureates).OrderBy(x => x.Year).ToList();
        }


        //Get All Records by PrizeID
        [HttpGet]
        [Route("ListByPrizeID")]
        public IEnumerable<Prize> GetAll2()
        {
            return db.Prizes.Include(c => c.Laureates).OrderBy(x => x.PrizeId).ToList();
        }


        //Get All Records by Category & Year.
        [HttpGet]
        [Route("ListByYear-Category")]
        public IEnumerable<Prize> GetByYearCategory(string cat, string year)
        {
            return db.Prizes.Include(c => c.Laureates).Where(x => x.Category.ToLower().Contains(cat.ToLower()) && x.Year.ToLower() == year).ToList();
        }



        //Get All Records by Laureate First Name.
        [HttpGet]
        [Route("LaureateByFirstName")]
        public async Task<ActionResult<Laureate>> GetLaureateByName(string name)
        {
            var data = await db.Laureates.Include(c => c.Prize).Where(x => x.Firstname.ToLower().Contains(name.ToLower())).ToListAsync();

            if(data==null)
            {
                return NotFound();
            }

            return Ok(data);
        }


        //Get List by ID
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Prize>> GetDetailsbyID(int id)
        {
            var record = await db.Prizes.Include(c => c.Laureates).Where(c => c.PrizeId == id).FirstOrDefaultAsync();

            if (record == null)
            {
                return NotFound();
            }
            return record;
        }


        //Add New Record
        [HttpPost]
        [Route("AddRecord")]
        public string Post(Prize prize1)
        {
            try
            {
                var data = db.Prizes.Add(prize1).Entity.Laureates;
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
        [Route("UpdateByPrizeID")]
        public Prize UpdatePrizeById(int id, Prize prize1)
        {

            var data = db.Prizes.Include(l => l.Laureates).FirstOrDefault(p => p.PrizeId == id);
            if (data != null)
            {
                data.Year = prize1.Year;
                data.Category = prize1.Category;
                data.Laureates = prize1.Laureates;
                data.OverallMotivation = prize1.OverallMotivation;

                db.SaveChanges();
            }

            return data;
        }


        //Delete Record
        [HttpDelete]
        [Route("DeleteByID")]
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


        //Code for file uploading.
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
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            var result = await WriteFile(file);
            return Ok(result + " File Uploaded");
        }

    }


}

