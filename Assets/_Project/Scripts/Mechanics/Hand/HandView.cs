using System;
using BackpackGame.Core.Abstractions;
using UnityEngine;

namespace BackpackGame.Hand
{
    public class HandView : View
    {
        [SerializeField] private Transform _handPoint;
        
        public event Action PlayerWantsDropItem;
        public event Action PlayerWantsUseItem;

        public Transform HandPoint => _handPoint;

        public void PickUpItem(StorageableItem item)
        {
            //аудио и всякие эффектики
        }

        public StorageableItem SpawnItem(StorageableItem prefab)
        {
            return Instantiate(prefab);
        }

        public void DestroyItem(StorageableItem item)
        {
            Destroy(item.gameObject);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
                PlayerWantsDropItem?.Invoke();
            
            if (Input.GetMouseButtonDown(0))
                PlayerWantsUseItem?.Invoke();
        }
    }
}