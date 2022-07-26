using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IRepository<Prize>
    {

        public IEnumerable<Laureate> GetLaureate();

        public IEnumerable<Prize> GetAll();

        public IEnumerable<Prize> GetPrizeByYear();

        public IEnumerable<Prize> GetByYearCategory(string cat, string year);

        public IEnumerable<Laureate> GetLaureatesAsc();

        public IEnumerable<Laureate> GetLaureateByName(string name);

        public string Post(Prize prize1);

        public Prize UpdatePrizeById(int id, Prize prize1);

        public string Delete(int id);

        public IEnumerable<NobelPrize> GetNobelPrizes();

        public Prize GetById(int Id);

    }
}
