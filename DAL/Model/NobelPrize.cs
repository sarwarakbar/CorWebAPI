using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class NobelPrize
    {
        public int PrizeId { get; set; }
        public string Year { get; set; }
        public string Category { get; set; }      

        public string OverallMotivation { get; set; }

        public int LaureateId { get; set; }
        public string RemoteIdentifier { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Motivation { get; set; }
        public string Share { get; set; }
    }
}
