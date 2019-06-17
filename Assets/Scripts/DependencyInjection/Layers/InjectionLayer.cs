using System;
using System.Collections.Generic;
using System.Reflection;
using DependencyInjection.Attributes;
using Utilities;

namespace DependencyInjection.Layers
{
    [Serializable]
    public abstract class InjectionLayer
    {
        public abstract void InjectDependencies(object @object);
        public abstract void DumpDependencies(object @object);
    }
    
    public abstract class InjectionLayer<T> : InjectionLayer where T : GenericInjectedAttribute
    {
        private readonly Dictionary<Type, object> dependencies = new Dictionary<Type, object>();
        private readonly Dictionary<object, List<object>> references = new Dictionary<object, List<object>>();
        
        public static InjectionLayer<T> Instance;
        
        protected InjectionLayer()
        {
            Instance = this;
        }

        public override void InjectDependencies(object @object)
        {
            FieldInfo[] fieldInfos = Reflection.GetFieldsWithAttribute<T>(@object.GetType());
            
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                object dependency;
                
                FieldInfo fieldInfo = fieldInfos[i];
                
                T attribute = fieldInfo.GetCustomAttribute<T>();
                
                if (attribute.Singleton)
                {
                    // Get or Create a Dependency, right now we approach them as if they are singletons
                    if (dependencies.ContainsKey(fieldInfo.FieldType))
                    {
                        dependency = dependencies[fieldInfo.FieldType];
                    }
                    else
                    {
                        // TODO: Add Monobehaviour support
                        dependencies[fieldInfo.FieldType] = dependency = Activator.CreateInstance(fieldInfo.FieldType);
                    }

                    if (dependency == null)
                    {
                        throw Log.Exception(
                            $"Something went wrong while trying to assign Service of type {fieldInfo.FieldType}");
                    }

                    // Track references to this Dependency
                    if (references.ContainsKey(dependency))
                    {
                        if (references[dependency].Contains(@object))
                        {
                            throw Log.Exception(
                                $"Trying to use the same Service of type {fieldInfo.FieldType} twice in {@object}");
                        }
                        
                        references[dependency].Add(@object);
                    }
                    else
                    {
                        references[dependency] = new List<object> {@object};
                    }
                }
                else
                {
                    // TODO: Add Monobehaviour support
                    dependency = Activator.CreateInstance(fieldInfo.FieldType);
                }
                
                fieldInfo.SetValue(@object, dependency);
            }
        }

        public override void DumpDependencies(object @object)
        {
            List<object> dependenciesToRemove = new List<object>();
            
            foreach (KeyValuePair<object, List<object>> keyValuePair in references)
            {
                object dependency = keyValuePair.Key;
                
                List<object> objects = keyValuePair.Value;
                
                for (int i = 0; i < objects.Count; i++)
                {
                    if (objects[i] != @object)
                    {
                        continue;
                    }
                    
                    objects.RemoveAt(i);
                    break;
                }

                // Service has no more references, time to clean it up
                if (objects.Count == 0)
                {
                    Type type = dependency.GetType();
                    
                    if (!dependencies.ContainsKey(type))
                    {
                        throw Log.Exception("");
                    }

                    dependencies.Remove(type);
                    dependenciesToRemove.Add(dependency);
                }
            }

            // Finally remove references by key
            for (int i = 0; i < dependenciesToRemove.Count; i++)
            {
                references.Remove(dependenciesToRemove[i]);
                
                // TODO: Add Monobehaviour support
                dependenciesToRemove[i] = null;
            }
        }
    }
}