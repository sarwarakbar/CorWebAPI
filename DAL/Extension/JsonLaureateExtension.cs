using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Extension
{
    public static class JsonLaureateExtension
    {
        public static List<Model.Laureate> ConvertToEntity(this List<JsonModel.Laureate> Laureate)
        {
            List<Model.Laureate> results = (from l in Laureate
                                             select new Model.Laureate()
                                             {
                                                 Firstname = l.firstname,
                                                 Motivation = l.motivation,
                                                 RemoteIdentifier = l.id,
                                                 Share = l.share,
                                                 Surname = l.surname
                                             }).ToList();
            return results;
        }
    }
}
