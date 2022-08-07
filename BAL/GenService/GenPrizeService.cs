using DAL.Generic;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.GenService
{
    public class GenPrizeService
    {
        private readonly IRepositoryDALGen<Prize> _prize;

        public GenPrizeService(IRepositoryDALGen<Prize> prize)
        {
            _prize = prize;
        }

        //Get All Prize Detail
        public IEnumerable<Prize> GetAllPrizeDetails()
        {
            try
            {
                return _prize.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }

        }

        //Add New Prize Record
        public async Task<Prize> AddPrize(Prize prize1)
        {
            return await _prize.Create(prize1);
        }

        //Get Record by ID
        public IEnumerable<Prize> GetById(int id)
        {
            var data = _prize.GetById(id);
            yield return data;
        }

        //    //Update Prize Details  
        //    public Prize Update(int id, Prize _object)
        //    {
        //        var result = _prize.Update(id, _object);
        //        return result;
        //    }

    }
}
