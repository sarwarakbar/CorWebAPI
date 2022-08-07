using DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Generic
{
    public class RepositoryGenData : IRepositoryDALGen<Prize>
    {
        AppDbContext _dbContext;

        public RepositoryGenData(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Prize> Create(Prize _object)
        {
            var obj = await _dbContext.Prizes.AddAsync(_object);
             _dbContext.SaveChanges();
            return obj.Entity;                
        }
        
        public IEnumerable<Prize> GetAll()
        {
          
            return _dbContext.Prizes.Include(c => c.Laureates).ToList();
        }

        public Prize GetById(int Id)
        {
            return _dbContext.Prizes.Include(x => x.Laureates).Where(y => y.PrizeId == Id).FirstOrDefault();
                
        }

        //public Prize Update(int id, Prize _object)
        //{
        //    {
        //        _dbContext.Prizes.Update(id, _object);
        //        _dbContext.SaveChanges();
        //        return _object;
        //    }
        //}
    }
    
}
