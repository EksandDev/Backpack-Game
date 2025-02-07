using BackpackGame.Core.Abstractions;
using BackpackGame.Hand;
using UnityEngine;
using Zenject;

namespace BackpackGame.Interactor
{
    public class PickUpInteractor : InteractorSubSystem
    {
        private HandPresenter _handPresenter;
        private const string _playerLayer = "IgnoreRaycast";

        public PickUpInteractor(HandPresenter handPresenter)
        {
            _handPresenter = handPresenter;
        }
        
        public override void Interact(Collider hitCollider)
        {
            if (hitCollider && hitCollider.TryGetComponent(out StorageableItem storageableItem) 
                            && Input.GetKeyDown(KeyCode.E) && storageableItem.IsAvailableToPickUp)
                _handPresenter.PickUpItem(storageableItem);
        }
    }
}