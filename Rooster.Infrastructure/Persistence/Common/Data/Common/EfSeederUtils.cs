using Newtonsoft.Json;
using Rooster.Domain.Common.Utilities;

namespace Rooster.Infrastructure.Persistence.Common.Data.Common;

public class EfSeederUtils
{
    public static bool SeedExistsInDb<T>(IEnumerable<T> seed, IEnumerable<T> db)
    {
        return seed.Select(db.Contains).All(exists => exists);
    }
    
    public static IEnumerable<T> ReadJson<T>(string path)
    {
        var fileContent = FileUtils.ReadFileContentsAsString<EfDataSeeder>(path, AboutMe.Assembly);
        return JsonConvert.DeserializeObject<IList<T>>(fileContent);
    }
}