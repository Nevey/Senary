using DependencyInjection.Attributes;
using DependencyInjection.Layers;
using Gameplay.InjectionLayers;
using UnityEngine;

namespace Gameplay.Factories
{
    public abstract class Factory : MonoBehaviour
    {
        public abstract MonoBehaviour Instantiate();
        public abstract MonoBehaviour Instantiate(Transform parent);
        public abstract MonoBehaviour Instantiate(Vector3 position, Quaternion rotation);
        public abstract void Destroy(MonoBehaviour instance);
    }

    public abstract class Factory<T> : Factory where T : MonoBehaviour
    {
        [SerializeField] private T prefab;

        [GameplayInjected] private FactoryMap factoryMap;

        protected virtual void Awake()
        {
            GameplayInjectionLayer.Instance.InjectDependencies(this);
            factoryMap.MapFactory(this);
        }

        protected virtual void OnDestroy()
        {
            factoryMap.UnMapFactory(this);
            GameplayInjectionLayer.Instance.DumpDependencies(this);
        }

        public override MonoBehaviour Instantiate()
        {
            return MonoBehaviour.Instantiate(prefab);
        }

        public override MonoBehaviour Instantiate(Transform parent)
        {
            return MonoBehaviour.Instantiate(prefab, parent);
        }

        public override MonoBehaviour Instantiate(Vector3 position, Quaternion rotation)
        {
            return MonoBehaviour.Instantiate(prefab, position, rotation);
        }

        public override void Destroy(MonoBehaviour instance)
        {
            MonoBehaviour.Destroy(instance);
        }
    }
}