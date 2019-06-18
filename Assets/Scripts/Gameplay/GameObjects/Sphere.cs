using DependencyInjection;
using DependencyInjection.Attributes;
using UnityEngine;

namespace Gameplay.GameObjects
{
    public class Sphere : MonoBehaviour
    {
        [Inject] private Test test;
        
        private void Awake()
        {
            Injector.Inject(this);

            test.testString = "Sphere";
        }

        private void Update()
        {
//            Log.Write("asd");
        }
    }
}