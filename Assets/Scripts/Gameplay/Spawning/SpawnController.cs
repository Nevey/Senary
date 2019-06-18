using Gameplay.Factories;
using Gameplay.InjectionLayers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Spawning
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private Vector3 maxSpawnVector;

        [GameplayInjected] private FactoryMap factoryMap;

        private CubeFactory cubeFactory;
        private SphereFactory sphereFactory;
        
        private void Start()
        {
            GameplayInjectionLayer.Instance.InjectDependencies(this);

            cubeFactory = factoryMap.GetFactory<CubeFactory>();
            sphereFactory = factoryMap.GetFactory<SphereFactory>();
            
            InvokeRepeating(nameof(SpawnCube), 0.5f, 1f);
            InvokeRepeating(nameof(SpawnSphere), 1f, 1f);
        }

        private void OnDestroy()
        {
            GameplayInjectionLayer.Instance.DumpDependencies(this);
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