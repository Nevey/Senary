using System.Collections.Generic;
using DependencyInjection.Attributes;
using Gameplay.InjectionLayers;
using Utilities;

namespace Gameplay.Factories
{
    [Injected(Layer = typeof(GameplayInjectionLayer), Singleton = true)]
    public class FactoryMap
    {
        // TODO: Vamp into dict
        private readonly List<Factory> factoryMap = new List<Factory>();

        /// <summary>
        /// Add a factory to the factory map
        /// </summary>
        /// <param name="factory"></param>
        /// <exception cref="Exception"></exception>
        public void AddFactory(Factory factory)
        {
            if (factoryMap.Contains(factory))
            {
                throw Log.Exception(
                    $"Factory reference {factory.GetType()} was already mapped, only one reference can be available!");
            }
            
            factoryMap.Add(factory);
        }

        /// <summary>
        /// Remove a factory from the factory map
        /// </summary>
        /// <param name="factory"></param>
        /// <exception cref="Exception"></exception>
        public void RemoveFactory(Factory factory)
        {
            if (!factoryMap.Contains(factory))
            {
                throw Log.Exception(
                    $"Factory reference {factory.GetType()} not mapped while trying to UnMap reference!");
            }
        }

        /// <summary>
        /// Get a factory based on Factory type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T GetFactory<T>() where T : Factory
        {
            for (int i = 0; i < factoryMap.Count; i++)
            {
                if (factoryMap[i].GetType() == typeof(T))
                {
                    return factoryMap[i] as T;
                }
            }

            throw Log.Exception($"Factory {typeof(T)} was not mapped!");
        }
    }
}