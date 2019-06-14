using DependencyInjection.Attributes;
using DependencyInjection.Layers;
using Gameplay.Factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Spawning
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private Vector3 maxSpawnVector;

        [GenericInjected] private FactoryMap factoryMap;

        private CubeFactory cubeFactory;
        private SphereFactory sphereFactory;
        
        private void Start()
        {
            GenericInjectionLayer.Instance.InjectDependencies(this);

            cubeFactory = factoryMap.GetFactory<CubeFactory>();
            sphereFactory = factoryMap.GetFactory<SphereFactory>();
            
            InvokeRepeating("SpawnCube", 0.5f, 1f);
            InvokeRepeating("SpawnSphere", 1f, 1f);
        }

        private void OnDestroy()
        {
            GenericInjectionLayer.Instance.DumpDependencies(this);
        }

        private void SpawnCube()
        {
            cubeFactory.Instantiate(GetSpawnPoint(), Quaternion.identity);
        }

        private void SpawnSphere()
        {
            sphereFactory.Instantiate(GetSpawnPoint(), Quaternion.identity);
        }

        private Vector3 GetSpawnPoint()
        {
            return new Vector3(
                Random.Range(-maxSpawnVector.x, maxSpawnVector.x),
                Random.Range(-maxSpawnVector.y, maxSpawnVector.y),
                Random.Range(-maxSpawnVector.z, maxSpawnVector.z));
        }
    }
}