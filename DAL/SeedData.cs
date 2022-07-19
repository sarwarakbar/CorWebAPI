using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Extension;
using DAL.Model;
using Newtonsoft.Json;
using System.IO;

namespace DAL
{
    public class SeedData
    { 
        
            public static void Initialize(AppDbContext context)
            {

                if (context.Prizes.Any())
                {
                    return;   // DB has been seeded
                }

                // Read the JSON file into json model(not the same as the EF models)
                var fileName = @"C:\Users\DELL\Desktop\prize.json";
                var PrizeList = File.ReadAllText(fileName);
                var jsonPrizes = JsonConvert.DeserializeObject<JsonModel.Rootobject>(PrizeList);

                try
                {
                    //Create the EF Models and save
                    foreach (JsonModel.Prize jsonPrize in jsonPrizes.prizes)
                    {
                        context.Prizes.AddRange(
                                new Prize()
                                {
                                    Year = jsonPrize.year,
                                    Category = jsonPrize.category,
                                    OverallMotivation = jsonPrize.overallMotivation ?? String.Empty,
                                    Laureates = jsonPrize.laureates.ConvertToEntity()
                                }); ; ;
                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        
    }
}
