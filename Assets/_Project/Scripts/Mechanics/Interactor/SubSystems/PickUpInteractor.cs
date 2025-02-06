using BackpackGame.Core.Abstractions;
using UnityEngine;

namespace BackpackGame.Interactor
{
    public class PickUpInteractor : InteractorSubSystem
    {
        public override void Interact(Collider hitCollider)
        {
            if (hitCollider && hitCollider.TryGetComponent(out IPickUp pickUp))
            {
                var so = pickUp.Data;
                //передать в скрипт рук сошник пик апа
            }
        }
    }
}