using BackpackGame.Core;
using BackpackGame.Core.Abstractions;
using UnityEngine;

namespace BackpackGame.Backpack
{
    public class BackpackModel : Model
    {
        private CustomStack<Storageable> _stack;

        public BackpackModel(int itemsLimit)
        {
            ItemsLimit = itemsLimit;
            _stack = new(itemsLimit);
        }
        
        public int ItemsLimit { get; }
        
        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }
        
        public bool TryTakeItem(out Storageable item)
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

        public bool TryStorageItem(Storageable item)
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