using System;

namespace uBlogger.Infrastructure
{
    public static class Guard
    {
        public static void NotNullOrEmpty(string value, string argName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Argument cannot be null or empty.", argName);
        }

        public static void NotNullOrEmpty(Guid value, string argName)
        {
            if (value == null || value == Guid.Empty)
                throw new ArgumentException("Argument cannot be null or empty.", argName);
        }
    }
}