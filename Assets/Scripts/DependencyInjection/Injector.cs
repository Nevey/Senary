using System.Reflection;
using DependencyInjection.Attributes;
using DependencyInjection.Layers;
using Utilities;

namespace DependencyInjection
{
    public static class Injector
    {
        public static void Inject(object @object)
        {
            FieldInfo[] fieldInfos = Reflection.GetFieldsWithAttribute<InjectAttribute>(@object.GetType());

            for (int i = 0; i < fieldInfos.Length; i++)
            {
                InjectionLayer injectionLayer = InjectionLayerManager.GetInjectionLayer(fieldInfos[i]);
                injectionLayer.InjectIntoField(fieldInfos[i], @object);
            }
        }

        public static void Dump(object @object)
        {
            FieldInfo[] fieldInfos = Reflection.GetFieldsWithAttribute<InjectAttribute>(@object.GetType());
            
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                InjectionLayer injectionLayer = InjectionLayerManager.GetInjectionLayer(fieldInfos[i]);
                injectionLayer.DumpDependencies(@object);
            }
        }
    }
}