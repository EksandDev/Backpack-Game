using System;
using BackpackGame.Core;
using BackpackGame.Core.Abstractions;
using BackpackGame.Hand;
using BackpackGame.ScriptableObjects;
using UnityEngine;

namespace BackpackGame.Backpack
{
    public class BackpackModel : Model
    {
        private CustomStack<StorageableItemData> _stack;
        private HandModel _handModel;

        public event Action ItemTaken;
        public event Action<GameObject> ItemStored;

        public BackpackModel(HandModel handModel, int itemsLimit)
        {
            _handModel = handModel;
            _stack = new(itemsLimit);
        }
        
        public bool TryTakeItem(out StorageableItemData itemData)
        {
            if (!IsEnabled || _handModel.CurrentItem)
            {
                itemData = null;
                return false;
            }

            if (_stack.TryTake(out itemData))
            {
                ItemTaken?.Invoke();
                return true;
            }
            
            Debug.LogWarning("Trying take an item from empty backpack!");
            return false;
        }

        public bool TryStorageItem()
        {
            if (!IsEnabled || !_handModel.CurrentItem)
                return false;

            if (_stack.TryPush(_handModel.CurrentItem.Data))
            {
                ItemStored?.Invoke(_handModel.CurrentItem.Data.Icon);
                return true;
            }
            
            Debug.LogWarning("Trying storage an item in full backpack!");
            return false;
        }
    }
}