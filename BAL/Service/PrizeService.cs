using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using DAL.Model;
using DAL.Repository;

namespace BAL.Service
{
    public class PrizeService
    {
        private readonly IRepository<Prize> _prize;
        public PrizeService(IRepository<Prize> _prize)
        {
            this._prize = _prize;
        }

        //Get All Prize Records List
        public IEnumerable<Prize> GetAllPrizes()
        {
            return _prize.GetAll().ToList();
        }

        //Get All Prize List by Year only
        public IEnumerable<Prize> GetYearPrize()
        {
            return _prize.GetPrizeByYear().ToList();
        }

        //Get List by Year and Category
        public IEnumerable<Prize> GetListByYearCategory(string cat, string year)
        {
            var result = _prize.GetByYearCategory(cat, year).ToList();
            return result;

        }

        //Find Laureate by FirstName
        public IEnumerable<Laureate> GetLaureateByFirstName(string name)
        {
            return _prize.GetLaureateByName(name).ToList();
        }

        //Add Prize
        public string Add(Prize prize1)
        {
            var result = _prize.Post(prize1);
            return result;
        }

        //Update Prize by ID
        public Prize UpdateByID(int id, Prize prize1)
        {
            var result = _prize.UpdatePrizeById(id, prize1);
            return result;
        }

        //Delete Record
        public string DeleteRecord(int id)
        {
            var result = _prize.Delete(id);
            return "Record Deleted";
        }



    }

}
