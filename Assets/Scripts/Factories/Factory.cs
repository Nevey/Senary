using DI;
using UnityEngine;
using MonoBehaviour = DI.MonoBehaviour;

namespace Factories
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

        [Inject] private FactoryManager factoryManager;

        protected override void Awake()
        {
            base.Awake();
            factoryManager.AddFactory(this);
        }

        protected override void OnDestroy()
        {
            factoryManager.RemoveFactory(this);
            base.OnDestroy();
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