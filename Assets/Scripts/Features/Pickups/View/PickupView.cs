using UnityEngine;

namespace RoomPuzzle.Features.Pickups.View
{
    public class PickupView : MonoBehaviour
    {
        [field: SerializeField]
        public int ScoreValue { get; private set; } = 10;

        public void Collect()
        {
            // Could play a particle effect or sound here before destroying
            Destroy(gameObject);
        }
    }
}
