using DependencyInjection.Attributes;
using DependencyInjection.Layers;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class Cube : MonoBehaviour
    {
        [GenericInjected] private Test test;
        
        private void Awake()
        {
            GenericInjectionLayer.Instance.InjectDependencies(this);

            test.testString = "Cube";
        }

        private void Update()
        {
            Log.Write("dsa");
        }
    }
}