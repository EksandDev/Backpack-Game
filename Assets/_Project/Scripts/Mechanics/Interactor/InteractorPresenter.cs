using BackpackGame.Core.Abstractions;
using UnityEngine;

namespace BackpackGame.Interactor
{
    public class InteractorPresenter : Presenter<InteractorModel, InteractorView>
    {
        public InteractorPresenter(InteractorModel model, InteractorView view) : base(model, view) {}

        public override void Initialize()
        {
            Model.InitializeSubSystems();
            View.RaycastReachedObject += SendColliderToModelFromView;
        }

        private void SendColliderToModelFromView(Collider collider)
        {
            Model.SendColliderToSubSystems(collider);
        }
    }
}