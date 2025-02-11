using BackpackGame.Core.Abstractions;
using UnityEngine;

namespace BackpackGame.Hand
{
    public class HandModel : Model
    {
        private Transform _handPoint;
        private const float _force = 100f;

        public StorageableItem CurrentItem { get; private set; }

        public HandModel(Transform handPoint)
        {
            _handPoint = handPoint;
        }
        
        public void PickUpItem(StorageableItem item)
        {
            CurrentItem = item;
            CurrentItem.OnPickUp();
            CurrentItem.Rigidbody.isKinematic = true;
            CurrentItem.transform.parent = _handPoint;
            CurrentItem.transform.rotation = _handPoint.rotation;
            CurrentItem.transform.localPosition = Vector3.zero;
        }
        
        public void DropItem()
        {
            if (!CurrentItem)
            {
                Debug.LogWarning("Item in hand is null");
                return;
            }
            
            CurrentItem.OnDrop();
            CurrentItem.Rigidbody.isKinematic = false;
            CurrentItem.transform.parent = null;
            CurrentItem.Rigidbody.AddForce(_handPoint.forward * _force, ForceMode.Force);
            CurrentItem = null;
        }

        public void UseItem()
        {
            if (!CurrentItem)
            {
                Debug.LogWarning("Item in hand is null");
                return;
            }

            if (!CurrentItem.TryGetComponent(out UsableInHandItem usableItem)) 
                return;
            
            usableItem.Use();
                
            if (CurrentItem.TryGetComponent(out ISelfDropItem selfDropItem))
                DropItem();
        }

        public void DestroyCurrentItem()
        {
            CurrentItem = null;
        }
    }
}