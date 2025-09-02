namespace SessionClone.MySession
{
    public class MySessionScopedContainer
    {
        public ISession? Session { get; set; }

        public MySessionScopedContainer(ILogger<MySessionScopedContainer> logger)
        {
            logger.LogInformation("MySessionScopedContainer created.");
        }
    }
}
