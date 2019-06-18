using System;
using System.Collections.Generic;
using System.Reflection;
using DependencyInjection.Layers;
using Utilities;

namespace DependencyInjection
{
    public static class InjectionLayerManager
    {
        private static Dictionary<Type, InjectionLayer> injectionLayers = new Dictionary<Type, InjectionLayer>();

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
                injectionLayers[injectionLayer.AttributeType] = injectionLayer;
                
                Log.Write($"Instantiated new <b>{type.Name}</b>");
            }
            else
            {
                Log.Write($"Using cached <b>{type.Name}</b>");
            }
        }
        
        public static InjectionLayer GetInjectionLayer(FieldInfo fieldInfo)
        {
            object[] customAttributes = fieldInfo.GetCustomAttributes(true);

            for (int j = 0; j < customAttributes.Length; j++)
            {
                Type attributeType = customAttributes[j].GetType();
                if (injectionLayers.ContainsKey(attributeType))
                {
                    return injectionLayers[attributeType];
                }
            }

            throw Log.Exception("");
        }
    }
}