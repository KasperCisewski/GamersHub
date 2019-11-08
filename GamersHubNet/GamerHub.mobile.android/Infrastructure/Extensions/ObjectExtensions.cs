using System;

namespace GamerHub.mobile.android.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static void DisposeIfNotNull(this IDisposable obj)
        {
            obj?.Dispose();
        }
    }
}