using System.Collections.Generic;
using BackpackGame.Core.Abstractions;
using BackpackGame.Hand;
using UnityEngine;

namespace BackpackGame.Interactor
{
    public class InteractorModel : Model
    {
        private List<InteractorSubSystem> _subSystems;

        public void InitializeSubSystems(HandPresenter handPresenter)
        {
            _subSystems = new()
            {
                new PickUpInteractor(handPresenter),
                new UseInteractor(),
                new ShowDescriptionInteractor()
            };
        }

        public void SendColliderToSubSystems(Collider collider)
        {
            foreach (var subSystem in _subSystems)
                subSystem.Interact(collider);
        }
    }
}