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
        public abstract Type AttributeType { get; }
        public abstract void InjectDependencies(object @object);
        public abstract void DumpDependencies(object @object);
    }
    
    public abstract class InjectionLayer<T> : InjectionLayer where T : InjectedAttribute
    {
        private readonly Dictionary<Type, object> dependencies = new Dictionary<Type, object>();
        private readonly Dictionary<object, List<object>> references = new Dictionary<object, List<object>>();

        public override Type AttributeType => typeof(T);

        public override void InjectDependencies(object @object)
        {
            FieldInfo[] fieldInfos = Reflection.GetFieldsWithAttribute<T>(@object.GetType());
            
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                object injectedInstance;
                
                FieldInfo fieldInfo = fieldInfos[i];

                SingletonInjectedAttribute singletonInjectedAttribute =
                    fieldInfo.FieldType.GetCustomAttribute<SingletonInjectedAttribute>();
                
                if (singletonInjectedAttribute != null)
                {
                    if (dependencies.ContainsKey(fieldInfo.FieldType))
                    {
                        injectedInstance = dependencies[fieldInfo.FieldType];
                    }
                    else
                    {
                        // TODO: Add Monobehaviour support
                        dependencies[fieldInfo.FieldType] = injectedInstance = Activator.CreateInstance(fieldInfo.FieldType);
                    }

                    if (injectedInstance == null)
                    {
                        throw Log.Exception(
                            $"Something went wrong while trying to assign Service of type <b>{fieldInfo.FieldType}</b>");
                    }

                    // Track references to this singleton injected instance
                    if (references.ContainsKey(injectedInstance))
                    {
                        if (references[injectedInstance].Contains(@object))
                        {
                            throw Log.Exception(
                                $"Trying to use the same Service of type <b>{fieldInfo.FieldType}</b> twice in <b>{@object}</b>");
                        }
                        
                        references[injectedInstance].Add(@object);
                    }
                    else
                    {
                        references[injectedInstance] = new List<object> {@object};
                    }
                    
                    Log.Write($"Singleton <b>{injectedInstance.GetType().Name}</b> has <b>{references[injectedInstance].Count}</b> reference(s)");
                }
                else
                {
                    // TODO: Add Monobehaviour support
                    injectedInstance = Activator.CreateInstance(fieldInfo.FieldType);
                    
                    Log.Write($"Non-Singleton <b>{injectedInstance.GetType().Name}</b> was created");
                }
                
                fieldInfo.SetValue(@object, injectedInstance);
                
                Log.Write($"<b>{injectedInstance.GetType().Name}</b> was injected into <b>{@object.GetType().Name}</b>");
            }
        }

        public override void DumpDependencies(object @object)
        {
            List<object> dependenciesToRemove = new List<object>();
            
            foreach (KeyValuePair<object, List<object>> keyValuePair in references)
            {
                object injectedInstance = keyValuePair.Key;
                
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

                // Injected instance has no more references, time to clean it up
                if (objects.Count == 0)
                {
                    Type type = injectedInstance.GetType();
                    
                    if (!dependencies.ContainsKey(type))
                    {
                        throw Log.Exception("");
                    }

                    dependencies.Remove(type);
                    dependenciesToRemove.Add(injectedInstance);
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
    
    public class GenericInjectionLayer : InjectionLayer<InjectedAttribute>
    {
        
    }
}