using System;
using System.Collections;
using BackpackGame.Core.Abstractions;
using UnityEngine;

namespace BackpackGame.Backpack
{
    public class BackpackView : View
    {
        public event Action PlayerWantsReleaseItem;
        public event Action<Storageable> PlayerWantsStorageItem;

        public void ReleaseItem(Storageable item)
        {
            if (!IsEnabled)
                return;
        }

        public void ReleaseItemFailure()
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
                PlayerWantsReleaseItem?.Invoke();
                
            if (Input.GetKeyDown(KeyCode.E))
                PlayerWantsStorageItem?.Invoke(null);
        }
    }
}