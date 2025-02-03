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
        
        public override void Initialize()
        {
            StartCoroutine(TrackInput());
        }

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

        private IEnumerator TrackInput()
        {
            while (IsEnabled)
            {
                if (Input.GetKeyDown(KeyCode.R))
                    PlayerWantsReleaseItem?.Invoke();
                
                if (Input.GetKeyDown(KeyCode.E))
                    PlayerWantsStorageItem?.Invoke(null);

                yield return new WaitForFixedUpdate();
            }
        }
    }
}