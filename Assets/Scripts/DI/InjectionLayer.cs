using System;
using System.Collections.Generic;
using System.Reflection;
using Utilities;
using Object = UnityEngine.Object;

namespace DI
{
    public class InjectionLayer
    {
        private readonly Dictionary<Type, object> dependencies = new Dictionary<Type, object>();
        private readonly Dictionary<object, List<object>> references = new Dictionary<object, List<object>>();

        public void InjectIntoField(FieldInfo fieldInfo, InjectedAttribute injectedAttribute, object @object)
        {
            object injectedInstance;

            if (injectedAttribute.Singleton)
            {
                if (dependencies.ContainsKey(fieldInfo.FieldType))
                {
                    injectedInstance = dependencies[fieldInfo.FieldType];
                }
                else
                {
                    if (fieldInfo.FieldType.IsSubclassOf(typeof(MonoBehaviour)))
                    {
                        dependencies[fieldInfo.FieldType] = injectedInstance = Object.FindObjectOfType(fieldInfo.FieldType);
                    }
                    else
                    {
                        dependencies[fieldInfo.FieldType] = injectedInstance = Activator.CreateInstance(fieldInfo.FieldType);
                    }
                }

                if (injectedInstance == null)
                {
                    throw Log.Exception(
                        $"Something went wrong while trying to assign " +
                        $"Service of type <b>{fieldInfo.FieldType}</b>");
                }

                // Track references to this singleton injected instance
                if (references.ContainsKey(injectedInstance))
                {
                    if (references[injectedInstance].Contains(@object))
                    {
                        throw Log.Exception(
                            $"Trying to use the same Service of type " +
                            $"<b>{fieldInfo.FieldType}</b> twice in <b>{@object}</b>");
                    }
                    
                    references[injectedInstance].Add(@object);
                }
                else
                {
                    references[injectedInstance] = new List<object> {@object};
                }
            }
            else
            {
                if (fieldInfo.FieldType.IsSubclassOf(typeof(MonoBehaviour)))
                {
                    throw Log.Exception($"Non-Singleton Monobehaviour support is not added yet!");
                }
                
                // TODO: Add Monobehaviour support
                injectedInstance = Activator.CreateInstance(fieldInfo.FieldType);
                
                Log.Write($"Non-Singleton <b>{injectedInstance.GetType().Name}</b> was created");
            }
            
            fieldInfo.SetValue(@object, injectedInstance);

            if (injectedAttribute.Singleton)
            {
                Log.Write($"Injected <i>Singleton</i> instance <b>{injectedInstance.GetType().Name}</b> " +
                          $"into <b>{@object.GetType().Name}</b> -- " +
                          $"Has <b>{references[injectedInstance].Count}</b> reference(s)");
            }
            else
            {
                Log.Write($"Created instance <b>{injectedInstance.GetType().Name}</b> -- " +
                          $"Was injected into <b>{@object.GetType().Name}</b>");
            }
        }

        public void DumpDependencies(object @object)
        {
            List<object> instancesToRemove = new List<object>();
            
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
                    
                    Log.Write($"<i>Singleton</i> instance <b>{injectedInstance.GetType().Name}</b> -- " +
                              $"Dumped by <b>{@object.GetType().Name}</b> -- " +
                              $"Has <b>{references[injectedInstance].Count}</b> reference(s)");
                    
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
                    instancesToRemove.Add(injectedInstance);
                }
            }

            // Finally remove references by key
            for (int i = 0; i < instancesToRemove.Count; i++)
            {
                Log.Write(
                    $"Clearing <i>Singleton</i> instance of " +
                    $"<b>{instancesToRemove[i].GetType().Name}</b> as it has no more references left");
                
                references.Remove(instancesToRemove[i]);
                
                // TODO: Add Monobehaviour support
                instancesToRemove[i] = null;
            }
        }
    }
}