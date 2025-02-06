using BackpackGame.Core.Abstractions;
using BackpackGame.ScriptableObjects;
using UnityEngine;

namespace BackpackGame.Items
{
    public class StorageableItem : MonoBehaviour, IPickUp, IHaveDescription
    {
        [SerializeField] private StorageableItemData _data;
        
        public StorageableItemData Data => _data;
        public string Description => Data.Description;
        public Rigidbody Rigidbody { get; private set; }
        public bool IsAvailableToPickUp { get; private set; } = true;

        public void OnPickUp()
        {
            IsAvailableToPickUp = false;
        }

        public void OnDrop()
        {
            IsAvailableToPickUp = true;
        }
    }

    public abstract class UsableInHandItem : StorageableItem
    {
        public abstract void Use();
    }
}