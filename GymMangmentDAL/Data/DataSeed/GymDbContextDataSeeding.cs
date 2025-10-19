using GymMangmentDAL.Data.Context;
using GymMangmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymMangmentDAL.Data.DataSeed
{
    public static class GymDbContextDataSeeding
    {
        public static bool SeedData( GymDbContext dbContext)
        {
            try
            {
                var HasPlans = dbContext.Plans.Any();
                var HasCategories = dbContext.Categories.Any();
                if (HasCategories && HasPlans) return false; // Data already seeded

                if (!HasPlans)
                {
                    var Plans = LoadDataFromJsonFile<Plan>("plans.json");
                    if (Plans.Any())
                        dbContext.Plans.AddRange(Plans);
                }
                if (!HasCategories)
                {
                    var Categories = LoadDataFromJsonFile<Category>("categories.json");
                    if (Categories.Any())
                        dbContext.Categories.AddRange(Categories);
                }
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding Failed {ex}");
                return false;
            }
        }

        private static List<T> LoadDataFromJsonFile<T>(string fileName)
        {
           var FilePath= Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", fileName);
           if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"The file {fileName} was not found at path {FilePath}");
            }
           string Data = File.ReadAllText(FilePath);
            // to allow case insensitive property names during deserialization
            var Options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
              return JsonSerializer.Deserialize<List<T>>(Data, Options) ?? new List<T>();

        }
    }
}
