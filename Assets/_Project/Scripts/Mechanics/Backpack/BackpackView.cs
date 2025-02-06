using System;
using BackpackGame.Core.Abstractions;
using BackpackGame.ScriptableObjects;
using UnityEngine;

namespace BackpackGame.Backpack
{
    public class BackpackView : View
    {
        public event Action PlayerWantsTakeItem;
        public event Action<StorageableItemData> PlayerWantsStorageItem;

        public void TakeItem(StorageableItemData itemData)
        {
            if (!IsEnabled)
                return;
        }

        public void TakeItemFailure()
        {
            if (!IsEnabled)
                return;
        }

        public void StorageItem()
        {
            if (!IsEnabled)
                return;
        }

        public void StorageItemFailure()
        {
            if (!IsEnabled)
                return;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                PlayerWantsTakeItem?.Invoke();
                
            if (Input.GetKeyDown(KeyCode.Q))
                PlayerWantsStorageItem?.Invoke(null);
        }
    }
}