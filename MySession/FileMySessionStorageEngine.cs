
namespace SessionClone.MySession
{
    public class FileMySessionStorageEngine : IMySessionStorageEngine
    {

        private readonly string _directoryPath;

        public FileMySessionStorageEngine(string dirPath)
        {
             this._directoryPath = dirPath;
        }

        public async Task CommitAsync(string id, Dictionary<string, byte[]> store, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(_directoryPath, id);
            using FileStream fileStream = new FileStream(filePath, FileMode.Create);
            using StreamWriter streamWriter = new StreamWriter(fileStream);

            await streamWriter.WriteAsync(System.Text.Json.JsonSerializer.Serialize(store));
        }

        public Dictionary<string, byte[]> Load(string id)
        {
            string filePath = Path.Combine(_directoryPath, id);
            if (!File.Exists(filePath))
            {
                return [];
            }
            using FileStream fileStream = new FileStream(filePath, FileMode.Open);
            using StreamReader streamReader = new StreamReader(fileStream);
            var json = streamReader.ReadToEnd();
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, byte[]>>(json) ?? [];
        }

        public async Task<Dictionary<string, byte[]>> LoadAsync(string id, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(_directoryPath, id);
            if (!File.Exists(filePath))
            {
                return [];
            }

            using FileStream fileStream = new FileStream(filePath, FileMode.Open);
            using StreamReader streamReader = new StreamReader(fileStream);

            var json = await streamReader.ReadToEndAsync(cancellationToken);

            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, byte[]>>(json) ?? [];
        }
    }
}
