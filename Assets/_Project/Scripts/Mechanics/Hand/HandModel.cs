using BackpackGame.Core.Abstractions;
using BackpackGame.Items;
using UnityEngine;

namespace BackpackGame.Hand
{
    public class HandModel : Model
    {
        private StorageableItem _currentItem;
        private Transform _handPoint;

        public HandModel(Transform handPoint)
        {
            _handPoint = handPoint;
        }
        
        public void PickUpItem(StorageableItem item)
        {
            _currentItem = item;
            _currentItem.OnPickUp();
            _currentItem.Rigidbody.isKinematic = true;
            _currentItem.transform.parent = _handPoint;
            _currentItem.transform.position = Vector3.zero;
        }
        
        public void DropItem()
        {
            if (!_currentItem)
            {
                Debug.LogWarning("Item in hand is null");
                return;
            }

            _currentItem.OnDrop();
            _currentItem.Rigidbody.isKinematic = false;
            _currentItem.transform.parent = null;
            _currentItem.Rigidbody.AddForce(_currentItem.transform.forward, ForceMode.Force);
            _currentItem = null;
        }

        public void UseItem()
        {
            if (!_currentItem)
            {
                Debug.LogWarning("Item in hand is null");
                return;
            }

            if (!_currentItem.TryGetComponent(out UsableInHandItem usableItem)) 
                return;
            
            usableItem.Use();
                
            if (_currentItem.TryGetComponent(out ISelfDropItem selfDropItem))
                DropItem();
        }
    }
}