using System.Reflection;
using Utilities;

namespace DI
{
    public static class Injector
    {
        public static void Inject(object @object)
        {
            FieldInfo[] fieldInfos = Reflection.GetFieldsWithAttribute<InjectAttribute>(@object.GetType());

            for (int i = 0; i < fieldInfos.Length; i++)
            {
                (InjectionLayer, InjectedAttribute) injectionLayer = InjectionLayerManager.GetInjectionLayer(fieldInfos[i]);
                injectionLayer.Item1.InjectIntoField(fieldInfos[i], injectionLayer.Item2, @object);
            }
        }

        public static void Dump(object @object)
        {
            FieldInfo[] fieldInfos = Reflection.GetFieldsWithAttribute<InjectAttribute>(@object.GetType());
            
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                (InjectionLayer, InjectedAttribute) injectionLayer = InjectionLayerManager.GetInjectionLayer(fieldInfos[i]);
                injectionLayer.Item1.DumpDependencies(@object);
            }
        }
    }
}