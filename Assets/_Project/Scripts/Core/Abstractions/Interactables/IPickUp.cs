using BackpackGame.ScriptableObjects;

namespace BackpackGame.Core.Abstractions
{
    public interface IPickUp : IInteractable
    {
        public StorageableItemData StorageableItemData { get; }
    }
}