using BackpackGame.Core;
using BackpackGame.Core.Abstractions;
using BackpackGame.ScriptableObjects;
using UnityEngine;

namespace BackpackGame.Backpack
{
    public class BackpackModel : Model
    {
        private CustomStack<StorageableItemData> _stack;

        public BackpackModel(int itemsLimit)
        {
            _stack = new(itemsLimit);
        }
        
        public bool TryTakeItem(out StorageableItemData itemData)
        {
            if (!IsEnabled)
            {
                itemData = null;
                return false;
            }
            
            if (_stack.TryTake(out itemData))
                return true;
            
            Debug.LogWarning("Trying take an item from empty backpack!");
            return false;
        }

        public bool TryStorageItem(StorageableItemData itemData)
        {
            if (!IsEnabled || !itemData)
                return false;
            
            if (_stack.TryPush(itemData))
                return true;
            
            Debug.LogWarning("Trying storage an item in full backpack!");
            return false;
        }
    }
}