using DependencyInjection;
using DependencyInjection.Attributes;
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

        [Inject] private FactoryMap factoryMap;

        protected virtual void Awake()
        {
            Injector.Inject(this);
            factoryMap.AddFactory(this);
        }

        protected virtual void OnDestroy()
        {
            factoryMap.RemoveFactory(this);
            Injector.Dump(this);
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