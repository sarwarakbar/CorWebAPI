using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.JsonModel
{
        public  class Rootobject
        {
             public List<Prize> prizes { get; set; }
        }
        public class Prize
        {
             public string year { get; set; }
             public string category { get; set; }
             public List<Laureate> laureates { get; set; }
             public string overallMotivation { get; set; }
        }

         public class Laureate
         {
             public string id { get; set; }
             public string firstname { get; set; }
             public string surname { get; set; }
             public string motivation { get; set; }
             public string share { get; set; }
         
         }
}
