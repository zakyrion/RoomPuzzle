using UnityEngine;    
using System;    
namespace RoomPuzzle    
{    
    [RequireComponent(typeof(Collider))]    
    public class KeyView : MonoBehaviour, IPickupable    
    {    
        public event Action<KeyView> OnPickedUp;    
        public KeyData KeyData;    
        public void Pickup() => OnPickedUp?.Invoke(this);    
        public void DestroyView() => Destroy(gameObject);    
    }    
}