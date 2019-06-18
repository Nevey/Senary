using System;

namespace DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectedAttribute : Attribute
    {
        public Type Layer { get; set; } = typeof(InjectionLayer);
        public bool Singleton { get; set; }
    }
}