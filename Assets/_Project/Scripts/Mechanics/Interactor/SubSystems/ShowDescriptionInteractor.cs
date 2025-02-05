using BackpackGame.Core.Abstractions;
using UnityEngine;

namespace BackpackGame.Interactor
{
    public class ShowDescriptionInteractor : InteractorSubSystem
    {
        public override void Interact(Collider hitCollider)
        {
            if (hitCollider && hitCollider.TryGetComponent(out IHaveDescription haveDescription))
            {
                var description = haveDescription.Description;
                //показать описание на экране
            }
        }
    }
}