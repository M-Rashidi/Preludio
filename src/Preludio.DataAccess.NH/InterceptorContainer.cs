using System;

namespace Preludio.DataAccess.NH
{
    internal static class InterceptorContainer
    {
        public static Type InterceptorType { get; set; }
        public static bool IsInterceptorRegistered()
        {
            return InterceptorType != null;
        }
    }
}
