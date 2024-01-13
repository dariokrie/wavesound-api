using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WaveSound.Domain.Services.Interfaces;

namespace WaveSound.Domain.Services
{
    public class DownloadPathUpdater : IDownloadPathUpdater
    {
        public async Task UpdateDownloadPathAsync(string newDownloadPath)
        {
            try
            {
                var jsonFilePath = GetJsonPath();

                var json = await File.ReadAllTextAsync(jsonFilePath);
                var jsonObject = JObject.Parse(json);

                jsonObject["DownloadPathConfig"]["DownloadPath"] = newDownloadPath;

                var updatedJson = jsonObject.ToString();

                await File.WriteAllTextAsync(jsonFilePath, updatedJson);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<string> GetDownloadPathAsync()
        {
            var jsonFilePath = GetJsonPath();

            var json = await File.ReadAllTextAsync(jsonFilePath);
            dynamic data = JsonConvert.DeserializeObject(json);

            return data.DownloadPathConfig.DownloadPath;
        }

        private static string GetJsonPath()
        {
            var jsonFileName = "DownloadPath.json";
            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var domainFolderPath = Path.GetDirectoryName(assemblyPath);
            var solutionRoot = Path.GetFullPath(Path.Combine(domainFolderPath, "../../../../"));

            return Path.Combine(solutionRoot, jsonFileName);
        }
    }
}
