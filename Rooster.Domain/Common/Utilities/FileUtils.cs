using System.Reflection;

namespace Rooster.Domain.Common.Utilities;

public static class FileUtils
{
    public static string ReadFileContentsAsString<T>(string relativePath, Assembly overrideAssembly = null)
    {
        var assembly = overrideAssembly ?? Assembly.GetExecutingAssembly();
        var resourceName = typeof(T).Namespace + relativePath;
        var stream = assembly.GetManifestResourceStream(resourceName);
        var reader = new StreamReader(stream ?? throw new FileNotFoundException());
        return reader.ReadToEnd();
    }

    public static StreamReader GetFileStreamReader<T>(string relativePath)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = typeof(T).Namespace + relativePath;
        var stream = assembly.GetManifestResourceStream(resourceName);
        return new StreamReader(stream ?? throw new FileNotFoundException());
    }

    public static Stream GetFileStream<T>(string relativePath)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = typeof(T).Namespace + relativePath;
        return assembly.GetManifestResourceStream(resourceName);
    }

    public static string GetFilePath<T>(string relativePath)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return typeof(T).Namespace + relativePath;
    }

    public static async Task<byte[]> DownloadFile(string url)
    {
        using (var client = new HttpClient())
        {
            using (var result = await client.GetAsync(url))
            {
                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsByteArrayAsync();
                }
            }
        }

        return null;
    }
}