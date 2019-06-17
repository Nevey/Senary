using System;

namespace DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class GenericInjectedAttribute : Attribute
    {
        public bool Singleton { get; set; }
    }
}