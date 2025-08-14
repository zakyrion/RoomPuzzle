using System;
using UnityEngine;

namespace RoomPuzzle.FinishPoint.View
{
    public class FinishPointView : MonoBehaviour, IFinishPointView
    {
        public event Action PlayerReached;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerReached?.Invoke();
            }
        }
    }
}
