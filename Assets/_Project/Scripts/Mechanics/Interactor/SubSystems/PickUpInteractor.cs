using BackpackGame.Core.Abstractions;
using BackpackGame.Hand;
using UnityEngine;
using Zenject;

namespace BackpackGame.Interactor
{
    public class PickUpInteractor : InteractorSubSystem
    {
        private HandPresenter _handPresenter;
        
        #region Zenject
        [Inject]
        private void Construct(HandPresenter handPresenter)
        {
            Debug.LogWarning("Construct");
            _handPresenter = handPresenter;
        }
        #endregion
        
        public override void Interact(Collider hitCollider)
        {
            if (hitCollider && hitCollider.TryGetComponent(out StorageableItem storageableItem)
                && Input.GetKeyDown(KeyCode.E))
                _handPresenter.PickUpItem(storageableItem);
        }
    }
}