using BackpackGame.ScriptableObjects;
using UnityEngine;

namespace BackpackGame.Core.Abstractions
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class StorageableItem : MonoBehaviour, IHaveDescription
    {
        [SerializeField] private StorageableItemData _data;
        
        public StorageableItemData Data => _data;
        public string Description => Data.Description;
        public Rigidbody Rigidbody { get; private set; }
        public bool IsAvailableToPickUp { get; private set; } = true;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        public void OnPickUp() => IsAvailableToPickUp = false;
        public void OnDrop() => IsAvailableToPickUp = true;
    }
}