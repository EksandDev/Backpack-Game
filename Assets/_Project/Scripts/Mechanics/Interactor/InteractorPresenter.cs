using BackpackGame.Core.Abstractions;

namespace BackpackGame.Interactor
{
    public class InteractorPresenter : Presenter<InteractorModel, InteractorView>
    {
        public InteractorPresenter(InteractorModel model, InteractorView view) : base(model, view) {}

        public override void Initialize()
        {
            View.RaycastReachedObject += Model.SendColliderToSubSystems;
        }
    }
}