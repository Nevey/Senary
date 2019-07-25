namespace DI
{
    public abstract class MonoBehaviour : UnityEngine.MonoBehaviour
    {
        protected virtual void Awake()
        {
            Injector.Inject(this);
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void OnEnable()
        {
            
        }

        protected virtual void OnDisable()
        {
            
        }

        protected virtual void OnDestroy()
        {
            Injector.Dump(this);
        }
    }
}