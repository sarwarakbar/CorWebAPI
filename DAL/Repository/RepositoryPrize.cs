using DAL.Interface;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace DAL.Repository
{
    public class RepositoryPrize :IRepositoryDAL
    {
        AppDbContext db;
        public RepositoryPrize(AppDbContext db)
        {
            this.db = db;
        }

     

        //Get all prize list
        public IEnumerable<Prize> GetAll()
        {
             var data = db.Prizes.Include(c => c.Laureates).ToList();
            return data;
        }

        //Get all Laureate List
        public IEnumerable<Laureate> GetLaureate()
        {
            return db.Laureates.ToList();
        }

        //Get All Prize List by Year only
        public IEnumerable<Prize> GetPrizeByYear()
        {
            return db.Prizes.Include(c => c.Laureates).OrderBy(x => x.Year).ToList();
        }


        //Get Prize List by Year & Category
        public IEnumerable<Prize> GetByYearCategory(string cat, string year)
        {
            return db.Prizes.Include(c => c.Laureates).Where(x => x.Category.ToLower().Contains(cat.ToLower()) && x.Year.ToLower() == year).ToList();
        }


        //Get Laureate List in Ascending order
        public IEnumerable<Laureate> GetLaureatesAsc()
        {
            var data = db.Laureates.Include(c => c.Prize).OrderBy(x => x.LaureateId).ToList();
            return data;
        }


        //Get Laureate by Firstname
        public IEnumerable<Laureate> GetLaureateByName(string name)
        {
            var data = db.Laureates.Include(c => c.Prize).Where(x => x.Firstname.ToLower().Contains(name.ToLower())).ToList();
            return data;
        }


        //Add Prize
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



        //Update Prize by ID
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

        //Delete Record by ID
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

        //Get Prize with Laureate list by another way.
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

        //Search by ID
        public Prize GetById(int id)
        {
            return db.Prizes.Include(c => c.Laureates).Where(c => c.PrizeId == id).FirstOrDefault();            
        
        }
    }
}
