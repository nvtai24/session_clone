
namespace SessionClone.MySession
{
    public class MySessionStorage : IMySessionStorage
    {
        private readonly IMySessionStorageEngine storageEngine;
        private readonly Dictionary<string, ISession> sessions = new Dictionary<string, ISession>();

        public MySessionStorage(IMySessionStorageEngine storageEngine)
        {
            this.storageEngine = storageEngine;
        }

        public ISession Create()
        {
            var newSession = new MySession(Guid.NewGuid().ToString("N"), storageEngine);
            sessions[newSession.Id] = newSession;
            return newSession;
        }

        public ISession Get(string sessionId)
        {
            if (sessions.ContainsKey(sessionId))
            {
                return sessions[sessionId];
            }    

            return Create();
        }
    }
}
