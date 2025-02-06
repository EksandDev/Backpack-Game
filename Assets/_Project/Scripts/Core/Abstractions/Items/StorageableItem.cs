using BackpackGame.ScriptableObjects;
using UnityEngine;

namespace BackpackGame.Core.Abstractions
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class StorageableItem : MonoBehaviour, IHaveDescription
    {
        [SerializeField] private StorageableItemData _data;
        [SerializeField] private Rigidbody _rigidbody;
        
        public StorageableItemData Data => _data;
        public string Description => Data.Description;
        public Rigidbody Rigidbody => _rigidbody;
        public bool IsAvailableToPickUp { get; private set; } = true;

        #region Validation
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        #endregion

        public void OnPickUp() => IsAvailableToPickUp = false;
        public void OnDrop() => IsAvailableToPickUp = true;
    }
}