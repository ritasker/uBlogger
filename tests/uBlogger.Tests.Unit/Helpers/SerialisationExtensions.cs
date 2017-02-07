using Newtonsoft.Json;

namespace uBlogger.Tests.Unit.Helpers
{
    public static class SerialisationExtensions
    {
        public static string ToJsonString(this object o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }
}