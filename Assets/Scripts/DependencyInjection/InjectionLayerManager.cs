using System;
using System.Collections.Generic;
using System.Reflection;
using DependencyInjection.Attributes;
using Utilities;

namespace DependencyInjection
{
    public static class InjectionLayerManager
    {
        private static readonly Dictionary<Type, InjectionLayer> injectionLayers = new Dictionary<Type, InjectionLayer>();

        private static bool IsInjectionLayer(Type type)
        {
            if (type.BaseType != typeof(InjectionLayer))
            {
                return false;
            }

            return type.BaseType == null || IsInjectionLayer(type.BaseType);
        }
        
        public static void CreateLayer(Type type)
        {
            if (IsInjectionLayer(type))
            {
                throw Log.Exception(
                    $"Trying to create InjectionLayer of type <b>{type.Name}</b>, but it's no InjectionLayer!");
            }
            
            if (!injectionLayers.ContainsKey(type))
            {
                InjectionLayer injectionLayer = (InjectionLayer) Activator.CreateInstance(type);
                injectionLayers[type] = injectionLayer;
                
                Log.Write($"Instantiated new <b>{type.Name}</b>");
            }
            else
            {
                Log.Write($"Using cached <b>{type.Name}</b>");
            }
        }
        
        public static (InjectionLayer, InjectedAttribute) GetInjectionLayer(FieldInfo fieldInfo)
        {
            InjectedAttribute injectedAttribute = fieldInfo.FieldType.GetCustomAttribute<InjectedAttribute>();
            if (injectedAttribute == null)
            {
                throw Log.Exception($"Class <b>{fieldInfo.FieldType.Name}</b> is missing <b>Injected</b> attribute");
            }
            
            Type type = injectedAttribute.Layer;
            if (injectionLayers.ContainsKey(type))
            {
                return (injectionLayers[type], injectedAttribute);
            }

            throw Log.Exception($"InjectionLayer of type {type.Name} is not available! Please check your ApplicationStates");
        }
    }
}