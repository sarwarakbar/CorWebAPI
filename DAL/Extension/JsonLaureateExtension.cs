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
                                                 Firstname = l.firstname ?? String.Empty,
                                                 Motivation = l.motivation ?? String.Empty,
                                                 RemoteIdentifier = l.id ?? String.Empty,
                                                 Share = l.share ?? String.Empty,
                                                 Surname = l.surname ?? String.Empty
                                             }).ToList();
            return results;
        }
    }
}
