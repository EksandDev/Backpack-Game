using BackpackGame.Core.Abstractions;
using BackpackGame.Hand;
using BackpackGame.Interactor;

namespace BackpackGame.Core.Initialization
{
    public class InteractorInitializer : MVPInitializer<InteractorView>
    {
        private InteractorPresenter _presenter;
        private HandPresenter _handPresenter;
        
        public InteractorInitializer(InteractorView view, HandPresenter handPresenter) : base(view)
        {
            InteractorModel model = new();
            _presenter = new(model, view);
            _handPresenter = handPresenter;
        }

        public override void Initialize()
        {
            _presenter.Initialize();
            _presenter.Model.InitializeSubSystems(_handPresenter);
        }
    }
}