using DependencyInjection;
using DependencyInjection.Attributes;
using UnityEngine;

namespace Gameplay
{
    public class Cube : MonoBehaviour
    {
        [Inject] private Test test;
        
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