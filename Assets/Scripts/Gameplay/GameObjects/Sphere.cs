using Gameplay.InjectionLayers;
using UnityEngine;

namespace Gameplay
{
    public class Sphere : MonoBehaviour
    {
        [GameplayInjected] private Test test;
        
        private void Awake()
        {
            GameplayInjectionLayer.Instance.InjectDependencies(this);

            test.testString = "Sphere";
        }

        private void Update()
        {
//            Log.Write("asd");
        }
    }
}