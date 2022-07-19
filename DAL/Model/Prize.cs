using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public class Prize
    {
        [Key]
        public int PrizeId { get; set; }
        public string Year { get; set; }
        public string Category { get; set; }

        //Navigation Property
        public List<Laureate> Laureates { get; set; }

        public string OverallMotivation { get; set; }
    }

    public class Laureate
    {
        [Key]
        public int LaureateId { get; set; }
        public string RemoteIdentifier { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Motivation { get; set; }
        public string Share { get; set; }

        //Navigation Properties
        public int PrizeId { get; set; }
        public Prize Prize { get; set; }
    }
}
