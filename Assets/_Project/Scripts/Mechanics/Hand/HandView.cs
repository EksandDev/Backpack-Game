using System;
using BackpackGame.Core.Abstractions;
using BackpackGame.Items;
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
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
                PlayerWantsDropItem?.Invoke();
            
            if (Input.GetMouseButtonDown(0))
                PlayerWantsUseItem?.Invoke();
        }
    }
}