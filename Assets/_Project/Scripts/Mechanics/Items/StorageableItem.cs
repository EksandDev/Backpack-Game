using BackpackGame.ScriptableObjects;
using UnityEngine;

namespace BackpackGame.Items
{
    public class StorageableItem : MonoBehaviour
    {
        [SerializeField] private StorageableItemData _data;

        public StorageableItemData Data => _data;
        public bool IsAvailable { get; private set; }
    }
}