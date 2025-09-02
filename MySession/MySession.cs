using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace SessionClone.MySession
{
    public class MySession(string id, IMySessionStorageEngine storageEngine) : ISession
    {
        private readonly Dictionary<string, byte[]> _store = new();

        public bool IsAvailable
        {
            get
            {
                //LoadAsync(CancellationToken.None).Wait();
                Load();

                return true;
            }
        }

      
        public string Id => id;

        public IEnumerable<string> Keys => _store.Keys;

        public void Clear()
        {
            _store.Clear();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await storageEngine.CommitAsync(Id, _store, cancellationToken);
        }

        public async Task LoadAsync(CancellationToken cancellationToken = default)
        {
            _store.Clear();
            var loadedStore = await storageEngine.LoadAsync(Id, cancellationToken);

            foreach (var kvp in loadedStore)
            {
                _store[kvp.Key] = kvp.Value;
            }
        }

        private void Load()
        {
            _store.Clear();
            var loadedStore = storageEngine.Load(Id);

            foreach (var kvp in loadedStore)
            {
                _store[kvp.Key] = kvp.Value;
            }
        }


        public void Remove(string key)
        {
            _store.Remove(key);
        }

        public void Set(string key, byte[] value)
        {
            _store[key] = value;
        }

        public bool TryGetValue(string key, [NotNullWhen(true)] out byte[]? value)
        {
            return _store.TryGetValue(key, out value);
        }
    }
}
