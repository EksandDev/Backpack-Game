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
        
        public bool TryTakeItem(out StorageableItemData item)
        {
            if (!IsEnabled)
            {
                item = null;
                return false;
            }
            
            if (_stack.TryTake(out item))
                return true;
            
            Debug.LogWarning("Trying take an item from empty backpack!");
            return false;
        }

        public bool TryStorageItem(StorageableItemData item)
        {
            if (!IsEnabled)
                return false;
            
            if (_stack.TryPush(item))
                return true;
            
            Debug.LogWarning("Trying storage an item in full backpack!");
            return false;
        }
    }
}