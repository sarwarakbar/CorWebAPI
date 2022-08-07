using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public interface IRepositoryBAL
    {
        public IEnumerable<Prize> GetAll();

        public IEnumerable<Prize> GetYearPrize();

        public IEnumerable<Prize> GetListByYearCategory(string cat, string year);

        public IEnumerable<Laureate> GetLaureateByFirstName(string name);

        public string Add(Prize prize1);

        public Prize UpdateByID(int id, Prize prize1);

        public string DeleteRecord(int id);

        public IEnumerable<Prize> GetById(int id);
    }
}
