using BackpackGame.Core.Abstractions;
using BackpackGame.Interactor;

namespace BackpackGame.Core.Initialization
{
    public class InteractorInitializer : MVPInitializer<InteractorView>
    {
        private InteractorPresenter _presenter;
        
        public InteractorInitializer(InteractorView view) : base(view)
        {
            InteractorModel model = new();
            _presenter = new(model, view);
        }

        public override void Initialize()
        {
            _presenter.Initialize();
        }
    }
}