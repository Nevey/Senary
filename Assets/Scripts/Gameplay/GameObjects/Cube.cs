using DependencyInjection;
using Gameplay.InjectionLayers;
using UnityEngine;

namespace Gameplay
{
    public class Cube : MonoBehaviour
    {
        [GameplayInjected] private Test test;
        
        private void Awake()
        {
            Injector.Inject(this);

            test.testString = "Cube";
        }

        private void Update()
        {
//            Log.Write("dsa");
        }
    }
}