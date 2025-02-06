using BackpackGame.Core.Abstractions;
using UnityEngine;

namespace BackpackGame.Interactor
{
    public class UseInteractor : InteractorSubSystem
    {
        public override void Interact(Collider hitCollider)
        {
            if (hitCollider && hitCollider.TryGetComponent(out IUsable usable))
                usable.Use();
        }
    }
}