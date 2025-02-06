using BackpackGame.Core.Abstractions;
using BackpackGame.Hand;

namespace BackpackGame.Core.Initialization
{
    public class HandInitializer : MVPInitializer<HandView>
    {
        public HandPresenter Presenter { get; }
        
        public HandInitializer(HandView view) : base(view)
        {
            HandModel model = new(view.HandPoint);
            Presenter = new(model, view);
        }

        public override void Initialize()
        {
            Presenter.Initialize();
        }
    }
}