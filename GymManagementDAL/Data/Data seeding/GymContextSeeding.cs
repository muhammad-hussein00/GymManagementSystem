using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Data_seeding
{
    public static class GymContextSeeding
    {
        public static bool IsSeeded(GymContext gymContext)
        {
            //Check for data is exest.
            try
            {
                var hasPlans = gymContext.Plans.Any();
                var hasCategories = gymContext.Categories.Any();
                if (hasPlans && hasCategories)
                    return false;
                if (!hasPlans)
                {
                    var plans = LoadDataFromJsonFile<Plan>("plans.json");
                    if (plans.Any())
                        gymContext.Plans.AddRange(plans);
                }
                if (!hasCategories)
                {
                    var categories = LoadDataFromJsonFile<Category>("categories.json");
                    if (categories.Any())
                        gymContext.Categories.AddRange(categories);
                }
                return gymContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding failed : {ex}");
                return false;
            }
        }
        private static List<T> LoadDataFromJsonFile<T>(string jsonFile)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//Files",jsonFile);
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);
            string data = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions()
            {
                 PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<T>>(data, options) ?? new List<T>();
        }

    }
}
