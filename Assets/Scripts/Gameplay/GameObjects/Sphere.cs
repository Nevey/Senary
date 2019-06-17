using DependencyInjection.Attributes;
using DependencyInjection.Layers;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class Sphere : MonoBehaviour
    {
        [GenericInjected] private Test test;
        
        private void Awake()
        {
            GenericInjectionLayer.Instance.InjectDependencies(this);

            test.testString = "Sphere";
        }

        private void Update()
        {
            Log.Write("asd");
        }
    }
}