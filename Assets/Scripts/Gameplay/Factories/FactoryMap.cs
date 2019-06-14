using System.Collections.Generic;
using Utilities;

namespace Gameplay.Factories
{
    public class FactoryMap
    {
        private readonly List<Factory> factoryMap = new List<Factory>();

        public void MapFactory(Factory factory)
        {
            if (factoryMap.Contains(factory))
            {
                throw Log.Exception(
                    $"Factory reference {factory.GetType()} was already mapped, only one reference can be available!");
            }
            
            factoryMap.Add(factory);
        }

        public void UnMapFactory(Factory factory)
        {
            if (!factoryMap.Contains(factory))
            {
                throw Log.Exception(
                    $"Factory reference {factory.GetType()} not mapped while trying to UnMap reference!");
            }
        }

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