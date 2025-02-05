using System.Collections.Generic;
using BackpackGame.Core.Abstractions;
using UnityEngine;

namespace BackpackGame.Interactor
{
    public class InteractorModel : Model
    {
        private List<InteractorSubSystem> _subSystems;

        public void InitializeSubSystems()
        {
            _subSystems = new()
            {
                new PickUpInteractor(),
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