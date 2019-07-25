using System;

namespace DI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectedAttribute : Attribute
    {
        public Type Layer { get; set; } = typeof(InjectionLayer);
        public bool Singleton { get; set; }
    }
}