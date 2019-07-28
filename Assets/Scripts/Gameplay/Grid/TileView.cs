using UnityEngine;
using MonoBehaviour = DI.MonoBehaviour;

namespace Gameplay.Grid
{
    public class TileView : MonoBehaviour
    {
        private const float PADDING = 0.1f;
        
        private new Renderer renderer;

        protected override void Awake()
        {
            base.Awake();
            renderer = GetComponent<Renderer>();
        }

        public void SetPosition(Coords coords)
        {
            Vector3 position = transform.position;
            position.x = (renderer.bounds.size.x + PADDING) * coords.x;
            position.z = (renderer.bounds.size.y + PADDING) * coords.y;
            transform.position = position;
        }
    }
}