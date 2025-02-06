using BackpackGame.Core.Abstractions;
using BackpackGame.Interactor;

namespace BackpackGame.Core.Initialization
{
    public class InteractorInitializer : MVPInitializer<InteractorView>
    {
        public InteractorPresenter Presenter { get; }
        
        public InteractorInitializer(InteractorView view) : base(view)
        {
            InteractorModel model = new();
            Presenter = new(model, view);
        }

        public override void Initialize()
        {
            Presenter.Initialize();
        }
    }
}