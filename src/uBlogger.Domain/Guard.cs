using System;

namespace uBlogger.Domain
{
    public static class Guard
    {
        public static void NotNullOrEmpty(string value, string argName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Argument cannot be null or empty.", argName);
        }
    }
}