namespace FileSystemWatcherTest.Repository.Database
{
    internal class CacheHelper
    {
        internal static string GetCacheKey<T>(string id = "")
        {
            var typeName = typeof (T).FullName;
            if (id != string.Empty)
            {
                return string.Format("{0}:{1}", typeName, id);
            }
            return string.Format("{0}", typeName);
        }
    }
}